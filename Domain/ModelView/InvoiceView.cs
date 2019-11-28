using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.ModelView
{
    public class InvoiceView
    {
        [Key]
        public int InvoiceId { get; set; }

        [DisplayName("Заказчик")]
        public string Cust { get; set; }
        public int CustId { get; set; }

        [DisplayName("Продукция")]
        public string Good { get; set; }
        public int GoodId { get; set; }
        [DisplayName("Количество")]
        public decimal Quant { get; set; }

        [DisplayName("Ед. изм.")]
        public string Unit { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [DisplayName("Ориентировочная сумма")]
        public decimal Sm { get; set; }
        [DisplayName("№")]
        public int Num { get; set; }
        [DisplayName("Договор")]
        public ContractView ContractView { get; set; }

        [DisplayName("Договор №")]
        public string ContractNum { get; set; }
    }
}