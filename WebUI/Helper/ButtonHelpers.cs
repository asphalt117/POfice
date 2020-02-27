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
    public static class ButtonHelpers
    {
        public static MvcHtmlString SmplBtn(this HtmlHelper helper, string action, string title, string caption, string url, string clss=null)
        {
            string faIcon="";

            if (string.IsNullOrEmpty(clss))
            {
                clss = "btn btn-light";
            }

            switch (action)
            {
                case "return":
                    faIcon = "fa fa-angle-double-left";
                    break;

                case "add":
                    faIcon = "fa fa-plus-square";
                    break;

                case "edit":
                    faIcon = "fa fa-edit";
                    break;

                case "delete":
                    faIcon = "fa fa-trash-alt";
                    break;

                case "copy":
                    faIcon = "fa fa-copy";
                    break;

                case "detail":
                    faIcon = "fa fa-ellipsis-h";
                    break;

                case "toxls":
                    faIcon = "fa fa-file-download";
                    break;
            }

            StringBuilder viewString = new StringBuilder();          // HTML - строка

            viewString.Append("<a class=\"" + clss + "\" role=\"button\" title=\"" + title);
            viewString.Append("\" href=\"" + url + "\">");
            viewString.Append("<i class=\"" + faIcon + "\"></i>");
            viewString.Append(string.IsNullOrEmpty(caption) ? "" : "&nbsp;&nbsp;" + caption);
            viewString.Append("</a>");

            return MvcHtmlString.Create(viewString.ToString());
        }
         
        public static MvcHtmlString SaveBtn(this HtmlHelper helper)
        {
            StringBuilder viewString = new StringBuilder();
            viewString.Append("<input type=\"submit\" value=\"Сохранить\" class=\"btn btn-success\" role=\"button\" />");
            return MvcHtmlString.Create(viewString.ToString());
        }

        public static MvcHtmlString SubmitBtn(this HtmlHelper helper, string caption, string title=null)
        {
            StringBuilder viewString = new StringBuilder();
            viewString.Append("<button type=\"submit\" class=\"btn btn-success\" title=\"" + title + "\" role=\"button\">" + caption + "</button>");
            return MvcHtmlString.Create(viewString.ToString());
        }
    }
}