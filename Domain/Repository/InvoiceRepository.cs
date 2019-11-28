using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ModelView;

namespace Domain.Repository
{
    public class InvoiceRepository
    {
        private AbzContext db = new AbzContext();
        private int custID;
        public InvoiceRepository(int cust)
        {            
            custID = cust;
        }

        public Price GetPrice(int good)
        {
            Price pr=new Price();
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@Good", good);
            System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@Custom", custID);
            var price = db.Database.SqlQuery<Price>("SELECT dbo.PriceForCust (@Good,@Custom)", param, param1);
            foreach(var prn in price)
            {
                pr = prn;
            }
            
            return pr;
        }
        public InvoiceView Insert(InvoiceView invSession)
        {
            Invoice invoice = new Invoice();
            invoice.GoodId = invSession.GoodId;
            invoice.CustId = custID;
            invoice.Quant = invSession.Quant;
            Price price = GetPrice(invoice.GoodId);
            invSession.Price = price.Nprice;

            invoice.Sm = invoice.Quant * invSession.Price;
            invSession.Sm = invoice.Sm;

            //Num num = db.Nums.First(n => n.NumId == 2);

            //invoice.Num = num.NumT;
            //invSession.Num = invoice.Num;

            //num.NumT = num.NumT + 1;

            //if (invParam.ContractView != null)
            //{
            //    //ContractRepository contract = new ContractRepository(custID);
            //    Contract contr;

            //    invoice.ContractID = Convert.ToInt32(invParam.ContractView.SelectedID);
            //    contr = contract.GetContract(invoice.ContractID);
            //    invSession.ContractNum = contr.Num;
            //}

            db.Invoices.Add(invoice);
            db.SaveChanges();

            return invSession;
        }

        public InvoiceView CreateInvoice(int id)
        {
            InvoiceView invoice = new InvoiceView();
            Good good = db.Goods.Find(id);
            invoice.Good = good.txt;
            Price price = GetPrice(id);
            invoice.Price = price.Nprice;
            invoice.GoodId = id;
            invoice.ContractView = new ContractView();            
            invoice.ContractView.Contracts = db.Contracts.Where(c => c.CustID == custID).ToList();
            invoice.ContractView.SelectedID = db.Contracts.FirstOrDefault(c => c.CustID==custID).ToString();
            return invoice;
        }


    }
}
