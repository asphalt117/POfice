using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.ModelView
{
    public class AdresView
    {
        public AdresView(int ord,int custid)
        {
            AbzContext db = new AbzContext();
            this.Adres = db.Adreses.Where(p => p.CustId == custid).OrderBy(x => x.txt).ToList();
            OrderId = ord;
            Centr = true;
        }
        //[HiddenInput(DisplayValue = false)]
        //public int AdresID { get; set; }
        public List<Adres> Adres { get; set; }
        //public SelectList SelectAdres { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }
        public bool Centr { get; set; }
    }
}