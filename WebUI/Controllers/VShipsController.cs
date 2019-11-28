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
using Domain.ModelView;
using Domain.Repository;

namespace WebUI.Controllers
{
    public class VShipsController : BaseController
    {
        private AbzContext db = new AbzContext();
        
        public async Task<ActionResult> Index()
        {
            Menu.ChangeSelected(4, 1);
            Session["Menu"] = Menu;
            List<VShip> vShip = await db.VShips.Where(d => d.CustId == Menu.CustId).OrderBy(x => x.Dat).ToListAsync();
            return View(vShip);
        }
        public ActionResult ChGood(int id)
        {
            Session["id"] = id;
            return RedirectToAction("Index", "Good", new { act = "Good", cont = "VShips" });
        }
        public ActionResult Good(int id)
        {
            Good good = db.Goods.Find(id);
            ShipOrderDetailView svd = new ShipOrderDetailView();
            svd.GoodId = good.GoodID;
            svd.Good = good.txt;
            svd.Unit = good.Unit;
            svd.ShipOrderDetailViewId = (int)Session["id"];
            return View(svd);
        }
        [HttpPost]
        public ActionResult Good(ShipOrderDetailView svd)
        {
            svd.ShipOrderDetailViewId = (int)Session["id"];
            List<ShipOrderDetailView> detal = (List<ShipOrderDetailView>)Session["Detal"];
            int ShipOrderId=0;
            foreach (var o in detal)
            {
                ShipOrderId = o.ShipOrderId;
            }
            VShipRepository repo = new VShipRepository();
            repo.SaveDetail(ShipOrderId, svd);
            //db.SaveChanges();
            return RedirectToAction("Edit", "VShips", new { id = ShipOrderId });
        }

        public ActionResult NewAdres()
        {
            Adres adres = new Adres();
            return View(adres);
        }
        [HttpPost]
        public ActionResult NewAdres(Adres adres )
        {
            adres.CustId = Menu.CustId;
            db.Adreses.Add(adres);
            db.SaveChanges();
            ShipView order = (ShipView)Session["Order"];
            int sid = order.ShipViewId;
            return RedirectToAction("Edit","VShips",new { id = sid });                
        }


        public ActionResult Create()
        {
            VShipRepository repo = new VShipRepository();
            ViewBag.cust = Menu.Cust;
            ShipView vShip = repo.GetCreate(Menu.CustId);

            return View(vShip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShipView vShip)
        {
            if (ModelState.IsValid)
            {
                VShipRepository repo = new VShipRepository();
                return RedirectToAction("Index");
            }
            return View(vShip);
        }

        public async Task<ActionResult> Edit(int id)
        {                
            VShipRepository repo = new VShipRepository();
            ShipView vShip = repo.GetChange(id);

            Session["Detal"]= vShip.Detal;
            if (vShip == null)
            {
                return HttpNotFound();
            }
            return View(vShip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ShipView vShip,string Adres, string Person)
        {
            List<ShipOrderDetailView> detal = (List<ShipOrderDetailView>)Session["Detal"];
            VShipRepository repo = new VShipRepository();
            Session["Order"] = vShip;
            repo.Save(vShip, detal);
            if (Adres == "Новый")
                    return RedirectToAction("NewAdres");
            if (Person == "Новый")
                return RedirectToAction("NewPerson");

            return RedirectToAction("Index");
        }

        // GET: VShips/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VShip vShip = await db.VShips.FindAsync(id);
            if (vShip == null)
            {
                return HttpNotFound();
            }
            return View(vShip);
        }

        // POST: VShips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VShip vShip = await db.VShips.FindAsync(id);
            db.VShips.Remove(vShip);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VShip vShip = await db.VShips.FindAsync(id);
            if (vShip == null)
            {
                return HttpNotFound();
            }
            return View(vShip);
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
