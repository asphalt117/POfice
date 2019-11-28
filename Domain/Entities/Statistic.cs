using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Domain.Entities
{
    [Table("Statistic")]
    public class Statistic
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int StatisticID { get; set; }
        public string UserId { get; set; }
        public int CustID { get; set; }
        public string UserAdres { get; set; }
        public string UserHost { get; set; }
    }
}
