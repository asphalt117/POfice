using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace Domain.Entities
{
    [Table("bContract")]
    public class Contract
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ContractID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CustID { get; set; }
        [Display(Name = "Договор")]
        public string Num { get; set; }
    }
}
