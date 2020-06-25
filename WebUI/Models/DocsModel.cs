using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class DocsModel
    {
        [Required]
        public bool IsCustom { get; set; }
        
        [Required]
        public Domain.Entities.CustomInfo CustomInfo { get; set; }

        [Required]
        public IEnumerable<Domain.Entities.Doc> Docs { get; set; }
    }
}