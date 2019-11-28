using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    ///
    ///Показывает транспорт используемый заказчиком.
    ///Из ТТН и доверенного
    ///

    [Table("bDrivCust")]
    public class DrivCust
    {
        [Key]
        public int DrivCustId { get; set; }
        public int CustId { get; set; }

        //[DisplayName("Водитель")]
        //public string Driv { get; set; }
        //[DisplayName("Автобаза")]
        //public string Ab { get; set; }
        [DisplayName("Гос №")]
        public string Gn { get; set; }
        [DisplayName("Модель")]
        public string TecModel { get; set; }
    }
}
