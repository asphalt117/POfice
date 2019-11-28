using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using WebUI.Models;

namespace WebUI.Helpers
{
    public static class Pagination
    {
        // Кнопки управления представлением информации
        public static MvcHtmlString PageSelectView(this HtmlHelper helper, string sortBy, string listView)
        {
            StringBuilder viewString = new StringBuilder();          // HTML - строка списка кнопок
            viewString.Append("<div class=\"col-md-12 page-view\">");
            viewString.Append("<div class=\"btn-group pull-left prod-sort\"><a class=\"btn btn-default dropdown-toggle\" id=\"drop-sortby\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\" href=\"#\">Сортировать по:&nbsp;&nbsp;&nbsp;<span class=\"caret\"></span></a>");
            viewString.Append("<ul class=\"dropdown-menu\" role=\"menu\" aria-labelledby=\"drop-sortby\">");
            viewString.Append("<li class=\"" + (sortBy == "rating" ? "active" : "") + "\"><a href=\"#\" onclick=\"JavaScript: set_prodsortby('rating');\">Популярности</a></li>");
            viewString.Append("<li class=\"" + (sortBy == "name" ? "active" : "") + "\"><a href=\"#\" onclick=\"JavaScript: set_prodsortby('name');\">Названию</a></li></ul></div>");
            viewString.Append("<div class=\"btn-group pull-right prod-view\" data-toggle=\"buttons\">");
            viewString.Append("<label class=\"btn btn-primary glyphicon glyphicon-list" + (listView == "list" ? " active " : "") + "\" title=\"Вывести список товаров в виде списка\">");
            viewString.Append("<input autocomplete=\"off\" disabled=\"disabled\" id=\"view-list\" name=\"list\" onchange=\"JavaScript: set_prodview(this.value);\" type=\"radio\" value=\"list\"></label>");
            viewString.Append("<label class=\"btn btn-primary glyphicon glyphicon-th" + (listView == "table" ? " active " : "") + "\" title=\"Вывести список товаров в виде таблицы\">");
            viewString.Append("<input autocomplete=\"off\" disabled=\"disabled\" id=\"view-table\" name=\"table\" onchange=\"JavaScript: set_prodview(this.value);\" type=\"radio\" value=\"table\"></label></div></div>");

            return MvcHtmlString.Create(viewString.ToString());
        }

        // Кнопки управления представлением информации
        public static MvcHtmlString PageSelectSort(this HtmlHelper helper, string sortBy)
        {
            StringBuilder viewString = new StringBuilder();          // HTML - строка списка кнопок
            viewString.Append("<div class=\"clearfix page-view\">");
            viewString.Append("<div class=\"btn-group pull-left prod-sort\"><a class=\"btn btn-default dropdown-toggle\" id=\"drop-sortby\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\" href=\"#\">Сортировать по:&nbsp;&nbsp;&nbsp;<span class=\"caret\"></span></a>");
            viewString.Append("<ul class=\"dropdown-menu\" role=\"menu\" aria-labelledby=\"drop-sortby\">");
            viewString.Append("<li class=\"" + (sortBy == "rating" ? "active" : "") + "\"><a href=\"#\" onclick=\"JavaScript: set_prodsortby('rating');\">Популярности</a></li>");
            viewString.Append("<li class=\"" + (sortBy == "name" ? "active" : "") + "\"><a href=\"#\" onclick=\"JavaScript: set_prodsortby('name');\">Названию</a></li></ul></div></div>");

            return MvcHtmlString.Create(viewString.ToString());
        }


