using Domain.Engine;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class OrdRepository
    {
        private readonly AbzContext db = new AbzContext();

        public async Task<Order> CloneableOrder(int id)
        {
            Order newOrder = await db.Orders.AsNoTracking().SingleOrDefaultAsync(a => a.OrderId == id);
            newOrder.OrderId = 0;
            db.Orders.Add(newOrder);
            return newOrder;
        }

        public async Task<OrderV> GetCopy(int id, int invoice = -1)
        {
            Order order = await CloneableOrder(id);
            order.DateExec = DateTime.Now.AddDays(1);
            if (invoice > -1)
                order.Invoice = invoice;
 
            db.Orders.Add(order);
            await db.SaveChangesAsync();


            OrderProduct item = await db.OrderProducts.FirstOrDefaultAsync(a => a.OrderId == id);
            OrderProduct product = new OrderProduct();
            product.GoodId = item.GoodId;
            product.OrderId = order.OrderId;
            product.Quant = item.Quant;

            db.OrderProducts.Add(product);
            await db.SaveChangesAsync();
            OrderV ord = await db.OrderVs.FindAsync(order.OrderId);
            return ord;
        }
        //Получение списка заказов-счетов
        public async Task<List<OrderV>> GetOrder(int id, int invoice)
        {
            List<OrderV> orders = await db.OrderVs.Where(d => d.CustId == id &&
                                             d.Invoice == invoice).OrderByDescending(x => x.DateExec).ToListAsync();
            //d.StatusId > 0 && d.Invoice == invoice).OrderByDescending(x => x.DateExec).ToListAsync();
            return orders;
        }

        public async Task<Order> GetNew(AbzHash abzHash, int invoice)
        {
            Order order = new Order();
            order.CustId = (int)abzHash.CustID;
            order.ContractId = abzHash.ContractID;
            order.email = MyCrypto.DeShifrovka(abzHash.Email);
            order.insDate = DateTime.Now;
            if (invoice == 0)
            {
                order.DateExec = DateTime.Now.AddDays(1);
                order.Dat = DateTime.Now.AddDays(1);
            }
            else
            {
                order.DateExec = DateTime.Now;
                order.Dat = DateTime.Now;
            }
            order.AdresId = 1;
            order.Invoice = invoice;
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return order;
        }

        public async Task<Order> SaveBooking(int ord, int status)
        {
            //Проверить что все заполнено, потом изменять статус
            Order order = db.Orders.Find(ord);
            order.StatusId = status;
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return order;
        }

        public async Task<Order> SaveStep(int ord, int step)
        {
            //Проверить что все заполнено, потом изменять статус
            Order order = db.Orders.Find(ord);
            order.Step = step;
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return order;
        }
        public async Task<Order> SaveGood(int GoodID, int OrdID, decimal Quantity)
        {
            Order order = await db.Orders.FindAsync(OrdID);
            OrderProduct products;
            bool nAdd=false;
            if (order.Step == 0)
            {
                //Значит 1е внесение
                products = new OrderProduct();
                await SaveStep(OrdID, 1);
            }
            else
            {
                products = await db.OrderProducts.FirstOrDefaultAsync(a => a.OrderId == OrdID);
                nAdd = true;
            }
            products.GoodId = GoodID;
            products.OrderId = OrdID;
            products.Quant = Quantity;
            if (!nAdd)
                //(order.Step == 1)
                db.OrderProducts.Add(products);
            else
                db.Entry(products).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return order;
        }
    }
}
