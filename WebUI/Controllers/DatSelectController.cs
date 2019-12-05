using Domain.Entities;
using Domain.ModelView;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class DatSelectController : BaseController
    {
        public async Task<ActionResult> Index(int id)
        {
            OrderV ord = await db.OrderVs.FindAsync(id);
            ord.SmenaID = 0;
            return View(ord);
        }

        [HttpPost]
        public async Task<ActionResult> DatAdd(OrderV ord)
        {
            //Закомментировано пока нет получения Даты, смены
            Order order= await db.Orders.FindAsync(ord.OrderId);
            order.DateExec = ord.DateExec;
            order.SmenaID = ord.SmenaID;
            order.note = ord.Note;
            order.Step = 2;
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("Booking", "Ord", new { ord = ord.OrderId });
        }

    }
}