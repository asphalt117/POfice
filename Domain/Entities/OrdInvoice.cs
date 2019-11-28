using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("OrdInvoice")]
    public class OrdInvoice
    {
        [Key]
        public int OrdInvoiceID { get; set; }
        public int CustId { get; set; }
        public string Note { get; set; }
    }
}