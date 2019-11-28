using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TecGn
    {
        [DisplayName("Гос №")]
        public string Gn { get; set; }
        [DisplayName("Модель")]
        public string TecModel { get; set; }
    }
}
