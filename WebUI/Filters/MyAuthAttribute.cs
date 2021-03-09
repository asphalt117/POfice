using Domain.Entities.Account;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace WebUI.Filter
{
    public class MyAuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        public bool Succes = false;
        AbzHashRepo hashRepo = new AbzHashRepo();
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            string auth = GetCookie("Auth",filterContext);
            AbzHash abzHash = hashRepo.GetHash(auth);
            if (abzHash==null)
            {
                Succes = true;
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
                Succes = false;
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (Succes)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                    { "controller", "Account" }, { "action", "Login" }
                   });
            }
        }
        private string GetCookie(string cookieName, AuthenticationContext filterContext)
        {
            string cookieValue = "";
            HttpCookie cookie = filterContext.HttpContext.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookieValue = cookie.Value;
            }
            return cookieValue;
        }
    }
}