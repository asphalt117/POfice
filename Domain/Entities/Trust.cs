using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("bTrust")]
    public class Trust
    {
        public int TrustId { get; set; }
        public int CustId { get; set; }

        [DisplayName("Продукция")]
        public string Good { get; set; }
        [DisplayName("Тип продукции")]
        public string Categ { get; set; }
        [DisplayName("Получатель")]
        public string Txt { get; set; }

        [DisplayName("Окончание")]
        public string Dend { get; set; }

    }
}