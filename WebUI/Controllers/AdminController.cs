using System.Web.Mvc;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repository;
using WebUI.Filter;
using System.Collections.Generic;
using System.Linq;
using WebUI.Models;
using Domain.Entities.Account;
using System;
using Domain.Engine;
using System.Data.Entity;

namespace WebUI.Controllers
{
    //Что нужно от администрирования?
    //0. Выбрать юзера +
    //1. Добавить контрагента +
    //2. Удалить контрагента 
    //3. Назначить роль +

    [MyAuthAttribute]
    public class AdminController : BaseController
    {
        //Получить всех юзеров
        public ActionResult Index(string sortOrder = "SmalName")
        {
            AdminRepository repo = new AdminRepository();
            ViewBag.MenuItem = "admin";
            return View(repo.GetAdmins(sortOrder));
        }

        public ActionResult Register()
        {
            RegisterAdmin reg = new RegisterAdmin();
            return View(reg);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterAdmin reg)
        {
            reg.Email.Trim();

            AbzContext db = new AbzContext();
            Usr user = new Usr();
            user.Email = reg.Email;
            string Password = GenerateRandomPassword(6);
            user.Password = Password;
            user.UserId = Guid.NewGuid().ToString();
            db.Users.Add(user);
            db.SaveChanges();

            UserInCust uc = new UserInCust();
            uc.CustID = reg.CustId;
            uc.UserId = user.UserId;
            uc.LastDat = DateTime.Now;
            uc.Email = reg.Email;
            //uc.Pwd = Password;
            db.UserInCusts.Add(uc);
            db.SaveChanges();
            await EmailSend.EMailRegAsync(reg.Email, Password);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AbzHashRepo hashRepo = new AbzHashRepo();
            string auth = GetCookie("Auth");
            AbzHash abzHash = hashRepo.GetHash(auth);
            AbzContext db = new AbzContext();
            Usr usr = db.Users.FirstOrDefault(u => u.Email == abzHash.Email);
            if ((usr != null) && (usr.Password == model.OldPassword))
            {
                usr.Password = model.NewPassword;
                db.Entry(usr).State = EntityState.Modified;
                db.SaveChanges();
                await EmailSend.EMailRegAsync(abzHash.Email, model.NewPassword);
            }
            return RedirectToAction("Index", "Home");
        }

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


        public ActionResult CustUser(string id)
        {
            AbzContext db = new AbzContext();
            List<UserAdmin> userincust = db.UserAdmins.Where(d => d.UserId == id).ToList();
            return View(userincust);
        }

        //public ActionResult Privat(string id)
        //{
        //    var user = UserManager.FindById(id);
        //    Cust cust = db.Custs.Find(user.CustID);
        //    abzHash.CustID = cust.CustId;
        //    UpdateHash(abzHash);

        //    BalanceRepository bl = new BalanceRepository();
        //    //menu.Cust = cust.SmalName;
        //    //menu.sm = bl.GetBalance(cust.CustId,Menu.ContractId);
        //    //menu.CustId = cust.CustId;
        //    //menu.UserId = id;
        //    //menu.ChangeSelected(1, 1);
        //    //Session["Menu"] = menu;
        //    return RedirectToAction("Index", "Home", new { SelectedCustId = cust.CustId });
        //}

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
            usr.UserId = UserID;
            usr.Email = abzHash.Email;
            usr.LastDat = DateTime.Now;
            context.UserInCusts.Add(usr);
            context.SaveChanges();
            
            ////Теперь надо прописать в юзера
            
            //ApplicationUser user = UserManager.FindById(usr.UserId);
            //user.CustID = cust.CustId;
            //UserManager.Update(user);
            //ViewBag.UserName = user.UserName;
            
            return View(cust);
        }
    }
}