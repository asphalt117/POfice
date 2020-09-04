using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class AdminRepository
    {
        private AbzContext db = new AbzContext();
        public List<UserAdmin> GetAdmins(string sortOrder)
        {
            List<UserAdmin> lst = new List<UserAdmin>();
            switch (sortOrder)
            {
                case "Email":
                    lst = db.UserAdmins.OrderBy(a => a.Email).ToList();
                    break;
                case "SmalName":
                    lst = db.UserAdmins.OrderBy(a => a.SmalName).ToList();
                    break;
                case "Inn":
                    lst = db.UserAdmins.OrderBy(a => a.Inn).ToList();
                    break;
                case "LastDat":
                    lst = db.UserAdmins.OrderByDescending(a => a.LastDat).ToList();

                    break;
                case "FullName":
                    lst = db.UserAdmins.OrderBy(a => a.FullName).ToList();
                    break;
            }

            //DefContext dd = new DefContext();
            //List<UserAdminFl> lst = new List<UserAdminFl>();

            //IEnumerable<AspNetUser> aspnetusers = (from a in dd.AspNetUsers select new AspNetUser { Id = a.Id, CustID = a.CustID, Email = a.Email, UserName = a.UserName }).ToArray();

            //switch (sortOrder)
            //{
            //    case "Email":
            //        //lst = (from a in db.UserAdmins
            //        //       join b in dd.AspNetUsers on a.UserId equals b.Id
            //        //       orderby b.Email
            //        //       select new UserAdminFl
            //        //       { VUserInCustId = a.VUserInCustId, FullName = a.FullName, Inn = a.Inn, SmalName = a.SmalName, LastDat = a.LastDat, CustID = a.CustID, UserId = a.UserId, Email = b.Email }).ToList();

            //        lst = (from a in db.UserAdmins
            //               join b in aspnetusers on a.UserId equals b.Id
            //               orderby b.Email
            //               select new UserAdminFl
            //               { VUserInCustId = a.VUserInCustId, FullName = a.FullName, Inn = a.Inn, SmalName = a.SmalName, LastDat = a.LastDat, CustID = a.CustID, UserId = a.UserId, Email = b.Email }).ToList();

            //        break;


            //    case "SmalName":
            //        lst = (from a in db.UserAdmins
            //               join b in aspnetusers on a.UserId equals b.Id
            //               orderby a.SmalName
            //               select new UserAdminFl
            //               { VUserInCustId = a.VUserInCustId, FullName = a.FullName, Inn = a.Inn, SmalName = a.SmalName, LastDat = a.LastDat, CustID = a.CustID, UserId = a.UserId, Email = b.Email }).ToList();
            //        break;


            //    case "Inn":
            //        lst = (from a in db.UserAdmins
            //               join b in aspnetusers on a.UserId equals b.Id
            //               orderby a.Inn
            //               select new UserAdminFl
            //               { VUserInCustId = a.VUserInCustId, FullName = a.FullName, Inn = a.Inn, SmalName = a.SmalName, LastDat = a.LastDat, CustID = a.CustID, UserId = a.UserId, Email = b.Email }).ToList();
            //        break;


            //    case "LastDat":
            //        lst = (from a in db.UserAdmins
            //               join b in aspnetusers on a.UserId equals b.Id
            //               orderby a.LastDat
            //               select new UserAdminFl
            //               { VUserInCustId = a.VUserInCustId, FullName = a.FullName, Inn = a.Inn, SmalName = a.SmalName, LastDat = a.LastDat, CustID = a.CustID, UserId = a.UserId, Email = b.Email }).ToList();
            //        break;


            //    case "FullName":
            //        lst = (from a in db.UserAdmins
            //               join b in aspnetusers on a.UserId equals b.Id
            //               orderby a.FullName
            //               select new UserAdminFl
            //               { VUserInCustId = a.VUserInCustId, FullName = a.FullName, Inn = a.Inn, SmalName = a.SmalName, LastDat = a.LastDat, CustID = a.CustID, UserId = a.UserId, Email = b.Email }).ToList();
            //        break;
            //}

            return lst;
        }
    }
}