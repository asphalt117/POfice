using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    //Действующие доверенности на транспорт в БД org
    [Table("OrgTrustTec")]
    public class OrgTrustTec
    {
        [Key]
        public int OrgTrustTecID { get; set; }
        public int CustId { get; set; }
        [DisplayName("Модель")]
        public String TecModel { get; set; }
        [DisplayName("Гос. №")]
        public String Gn { get; set; }
        [DisplayName("Водитель")]
        public String Driv { get; set; }

        [DisplayName("Продукция")]
        public String Good  { get; set; }

        [DisplayName("Начало")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BeginDat { get; set; }
        [DisplayName("Окончание")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDat { get; set; }
    }
}