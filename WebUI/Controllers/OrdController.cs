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
        private OrdRepository repo = new OrdRepository();
        //private OrderView order;
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
            //В ответ получить Order
            int ordertype = (act == "Заказы") ? 0 : 1;
            Order order = await repo.GetNew(abzHash, ordertype);
            //Выбор материала
            return RedirectToAction("Categ", "Good", new { ord = order.OrderId });
        }
        
        public async Task<ActionResult> Booking(int ord)
        {
            //Order order = db.Orders.Find(ord);
            OrderView ordview = new OrderView(ord);
            ViewBag.Order = ordview.OrderV.Invoice == 1 ? "Заказы" : "Счета";
            //ViewData["Contract"] = new SelectList(contracts, "ContractID", "Num", ContractID); ;

            return View(ordview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Booking(OrderV ord)
        {
            //Не хватает- Сохранить:
            //Примечание
            //Оплата он-лайн
            Order order = await repo.SaveBooking(ord.OrderId,1);
            return View("Saved", order);
        }
    }
}
