using Domain.Entities;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using WebUI.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;
using WebUI.Filter;
using Domain.Repository;
using Domain.Entities.Account;

namespace WebUI.Controllers
{
    [MyAuthAttribute]
    public class DocsController : BaseController
    {
        //DocsController()
        //{
        //    db = _db;
        //    hashRepo = _hashRepo;
        //    abzHash=_abzHash;
        //    Cust=_Cust;
        //    CustID=_CustID;
        //    Init();
        //}

        //[Authorize]
        public ActionResult Index()
        {
            ViewBag.MenuItem = "trust";
            List<Trust> trusts = db.Trusts.Where(t => t.CustId == Cust.CustId).ToList();
            return View(trusts);
        }

        public ActionResult AllDocs()
        {
            ViewBag.MenuItem = "doc";
            List<Doc> Docs = new List<Doc>();
            Docs =  db.Docs.Where(d => d.CustID == Cust.CustId).OrderByDescending(a => a.Ddat).ToList();
            return View(Docs);
        }

        public ActionResult Doc()
        {
            //this.Init();
            ViewBag.MenuItem = "doc";

            DocsModel docsModel = new DocsModel();
            docsModel.Docs = db.Docs.Where(d => d.CustID == Cust.CustId).OrderByDescending(a => a.Ddat).Take(10).ToList();
            docsModel.IsCustom = false;

            if (!String.IsNullOrWhiteSpace(Cust.Cod1s))
            {
                docsModel.IsCustom = true;
                docsModel.CustomInfo = db.CustomInfos.FirstOrDefault(d => d.Cod1s == Cust.Cod1s);
            }
                
            return View(docsModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Doc(CustomInfo custominfo)
        {
            custominfo.Cod1s = Cust.Cod1s;
            custominfo.txt = Cust.SmalName;

            if (ModelState.IsValid)
            {
                //CustomInfo customInfo = docsModel.CustomInfo;
                db.CustomInfos.Add(custominfo);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Doc");
        }

        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            Doc doc = db.Docs.Find(id);
            return File(doc.DocBin, "application//vnd.ms-excel", doc.FileName);
        }

        public ActionResult Contract()
        {
            ViewBag.MenuItem = "contract";
            List<Contract> contracts = db.Contracts.Where(t => t.CustID == Cust.CustId).ToList();                
            return View(contracts);
        }
        public ActionResult SchFact()
        {
            ViewBag.MenuItem = "chfact";
            ViewBag.mail = User.Identity.Name;                
            return View();
        }
        [HttpPost]
        public ActionResult SchFact(string note)
        {
            OrdInvoice ord = new OrdInvoice();
            ord.CustId = Cust.CustId;
            ord.Note = note;
            db.OrdInvoices.Add(ord);
            db.SaveChanges();

            ViewBag.mail = User.Identity.Name;
            return View("SchFactZak");
        }
    }
}
