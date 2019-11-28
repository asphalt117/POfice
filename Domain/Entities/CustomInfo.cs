using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.ComponentModel;

namespace Domain.Entities
{
    [Table("CustomInfo")]
    public class CustomInfo
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int CustomInfoId { get; set; }
        [Display(Name = "Код 1С")]
        public string Cod1s { get; set; }
        //[Display(Name = "Наименование")]
        //public string Txt { get; set; }        
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты для получения документов")]
        public string Mail { get; set; }
        [Display(Name = "Присылать уведомление об отсутствии документов")]
        public bool isEmpty { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string txt { get; set; }
    }
}
