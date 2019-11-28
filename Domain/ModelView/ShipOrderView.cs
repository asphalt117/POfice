using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Domain.ModelView
{
    public class ShipOrderView
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ShipOrderId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CustId { get; set; }
        [DisplayName("Адрес")]
        public string Adres { get; set; }
        [DisplayName("Доставка")]
        public bool Centr { get; set; }
        [DisplayName("Контакт")]
        public string Person { get; set; }
        [DisplayName("Договор")]
        public string Contract { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("На дату")]
        public DateTime? Dat { get; set; }
        [DisplayName("Телефон")]
        [StringLength(20)]
        public string tel { get; set; }
        [StringLength(50)]
        public string email { get; set; }
        [DisplayName("Примечание")]
        public string note { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayName("Сумма")]
        public decimal? OrderSum { get; set; }
        [DisplayName("Создан")]
        public DateTime? insDate { get; set; }
        [DisplayName("Оплата")]
        public bool isOnlinePay { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
    }
}