        // Кнопки управления представлением информации - число записей на странице
        public static MvcHtmlString PageSelectNRecByPage(this HtmlHelper helper, int npsize)
        {
            StringBuilder viewString = new StringBuilder();          // HTML - строка списка кнопок
            viewString.Append("<div class=\"clearfix page-view\">");
            viewString.Append("<div class=\"btn-group pull-right nrecbypage\"><a class=\"btn btn-default dropdown-toggle\" id=\"drop-nrecbypage\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\" href=\"#\">Выводить записей:&nbsp;&nbsp;&nbsp;<span class=\"caret\"></span></a>");
            viewString.Append("<ul class=\"dropdown-menu\" role=\"menu\" aria-labelledby=\"drop-nrecbypage\">");
            viewString.Append("<li class=\"" + (npsize == 20 ? "active" : "") + "\"><a href=\"#\" onclick=\"JavaScript: set_nrecbypage('20');\">20</a></li>");
            viewString.Append("<li class=\"" + (npsize == 40 ? "active" : "") + "\"><a href=\"#\" onclick=\"JavaScript: set_nrecbypage('40');\">40</a></li>");
            viewString.Append("<li class=\"" + (npsize == 60 ? "active" : "") + "\"><a href=\"#\" onclick=\"JavaScript: set_nrecbypage('60');\">60</a></li>");
            viewString.Append("<li class=\"" + (npsize == 100 ? "active" : "") + "\"><a href=\"#\" onclick=\"JavaScript: set_nrecbypage('100');\">100</a></li>");
            viewString.Append("<li class=\"" + (npsize > 100 ? "active" : "") + "\"><a href=\"#\" onclick=\"JavaScript: set_nrecbypage('0');\">Все</a></li></ul></div></div>");
            return MvcHtmlString.Create(viewString.ToString());
        }

        // Вывод информации о текущей странице в общем списке
        public static MvcHtmlString PageNavigatorInfo(this HtmlHelper helper, PageInfo pageInfo)
        {
            StringBuilder navString = new StringBuilder();          // HTML - строка навигатора 

            // Математические расчеты
            int curPageNum = pageInfo.pageNum + 1;                  // Выбранная страница диапазона
            int pagesCount = (pageInfo.itemsCount / pageInfo.pageSize) + (pageInfo.itemsCount % pageInfo.pageSize > 0 ? 1 : 0); // Общее число страниц
            navString.Append("<div class=\"text-center page-info\"><span>Страница " + (pagesCount < curPageNum ? 0 : curPageNum).ToString() + " из " + pagesCount.ToString() + "</span></div>");

            return MvcHtmlString.Create(navString.ToString());
        }


