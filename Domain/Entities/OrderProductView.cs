using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("bOrderProd")]
    public class OrderProductView
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int OrderProductId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? GoodId { get; set; }

        [Display(Name = "Продукция")]
        public string Good { get; set; }
        [Display(Name = "Ед. изм.")]
        public string Unit { get; set; }
        [Display(Name = "Количество")]
        public decimal? Quant { get; set; }        
    }
}
