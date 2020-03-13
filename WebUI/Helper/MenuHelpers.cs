using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using WebUI.Models;

namespace WebUI.Helpers
{
    public static class MenuHelpers
    {
        public static MvcHtmlString MenuItemLink(this HtmlHelper helper, string caption, string url)
        {
            StringBuilder viewString = new StringBuilder();
            viewString.Append("<a class=\"nav-link\" role=\"button\" href=\"" + url + "\">");
            viewString.Append(caption);
            viewString.Append("<span class=\"chevron-item-wrapper\"><i class=\"chevron-item\"></i></span></a>");                                       
            return MvcHtmlString.Create(viewString.ToString());
        }
    }
}