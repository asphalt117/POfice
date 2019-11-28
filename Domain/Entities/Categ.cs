using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Domain.Entities
{
    [Table("bCateg")]
    public class Categ
    {
        [Key]     
        [HiddenInput(DisplayValue = false)]
        public int CategID { get; set; }
        [Display(Name = "Наименование категории товаров")]
        [Required(ErrorMessage = "Наименование обязательно")]
        [DataType(DataType.Text)]
        [UIHint("Text")]
        [StringLength(60, ErrorMessage = "Значение {0} должно содержать не более 60 символов.")]
        public string txt { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int id_site { get; set; }
    }
}
