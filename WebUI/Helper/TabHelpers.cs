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
    public static class TabHelpers
    {
        public static MvcHtmlString TableHeader(this HtmlHelper helper,  List<string> headers)
        {
            StringBuilder viewString = new StringBuilder();          // HTML - строка
            if (headers.Count > 0) 
            {
                viewString.Append("<table class=\"table table-bordered table-hover mt-3\"><thead><tr class=\"table-active\">");

                foreach (string header in headers)
                {
                    viewString.Append("<th scope=\"col\">" + header + "</th>");
                }
      
                viewString.Append("</tr></thead><tbody>");
            }
             
            return MvcHtmlString.Create(viewString.ToString());
        }

        public static MvcHtmlString TableEnd(this HtmlHelper helper) 
        {
            return MvcHtmlString.Create("</tbody></table>");
        }
    }
}