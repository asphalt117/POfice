using Domain.Entities;
using Domain.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Engine;

namespace Domain.Repository
{
    public class TrustTecsRepository
    {
        private readonly AbzContext db = new AbzContext();
        private int custID;

        public TrustTecsRepository(int cust)
        {
            custID = cust;
        }
        public async Task<TrustTecDet> Get(string gn)
        {
            Transport trans= await db.Transports.Where(t => (Int32)t.CustID == (Int32)custID).FirstOrDefaultAsync(o => o.Gn == gn);
            TrustTecDet tecDet = new TrustTecDet();
           // tecDet.Driv = trans.Driv;
            tecDet.Gn = trans.Gn;
            tecDet.TecModel = trans.TecModel;
            return tecDet;
        }

        public async Task<List<Transport>> GetList()
        {
            return await db.Transports.Where(t => (Int32)t.CustID == (Int32)custID).OrderByDescending(t => t.Dat).ToListAsync();
        }

        public async Task<List<TrustTecV>> Get()
        {
            List<TrustTec> tec = await db.TrustTecs.Where(t => t.CustId == custID).OrderByDescending(d=>d.DatCreate).ToListAsync();
            List<TrustTecV> trans = new List<TrustTecV>();
            foreach (var t in tec)
            {
                List<TrustTecDet> tdet = await db.TrustTecDets.Where(d=>d.TrustTecId==t.TrustTecId).ToListAsync();
                foreach (var d in tdet)
                {
                    TrustTecV tr = new TrustTecV();
                    tr.Gn = d.Gn;
                    tr.TecModel = d.TecModel;
                    tr.TrustTecID = d.TrustTecId;
                    tr.TrustTecDetID = d.TrustTecDetId;
                    tr.CustId = custID;
                    tr.Driv = d.Driv;
                    tr.BeginDat = t.BeginDat;
                    tr.EndDat = t.EndDat;
                    //tr.BeginDat = DateToString.CDat(t.BeginDat);
                    //tr.EndDat = DateToString.CDat(t.EndDat);
                    tr.Note = t.Note;
                    trans.Add(tr);
                }
            }
            //List<TrustTecV> tr = trans.OrderByDescending.
             return trans;
                //await db.TrustTecVs.Where(t => t.CustId == custID).ToListAsync();                
        }
        public async Task<int> Save(TrustTecView tecView)
        {
            TrustTec tec = tecView.Tec;
            tec.DatCreate= DateTime.Now;
            db.TrustTecs.Add(tec);
            await db.SaveChangesAsync();
            await SaveDetail(tec.TrustTecId, tecView.Detail);
            return tec.TrustTecId;
        }

        public async Task<int> Save(TrustTecV tecV)
        {
            TrustTec tec = new TrustTec();
            string BeginDat = tecV.BeginDat;
            string EndDat = tecV.EndDat;
            tec.BeginDat = tecV.BeginDat;
            tec.EndDat = tecV.EndDat;
            tec.CustId = custID;
            tec.Note = tecV.Note;
            tec.DatCreate = DateTime.Now;
            db.TrustTecs.Add(tec);
            await db.SaveChangesAsync();
            await SaveDetail(tec.TrustTecId, tecV.Detail);
            return tec.TrustTecId;
        }
        public async Task<int> SaveDetail(TrustTecDet det)
        {
            TrustTec tec = new TrustTec();
            tec.CustId = custID;
            tec.DatCreate= DateTime.Now;
            db.TrustTecs.Add(tec);
            int id= await db.SaveChangesAsync();
            TrustTecDet tecDet = new TrustTecDet();
            tecDet.TrustTecId = tec.TrustTecId;
            tecDet.TecModel = det.TecModel;
            tecDet.Gn = det.Gn;
            tecDet.Driv = det.Driv;
            db.TrustTecDets.Add(tecDet);
            int iddet = await db.SaveChangesAsync();
            return iddet;
        }

        public async Task<int> SaveDetail(int id, List<TrustTecDet> det)
        {
            foreach (var item in det)
            {
                TrustTecDet tecDet = new TrustTecDet();
                tecDet.TrustTecDetId = item.TrustTecDetId;
                tecDet.TrustTecId = id;
                tecDet.TecModel = item.TecModel;
                tecDet.Gn = item.Gn;
                tecDet.Driv = item.Driv;

                if (tecDet.TrustTecDetId == 0)
                    db.TrustTecDets.Add(tecDet);
                else
                    db.Entry(tecDet).State = EntityState.Modified;
            }
            int iddet = await db.SaveChangesAsync();
            return iddet;
        }

        public void delete(int id)
        {
            TrustTecDet tecDet = db.TrustTecDets.Find(id);
            TrustTec tec = db.TrustTecs.Find(tecDet.TrustTecId);
            int cnt = db.TrustTecDets.Count(c => c.TrustTecId == tec.TrustTecId);
            if (cnt < 2)
                db.TrustTecs.Remove(tec);
            db.TrustTecDets.Remove(tecDet);
            db.SaveChanges();

        }
    }
}
