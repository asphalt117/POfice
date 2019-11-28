using System.Data.Entity;

namespace Domain.Entities
{
    public class AbzContext : DbContext
    {
        public AbzContext()
            : base("name=AbzConnection")
        {
        }
        public DbSet<Cust> Custs { get; set; }
        public DbSet<UserInCust> UserInCusts { get; set; }
        public DbSet<Categ> Categs { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Adres> Adreses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<FinBal> FinBals { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Ttn> Ttns { get; set; }
        public DbSet<Trust> Trusts { get; set; }
        public DbSet<OrgTrustTec> OrgTrustTecs { get; set; }
        public DbSet<TrustTec> TrustTecs { get; set; }
        public DbSet<TrustTecDet> TrustTecDets { get; set; } 
        public DbSet<OrdInvoice> OrdInvoices { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<OrderV> OrderVs { get; set; }
        public DbSet<OrderProductView> OrderProductViews { get; set; }
        public DbSet<GraphSale> GraphSales { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<CustomInfo> CustomInfos { get; set; }
        public DbSet<OrderDriv> OrderDrivs { get; set; }
        public DbSet<Transport> Transports { get; set; }

        public DbSet<UserAdmin> UserAdmins { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Smena> Smenas { get; set; }
        
    }

}
