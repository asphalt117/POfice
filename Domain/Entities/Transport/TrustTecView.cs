using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using System;

namespace Domain.ModelView
{
    public class TrustTecView
    {
        public TrustTec Tec { get; set; }
        public List<TrustTecDet> TrustTecDets { get; set; }
    }
}