using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
     [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        public int CustId { get; set; }
        public int GoodId { get; set; }

        [DisplayName("Количество")]
        public decimal Quant { get; set; }
        [DisplayName("Сумма")]
        public decimal Sm { get; set; }
        //[DisplayName("№")]
        //public int Num { get; set; }
        public int? ContractID { get; set; }
    }
}