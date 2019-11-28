using Domain.Entities;
using Domain.ModelView;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class GoodController : BaseController
    {
        private GoodRepository repo = new GoodRepository();
        private static int pagesize = 20;          // Items count on one page
        private static int vsblpagescount = 10;    // Visible page numbers in page navigator central list 
        //private AbzContext db = new AbzContext();

        public ActionResult GoodOrder(int ord)
        {
            List<OrderProductView> products = db.OrderProductViews.Where(a=>a.OrderId==ord).ToList();
            return PartialView(products);
        }
        public ActionResult CategoryMenu(int ord)
        {
            var assortmentMenu = new AssortmentMenu();
            assortmentMenu.Categs = db.Categs;
            assortmentMenu.OrderID = ord;
            return PartialView(assortmentMenu);
        }


        public ActionResult Order(int? ord, int categid = 2, int pageNum = 0)
        {
            int countitems = repo.GetCountItems(categid);
            GoodList goodlist = new GoodList();
            goodlist.CategId = categid;
            goodlist.CategName = db.Categs.Find(categid).txt;
            goodlist.PageInfo = new PageInfo { pageNum = pageNum, itemsCount = countitems, pageSize = pagesize, vsblPagesCount = vsblpagescount };
            goodlist.Products = repo.GetSkipTake(pageNum * pagesize, pagesize, categid).ToList();
            goodlist.OrderID = (int)ord;
            return View(goodlist);
        }

        [HttpPost]
        public async Task<ActionResult> GoodAdd(int GoodID, int OrdID, decimal Quantity)
        {
            Good good = await db.Goods.FindAsync(GoodID);
            OrderProductView svd = new OrderProductView();
            svd.GoodId = good.GoodID;
            svd.Good = good.txt;
            svd.Unit = good.Unit;
            svd.OrderId = OrdID;
            svd.Quant = Quantity;

            OrderRepository repo = new OrderRepository();
            await repo.SaveDetail(svd.OrderId, svd);
            OrderView ord = await repo.GetChange(svd.OrderId);
            if (ord.Invoice == 0)
            {
                //заказ. Запрос времени 
                return View("DatAdd", ord);
            }
            else
                return RedirectToAction("Booking", "Ord", new { ord = OrdID });

        }

        [HttpPost]
        public async Task<ActionResult> DatAdd(Order ord)
        {
            //Закомментировано пока нет получения Даты, смены
            //Order order= await db.Orders.FindAsync(ord.OrderId);
            //order.DateExec = ord.DateExec;
            //order.SmenaID = ord.SmenaID;
            //db.Entry(order).State = EntityState.Modified;
            //await db.SaveChangesAsync();
            return RedirectToAction("Booking", "Ord", new { ord = ord.OrderId });
        }
        public ActionResult DelVisible(int id)
        {
            Good good = db.Goods.Find(id);
            good.to_site = 1;
            db.SaveChanges();
            return RedirectToAction("Index", "Good");
        }


    }
}