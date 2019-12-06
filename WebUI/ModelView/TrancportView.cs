using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.ModelView
{
    public class TrancportView
    {
        public TrancportView(int ord, int custid)
        {
            AbzContext db = new AbzContext();
            this.Transports = db.Transports.Where(p => p.CustID == custid).OrderBy(x => x.Dat).ToList();
            OrderId = ord;
            Centr = true;
        }

        public List<Transport> Transports { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }
        public bool Centr { get; set; }

    }
}