using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class RegisterAdmin
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "ID")]        
        public int CustId { get; set; }        
    }

}