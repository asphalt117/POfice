using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Domain.Entities
{
    [Table("VUserInCust")]
    public class UserAdmin
    {
        [Key]
        public int VUserInCustId { get; set; }
        [Required]
        [DisplayName("Полное наименование")]
        public string FullName { get; set; }
        [DisplayName("ИНН")]
        public string Inn { get; set; }
        [DisplayName("Краткое наименование")]
        public string SmalName { get; set; }
        [Display(Name = "Последний вход на сайт")]
        public DateTime LastDat { get; set; }
        [Display(Name = "ID")]
        public int CustID { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
    }

}
