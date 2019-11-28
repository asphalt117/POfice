using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;

namespace WebUI.Controllers
{

    public class AdresController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            //Menu.ChangeSelected(2, 6);
            List<Adres> adres = await db.Adreses.Where(p => p.CustId == Cust.CustId).OrderBy(x=>x.txt).ToListAsync();
            return View(adres);
        }

        // GET: Adres/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adres adres = await db.Adreses.FindAsync(id);
            if (adres == null)
            {
                return HttpNotFound();
            }
            return View(adres);
        }                
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AdresID,txt,Rast")] Adres adres)
        {
            if (ModelState.IsValid)
            {
                adres.CustId = Cust.CustId;
                db.Adreses.Add(adres);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(adres);
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
