using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repository;
using Domain.Engine;
using System;

namespace WebUI.Controllers
{
    //Что нужно от администрирования?
    //0. Выбрать юзера +
    //1. Добавить контрагента +
    //2. Удалить контрагента 
    //3. Назначить роль +
    public class AdminController : BaseController
    {
        //public AbzContext db = new AbzContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
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
        //Получить всех юзеров
        public ActionResult Index(string sortOrder = "SmalName")
        {
            AdminRepository repo = new AdminRepository();
            return View(repo.GetAdmins(sortOrder));
        }
        public ActionResult Good()
        {
            //Menu.ChangeSelected(6, 3);
            return RedirectToAction("Index", "Good", new { act = "Products", cont = "Admin" });
        }


        public ActionResult Privat(string id)
        {
            var user = UserManager.FindById(id);
            Cust cust = db.Custs.Find(user.CustID);
            abzHash.CustID = cust.CustId;
            UpdateHash(abzHash);

            BalanceRepository bl = new BalanceRepository();
            //menu.Cust = cust.SmalName;
            //menu.sm = bl.GetBalance(cust.CustId,Menu.ContractId);
            //menu.CustId = cust.CustId;
            //menu.UserId = id;
            //menu.ChangeSelected(1, 1);
            //Session["Menu"] = menu;
            return RedirectToAction("Index", "Home",new { SelectedCustId = cust.CustId });
        }
        public ActionResult Register()
        {
            //Menu.ChangeSelected(6, 2);
            RegisterAdmin reg = new RegisterAdmin();
            return View(reg);
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
        [HttpPost]
        public async Task<ActionResult> Register(RegisterAdmin reg)
        {
            //Отладка

            //EmailSend.EMailReg("kv@abz4.ru", "123");
            //return View("NoRegister");
            reg.Email.Trim();

            AbzContext db = new AbzContext();
            var user = new ApplicationUser { UserName = reg.Email, Email = reg.Email, CustID = reg.CustId };
            string Password = GenerateRandomPassword(6);
        
            var result = UserManager.Create(user, Password);
            if (result.Succeeded)
            {
                UserManager.AddToRole(user.Id, "CustManager");
                SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

                UserInCust uc = new UserInCust();
                uc.CustID = reg.CustId;
                uc.UserId = user.Id;
                uc.LastDat = DateTime.Now;
                db.UserInCusts.Add(uc);
                db.SaveChanges();
                await EmailSend.EMailRegAsync(reg.Email, Password);


                //EmailSend.EMailRegAsync(reg.Email, Password);

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Отправка сообщения электронной почты с этой ссылкой
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Подтверждение учетной записи", "Подтвердите вашу учетную запись, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");

                return RedirectToAction("Index", "Home");
            }

            return View("NoRegister");
        }

        public ActionResult CustUser(string id)
        {
            if (id==null)
                id= (string)Session["usrid"];
            var user = UserManager.FindById(id);
            Session["usrid"] = id;
            ViewBag.Use = user.UserName;
            AbzContext db = new AbzContext();
            List<UserAdmin> userincust = db.UserAdmins.Where(d => d.UserId == id).ToList();
            return View(userincust);
        }

        public async Task<ActionResult> DelCust(int id)
        {
            //string iduser = (string)Session["usrid"];

            AbzContext db = new AbzContext();
            UserInCust userInCust = await db.UserInCusts.FindAsync(id);

            db.UserInCusts.Remove(userInCust);
            await db.SaveChangesAsync();
            
            return RedirectToAction("CustUser");
        }


        public ActionResult FindUser()
        {
            return View();
        }
        public ActionResult CustSearch(int id)
        {
            AbzContext context = new AbzContext();
            CustRepository repo = new CustRepository();
            Cust cust;
            cust = repo.Get(id);

            UserInCust usr = new UserInCust();
            usr.CustID = cust.CustId;
            usr.Inn = cust.Inn;
            usr.UserId = (string)Session["usrid"];
            usr.LastDat = DateTime.Now;
            context.UserInCusts.Add(usr);
            context.SaveChanges();
            //Теперь надо прописать в юзера
            ApplicationUser user = UserManager.FindById(usr.UserId);
            user.CustID= cust.CustId;
            UserManager.Update(user);
            ViewBag.UserName = user.UserName;
            return View(cust);
        }

        public ActionResult Role(string id)
        {
            ApplicationUser user;
            //userManager.RemoveFromRole(user.Id, "user");
            IList<string> roles = new List<string> { "Роль не определена" };
            if (id == null)
            {
                user = (ApplicationUser)Session["usr"];
            }
            else
            {
                user = UserManager.FindById(id);
            }
            if (user != null)
                roles = UserManager.GetRoles(user.Id);
            
            Session["usr"] = user;
            ViewBag.User = user.UserName;
            return View(roles);
        }

        public ActionResult CreateRole()
        {  
            ApplicationUser user= (ApplicationUser)Session["usr"];
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Заказы", Value = "0", Selected = true });
            items.Add(new SelectListItem { Text = "Бухгалтер", Value = "1" });
            items.Add(new SelectListItem { Text = "Мэнеджер", Value = "2" });
            items.Add(new SelectListItem { Text = "Администратор", Value = "3" });

            ViewData["Role"] = new SelectList(items, "Value", "Text", "0");
            ViewBag.Lst = items;
            ViewBag.User = user.UserName;
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(int SelectedId = 0)
        {
            ApplicationUser user = (ApplicationUser)Session["usr"];
            switch (SelectedId)
            {
                case 0:
                    UserManager.AddToRole(user.Id, "CustOrder");             
                    break;
                case 1:
                    UserManager.AddToRole(user.Id, "CustBuh");
                    break;
                case 2:
                    UserManager.AddToRole(user.Id, "CustManager");
                    break;
                case 3:
                    UserManager.AddToRole(user.Id, "CustAdmin");
                    break;           
            }
            return RedirectToAction("Role", "Admin");
        }
     }
}