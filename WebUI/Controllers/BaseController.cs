using System;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Entities.Account;
using Domain.Repository;

namespace WebUI.Controllers
{
    public class BaseController : Controller
    {
        public AbzContext db = new AbzContext();
        public AbzHashRepo hashRepo = new AbzHashRepo();
        public AbzHash abzHash;
        public Cust Cust;
        public int CustID;
        public string UserID;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            ViewBag.AuthSuccess = false;
            db = new AbzContext();
            hashRepo = new AbzHashRepo();
            abzHash = hashRepo.GetHash(GetCookie("Auth").ToString());
            
            if (abzHash != null)
            {
                Cust = db.Custs.Find((int)abzHash.CustID);
                CustID = (int)abzHash.CustID;
                UserID = abzHash.UserId;
                ViewBag.AuthSuccess = true;
            }
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

        public void SetCookie(string cookieName, string cookieValue)
        {
            string StrValue = cookieValue;
            Response.Cookies[cookieName].Value = StrValue;
            Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(2);
        }
    }
}