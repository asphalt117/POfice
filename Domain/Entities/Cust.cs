using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
     [Table("bCust")]
    public class Cust
    {
        [Key]
        public int CustId { get; set; }
        [Required]
        [DisplayName("Полное наименование")]
        public string FullName { get; set; }
        [DisplayName("Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string Tel { get; set; }
        [DisplayName("ИНН")]
        public string Inn { get; set; }
        [DisplayName("КПП")]
        public string Kpp { get; set; }
        [DisplayName("Адрес")]
        public string Adres { get; set; }
        [DisplayName("Расчетный счет")]
        public string Rasch { get; set; }
        [DisplayName("Банк")]
        public string Bank { get; set; }
        [DisplayName("Кор. счет")]
        public string Korsch { get; set; }
        [DisplayName("БИК")]
        public string Bik { get; set; }
        [DisplayName("ОКПО")]
        public string Okpo { get; set; }
        [DisplayName("Факс")]
        public string Fax { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Краткое наименование")]
        public string SmalName { get; set; }       
      
         [DisplayName("Код 1С")]
        public string Cod1s { get; set; }
        //public int ContractId { get; set; }
        //public string Contract { get; set; }
    }
}
