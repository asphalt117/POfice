using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Domain.Entities
{
    [Table("Smena")]
    public class Smena
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int SmenaID { get; set; }
        [Display(Name = "Смена")]
        public string Name { get; set; }

    }
}
