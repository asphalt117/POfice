using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System;
namespace Domain.Entities
{
    [Table("bSale")]
    public class Sale
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int SaleId { get; set; }        
        [HiddenInput(DisplayValue = false)]
        public int GraphSaleId { get; set; }
        [Display(Name = "№")]
        public string num { get; set; }
        [Display(Name = "Дата")]
        [Required(ErrorMessage = "Дата обязательна")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dat { get; set; }
        [Display(Name = "Сумма")]
        public decimal? Sm { get; set; }
        [Display(Name = "Количество")]
        public decimal? Kol { get; set; }
        [Display(Name = "Водитель")]
        public string Driv { get; set; }
        [Display(Name = "Модель")]
        public string Model { get; set; }
        //[Display(Name = "Автобаза")]
        //public string Ab { get; set; }

        [Display(Name = "Гос. №")]
        public string Gn { get; set; }
        [Display(Name = "Время")]
        public string HM { get; set; }
        [Display(Name = "Товар")]
        public string Good { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int Status { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CustId { get; set; }
    }
}
