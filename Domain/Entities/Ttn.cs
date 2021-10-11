using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("bTtn")]
    public class Ttn
    {
        [Key]
        public int Id { get; set; }
        public int CustID { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DisplayName("Дата")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]

        public DateTime Dat { get; set; }
        [DisplayName("Модель")]
        public string Amodel { get; set; }
        [DisplayName("Автобаза")]
        public string Ab { get; set; }
        [DisplayName("Гос. №")]
        public string Gn { get; set; }
        [DisplayName("ТТН №")]
        public string Num { get; set; }
        [DisplayName("Водитель")]
        public string Driv { get; set; }
        [DisplayName("Время")]
        public string Tm { get; set; }
        [DisplayName("Кол")]
        public decimal Kol { get; set; }
        //[DisplayName("Цена")]
        //public decimal Price { get; set; }
        //[DisplayName("Сумма")]
        //public decimal Sm { get; set; }
        [DisplayName("Продукция")]
        public string Good { get; set; }
        [DisplayName("Адрес")]
        public string Adres { get; set; }
        [DisplayName("Договор")]
        public string Dog { get; set; }
        public int  ContractID { get; set; }
        public int ismark { get; set; }
    }
}