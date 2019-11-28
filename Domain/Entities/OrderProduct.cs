namespace Domain.Entities
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderProduct")]
    public partial class OrderProduct
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int OrderProductId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? GoodId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Quant { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sum { get; set; }
    }
}
