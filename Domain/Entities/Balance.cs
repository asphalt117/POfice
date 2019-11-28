using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Balance")]
    public class Balance
    {
        [Key]
        [Display(Name = "Дата")]
        [Required(ErrorMessage = "Дата обязательна")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dat { get; set; }

        [DisplayName("Кол-во")]
        [DisplayFormat(DataFormatString = "{0:###,#.#}")]
        public decimal kol { get; set; }

        [DisplayName("Отгрузка (Руб)")]
        [DisplayFormat(DataFormatString = "{0:###,#.##}")]
        public decimal Ssm { get; set; }

        [DisplayName("Платежи (руб)")]
        [DisplayFormat(DataFormatString = "{0:###,#.##}")]
        public decimal Psm { get; set; }

        public int CustID { get; set; }
        public int ContractID { get; set; }
    }
}