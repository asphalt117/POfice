using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class AdminContext : DbContext
    {
        public AdminContext()
            : base("name=DefaultConnection")
        {
        }
        public DbSet<AbzHash> AbzHashs { get; set; }
    }
}