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
using Domain.Repository;
using WebUI.Models;
using Domain.ModelView;

namespace WebUI.Controllers
{
    public class TrustTecsController : BaseController
    {
        private TrustTecsRepository repo;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            repo = new TrustTecsRepository(Cust.CustId);
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.MenuItem = "tec";
            return View(await repo.Get());
        }
        public async Task<ActionResult> SelTrans()
        {
            List<Transport> trans = await repo.GetList(); 
            return View(trans);
        }
 
        public ActionResult Create()
        {
            TrustTecV tecV= new TrustTecV();
            if (Session["TecDet"] != null)            
            {
                tecV.Detail = (List<TrustTecDet>)Session["TecDet"];
            }
            return View(tecV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TrustTecV tecV,string tr)
        {
            //Не возвращаются детали
            if (ModelState.IsValid && tr == null && Session["TecDet"] != null)
            {      
                tecV.Detail = (List<TrustTecDet>)Session["TecDet"];
                await repo.Save(tecV);
                Session["TecDet"] = null;
                return RedirectToAction("Index");
            }
            Session["TrustTecV"] = tecV;
            if (tr != null)
            {
                return RedirectToAction("CreateTec");
            }
            return View(tecV);
        }
        public async Task<ActionResult> CreateTec(string id)
        {
            if (id != null)
            {
                TrustTecDet detal = await repo.Get(id);
                return View(detal);
            }
            return View(new TrustTecDet());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTec(TrustTecDet det)
        {
            if (ModelState.IsValid)
            {
                List<TrustTecDet> tecs;
                if (Session["TecDet"] == null)
                    Session["TecDet"] = new List<TrustTecDet>();

                tecs = (List<TrustTecDet>)Session["TecDet"];
                tecs.Add(det);
                Session["TecDet"] = tecs;
                return RedirectToAction("Create");
            }
            return HttpNotFound();
        }

        //public ActionResult CreateTransport(string fnew)
        //{
        //    TrustTecView transport = new TrustTecView();

        //    if (fnew == null)
        //    {
        //        if (Session["TrustTec"] != null)
        //            transport.Tec = (TrustTec)Session["TrustTec"];
        //        else
        //        {
        //            transport.Tec = new TrustTec();
        //            transport.Tec.CustId = Menu.CustId;
        //            Session["TrustTec"] = transport.Tec;
        //        }

        //        if (Session["TecDet"] != null)
        //            transport.Detail = (List<TrustTecDet>)Session["TecDet"];
        //        else
        //        {
        //            transport.Detail = new List<TrustTecDet>();
        //            Session["TecDet"] = transport.Detail;
        //        }
        //    }
        //    else
        //    {
        //        transport.Tec = new TrustTec();
        //        transport.Tec.CustId = Menu.CustId;
        //        Session["TrustTec"] = transport.Tec;
        //        transport.Detail = new List<TrustTecDet>();
        //        Session["TecDet"] = transport.Detail;
        //    }

        //    return View(transport);
        //}

        //public ActionResult CreateTransportFromSel(TrustTecDet item)
        //{
        //    if (Session["TecDet"] == null)
        //        return HttpNotFound();

        //    var detail = (List<TrustTecDet>)Session["TecDet"];
        //    detail.Add(item);
        //    Session["TecDet"] = detail;

        //    TrustTecView transport = new TrustTecView();
        //    transport.Tec = (TrustTec)Session["TrustTec"];
        //    transport.Detail = detail;

        //    return RedirectToAction("CreateTransport");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateTransport(TrustTec Tec, string tr, string st, string tecmodel, string gn, string driv)
        //{
        //    Session["TrustTec"] = Tec;

        //    if (Session["TecDet"] == null)
        //        return HttpNotFound();

        //    TrustTecView transport = new TrustTecView();
        //    transport.Tec = Tec;
        //    transport.Detail = (List<TrustTecDet>)Session["TecDet"];
        //    if (!string.IsNullOrEmpty(gn))
        //        transport.Detail.Add(new TrustTecDet() { TecModel = tecmodel, Gn = gn, Driv = driv });
        //    Session["TecDet"] = transport.Detail;

        //    if (tr != null)
        //        return RedirectToAction("GetTransportFromList", "Transports", null);

        //    if (st != null)
        //    {
        //        return View(transport);
        //    }
        //    else
        //    {
        //        repo.Save(transport);
        //        Session["TrustTec"] = null;
        //        Session["TecDet"] = null;
        //    }

        //    return RedirectToAction("Index", "Transports", null);
        //}

        public RedirectToRouteResult RemoveTec(int TecNum)
        {
            if (Session["TecDet"] == null)
                return RedirectToAction("CreateTransport");

            List<TrustTecDet> detail = (List<TrustTecDet>)Session["TecDet"];

            foreach (var item in detail)
            {
                if (item.GetHashCode() == TecNum)
                {
                    detail.Remove(item);
                    break;
                }
            }

            Session["TecDet"] = detail;

            return RedirectToAction("CreateTransport");
        }

        //public ActionResult CreateTec()
        //{
        //    TrustTecDet det;
        //    det = new TrustTecDet();
        //    return View("CreateTec", det);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateTec(TrustTecDet det)
        //{
        //    TrustTecV tecV = (TrustTecV)Session["TrustTecV"];
        //    if (tecV.Detail == null)
        //        tecV.Detail = new List<TrustTecDet>();
        //    tecV.Detail.Add(det);
        //    Session["TrustTecV"] = tecV;
        //    return RedirectToAction("Create");            
        //}

        public async Task<ActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrustTecDet tecDet= await db.TrustTecDets.FindAsync(id);
                
            if (tecDet == null)
            {
                return HttpNotFound();
            }
            return View(tecDet);
        }

        // POST: OrgTrustTecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.delete(id);
            return RedirectToAction("Index","Transports",null);
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
