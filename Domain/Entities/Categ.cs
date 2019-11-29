using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Domain.Entities
{
    [Table("Categ")]
    public class Categ
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int CategID { get; set; }
        [Display(Name = "Категория товаров")]
        [DataType(DataType.Text)]
        [UIHint("Text")]
        public string CategName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ParentCategId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int IsVisible { get; set; }

    }
}
