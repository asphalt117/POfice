using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;

namespace WebUI.Models
{
    public class OrderView
    {
        [Key]
        public int OrderViewId { get; set; }
        [Display(Name = "Продукция")]
        public string Good { get; set; }
        [Display(Name = "Адрес")]
        public string Adres { get; set; }
        [Display(Name = "Договор")]
        public string Contract { get; set; }
        [Display(Name = "Контакт")]
        public string Person { get; set; }
        [Display(Name = "Доставка нашим транспортом")]
        public bool Centr { get; set; }
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dat { get; set; }
        [Display(Name = "Количество")]
        public decimal Quant { get; set; }
        [Display(Name = "Примечание")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }


        public OrderView(Order order)
        {
            AbzContext db = new AbzContext();
            Good good = db.Goods.Find(order.GoodId);
            Good = good.txt;
            Adres adres = db.Adreses.Find(order.AdresId);
            Adres = adres.txt;
            Contract contr = db.Contracts.Find(order.ContractID);
            Contract = contr.Num;
            Person cnt = db.Persons.Find(order.PersonID);
            if (cnt!=null)
                Person = cnt.Name;
            Centr = order.Centr;
            Dat = order.Dat;
            Quant = order.Quant;
            Note = order.Note;
        }

    }
}