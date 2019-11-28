using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Domain.Entities
{
    //[Table("bTrustTec")]
    public class TrustTecV
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int TrustTecID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int TrustTecDetID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CustId { get; set; }
        [DisplayName("Модель")]
        public String TecModel { get; set; }
        [DisplayName("Гос. №")]
        public String Gn { get; set; }
        [DisplayName("Водитель")]
        public String Driv { get; set; }
        [DisplayName("Начало")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public string BeginDat { get; set; }
        [DisplayName("Окончание")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public string EndDat { get; set; }
        [DisplayName("Примечание")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
        [HiddenInput(DisplayValue = false)]
        public byte isReady { get; set; }
        public List<TrustTecDet> Detail { get; set; }
        public DateTime DatCreate { get; set; }
    }
}
