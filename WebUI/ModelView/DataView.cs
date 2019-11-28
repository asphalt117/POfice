using Domain.Engine;
using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;

namespace WebUI.ModelView
{
    public class DataView
    {
        public DataView(int id)
        {
            AbzContext db = new AbzContext();
            OrderV order = db.OrderVs.Find(id);
            OrderId = id;
            Dat = order.DateExec;
            CDat = DateToString.CDat(order.DateExec);
            SmenaID = order.SmenaID;
            this.Smena = order.Smena;
            SelectSmena = new SelectList( db.Smenas.ToList(), "SmenaID", "Name", SmenaID);
        }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }
        [Display(Name = "Дата выполнения")]
        [DataType(DataType.Date)]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dat { get; set; }
        [Display(Name = "Дата выполнения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public string CDat { get; set; }
        [Display(Name = "Время выполнения")]
        public SelectList SelectSmena { get; set; }
        public int SmenaID { get; set; }
        [Display(Name = "Смена")]
        public string Smena { get; set; }
    }
}
