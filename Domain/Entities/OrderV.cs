﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Domain.Entities
{
    [Table("bOrderv")]
    public class OrderV
    {
        [Key]
        //[HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CustId { get; set; }
        [Display(Name = "Продукция")]
        public string Good { get; set; }
        [Display(Name = "Ед. изм.")]
        public string Unit { get; set; }
        public int? AdresId { get; set; }
        [Display(Name = "Адрес")]
        public string Adres { get; set; }
        [Display(Name = "Договор")]
        public string Contract { get; set; }
        public int ContractId { get; set; }
        [Display(Name = "Контакт")]
        public string Person { get; set; }
        public int PersonId { get; set; }
        [Display(Name = "Доставка нашим транспортом")]
        public bool Centr { get; set; }

        [Display(Name = "Доставка")]
        public string CentrName { get; set; }
        
        [Display(Name = "Дата выпонения")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateExec { get; set; }

        //[Display(Name = "Дата")]
        ////[DataType(DataType.Date)]
        ////[UIHint("Date")]
        ////[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime Dat { get; set; }
        //[Display(Name = "Количество")]
        //public decimal Quant { get; set; }


        [Display(Name = "Дата выполнения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public string CDat { get; set; }

        [Display(Name = "Примечание")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
        [Display(Name = "Состояние заказа")]
        public string Status { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int StatusId { get; set; }
        [Display(Name = "Оплата OnLine")]
        public bool isOnlinePay { get; set; }
        public int Invoice { get; set; }
        public string email { get; set; }
        public int? RelatedOrderId { get; set; }
        public string Smena { get; set; }
        public int SmenaID { get; set; }
        public int Step { get; set; }
    }
}
