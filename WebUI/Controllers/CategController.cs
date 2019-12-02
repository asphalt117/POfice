using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.ModelView;

namespace WebUI.Controllers
{
    public class CategController : BaseController
    {
        private GoodRepository repo = new GoodRepository();

        public ActionResult Good(int id=16)
        {
            List<Good> goods = db.Goods.Where(g => g.IsFolder == id).ToList();
            return PartialView(goods);
        }

        public ActionResult Index(int categ=2,int subcateg=16)
        {
            List<Categ> categs = db.Categs.Where(c => c.ParentCategId == null && c.IsVisible == 0).ToList();
            return View("Index1",categs);
        }

        public ActionResult Edit(int id=2)
        {
            List<Categ> categs = db.Categs.Where(c => c.ParentCategId == id && c.IsVisible==0).ToList();
            return  PartialView(categs);
        }
        public ActionResult Categ(int categ = 2, int subcateg=16)
        {
            List<Good> goods;
            List<Categ> subcategs;
            GoodView goodView = new GoodView();
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


    }
}