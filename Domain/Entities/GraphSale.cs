using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System;

namespace Domain.Entities
{
    [Table("bGraph")]
    public class GraphSale
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int GraphSaleId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CustId { get; set; }
        [Display(Name = "Дата")]
        [Required(ErrorMessage = "Дата обязательна")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dat { get; set; }
        public int Centr { get; set; }
        [Display(Name = "Адрес")]
        public string Adres { get; set; }
        [Display(Name = "Продукция")]
        public string Good { get; set; }
        [Display(Name = "Договор")]
        public string Num { get; set; }
        [Display(Name = "План")]
        public decimal? Plan { get; set; }
        [Display(Name = "Отпущено")]
        public decimal? Fact { get; set; }
        [Display(Name = "Загрузка")]
        public decimal? Ld { get; set; }
    }
}
