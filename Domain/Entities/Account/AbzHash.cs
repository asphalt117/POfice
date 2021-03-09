using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Account
{
    [Table("AbzHash")]
    public class AbzHash
    {
        public string AbzHashID { get; set; }
        public int? CustID { get; set; }
        public int? ContractID { get; set; }
        public string Email { get; set; }
   //    public string Password { get; set; }

        public string UserId { get; set; }
        public string IP { get; set; }
        public DateTime TerminationDate { get; set; }
    }
}