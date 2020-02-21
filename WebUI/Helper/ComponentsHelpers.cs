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
    public static class ComponentsHelpers
    {
        public static MvcHtmlString ButtonAdd(this HtmlHelper helper, string title, string action, string controller, string prms)
        {
            StringBuilder viewString = new StringBuilder();          // HTML - строка

            viewString.Append("<p><a class=\"btn btn-light\" role=\"button\" title=\"" + title);
            viewString.Append("\" href=\"@Url.Action(\"" + action);
            viewString.Append("\", \"" + controller);
            viewString.Append("\", " + prms + ")>");
            viewString.Append("<i class=\"fa fa-plus-square\"></i>&nbsp;&nbsp;Создать</a></p>");

            return MvcHtmlString.Create(viewString.ToString());
        }

        public static MvcHtmlString ButtonReturn(this HtmlHelper helper, string title, string action, string controller, string prms, string caption)
        {
            StringBuilder viewString = new StringBuilder();          // HTML - строка

            viewString.Append("<p><a class=\"btn btn-light\" role=\"button\" title=\"" + title);
            viewString.Append("\" href=\"@Url.Action(\"" + action);
            viewString.Append("\", \"" + controller);
            viewString.Append((prms != null ? "\", " + prms : "") + ")>");
            viewString.Append("<i class=\"fa fa-angle-double-left\"></i>&nbsp;&nbsp;" + caption + "</a></p>");

            return MvcHtmlString.Create(viewString.ToString());
        }
    }
}