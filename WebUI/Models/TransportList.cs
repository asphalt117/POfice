using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class TransportList
    {
        public IEnumerable<Transport> Trans { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}