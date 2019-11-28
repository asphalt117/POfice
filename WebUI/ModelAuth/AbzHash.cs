using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.ModelAuth
{
    public class AbzHash
    {
        public int AbzHashID { get; set; }
        public string Hash { get; set; }
        public string IP { get; set; }
        public DateTime TerminationDate { get; set; }
    }
}