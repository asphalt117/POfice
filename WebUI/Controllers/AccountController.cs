using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebUI.Models;
using Domain.Engine;
using Domain.Entities;

namespace WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            string auth = GetCookie("Auth");
            if (!String.IsNullOrWhiteSpace(auth) )
            {
                AdminContext dba = new AdminContext();
                AbzHash abzHash = dba.AbzHashs.Find(auth);
                if ((abzHash != null) & (abzHash.IP == HttpContext.Request.UserHostAddress))
                {
                    string Email = MyCrypto.DeShifrovka(abzHash.Email);
                    string Password = MyCrypto.DeShifrovka(abzHash.Password);

                    var result = SignInManager.PasswordSignIn(Email, Password, true, shouldLockout: false);

                    return RedirectToLocal(returnUrl);
                }
            }

            //string Email = MyCrypto.DeShifrovka(GetCookie("MyAuth"));
            //string Password = MyCrypto.DeShifrovka(GetCookie("MyPWD"));

            //if (!String.IsNullOrWhiteSpace(Email) && !String.IsNullOrWhiteSpace(Password))
            //{
            //    //FormsAuthentication.SetAuthCookie(cuc, true);
            //    var result = SignInManager.PasswordSignIn(Email, Password, true, shouldLockout: false);
            //    return RedirectToLocal(returnUrl);
            //}

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Login = true;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Сбои при входе не приводят к блокированию учетной записи
            // Чтобы ошибки при вводе пароля инициировали блокирование учетной записи, замените на shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    //string cookieValue = MyCrypto.Shifrovka(model.Email);
                    //SetCookie("MyAuth", cookieValue);
                    //cookieValue = MyCrypto.Shifrovka(model.Password);
                    //SetCookie("MyPWD", cookieValue);

                    //Создание AbzHash. Хранение по новому  03.07.2019

                    AdminContext dba =new AdminContext();
                    AbzHash abzHash = new AbzHash();

                    abzHash.AbzHashID = Guid.NewGuid().ToString();
                    SetCookie("Auth", abzHash.AbzHashID);
                    abzHash.Email = MyCrypto.Shifrovka(model.Email);
                    abzHash.Password = MyCrypto.Shifrovka(model.Password);
                    abzHash.TerminationDate= DateTime.Now.AddDays(2);
                    string ip = HttpContext.Request.UserHostAddress;
                    abzHash.IP = ip;
                    dba.AbzHashs.Add(abzHash);
                    dba.SaveChanges();
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Неудачная попытка входа.");
                    return View(model);
            }
        }

         // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string qq = model.Email;
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)                    
                {
                    return View("Error");
                }
                string newpassword = GenerateRandomPassword(6);
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                var result = await UserManager.ResetPasswordAsync(user.Id, code, newpassword);
                if (result.Succeeded)
                {
                    await EmailSend.EMailFPassw(model.Email, newpassword);
                    return View("ForgotPasswordConfirmation");
                }
            }
            return View(model);
        }

        private string GenerateRandomPassword(int length)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyz" +
                                         "ABCDEFGHJKLMNOPQRSTUVWXYZ" +
                                         "0123456789!@$?_-*&#+";
            char[] chars = new char[length];
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }


        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            DeleteCookie("MyAuth");
            DeleteCookie("MyPWD");
            DeleteCookie("Dog");
            DeleteCookie("Cust");
            DeleteCookie("Auth");

            return RedirectToAction("Index", "Home");
        }

        //[ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            DeleteCookie("MyAuth");
            DeleteCookie("MyPWD");
            DeleteCookie("Dog");
            DeleteCookie("Cust");
            DeleteCookie("Auth");

            return RedirectToAction("Index", "Home");
        }

        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            //MyMenu menu = (MyMenu)Session["Menu"];
            //menu.ChangeSelected(1, 2);
            //Session["Menu"] = menu;
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return View("Error");
                    //RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Вспомогательные приложения

        public string GetCookie(string cookieName)
        {
            string cookieValue = "";
            HttpCookie cookie = Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookieValue = cookie.Value;
            }
            return cookieValue;
        }

        public void SetCookie(string cookieName, string cookieValue)
        {
            string StrValue = cookieValue;
            Response.Cookies[cookieName].Value = StrValue;
            Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(2);
        }

        public void DeleteCookie(string cookieName)
        {
            Response.Cookies[cookieName].Value = string.Empty;
            Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1);
        }

        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        //    internal class ChallengeResult : HttpUnauthorizedResult
        //    {
        //        public ChallengeResult(string provider, string redirectUri)
        //            : this(provider, redirectUri, null)
        //        {
        //        }

        //        public ChallengeResult(string provider, string redirectUri, string userId)
        //        {
        //            LoginProvider = provider;
        //            RedirectUri = redirectUri;
        //            UserId = userId;
        //        }

        //        public string LoginProvider { get; set; }
        //        public string RedirectUri { get; set; }
        //        public string UserId { get; set; }

        //        public override void ExecuteResult(ControllerContext context)
        //        {
        //            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
        //            if (UserId != null)
        //            {
        //                properties.Dictionary[XsrfKey] = UserId;
        //            }
        //            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //        }
        //    }
        #endregion
    }
}