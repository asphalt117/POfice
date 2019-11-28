namespace Domain.Entities
{
    using System;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Orders")]
    public partial class Order
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CustId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? AdresId { get; set; }

        public bool Centr { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int PersonId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? ContractId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateExec { get; set; }
        [Column(TypeName = "date")]
        public DateTime Dat { get; set; }
        //[StringLength(20)]
        //public string tel { get; set; }

        //[StringLength(50)]
        public string email { get; set; }

        public string note { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OrderSum { get; set; }

        public DateTime? insDate { get; set; }

        public bool isOnlinePay { get; set; }
        public int StatusId { get; set; }
        public int Invoice { get; set; }
        public int OrderType { get; set; }
        public int SmenaID { get; set; }
        public int? RelatedOrderId { get; set; }
    }
}
