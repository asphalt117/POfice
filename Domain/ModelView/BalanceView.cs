using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.ModelView
{
    public class BalanceView
    {
        public FinBal Fin { get; set; }
        //public IEnumerable<Balance> Balances { get; set; }
        public List<Balance> Balances { get; set; }
        [DisplayFormat(DataFormatString = "{0:###,#.##}")]
        [DisplayName("Кол-во")]
        public decimal kol { get; set; }
        [DisplayName("Отгрузка (Руб)")]
        [DisplayFormat(DataFormatString = "{0:###,#.##}")]
        public decimal Ssm { get; set; }
        [DisplayName("Платежи (руб)")]
        [DisplayFormat(DataFormatString = "{0:###,#.##}")]
        public decimal Psm { get; set; }
        public string Contract { get; set; }
    }
}
