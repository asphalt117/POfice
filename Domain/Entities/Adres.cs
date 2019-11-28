using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Domain.Entities
{
    [Table("bAdres")]
    public class Adres
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int AdresID { get; set; }
        [Display(Name = "Адрес")]
        public string txt { get; set; }
        [Display(Name = "Расстояние")]
        public int Rast { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? CustId { get; set; }

    }
}