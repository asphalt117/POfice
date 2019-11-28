using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using System;

namespace Domain.ModelView
{
    public class TrustTecView
    {
        //[DisplayName("Доверенный транспорт")]
        //public bool IsTrust { get; set; }
        public TrustTec Tec { get; set; }
        public List<TrustTecDet> Detail { get; set; }
        //[DisplayName("Начало")]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        //public string BeginDat { get; set; }
        //[DisplayName("Окончание")]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        //public string EndDat { get; set; }

    }
}