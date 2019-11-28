using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TrustTecDet")]
    public class TrustTecDet
    {
        [Key]
        public int TrustTecDetId { get; set; }

        public int TrustTecId { get; set; }

        [DisplayName("Модель")]
        public String TecModel { get; set; }
        [DisplayName("Гос. №")]
        public String Gn { get; set; }
        
        [DisplayName("Водитель")]
        public String Driv { get; set; }
    }
}