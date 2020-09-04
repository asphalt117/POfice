using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using WebUI.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace WebUI.Controllers
{
    public class BaseController : Controller
    {
        public Cust Cust;
        public AbzContext db = new AbzContext();
        //public DefContext dd = new DefContext();
        public AbzHash abzHash;
        public AdminContext dba;
        public int CustID;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            try
            {

                base.Initialize(requestContext);

                string auth = GetCookie("Auth");

                if (!String.IsNullOrWhiteSpace(auth))
                {
                    dba = new AdminContext();
                    abzHash = dba.AbzHashs.Find(auth);
                    if (abzHash != null)
                    {

                        if (abzHash.CustID != null)
                        {
                            CustID = (int)abzHash.CustID;
                        }
                        else
                        {
                            string usr = User.Identity.Name;

                            CustID = db.UserAdmins.FirstOrDefault(u => u.Email == usr).CustID;

                            //IEnumerable<AspNetUser> aspnetusers = (from a in dd.AspNetUsers where a.Email == usr select new AspNetUser { Id = a.Id, CustID = a.CustID, Email = a.Email, UserName = a.UserName }).ToArray();

                            //CustID = (from a in db.UserAdmins
                            //         join b in aspnetusers on a.UserId equals b.Id
                            //         select (a.CustID)).FirstOrDefault();



                            //              //CustID = (from a in db.UserAdmins
                            //              //         join b in dd.AspNetUsers on a.UserId equals b.Id
                            //              //         where b.Email == usr
                            //              //         select (a.CustID)).FirstOrDefault();




                            abzHash.CustID = CustID;

                            UpdateHash(abzHash);
                        }
                        Cust = db.Custs.FirstOrDefault(c => c.CustId == CustID);
                    }
                }
            }
            catch
            {
                RedirectToAction("Logout", "Account");
            }
        }
        
        public void UpdateHash(AbzHash hash)
        {
            dba = new AdminContext();
            dba.AbzHashs.Add(hash);
            dba.Entry(abzHash).State = EntityState.Modified;
            dba.SaveChanges();
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