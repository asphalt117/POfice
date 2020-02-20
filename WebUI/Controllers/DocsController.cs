using Domain.Entities;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebUI.Controllers
{
    public class DocsController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.MenuItem = "trust";
            List<Trust> trusts = db.Trusts.Where(t => t.CustId == Cust.CustId).ToList();
            return View(trusts);
        }

        public ActionResult Doc()
        {
            ViewBag.MenuItem = "doc";
            List<Doc> docs = db.Docs.Where(d => d.CustID == Cust.CustId).ToList();
            return View(docs);
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
