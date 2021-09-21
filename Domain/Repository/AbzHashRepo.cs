using Domain.Engine;
using Domain.Entities;
using Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class AbzHashRepo
    {
        public AbzContext db = new AbzContext();
        //public AbzHash abzHash;

        public AbzHash GetHash(string auth)
        {
            return db.AbzHashes.Find(auth);
        }

        public Cust GetCust(string auth)
        {
            int CustID;
            AbzHash abzHash = GetHash(auth);
            if (abzHash != null)
            {
                if (abzHash.CustID != null)
                {
                    CustID = (int)abzHash.CustID;
                }
                else
                {
                    CustRepository repo = new CustRepository();
                    string usr = abzHash.Email;
                    CustID = db.UserAdmins.FirstOrDefault(u => u.Email == usr).CustID;
                    abzHash.CustID = CustID;
                    abzHash.ContractID= repo.GetContract(CustID).ContractID;
                    UpdateHash(abzHash);
                }
                return db.Custs.Find(CustID);
            }
            return null;
        }

        public void SetDafault(AbzHash hash)
        {            
            CustRepository repo = new CustRepository();
            string usr = hash.Email;
            hash.CustID = db.UserAdmins.FirstOrDefault(u => u.Email == usr).CustID;
            hash.ContractID =  repo.GetContract((int)hash.CustID).ContractID;
            db.AbzHashes.Add(hash);
            db.SaveChanges();
        }
        public void UpdateHash(AbzHash hash)
        {
            db.Entry(hash).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