        // Karasiov aka TEREK, BootStrap page navigator...............................................................................................................................
        // User interface struct: << < 1,2,3,....n > >> , there is have a Left Control (<< <), Central Page List and Right Control (> >>)
        public static MvcHtmlString PageNavigatorBootStrap(this HtmlHelper helper, PageInfo pageInfo, string actionName, Func<int, object> paramList)
        {
            StringBuilder navString = new StringBuilder();                      // HTML - строка навигатора 
            TagBuilder emptyLink = new TagBuilder("a");                         // Пустая ссылка
            emptyLink.MergeAttribute("href", "#");
            emptyLink.MergeAttribute("role", "button");

            // Математические расчеты
            int curPageNum = pageInfo.pageNum + 1;                              // Выбранная страница диапазона
            int pagesCount = (pageInfo.itemsCount / pageInfo.pageSize) + (pageInfo.itemsCount % pageInfo.pageSize > 0 ? 1 : 0); // Общее число страниц
            int midPageNum = (pagesCount / 2) + (pagesCount % 2 > 0 ? 1 : 0);   // Средняя страница
            int leftCount = 0;                                                  // Количество страниц слева от выбранной страницы в рамках диапазона
            int rightCount = 0;                                                 // Количество страниц справа от выбранной страницы в рамках диапазона

            if (curPageNum <= midPageNum)                                       // Проверка в какую сторону сдвигать выбранную страницу в диапазоне, если дипазон четный
            {
                leftCount = ((pageInfo.vsblPagesCount - 1) / 2);
                rightCount = ((pageInfo.vsblPagesCount - 1) / 2) + ((pageInfo.vsblPagesCount - 1) % 2 > 0 ? 1 : 0);
            }
            else
            {
                leftCount = ((pageInfo.vsblPagesCount - 1) / 2) + ((pageInfo.vsblPagesCount - 1) % 2 > 0 ? 1 : 0);
                rightCount = ((pageInfo.vsblPagesCount - 1) / 2);
            }

            int startPageNum = curPageNum - leftCount;                          // Начальная страница диапазона
            int endPageNum = curPageNum + rightCount;                           // Конечная страница диапазона
            if (startPageNum <= 0)
            {
                startPageNum = 1;
                endPageNum = pageInfo.vsblPagesCount > pagesCount ? pagesCount : pageInfo.vsblPagesCount;
            }

            if (endPageNum > pagesCount)
            {
                endPageNum = pagesCount;
                startPageNum = pagesCount - pageInfo.vsblPagesCount <= 0 ? 1 : pagesCount - pageInfo.vsblPagesCount;
            }

            // Begin part
            navString.Append("<div class=\"col-md-12 text-center container-fluid clearfix page-navigator\"><ul class=\"pagination\">");

            // Right control
            TagBuilder strToFirst = new TagBuilder("li");   // to first
            if (curPageNum == 1)
            {
                strToFirst.AddCssClass("disabled");
                emptyLink.MergeAttribute("rel", "first");
                emptyLink.MergeAttribute("aria_label", "First");
                emptyLink.InnerHtml = "&laquo;";
                strToFirst.InnerHtml = emptyLink.ToString();
            }
            else { strToFirst.InnerHtml = helper.ActionLink(HttpUtility.HtmlDecode("&laquo;"), actionName, paramList(0), new { @aria_label = "First", @rel = "first", @role = "button" }).ToHtmlString(); }
            navString.Append(strToFirst.ToString());

            TagBuilder strToPrev = new TagBuilder("li");    // to previous
            if (curPageNum == 1)
            {
                strToPrev.AddCssClass("disabled");
                emptyLink.MergeAttribute("rel", "previous", true);
                emptyLink.MergeAttribute("aria_label", "Previous", true);
                emptyLink.InnerHtml = "&lsaquo;";
                strToPrev.InnerHtml = emptyLink.ToString();
            }
            else { strToPrev.InnerHtml = helper.ActionLink(HttpUtility.HtmlDecode("&lsaquo;"), actionName, paramList(curPageNum - 2), new { @aria_label = "Previous", @rel = "previous", @role = "button" }).ToHtmlString(); }
            navString.Append(strToPrev.ToString());

            // Central control
            for (int i = 0; i < (endPageNum - startPageNum + 1); i++)
            {
                TagBuilder strToCentral = new TagBuilder("li");
                if (startPageNum + i == curPageNum) { strToCentral.AddCssClass("active"); }
                strToCentral.InnerHtml = helper.ActionLink((startPageNum + i).ToString(), actionName, paramList((startPageNum + i) - 1), new { @role = "button" }).ToHtmlString();
                navString.Append(strToCentral.ToString());
            }

            // Left control
            TagBuilder strToNext = new TagBuilder("li");   // to next
            if (curPageNum == pagesCount)
            {
                strToNext.AddCssClass("disabled");
                emptyLink.MergeAttribute("rel", "next", true);
                emptyLink.MergeAttribute("aria_label", "Next", true);
                emptyLink.InnerHtml = "&rsaquo;";
                strToNext.InnerHtml = emptyLink.ToString();
            }
            else { strToNext.InnerHtml = helper.ActionLink(HttpUtility.HtmlDecode("&rsaquo;"), actionName, paramList(curPageNum), new { @aria_label = "Next", @rel = "next", @role = "button" }).ToHtmlString(); }
            navString.Append(strToNext.ToString());

            TagBuilder strToLast = new TagBuilder("li");    // to last
            if (curPageNum == pagesCount)
            {
                strToLast.AddCssClass("disabled");
                emptyLink.MergeAttribute("rel", "last", true);
                emptyLink.MergeAttribute("aria_label", "Last", true);
                emptyLink.InnerHtml = "&raquo;";
                strToLast.InnerHtml = emptyLink.ToString();
            }
            else { strToLast.InnerHtml = helper.ActionLink(HttpUtility.HtmlDecode("&raquo;"), actionName, paramList(pagesCount - 1), new { @aria_label = "Last", @rel = "last", @role = "button" }).ToHtmlString(); }
            navString.Append(strToLast.ToString());

            // End part
            navString.Append("</ul></div>");

            return MvcHtmlString.Create(navString.ToString());
        }
        //............................................................................................................................................................................

