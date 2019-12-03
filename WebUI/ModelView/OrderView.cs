using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.ModelView
{
    public class OrderView
    {
        public  OrderV OrderV { get; set; }
        public SelectList SelectContract { get; set; }
        public SelectList SelectPerson { get; set; }
        public List<OrderDriv> OrderDrivs { get; set; }
    }
}