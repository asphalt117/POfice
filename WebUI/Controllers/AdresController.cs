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
using WebUI.ModelView;

namespace WebUI.Controllers
{
    public class AdresController : BaseController
    {
        public ActionResult AdresOrder(int ord)
        {
            AdresView adres = new AdresView(ord, (int)abzHash.CustID);
            return View(adres);
        }

        public ActionResult SelectAdres(int id,int ord)
        {

            Order order = db.Orders.Find(ord);
            order.AdresId = id;
            order.Centr = true;
            order.Step = 4;
            db.Orders.Add(order);
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Booking", "Ord", new { ord = ord });
        }
        [Authorize]
        public async Task<ActionResult> Index()
        {
            ViewBag.MenuItem = "adres";
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

                string cord = GetCookie("Order");
                if (String.IsNullOrWhiteSpace(cord))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    int ord = int.Parse(cord);
                    return RedirectToAction("AdresOrder",  new { ord = ord });
                }
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
