using Domain.Entities;
using Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Filter
{
    public class UserAttribute : AuthorizeAttribute
    {
        private bool LocalAl;
        public UserAttribute(bool alParam)
        {
            LocalAl = alParam;
        }
        public UserAttribute()
        {            
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string auth = GetCookie("Auth", httpContext);
            if (!String.IsNullOrWhiteSpace(auth))
            {
                AbzContext db = new AbzContext();
                AbzHash abzHash = db.AbzHashes.Find(auth);
                if (abzHash != null)
                    { return true; }
            }
            return false;
        }
        public string GetCookie(string cookieName, HttpContextBase httpContext)
        {
            string cookieValue = "";
            HttpCookie cookie = httpContext.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookieValue = cookie.Value;
            }
            return cookieValue;
        }

    }
}