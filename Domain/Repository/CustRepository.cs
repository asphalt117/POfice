using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Domain.ModelView;
using System.Data;
using System.Data.Entity;
using System;


namespace Domain.Repository
{
    public class CustRepository
    {
        private readonly AbzContext db = new AbzContext();

        public IEnumerable<Cust> GetINN(string inn)
        {
            return db.Custs.Where(d => d.Inn == inn);
        }

        public Cust GetInn(string inn)
        {
            return db.Custs.FirstOrDefault(d => d.Inn == inn);
        }
        public Cust Get1S(string Cod)
        {
            return db.Custs.FirstOrDefault(d => d.Cod1s == Cod);
        }
        public Cust Get(int id)
        {
            return db.Custs.FirstOrDefault(c => c.CustId == id);                
        }

        public IEnumerable<OrgView> GetCust(string id)
        {
            var orgView = from u in db.UserAdmins                          
                          join c in db.Custs
                              on u.CustID equals c.CustId
                          where u.Email == id                          
                          orderby u.LastDat descending
                          select new OrgView
                          {
                              txt = c.FullName,
                              ID = c.CustId
                          };
            return orgView;
        }

        public IEnumerable<OrgView> GetCust(int id)
        {
            var orgView = from c in db.Custs
                          where c.CustId == id
                          select new OrgView
                          {
                              txt = c.FullName,
                              ID = c.CustId
                          };
            return orgView;
        }

        public IEnumerable<Contract> GetContracts(int custid)
        {
            return db.Contracts.Where(a => a.CustID == custid).OrderBy(a => a.Num).AsEnumerable();
        }
        public Contract GetContract(int custid)
        {
            return db.Contracts.FirstOrDefault(a => a.CustID == custid);
        }

        //public Contract GetContract(int custid,int contrictid)
        //{
        //    return db.Contracts.FirstOrDefault(a => a.CustID == custid && );
        //}

        public int GetCustEmail(string email)
        {
            return db.UserAdmins.FirstOrDefault(u => u.Email == email).CustID;
        }

        public void CustUser(string userId,int custId, string ip, string adres)
        {
            Statistic statistic = new Statistic();
            statistic.CustID = custId;
            statistic.UserId = userId;
            statistic.UserAdres = ip;
            statistic.UserHost = adres;
            db.Statistics.Add(statistic);
            UserInCust userInCust = db.UserInCusts.FirstOrDefault(u => u.UserId == userId && u.CustID == custId);
            if (userInCust != null)
            {
                userInCust.LastDat= DateTime.Now;
                db.Entry(userInCust).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
