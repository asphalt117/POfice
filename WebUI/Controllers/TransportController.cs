using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebUI.ModelView;

namespace WebUI.Controllers
{
    public class TransportController : BaseController
    {
        // GET: Transport
        public ActionResult Index(int ord)
        {
            Order order = db.Orders.Find(ord);
            if (order.Invoice == 0)
            {
                TrancportView trans = new TrancportView(ord, (int)abzHash.CustID);
                return View(trans);
            }
            else
            {
                return RedirectToAction("Booking", "Ord", new { ord = order.OrderId });
            }
        }
        public ActionResult Trans(string id, int ord)
        {
            Transport transport = db.Transports.FirstOrDefault(p => p.CustID == (int)abzHash.CustID && p.Gn == id);
            OrderDriv orderDriv = new OrderDriv();
            orderDriv.OrderId = ord;
            orderDriv.Gn = id;
            orderDriv.TecModel = transport.TecModel;
            return View(orderDriv);
        }
        [HttpPost]
        public ActionResult Trans(OrderDriv orderDriv)
        {
            db.OrderDrivs.Add(orderDriv);
            db.SaveChanges();
            Order order = db.Orders.Find(orderDriv.OrderId);
            order.Centr = false;
            if (order.Step < 4)
                order.Step = 4;
            db.Orders.Add(order);
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Booking", "Ord", new { ord = orderDriv.OrderId });
        }

        public ActionResult TransOrder(int ord)
        {
            OrderV order = db.OrderVs.Find(ord);
            if (order.Step <4)
                return PartialView("Seltr", order);
            else
            {
                if (order.Centr)
                    return PartialView("TransCentr", order);

                List<OrderDriv> orderDrivs = db.OrderDrivs.Where(a => a.OrderId == ord).ToList();
                ViewBag.OrderId = ord;
                //TrancportView trancportView = new TrancportView(ord, abzHash.CustID);
                if (orderDrivs.Count > 0)
                    return PartialView(orderDrivs);
                else
                {
                    ViewBag.tr = "Самовывоз";
                    return PartialView();
                }
            }
        }
    }
}