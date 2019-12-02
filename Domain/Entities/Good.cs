using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Domain.Entities
{
    [Table("bGood")]
    public class Good
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int GoodID { get; set; }
        [Display(Name = "Продукция")]
        public string txt { get; set; }
        [Display(Name = "Ед. изм.")]
        public string Unit { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Группа товаров")]
        [Required(ErrorMessage = "Группа обязательна")]
        public int CategId { get; set; }
        public int IsFolder { get; set; }
    }
}
