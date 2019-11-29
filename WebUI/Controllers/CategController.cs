using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class CategController : BaseController
    {
        private GoodRepository repo = new GoodRepository();

        public ActionResult Good(int id=16)
        {
            List<Good> goods = db.Goods.Where(g => g.IsFolder == id).ToList();
            return View(goods);
        }

        public ActionResult Index()
        {
            List<Categ> categs = db.Categs.Where(c => c.ParentCategId == null && c.IsVisible == 0).ToList();
            return View("Index1",categs);
        }

        public ActionResult Edit(int id=2)
        {
            List<Categ> categs = db.Categs.Where(c => c.ParentCategId == id && c.IsVisible==0).ToList();
            return  PartialView(categs);
        }
        //public ActionResult Good(int id = 2)
        //{
        //    List<Categ> categs = db.Categs.Where(c => c.ParentCategId == id && c.IsVisible == 0).ToList();
        //    return PartialView("Edit",categs);
        //}
    }
}