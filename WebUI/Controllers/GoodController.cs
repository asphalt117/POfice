﻿using Domain.Entities;
using Domain.ModelView;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.ModelView;

namespace WebUI.Controllers
{
    public class GoodController : BaseController
    {
        //private GoodRepository repo = new GoodRepository();

        public ActionResult GoodOrder(int ord)
        {
            OrderProductView product = db.OrderProductViews.FirstOrDefault(a => a.OrderId == ord);
            return PartialView(product);
        }

        //Выбор продукции
        public ActionResult Categ(int ord,int categ = 2, int subcateg = 16)
        {
            List<Good> goods;
            List<Categ> subcategs;
            GoodView goodView = new GoodView();
            goodView.OrderID = ord;
            if (categ == 2)
            {
                goods = db.Goods.Where(g => g.IsFolder == subcateg).ToList();
                subcategs = db.Categs.Where(c => c.ParentCategId == categ && c.IsVisible == 0).ToList();
            }
            else
            {
                subcategs = null;
                goods = db.Goods.Where(g => g.CategId == categ).ToList();
            }
            List<Categ> categs = db.Categs.Where(c => c.ParentCategId == null && c.IsVisible == 0).ToList();
            goodView.Categs = categs;
            goodView.SubCategs = subcategs;
            goodView.Goods = goods;
            goodView.CategID = categ;
            goodView.SubCategID = subcateg;
            return View(goodView);
        }

        [HttpPost]
        public async Task<ActionResult> GoodAdd(int GoodID, int OrdID, decimal Quantity)
        {
            OrdRepository repo = new OrdRepository();
            Order order = await repo.SaveGood(GoodID, OrdID, Quantity);

            if (order.Invoice==0)
                return RedirectToAction( "Index","datSelect", new {id= OrdID });
            else
                return RedirectToAction("Booking", "Ord", new { ord = OrdID });
        }

    }
}