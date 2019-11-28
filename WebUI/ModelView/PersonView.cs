using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.ModelView
{    
    public class PersonView
    {
        public PersonView(int id)
        {
            AbzContext db = new AbzContext();
            OrderV order = db.OrderVs.Find(id);
            OrderId = id;
            Person = order.Person;
            PersonId = order.PersonId;
            List<Person> persons = db.Persons.Where(p => p.CustId == order.CustId | p.PersonId == 0).ToList();
            SelectPerson = new SelectList(persons, "PersonId", "Name", PersonId);
        }
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }
        [Display(Name = "Контакт")]
        public string Person { get; set; }
        public int PersonId { get; set; }
        public SelectList SelectPerson { get; set; }
    }
}