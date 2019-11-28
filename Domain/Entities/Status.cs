using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    public class Status
    {
        public int StatusId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Статус")]
        public string Txt { get; set; }
    }
}
