using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TrustTec")]
    public class TrustTec
    {
        [Key]
        public int TrustTecId { get; set; }
        public int CustId { get; set; }
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
        public DateTime DatCreate { get; set; }
    }
}