using Domain.Engine;
using Domain.Entities;
using Domain.Entities.Account;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using WebUI.Models;
using WebUI.Filter;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            //AbzContext db = new AbzContext();
            //string auth = GetCookie("Auth");
            //if (!String.IsNullOrWhiteSpace(auth))
            //{                
            //    AbzHash abzHash = db.AbzHashes.Find(auth);
            //    if ((abzHash != null) & (abzHash.IP == HttpContext.Request.UserHostAddress))
            //    {
            //        return RedirectToLocal(returnUrl);
            //    }
            //}
            //ViewBag.ReturnUrl = returnUrl;
            //ViewBag.Login = true;
            Usr principal = new Usr();
            return View(principal);
        }


        [HttpPost]
        //public async Task<ActionResult> Login(Usr model, string returnUrl)
        public async Task<ActionResult> Login(Usr model, string rememberme)
        {
            AbzContext db = new AbzContext();
            AbzHashRepo hashRepo = new AbzHashRepo();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Usr usr = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (usr==null)
                return View(model);

            if (rememberme == "true")
            {
                SetCookie("RememberMe", "true", 365);
                SetCookie("RememberUser", model.Email, 365);
                SetCookie("RememberPw", model.Password, 365);
            }
            else
            {
                SetCookie("RememberMe", "false", 365);
                DeleteCookie("RememberUser");
                DeleteCookie("RememberPw");
            }

            //Зарегить юзера, со значениями по умолчанию
            AbzHash abzHash = new AbzHash();
            abzHash.AbzHashID = Guid.NewGuid().ToString();
            abzHash.Email = model.Email;
            //abzHash.Password = MyCrypto.Shifrovka(model.Password);
            abzHash.UserId = usr.UserId;
            abzHash.TerminationDate = DateTime.Now.AddDays(2);
            string ip = HttpContext.Request.UserHostAddress;
            abzHash.IP = ip;
            hashRepo.SetDafault(abzHash);

            SetCookie("Auth", abzHash.AbzHashID);
            SetCookie("AuthUser", abzHash.Email);
            //return RedirectToLocal(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            try
            {
                DeleteCookie("Auth");
                DeleteCookie("AuthUser");
                DeleteCookie("balance");
                DeleteCookie("contract");
                DeleteCookie("contractid");
                DeleteCookie("custid");
                DeleteCookie("customer");
                DeleteCookie("menuitem");
            }
            catch { }

            return RedirectToAction("Index", "Home");
        }

        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    AbzHashRepo hashRepo = new AbzHashRepo();
        //    string auth = GetCookie("Auth");
        //    AbzHash abzHash = hashRepo.GetHash(auth);
        //    AbzContext db = new AbzContext();
        //    Usr usr = db.Users.FirstOrDefault(u => u.Email == abzHash.Email);
        //    if ((usr != null) && (usr.Password == model.OldPassword))
        //    {
        //        usr.Password = model.NewPassword;
        //        db.Entry(usr).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //        if (user != null)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //        return View("Error");
        //        //RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
        //    }
        //    return View(model);
        //}

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            ForgotPasswordViewModel forgot = new ForgotPasswordViewModel();
            return View(forgot);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                AbzContext db = new AbzContext();
                Usr user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    return View("Error");
                }
                string NewPassword = GenerateRandomPassword(6);
                user.Password = NewPassword;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                EmailSend.EMailFPassw(model.Email, NewPassword);
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string qq = model.Email;
        //        var user = await UserManager.FindByNameAsync(model.Email);
        //        if (user == null)
        //        {
        //            return View("Error");
        //        }
        //        string newpassword = GenerateRandomPassword(6);
        //        string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

        //        var result = await UserManager.ResetPasswordAsync(user.Id, code, newpassword);
        //        if (result.Succeeded)
        //        {
        //            await EmailSend.EMailFPassw(model.Email, newpassword);
        //            return View("ForgotPasswordConfirmation");
        //        }
        //    }
        //    return View(model);
        //}

        //[MyAuthAttribute]
        //public ActionResult Register()
        //{
        //    RegisterAdmin reg = new RegisterAdmin();
        //    return View(reg);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Register(RegisterAdmin reg)
        //{
        //    reg.Email.Trim();

        //    AbzContext db = new AbzContext();
        //    Usr user = new Usr();
        //    user.Email = reg.Email;
        //    string Password = GenerateRandomPassword(6);
        //    user.Password = Password;
        //    user.UserId = Guid.NewGuid().ToString();
        //    db.Users.Add(user);
        //    db.SaveChanges();

        //    UserInCust uc = new UserInCust();
        //    uc.CustID = reg.CustId;
        //    uc.UserId = user.UserId;
        //    uc.LastDat = DateTime.Now;
        //    uc.Email = reg.Email;
        //    //uc.Pwd = Password;
        //    db.UserInCusts.Add(uc);
        //    db.SaveChanges();
        //    await EmailSend.EMailRegAsync(reg.Email, Password);

        //    return RedirectToAction("Index", "Home");
        //}

        private string GenerateRandomPassword(int length)
        {
            //string allowedChars = "abcdefghijkmnopqrstuvwxyz" +
            //                             "ABCDEFGHJKLMNOPQRSTUVWXYZ" +
            //                             "0123456789!@$?_-*&#+";
            string allowedChars = "abcdefghijkmnopqrstuvwxyz" +
                                         "ABCDEFGHJKLMNOPQRSTUVWXYZ" +
                                         "0123456789";
            char[] chars = new char[length];
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
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

        public void SetCookie(string cookieName, string cookieValue, int days)
        {
            string StrValue = cookieValue;
            Response.Cookies[cookieName].Value = StrValue;
            Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(days);
        }

        public void DeleteCookie(string cookieName)
        {
            Response.Cookies[cookieName].Value = string.Empty;
            Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1);
        }

        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        //private const string XsrfKey = "XsrfId";

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void ClearAllCookies()
        {
            if (HttpContext.CurrentHandler != null)
            {
                int cookieCount = HttpContext.Request.Cookies.Count;

                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var expiredCookie = new HttpCookie(cookie.Name)
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            Domain = cookie.Domain
                        };

                        HttpContext.Response.Cookies.Add(expiredCookie);
                    }
                }

                HttpContext.Request.Cookies.Clear();
            }
        }

        #endregion

    }
}