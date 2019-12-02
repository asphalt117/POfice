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
            return RedirectToAction("Categ", "Good", new { ord = order.OrderId });
        }

        public async Task<ActionResult> Booking(int ord)
        {
            Order order = db.Orders.Find(ord);
            ViewBag.Order = order.OrderType == 1 ? "Заказы" : "Счета";
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Booking(Order ord)
        {
            //Не хватает- Сохранить:
            //Примечание
            //Оплата он-лайн
            Order order = await repo.SaveBooking(ord.OrderId,1);
            return View("Saved", order);
        }
    }
}
