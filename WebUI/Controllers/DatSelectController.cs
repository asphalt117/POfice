using Domain.Engine;
using Domain.Entities;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.ModelView;

namespace WebUI.Controllers
{
    public class DatSelectController : BaseController
    {
        public ActionResult DateOrder(int ord)
        {
            DatView dataview = new DatView(ord);
            return PartialView(dataview);
        }

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
            order.DateExec = StringToDate.Date(ord.CDat);
            order.SmenaID = ord.SmenaID;
            order.note = ord.Note;
            order.Step = 2;
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("Booking", "Ord", new { ord = ord.OrderId });
        }

    }
}