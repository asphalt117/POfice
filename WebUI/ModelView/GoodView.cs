using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.ModelView
{
    public class GoodView
    {
        public List<Categ> Categs { get; set; }
        public List<Categ> SubCategs { get; set; }
        public List<Good> Goods { get; set; }
        public int CategID { get; set; }
        public int SubCategID { get; set; }
        public decimal Quantity { get; set; }
        public int OrderID { get; set; }
    }
}