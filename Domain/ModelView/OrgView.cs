using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Domain.ModelView
{
    public class OrgView
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Полное наименование")]
        public string txt { get; set; }
    }
}
