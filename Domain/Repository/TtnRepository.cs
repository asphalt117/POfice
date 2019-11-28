using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using System.Threading.Tasks;


namespace Domain.Repository
{
    public class TtnRepository
    {
        private AbzContext db = new AbzContext();
        public List<Ttn> GetTtn(int custId, DateTime begDt, DateTime endDt,int contact)
        {
            if (contact==0)
                return db.Ttns.Where(w => w.CustID == custId && w.Dat >= begDt
                                               && w.Dat < endDt
                                               && w.ismark == 0)
                                               .OrderByDescending(b => b.Id).ToList();
                        
            else
                return db.Ttns.Where(w => w.CustID == custId && w.Dat >= begDt
                                               && w.Dat < endDt
                                               && w.ismark == 0
                                               && w.ContractID==contact)
                        .OrderByDescending(b => b.Id).ToList();
        
        }

        public IQueryable<Ttn> GetSkipTake(int skip, int take, int custId,DateTime begDt,DateTime endDt, int contact)
        {
            if (contact == 0)
                return db.Ttns.Where(w => w.CustID == custId 
                                           && w.Dat >= begDt
                                           && w.Dat < endDt
                                           && w.ismark == 0)
                    .OrderByDescending(b => b.Id)
                    .Skip(skip)
                    .Take(take);
            else
                return db.Ttns.Where(w => w.CustID == custId && w.Dat >= begDt
                                           && w.Dat < endDt && w.ContractID == contact
                                           && w.ismark == 0)
                    .OrderByDescending(b => b.Id)
                    .Skip(skip)
                    .Take(take);

        }

        public int GetCount(int custId, DateTime begDt, DateTime endDt, int contact)
        {
            if (contact == 0)
                return db.Ttns.Count(t => t.CustID == custId
                                           && t.ismark == 0
                                           && t.Dat >= begDt
                                           && t.Dat < endDt);
            else
                return db.Ttns.Count(t => t.CustID == custId
                                           && t.ismark == 0
                                           && t.Dat >= begDt
                                           && t.Dat < endDt && t.ContractID == contact);

        }

    }
}
