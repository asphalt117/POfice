using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Domain.Entities;

namespace WebUI.Controllers
{
    public class CustomInfoController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            if (String.IsNullOrWhiteSpace(Cust.Cod1s))
                return View("NoCod1s");                

            CustomInfo cinfo = db.CustomInfos.FirstOrDefault(d => d.Cod1s == Cust.Cod1s);
            if (cinfo==null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("Details");
            }
        }
        public ActionResult Details()
        {
            ViewBag.MenuItem = "info";
            CustomInfo customInfo = db.CustomInfos.FirstOrDefault(d => d.Cod1s == Cust.Cod1s);
            if (customInfo == null)
            {
                return HttpNotFound();
            }
            return PartialView(customInfo);
        }
        public ActionResult Create()
        {
            ViewBag.MenuItem = "info";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CustomInfoId,Cod1s,Txt,Mail,isEmpty")] CustomInfo customInfo)
        {            
            customInfo.Cod1s = Cust.Cod1s;
            customInfo.txt = Cust.SmalName;
            if (ModelState.IsValid)
            {
                db.CustomInfos.Add(customInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customInfo);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomInfo customInfo = await db.CustomInfos.FindAsync(id);
            if (customInfo == null)
            {
                return HttpNotFound();
            }
            return View(customInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CustomInfoId,Cod1s,Txt,Mail,isEmpty")] CustomInfo customInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                // return RedirectToAction("Index");
                return RedirectToAction("Doc", "Docs");
            }
            return View(customInfo);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomInfo customInfo = await db.CustomInfos.FindAsync(id);
            if (customInfo == null)
            {
                return HttpNotFound();
            }
            return View(customInfo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CustomInfo customInfo = await db.CustomInfos.FindAsync(id);
            db.CustomInfos.Remove(customInfo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
