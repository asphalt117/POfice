using System;
using Domain.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class GoodRepository 
    {
        private readonly AbzContext db = new AbzContext();
        public IQueryable<Good> Get()
        {
            return db.Goods.OrderBy(b => b.CategId).ThenBy(c => c.txt);
        }

        public async Task<Order> SaveGood(int GoodID, int OrdID, decimal Quantity)
         {
            Order order = await db.Orders.FindAsync(OrdID);
            OrderProduct products;
            if (order.Step == 0)
            {
                //Значит 1е внесение
                products = new OrderProduct();
            }
            else
            {
                products = await db.OrderProducts.FirstOrDefaultAsync(a => a.OrderId == OrdID);
            }
            products.GoodId = GoodID;
            products.OrderId = OrdID;
            products.Quant = Quantity;
            if (products.OrderProductId == 0)
                db.OrderProducts.Add(products);
            else
                db.Entry(products).State = EntityState.Modified;
            int iddet = await db.SaveChangesAsync();
            return order;
        }
    }
}