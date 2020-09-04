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


            //using (var defDb = new DefContext())
            //{
            //    var tmpUsers = new List<AspNetUser>();
            //    tmpUsers.AddRange(defDb.AspNetUsers
            //            .AsEnumerable()
            //            .Select(a => new AspNetUser { Id = a.Id, CustID = a.CustID, Email = a.Email, UserName = a.UserName })
            //            .Where(b => b.Email == id)
            //    );

            //    var aspnetusers = tmpUsers.ToArray();

            //    var orgView = from u in db.UserAdmins
            //                  join c in db.Custs on u.CustID equals c.CustId
            //                  join b in aspnetusers on u.UserId equals b.Id
            //                  orderby u.LastDat descending
            //                  select new OrgView
            //                  {
            //                      txt = c.FullName,
            //                      ID = c.CustId
            //                  };

            //    return orgView;
            //}
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
            // AdminContext admindb = new AdminContext();

            return db.UserAdmins.FirstOrDefault(u => u.Email == email).CustID;


            //using (var defDb = new DefContext())
            //{
            //    var lst = defDb.AspNetUsers.Select(a=>a.Id).ToArray();
                    
                    
                    
    //                result.AddRange(db.PublicationType
    //    .AsEnumerable() // переход на LINQ to Objects
    //    .Select(p => new PublicationType { TitleType = p.TitleType })
    //);





                //List<AspNetUser> tmpUsers = new List<AspNetUser>();

                // var ttttt = new List<AspNetUser>();



                //var rrr = (from a in defDb.AspNetUsers where a.Email == email select new AspNetUser { Id = a.Id, CustID = a.CustID, Email = a.Email, UserName = a.UserName }).ToArray();



                //ttttt.AddRange(defDb.AspNetUsers
                //        .AsEnumerable()
                //        .Select(a => new AspNetUser { Id = a.Id, CustID = a.CustID, Email = a.Email, UserName = a.UserName })
                //        .Where(b => b.Email == email)
                //);




                //tmpUsers.AddRange(defDb.AspNetUsers
                //        .AsEnumerable()
                //        .Select(a => a)
                //        .Where(b => b.Email == email)
                //);

                // tmpUsers.AddRange(defDb.AspNetUsers
                //        .AsEnumerable()
                //        .Select(a => new AUser{ Id = a.Id, CustID = a.CustID, Email = a.Email, UserName = a.UserName })
                //        .Where(b => b.Email == email)
                //);


                //var aspnetusers = ttttt.ToArray();

                //var tt = db.UserAdmins.Join(aspnetusers, a => a.UserId, b => b.Id, (a, b) => new { aa = a.UserId });

                //var ii = (from a in db.UserAdmins
                //          join b in aspnetusers on a.UserId equals b.Id
                //          select (a.CustID)).ToArray().FirstOrDefault();



               // var result = context.Customers
               //.Select(c =>
               //    c.Orders
               //    .Join(products, o => o.Id, i => i.Id, (o, i) => new { o, i })).ToList();

                 



             //   ar result = players.Join(teams, // второй набор
             //p => p.Team, // свойство-селектор объекта из первого набора
             //t => t.Name, // свойство-селектор объекта из второго набора
             //(p, t) => new { Name = p.Name, Team = p.Team, Country = t.Country }); // результат


                //var same = _db.Articles.Where(a => a.Tags.Any(t => ids.Contains(t.Id)));

                //var cLIENTS = await _context.CLIENTS
                //.Include(c => c.CATEGORY)
                //.Include(c => c.AccountList)
                //.FirstOrDefaultAsync(m => m.CLIENTID == id);




               // return (from a in db.UserAdmins
               //         join b in aspnetusers on a.UserId equals b.Id
                //        select (a.CustID)).ToArray().FirstOrDefault(); 


                //return (from a in db.UserAdmins
                //        join b in aspnetusers on a.UserId equals b.Id
                //        select new { CustID = a.CustID }).ToArray().FirstOrDefault().CustID; 
           //}
            //var aspnetusers = (from a in dd.AspNetUsers where a.Email == email select new AspNetUser { Id = a.Id, CustID = a.CustID, Email = a.Email, UserName = a.UserName }).ToArray();
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
