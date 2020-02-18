using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Repository;
using System.Linq;
using System;

namespace WebUI.Controllers
{
    public class PeopleController : BaseController
    {
        PersonRepository repo = new PersonRepository();

        public ActionResult PersonOrder(int ord)
        {
            OrderV order =  db.OrderVs.Find(ord);
            if (order.Invoice==0)
            {
                if (order.Step < 5)
                {
                    List<Person> people = db.Persons.Where(a => a.CustId == order.CustId).ToList();
                    Person person = new Person();
                    person.PersonId = 0;
                    people.Add(person);

                    ViewData["Person"] = new SelectList(people, "PersonID", "Name", 0);
                    return PartialView(order);
                }
                else
                {
                    return PartialView("Person",order);
                }
            }
            return PartialView("PersonEmpty",order);
        }
        [HttpPost]
        public async Task<ActionResult> PersonSelect(OrderV ord, int SelectedPersonId = -1)
        {
            Order order = db.Orders.Find(ord.OrderId);
            order.PersonId = SelectedPersonId;
            order.Step = 5;
            db.Orders.Add(order);
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Booking", "Ord", new { ord = order.OrderId });
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.MenuItem = "people";
            List<Person> persons =await repo.GetPerson(Cust.CustId);
            return View(persons);
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = await db.Persons.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PersonId,CustId,Name,Tel,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.CustId = Cust.CustId;
                db.Persons.Add(person);
                await db.SaveChangesAsync();

                string cord = GetCookie("Order");
                if (String.IsNullOrWhiteSpace(cord))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    int ord = int.Parse(cord);
                    return RedirectToAction("PersonOrder", ord);
                }
            }

            return View(person);
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = await db.Persons.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PersonId,CustId,Name,Tel,Email")] Person person)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(person).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(person);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = await db.Persons.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await repo.DelPerson(id);

            //Person person = await db.Persons.FindAsync(id);
            //person.IsMark = 1;
            //db.Entry(person).State = EntityState.Modified;
            //await db.SaveChangesAsync();

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