        // Karasiov aka TEREK page navigator............................................................................................................................................
        // User interface struct: << < 1,2,3,....n > >> , there is have a Left Control (<< <), Central Page List and Right Control (> >>)
        public static MvcHtmlString PageNavigator(this HtmlHelper helper, PageInfo pageInfo, string actionName, Func<int, object> paramList)
        {
            StringBuilder navString = new StringBuilder();         // HTML - строка навигатора 

            // Математические расчеты
            int curPageNum = pageInfo.pageNum + 1;                                                // Выбранная страница диапазона
            int pagesCount = (int)Math.Ceiling((double)pageInfo.itemsCount / pageInfo.pageSize); // Общее число страниц
            int midPageNum = (int)Math.Ceiling((double)pagesCount / 2);                         // Средняя страница
            int leftCount = 0;                                                                 // Количество страниц слева от выбранной страницы в рамках диапазона
            int rightCount = 0;                                                               // Количество страниц справа от выбранной страницы в рамках диапазона
            if (curPageNum <= midPageNum)                                                    // Проверка в какую сторону сдвигать выбранную страницу в диапазоне, если дипазон четный
            {
                leftCount = (int)Math.Floor((double)(pageInfo.vsblPagesCount - 1) / 2);
                rightCount = (int)Math.Ceiling((double)(pageInfo.vsblPagesCount - 1) / 2);
            }
            else
            {
                leftCount = (int)Math.Ceiling((double)(pageInfo.vsblPagesCount - 1) / 2);
                rightCount = (int)Math.Floor((double)(pageInfo.vsblPagesCount - 1) / 2);
            }
            int startPageNum = curPageNum - leftCount;                         // Начальная страница диапазона
            int endPageNum = curPageNum + rightCount;                         // Конечная страница диапазона
            if (startPageNum <= 0)
            {
                startPageNum = 1;
                endPageNum = pageInfo.vsblPagesCount > pagesCount ? pagesCount : pageInfo.vsblPagesCount;
            }

            if (endPageNum > pagesCount)
            {
                endPageNum = pagesCount;
                startPageNum = pagesCount - pageInfo.vsblPagesCount <= 0 ? 1 : pagesCount - pageInfo.vsblPagesCount;
            }

            // Левая управляющая часть навигатора
            if (curPageNum == 1)
            {
                TagBuilder strToFirst = new TagBuilder("span");
                strToFirst.AddCssClass("page-navigator-btn");
                strToFirst.InnerHtml = "<<";
                navString.Append(strToFirst.ToString());
                //navString.Append(" ");
                TagBuilder strToPrev = new TagBuilder("span");
                strToPrev.AddCssClass("page-navigator-btn");
                strToPrev.InnerHtml = "<";
                navString.Append(strToPrev.ToString());
            }
            else
            {
                navString.Append(helper.ActionLink("<<", actionName, paramList(0), new { @class = "page-navigator-btn", @rel = "first", @role = "button" }));
                //navString.Append(" ");
                // Вычитание 2 из-за того что нумерация страниц начинается с нуля
                navString.Append(helper.ActionLink("<", actionName, paramList(curPageNum - 2), new { @class = "page-navigator-btn", @rel = "prev", @role = "button" }));
            }
            //navString.Append(" ");

            // Центральная часть - список страниц диапазона
            for (int i = 0; i < (endPageNum - startPageNum + 1); i++)
            {
                if (startPageNum + i == curPageNum) // Текущая страница
                {
                    //navString.Append(" ");
                    TagBuilder strCur = new TagBuilder("span");
                    strCur.AddCssClass("page-navigator-btn-selected");
                    strCur.InnerHtml = curPageNum.ToString();
                    navString.Append(strCur.ToString());
                    //navString.Append(" ");
                }
                else
                {
                    navString.Append(helper.ActionLink((startPageNum + i).ToString(), actionName, paramList((startPageNum + i) - 1), new { @class = "page-navigator-btn", @role = "button" }));
                }
                //navString.Append(" ");
            }

            // Правая управляющая часть навигатора
            if (curPageNum == pagesCount)
            {
                TagBuilder strToNext = new TagBuilder("span");
                strToNext.AddCssClass("page-navigator-btn");
                strToNext.InnerHtml = ">";
                navString.Append(strToNext.ToString());
                //navString.Append(" ");
                TagBuilder strToEnd = new TagBuilder("span");
                strToEnd.AddCssClass("page-navigator-btn");
                strToEnd.InnerHtml = ">>";
                navString.Append(strToEnd.ToString());
            }
            else
            {
                navString.Append(helper.ActionLink(">", actionName, paramList(curPageNum), new { @class = "page-navigator-btn", @rel = "next", @role = "button" }));
                //navString.Append(" ");
                navString.Append(helper.ActionLink(">>", actionName, paramList(pagesCount - 1), new { @class = "page-navigator-btn", @rel = "last", @role = "button" }));
            }

            return MvcHtmlString.Create(navString.ToString());
        }
        //............................................................................................................................................................................

