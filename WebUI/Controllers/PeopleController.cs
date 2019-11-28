using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Repository;

namespace WebUI.Controllers
{
    public class PeopleController : BaseController
    {
        PersonRepository repo = new PersonRepository();
        public async Task<ActionResult> Index()
        {

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
                return RedirectToAction("Index");
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
