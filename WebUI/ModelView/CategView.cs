using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.ModelView
{
    public class CategView
    {
        public List<Categ> Categs { get; set; }
        public int? OrderID { get; set; }
    }
}