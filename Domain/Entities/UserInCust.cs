using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("UserInCust")]
    public class UserInCust
    {
        [Key]
        [HiddenInput]
        public int UserInCustID { get; set; }
        [HiddenInput]        
        public string UserId { get; set; }
        [Display(Name = "ИНН")]
        public string Inn { get; set; }
        
        [Display(Name = "Последний вход на сайт")]
        public DateTime LastDat { get; set; }
        [Display(Name = "Код")]
        public int CustID { get; set; }

        public string Pwd { get; set; }

        public string Email { get; set; }
    }
}
