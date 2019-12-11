using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Domain.Entities;
using Domain.ModelView;
using WebUI.Models;
using Domain.Repository;
using Calabonga.Xml.Exports;
using WebUI.Infrastructure;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace WebUI.Controllers
{
    public class BalancesController : BaseController
    {
        public int year = DateTime.Today.Year;
        public int dm = DateTime.Today.Month;
        private static int vsblpagescount = 10;    // Visible page numbers in page navigator central list 

        public int GetNRecByPage()
        {
            string cookieName = "npsize";
            HttpCookie cookie = Request.Cookies[cookieName];

            int npsize = 10;

            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName, npsize.ToString());
                Response.Cookies.Add(cookie);
            }
            else
            {
                npsize = Int32.Parse(cookie.Value);
            }

            if (npsize == 0)
                npsize = 1999999999;

            return npsize;
        }

        public void SetDat(int? selectedId, int? Id)
        {
            if (Id != null)
                year = (int)Id;
            else
            {
                if (Session["Year"] != null)
                    year = (int)Session["Year"];
                else
                    year = DateTime.Today.Year;
            }
            if (selectedId != null)
                dm = (int)selectedId;
            else
            {
                if (Session["Month"] != null)
                    dm = (int)Session["Month"];
                else
                    dm = DateTime.Today.Month;
            }
            Session["Month"] = dm;
            Session["Year"] = year;
        }
        [Authorize]
        public ActionResult Index(int? selectedId, int? Id, int pageNum = 1)
        {
            ViewBag.MenuItem = "rst";
            SetDat(selectedId, Id);
            DateOf datof = new DateOf(dm, year);
            ViewData["Month"] = new SelectList(datof.getMonth, "ID", "Mnth", dm);
            ViewData["Year"] = new SelectList(datof.getYear, "ID", "Mnth", year);
            return View();
        }
        public ActionResult SelectTtn(int? selectedId, int? Id, int pageNum = 0)
        {
            TtnRepository repo = new TtnRepository();
            SetDat(selectedId, Id);
            //Установили 1е число
            DateTime begDt = new DateTime(year, dm, 01, 0, 0, 0);
            //Установили 1е число следующего месяца
            DateTime endDt = begDt.AddMonths(1);

            int npsize = this.GetNRecByPage();

            TtnList ttn = new TtnList();


            int ContractID;

            string dog = GetCookie("Dog");
            if (!String.IsNullOrWhiteSpace(dog))
                ContractID = Convert.ToInt32(dog);
            else
                ContractID = 0;

            int count = repo.GetCount(Cust.CustId, begDt, endDt, ContractID);
            ttn.PageInfo = new PageInfo { pageNum = pageNum, itemsCount = count, pageSize = npsize, vsblPagesCount = vsblpagescount };
            ttn.Ttns = repo.GetSkipTake((pageNum)*npsize, npsize, Cust.CustId, begDt, endDt, ContractID).ToList();
            return PartialView(ttn);
        }

        public ExcelResult Export()
        {
            const string fileName = "Reestr.xls";
            // get data
            int dm = (int)Session["Month"];

            int year = (int)Session["Year"];
            int ContractID;

            string dog = GetCookie("Dog");
            if (!String.IsNullOrWhiteSpace(dog))
                ContractID = Convert.ToInt32(dog);
            else
                ContractID = 0;
            //Установили 1е число
            DateTime begDt = new DateTime(year, dm, 01, 0, 0, 0);
            //Установили 1е число следующего месяца
            DateTime endDt = begDt.AddMonths(1);
            TtnRepository repo = new TtnRepository();
            List<Ttn> ttn = repo.GetTtn(Cust.CustId, begDt, endDt, ContractID);

            var items = repo.GetTtn(Cust.CustId, begDt, endDt, ContractID);
            var title = $"Реестр отрузки Абз-4 с {begDt.ToShortDateString()} по {endDt.ToShortDateString()}";
            var html = ExportBuilder.Build(items, title);

            // prepare virtual file
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(html);
                writer.Flush();
                stream.Position = 0;
              }
            return new ExcelResult(fileName, html);
        }

        [Authorize]
        public ActionResult Balance()
        {
            BalanceRepository repo = new BalanceRepository();
            ViewBag.MenuItem = "balance";
            int ContractID;
            string dog = GetCookie("Dog");
            if (!String.IsNullOrWhiteSpace(dog))
                ContractID = Convert.ToInt32(dog);
            else
                ContractID = 0;

            return View(repo.GetListBalance(Cust.CustId, ContractID));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
