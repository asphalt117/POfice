using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Account
{
    [Table("svcUsers")]
    public class Usr
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        //[Display(Name = "Запомнить меня")]
        //public bool RememberMe { get; set; }
    }
}