        public static MvcHtmlString AjaxPagingNavigator(this AjaxHelper helper, string updateTargetId,
            int pageNum, int itemsCount, int pageSize, int linksPerPage = 10,
            string actionName = "Index")
        {
            StringBuilder sb = new StringBuilder();
            int pagesCount = (int)Math.Ceiling((double)itemsCount / pageSize);
            int startPage = pageNum / linksPerPage * linksPerPage;
            int visiblePages = startPage + linksPerPage <=
                pagesCount ? linksPerPage : pagesCount - startPage;
            AjaxOptions ao = new AjaxOptions();
            ao.UpdateTargetId = updateTargetId;
            ao.OnBegin = "beginUpdate" + updateTargetId;
            ao.OnComplete = "completeUpdate" + updateTargetId;

            string script = @"
    <script type=""text/javascript"">
        var _currentPageNum = -1;

        Sys.Application.add_init(pageInitupdateTargetId);

        function pageInitupdateTargetId() {
            // Enable history
            Sys.Application.set_enableHistory(true);

            // Add Handler for history
            Sys.Application.add_navigate(navigateupdateTargetId);
        }

        function navigateupdateTargetId(sender, e) {
            // Get pageNum from address bar
            var pageNum = e.get_state().pageNum;

            // If pageNum != currentPageNum then navigate
            if (pageNum != _currentPageNum) {
                _currentPageNum = pageNum;
                $('#updateTargetId').load(""?pageNum="" + pageNum);
            }
        }

        function beginUpdateupdateTargetId(args) {
            _currentPageNum = this.getAttribute(""rel"");

            // Add history point
            Sys.Application.addHistoryPoint({ ""pageNum"": _currentPageNum });

            // Animate
            $('#updateTargetId').fadeOut('normal');
        }

        function completeUpdateupdateTargetId() {
            // Animate
            $('#updateTargetId').fadeIn('normal');
        }
    </script>";

            sb.AppendLine(script.Replace("updateTargetId", updateTargetId));

            if (pageNum > 0)
            {
                sb.Append(helper.ActionLink("<<", actionName, new { pageNum = 0 }, ao, new { rel = 0 }));
                sb.Append(" ");
                int pageBackNum = startPage - 1;
                if (pageBackNum > 0)
                {
                    sb.Append(helper.ActionLink("<", actionName, new { pageNum = pageBackNum },
                        ao, new { rel = pageBackNum }));
                }
                else
                {
                    sb.Append(HttpUtility.HtmlEncode("<"));
                }
            }
            else
            {
                sb.Append(HttpUtility.HtmlEncode("<< <"));
            }

            sb.Append(" ");

            for (int i = 0; i < visiblePages; i++)
            {
                int currentPage = i + startPage;
                string currentPageText = (currentPage + 1).ToString();
                if (currentPage != pageNum)
                {
                    sb.Append(helper.ActionLink(currentPageText, actionName,
                        new { pageNum = currentPage }, ao, new { rel = currentPage }));
                }
                else
                {
                    sb.Append(currentPageText);
                }
                sb.Append(" ");
            }

            sb.Append(" ");

            if (pageNum < pagesCount - 1)
            {
                int pageNextNum = startPage + visiblePages;
                if (pageNextNum < pagesCount)
                {
                    sb.Append(helper.ActionLink(">", actionName, new { pageNum = pageNextNum },
                        ao, new { rel = pageNextNum }));
                }
                else
                {
                    sb.Append(HttpUtility.HtmlEncode(">"));
                }
                sb.Append(" ");
                sb.Append(helper.ActionLink(">>", actionName, new { pageNum = (pagesCount - 1) },
                    ao, new { rel = (pagesCount - 1) }));
            }
            else
            {
                sb.Append(HttpUtility.HtmlEncode("> >>"));
            }

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}