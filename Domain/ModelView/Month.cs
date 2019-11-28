using System.Collections.Generic;
using System;

namespace Domain.ModelView
{
    public class Month
    {
        public int Id { get; set; }
        public string Mnth { get; set; }
    }

    public class GetDat
    {        
        public List<Month> GetYears()
        {
            int year = DateTime.Today.Year;
            int i;
            Month dt;
            List<Month> dyear = new List<Month>();
            for (i = 2011; i < year + 1; i++)
            {
                dt = new Month { Id = i, Mnth = i.ToString() };
                dyear.Add(dt);
            }
            return dyear;
        }

        public IEnumerable<Month> data = new[]
        {
            new Month {Id = 1, Mnth = "Январь"},
            new Month {Id = 2, Mnth = "Февраль"},
            new Month {Id = 3, Mnth = "Март"},
            new Month {Id = 4, Mnth = "Апрель"},
            new Month {Id = 5, Mnth = "Май"},
            new Month {Id = 6, Mnth = "Июнь"},
            new Month {Id = 7, Mnth = "Июль"},
            new Month {Id = 8, Mnth = "Август"},
            new Month {Id = 9, Mnth = "Сентябрь"},
            new Month {Id = 10, Mnth = "Октябрь"},
            new Month {Id = 11, Mnth = "Ноябрь"},
            new Month {Id = 12, Mnth = "Декабрь"},
        };
    }
    public class DateOf
    {
        GetDat getdat = new GetDat();
        public IEnumerable<Month> getMonth { get; set; }
        public string SelectedMonth { get; set; }
        public IEnumerable<Month> getYear { get; set; }
        public string SelectedYear { get; set; }
        public DateOf(int? month,int? year)
        {
            getMonth = getdat.data;
            getYear= getdat.GetYears();
            SelectedMonth = month.ToString();
            SelectedYear = year.ToString();
        }
    }
}
