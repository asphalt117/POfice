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
    public class OrdController : BaseController
    {
        private OrdRepository repo = new OrdRepository();
        //private OrderV order;

        public async Task<ActionResult> Copy(int id)
        {
            OrderV order = await repo.GetCopy(id);
            //string cord = id.ToString();
            //string act = "Копия заказа №" + cord + " Новый заказ";
            return RedirectToAction("Finish", new { ord = order.OrderId });
        }
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
            int ordertype = (act == "Заказы") ? 0 : 1;
            Order ord = await repo.GetNew(abzHash, ordertype);
            //Выбор материала
            return RedirectToAction("Categ", "Good", new { ord = ord.OrderId });
        }
        
        public async Task<ActionResult> Booking(int ord)
        {
            OrderV order = await db.OrderVs.FindAsync(ord);
            string cTip= order.Invoice == 0 ? "Заказа" : "Счета";
            ViewBag.Order = "Оформление нового " + cTip;
            if (order.Invoice == 1)
            {
                ViewBag.Order = ViewBag.Order + order.OrderId.ToString();
            }
            IEnumerable<Contract> contracts= db.Contracts.Where(a => a.CustID == order.CustId).OrderBy(a => a.Num).AsEnumerable();
            ViewData["Contract"] = new SelectList(contracts, "ContractID", "Num", order.ContractId);
            return View(order);
        }

        public async Task<ActionResult> BookingNext(int ord)
        {
            OrderV order = db.OrderVs.Find(ord);
            string cTip = order.Invoice == 0 ? "Заказа" : "Счета";
            ViewBag.Order = "Оформление нового " + cTip;
            if (order.Invoice == 0)
            {
                ViewBag.Order = ViewBag.Order + order.OrderId.ToString();
            }
            //IEnumerable<Person> persons = db.Persons.Where(a => a.CustId == order.CustId).OrderBy(a => a.Name).AsEnumerable();
            List<Person> people = db.Persons.Where(a => a.CustId == order.CustId).ToList();
            Person person = new Person();
            person.PersonId = 0;
            people.Add(person);
            //ViewData["Person"] = new SelectList(people, "PersonID", "Name", 0);
            ViewData["Person"] = new SelectList(people, "PersonID", "Name", 0);
            return View(order);
        }

        public async Task<ActionResult> Finish(int ord)
        {
            OrderV order = db.OrderVs.Find(ord);
            string cTip = order.Invoice == 1 ? "Заказа" : "Счета";
            ViewBag.Order = "Оформление нового " + cTip;
            if (order.Invoice == 0)
            {
                ViewBag.Order = ViewBag.Order + order.OrderId.ToString();
            }
            IEnumerable<Person> persons = db.Persons.Where(a => a.CustId == order.CustId).OrderBy(a => a.Name).AsEnumerable();
            ViewData["Person"] = new SelectList(persons, "PersonID", "Name", 0);
            return View(order);
        }
        [HttpPost]
        public async Task<ActionResult> Contract(OrderV ord, int SelectedContractId = -1)
        {
            Order order = db.Orders.Find(ord.OrderId);
            order.ContractId = SelectedContractId;
            order.Step = 3;
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
            order.note = ord.Note;
            order.StatusId = 1;
            order.Step = 6;
            db.Orders.Add(order);
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();

            ViewBag.Order = "Заказ №  " + order.OrderId.ToString() + " отправлен";
            OrderV orderV= db.OrderVs.Find(ord.OrderId);
            return View("Saved", orderV);
        }
    }
}
