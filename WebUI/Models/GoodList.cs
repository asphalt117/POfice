using System;
using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class GoodList
    {
        public int CategId { get; set; }
        public string CategName { get; set; }
        public IEnumerable<Good> Products { get; set; }
        public PageInfo PageInfo { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public int OrderID { get; set; }
        public decimal Quantity { get; set; }
}
}