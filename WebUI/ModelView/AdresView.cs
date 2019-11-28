using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.ModelView
{
    public class AdresView
    {
        public int AdresID { get; set; }
        public SelectList SelectAdres { get; set; }
        public bool Centr { get; set; }
    }
}