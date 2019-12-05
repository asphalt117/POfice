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
        private OrderV order;
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
            Order ord = await repo.GetNew(abzHash, ordertype);
            //Выбор материала
            return RedirectToAction("Categ", "Good", new { ord = ord.OrderId });
        }
        
        public async Task<ActionResult> Booking(int ord)
        {
            OrderV order = await db.OrderVs.FindAsync(ord);
            string cTip= order.Invoice == 1 ? "Заказа" : "Счета";
            ViewBag.Order = "Оформление нового " + cTip;
            if (order.Invoice == 1)
            {
                ViewBag.Order = ViewBag.Order + order.OrderId.ToString();
            }

            //OrderView ordview = await repo.GetChange(ord);
                IEnumerable<Contract> contracts= db.Contracts.Where(a => a.CustID == order.CustId).OrderBy(a => a.Num).AsEnumerable();
            ViewData["Contract"] = new SelectList(contracts, "ContractID", "Num", order.ContractId);
            return View(order);
        }

        public async Task<ActionResult> BookingNext(int ord)
        {
            OrderV order = db.OrderVs.Find(ord);
            string cTip = order.Invoice == 1 ? "Заказа" : "Счета";
            ViewBag.Order = "Оформление нового " + cTip;
            if (order.Invoice == 1)
            {
                ViewBag.Order = ViewBag.Order + order.OrderId.ToString();
            }
            //IEnumerable<Contract> contracts = db.Contracts.Where(a => a.CustID == ordview.CustId).OrderBy(a => a.Num).AsEnumerable();
            //ViewData["Contract"] = new SelectList(contracts, "ContractID", "Num", ordview.ContractId);
            return View(order);
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
