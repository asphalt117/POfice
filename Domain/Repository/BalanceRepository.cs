using System;
using Domain.Entities;
using Domain.ModelView;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class BalanceRepository
    {
        private AbzContext db = new AbzContext();
        public BalanceView GetListBalance(int cust,int contract)
        {
            BalanceView balance = new BalanceView();
            DateTime LastDat;
            int year = DateTime.Today.Year;
            int dm = DateTime.Today.Month;
            LastDat= new DateTime(year, dm, 01, 0, 0, 0);
            var dat = db.Database.SqlQuery<DateTime>("SELECT dbo.LastDat1s()");
            foreach (var prn in dat)
            {
                LastDat = prn;
            }
            if (contract > 0)
            {
                balance.Fin = db.FinBals.FirstOrDefault(f => f.CustID == cust && f.Dat >= LastDat && f.ContractID == contract);
                balance.Balances = db.Balances.Where(b => b.CustID == cust && b.Dat >= LastDat && b.ContractID == contract).OrderBy(b => b.Dat).ToList();
            }
            else
            {
                balance.Fin = db.FinBals.FirstOrDefault(f => f.CustID == cust && f.Dat >= LastDat);
                balance.Balances = db.Balances.Where(b => b.CustID == cust && b.Dat >= LastDat).OrderBy(b => b.Dat).ToList();
            }

            var blTotal = from b in balance.Balances
                          group b by b.CustID
                into q
                          select new { kol = q.Sum(b => b.kol), ssm = q.Sum(b => b.Ssm), psm = q.Sum(b => b.Psm) };

            foreach (var bl in blTotal)
            {
                balance.kol = bl.kol;
                balance.Ssm = bl.ssm;
                balance.Psm = bl.psm;
            }

            if (balance.Fin == null)
            {
                balance.Fin = new FinBal();
                balance.Fin.Dat = LastDat;
                balance.Fin.Sm = 0.0m;
            }

            Contract _contract = db.Contracts.Find(contract);
            if (_contract == null)
                balance.Contract = "Нет договора";
            else 
                balance.Contract = _contract.Num;

            return balance;
        }
        public decimal GetBalance(int cust,int contract)
        {
            BalanceView balance= GetListBalance(cust, contract);
            decimal sm = balance.Psm - balance.Ssm + balance.Fin.Sm;
            return sm;
        }
    }
}
