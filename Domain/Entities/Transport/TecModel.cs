using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TecModel")]
    public class TecModel
    {
        [Key]
        public int TecModelId { get; set; }

        [DisplayName("Модель")]
        public string Name { get; set; }
    }
}