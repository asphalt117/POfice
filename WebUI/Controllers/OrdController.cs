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
using System.Data.Entity;

namespace WebUI.Controllers
{
    //Step
    //1. После выбора материала
    //2. После выбора даты
    //3. После выбора контракта
    //4. После выбора доставки
    //5. После выбора контакта
    //6. После сохранения

    // При заказе
    // 1. Материал
    // 2. Дата
    // 3. Контракт
    // 4. Доставка
    // 5. Контакт

    // Счет
    // 1. Материал
    // 3. Контракт
    // 4. Доставка

    public class OrdController : BaseController
    {
        private OrdRepository repo = new OrdRepository();
 
        public async Task<ActionResult> Copy(int id)
        {
            OrderV order = await repo.GetCopy(id);
            return RedirectToAction("Booking", new { ord = order.OrderId });
        }
        public async Task<ActionResult> Index(int id)
        {
            List<OrderV> orders = await repo.GetOrder(Cust.CustId, id);
            ViewBag.typeord = id;
            ViewBag.MenuItem = (id == 0) ? "ord" : "ordch";
            return View(orders);
        }

        public async Task<ActionResult> Create(string act)
        {
            //Сохранить заказ в БД со всеми известными реквизитами.
            int ordertype = (act == "Заказы") ? 0 : 1;
            Order ord = await repo.GetNew(abzHash, ordertype);
            Response.Cookies["Order"].Value = ord.OrderId.ToString();
            Response.Cookies["Order"].Expires = DateTime.Now.AddHours(2);
                
            //SetCookie("Order", ord.OrderId.ToString());
            //Выбор материала
            return RedirectToAction("Categ", "Good", new { ord = ord.OrderId });
        }
        
        public async Task<ActionResult> Booking(int ord)
        {
            OrderV order = await db.OrderVs.FindAsync(ord);
            string cTip= order.Invoice == 0 ? "Заказа " : "Счета ";
            ViewBag.Order = "Оформление нового " + cTip;
            ViewBag.Debug = order.Step.ToString();
            if (order.Invoice == 0)
            {
                ViewBag.Order = ViewBag.Order + order.OrderId.ToString();
            }
            return View(order);
        }
        
        public  ActionResult ContractOrder(int ord)
        {
            OrderV order = db.OrderVs.Find(ord);
            IEnumerable<Contract> contracts = db.Contracts.Where(a => a.CustID == abzHash.CustID).OrderBy(a => a.Num).AsEnumerable();
            ViewData["Contract"] = new SelectList(contracts, "ContractID", "Num", order.ContractId);
            return PartialView(order);
        }

        [HttpPost]
        public async Task<ActionResult> Contract(OrderV ord, int SelectedContractId = -1)
        {
            Order order = db.Orders.Find(ord.OrderId);
            order.ContractId = SelectedContractId;
            if (order.Step < 3) order.Step = 3;
            db.Orders.Add(order);
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Booking", "Ord", new { ord = order.OrderId });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Booking(OrderV ord)
        {
            //Не хватает- Сохранить:
            //Примечание
            //Оплата он-лайн
            Order order = db.Orders.Find(ord.OrderId);
            //Проверить дату, и возможно другие поля.

            order.note = ord.Note;
            order.StatusId = 1;
            order.Step = 6;
            order.isOnlinePay = ord.isOnlinePay;
            db.Orders.Add(order);
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();

            //ViewBag.Order = "Заказ №  " + order.OrderId.ToString() + " отправлен";
            //OrderV orderV= db.OrderVs.Find(ord.OrderId);
            return RedirectToAction("Index", "Ord", new { id = order.Invoice });
            //return View("Saved", orderV);
        }

        public async Task<ActionResult> Delete(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            int invoice = order.Invoice;
            order.Ismark = 1;
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Ord", new { id = order.Invoice });
            ViewBag.Order = "Удаление заказа";
            return View(order);
        }
     }
}
