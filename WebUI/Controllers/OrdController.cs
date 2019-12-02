using Domain.ModelView;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.Models;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using WebUI.ModelView;
using System.Linq;

namespace WebUI.Controllers
{
    public class OrdController : BaseController
    {
        private OrderRepository repo = new OrderRepository();
        private OrderView order;

        public async Task<ActionResult> Index(int id)
        {
            List<OrderV> orders = await repo.GetOrder(Cust.CustId, id);
            ViewBag.Title = (id == 0) ? "Заказы" : "Счета";
            return View(orders);
        }
        public ActionResult DateOrder(int ord)
        {
            DataView dataview = new DataView(ord);
            return PartialView(dataview);
        }
        public async Task<ActionResult> Create(string act)
        {
            //Сохранить заказ в БД со всеми известными реквизитами.
            //В ответ получить OrderView
            int ordertype = (act == "Заказы") ? 0 : 1;
            order = await repo.GetNew(abzHash, ordertype);
            //Выбор материала
            return RedirectToAction("Order", "Good", new { ord = order.OrderId });
        }

        public async Task<ActionResult> Booking(int ord)
        {
            Order order = db.Orders.Find(ord);
            ViewBag.Order = order.OrderType == 1 ? "Заказы" : "Счета";

            OrderView ordview = new OrderView();
            ordview.Products = db.OrderProductViews.Where(a => a.OrderId == ord).ToList();
            ordview.OrderId = ord;
            ordview.Dat = order.Dat;
            ordview.Note = order.note;
            ordview
            ordview
            ordview
            ordview
            ordview
            ordview
            ordview
            ordview
            ordview



            return View(ordview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Booking(OrderView ord)
        {
            //Не хватает- Сохранить:
            //Примечание
            //Оплата он-лайн
            Order order = await repo.SaveBooking(ord.OrderId,1);
            return View("Saved", order);
        }
    }
}
