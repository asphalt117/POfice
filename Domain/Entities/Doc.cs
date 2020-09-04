using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    [Table("bDocs")]
    public class Doc
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int DocsId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CustID { get; set; }

        [Display(Name = "Документ")]
        public string DocName { get; set; }

        [Display(Name = "Файл")]
        public string FileName { get; set; }

        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Ddat { get; set; }

        [Display(Name = "файл Бин")]
        public byte[] DocBin { get; set; }

    }
}
