using System;
using System.Web;
using System.Web.Mvc;
using Domain.Engine;
using Domain.Entities;
using Domain.Entities.Account;
using WebUI.Filter;

namespace WebUI.Controllers
{
    public class PartialGeneralController : Controller
    {
        //[UserAttribute]
        //[MyAuthAttribute]
        public ActionResult LoginPartial()
        {
            ViewBag.Login = "Войти";
            string auth = GetCookie("Auth");
            if (!String.IsNullOrWhiteSpace(auth))
            {
                AbzContext db = new AbzContext();
                AbzHash abzHash = db.AbzHashes.Find(auth);
                ViewBag.Login = abzHash.Email;
            }
            return PartialView();

        }

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
    }
}