using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class TtnList
    {
        public IEnumerable<Ttn> Ttns { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}