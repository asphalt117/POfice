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
        public OrderView(int id)
        {
            AbzContext db = new AbzContext();
            OrderV order = db.OrderVs.Find(id);
            OrderV = order;
            IEnumerable<Contract> contracts;
            contracts= db.Contracts.Where(a => a.CustID == order.CustId).OrderBy(a => a.Num).AsEnumerable();
            SelectContract = new SelectList(contracts, "ContractID", "Num", order.ContractId);

        }
        public  OrderV OrderV { get; set; }
        public SelectList SelectContract { get; set; }
        //public SelectList SelectPerson { get; set; }
        //public List<OrderDriv> OrderDrivs { get; set; }
    }
}