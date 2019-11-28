using System;
using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class AssortmentMenu
    {
        public IEnumerable<Categ> Categs { get; set; }
        public int OrderID { get; set; }
    }
}