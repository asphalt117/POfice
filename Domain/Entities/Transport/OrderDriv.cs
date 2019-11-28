using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    ///
    /// Транспорт по заказазу
    /// 
    [Table("OrderDriv")]
    public class OrderDriv
    {
        [Key]
        public int OrderDrivId { get; set; }
        public int OrderId { get; set; }
        //public int DrivTecId { get; set; }
        [DisplayName("Модель")]
        public string TecModel { get; set; }
        [DisplayName("Гос №")]
        public string Gn { get; set; }                
    }
}
