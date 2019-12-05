using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Collections.Generic;
using Domain.Entities;
using System.Linq;

namespace Domain.ModelView
{
    public class OrderViewOld
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CustId { get; set; }
        [Display(Name = "Продукция")]
        public string Good { get; set; }
        [Display(Name = "Ед. изм.")]
        public string Unit { get; set; }

        public List<OrderProductView> Products { get; set; }

        public int? AdresId { get; set; }
        [Display(Name = "Адрес")]

        public string Adres { get; set; }

        [Display(Name = "Договор")]
        //public IEnumerable<Contract> Contract { get; set; }
        public string Contract { get; set; }
        public int ContractId { get; set; }
        [Display(Name = "Контакт")]
        public string Person { get; set; }
        public int PersonId { get; set; }
        [Display(Name = "Доставка транспортом АБЗ-4")]
        public bool Centr { get; set; }

        [Display(Name = "Дата выполнения")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dat { get; set; }

        [Display(Name = "Дата выполнения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public string CDat { get; set; }



        [Display(Name = "Время заказа")]
        [DataType(DataType.Time)]
        [UIHint("Time")]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public string CTime { get; set; }

        [Display(Name = "Время суток заказа")]
        public int DayNight { get; set; }

        [Display(Name = "Примечание")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
        [Display(Name = "Состояние заказа")]
        public string Status { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int StatusId { get; set; }

        public SelectList SelectAdres { get; set; }
        public SelectList SelectContract { get; set; }
        public SelectList SelectPerson { get; set; }
        public SelectList SelectDriv { get; set; }
        public SelectList SelectSmena { get; set; }

        [Display(Name = "Транспорт")]
        public string Gn { get; set; }
        public string Tmp { get; set; }
        [Display(Name = "Оплата OnLine")]
        public bool isOnlinePay { get; set; }
        public int Invoice { get; set; }
        public string email { get; set; }

        public int? RelatedOrderId { get; set; }
        public int SmenaID { get; set; }
        [Display(Name = "Смена")]
        public string Smena { get; set; }
        public List<OrderDriv> OrderDrivs { get; set; }
        public decimal Quantity { get; set; }
    }
}
