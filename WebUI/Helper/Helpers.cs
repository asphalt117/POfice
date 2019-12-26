using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using WebUI.Models;

namespace WebUI.Helpers
{
    public static class Helpers
    {
        static string spanspacermd = "<span class=\"d-none d-md-inline\">&nbsp;</span>";

        public static MvcHtmlString SpanSpacerMd(this HtmlHelper helper)
        {
            StringBuilder spanspacerString = new StringBuilder();        
            spanspacerString.Append(spanspacermd);
            return MvcHtmlString.Create(spanspacerString.ToString());
        }

        public static MvcHtmlString logobrand1(this HtmlHelper helper)
        {
            StringBuilder logoString = new StringBuilder();
            logoString.Append("А" + spanspacermd + "Б" + spanspacermd + "З");
            return MvcHtmlString.Create(logoString.ToString());
        }

        public static MvcHtmlString logobrand2(this HtmlHelper helper)
        {
            StringBuilder logoString = new StringBuilder();
            logoString.Append("К" + spanspacermd + "А" + spanspacermd + "П" + spanspacermd + "О" + spanspacermd + "Т" + spanspacermd + "Н" + spanspacermd + "Я");
            return MvcHtmlString.Create(logoString.ToString());
        }
    }
}