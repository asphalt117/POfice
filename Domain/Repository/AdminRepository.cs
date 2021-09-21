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
            return lst;
        }
    }
}