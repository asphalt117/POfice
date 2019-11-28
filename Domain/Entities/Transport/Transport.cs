using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Domain.Entities
{
    ///
    ///Показывает транспорт используемый заказчиком.
    ///Из ТТН и доверенного
    ///

    [Table("bTrans")]
    public class Transport
    {        
        [Key]
        [DisplayName("Гос. №")]
        public string Gn { get; set; }
        [DisplayName("Доверенный транспорт")]
        public bool istrust { get; set; }
        [DisplayName("Модель")]
        public string TecModel { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? CustID { get; set; }
        //[HiddenInput(DisplayValue = false)]
        //public int? TrustTecID { get; set; }
        //[HiddenInput(DisplayValue = false)]
        //public int? TrustTecDetID { get; set; }
        public DateTime Dat { get; set; }
        [DisplayName("Начало")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime? BeginDat { get; set; }
        public string BeginDat { get; set; }
        [DisplayName("Окончание")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime? EndDat { get; set; }
        public string EndDat { get; set; }
        [DisplayName("Примечание")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
        [DisplayName("Водитель")]
        public String Driv { get; set; }
    }
}
