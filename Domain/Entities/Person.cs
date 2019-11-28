using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace Domain.Entities
{
    [Table("Person")]
    public class Person
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int PersonId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CustId { get; set; }
        [Display(Name = "Контакт")]
        public string Name { get; set; }
        [Display(Name = "Телефон")]
        public string Tel { get; set; }        
        public string Email { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int IsMark { get; set; }
    }
}
