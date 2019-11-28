using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    [Table("bFinBal")]
    public class FinBal
    {
        [Key]
        //public int FinBalId { get; set; }
        public int CustID { get; set; }
        [Display(Name = "Дата")]
        [Required(ErrorMessage = "Дата обязательна")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dat { get; set; }
        [DisplayName("Сумма")]
        public decimal Sm { get; set; }
        public int ContractID { get; set; }

    }
}
