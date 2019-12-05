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
        private AbzContext db = new AbzContext();

        //Получение списка заказов-счетов
        public async Task<List<OrderV>> GetOrder(int id, int invoice)
        {
            List<OrderV> orders = await db.OrderVs.Where(d => d.CustId == id &&
                                                d.StatusId > 0 && d.Invoice == invoice).OrderByDescending(x => x.DateExec).ToListAsync();
            return orders;
        }

        public async Task<Order> GetNew(AbzHash abzHash, int invoice)
        {
            Order order = new Order();
            order.CustId = (int)abzHash.CustID;
            order.ContractId = abzHash.ContractID;
            order.email = MyCrypto.DeShifrovka(abzHash.Email);
            order.insDate = DateTime.Now;
            order.DateExec = DateTime.Now.AddDays(1);
            order.Dat = DateTime.Now.AddDays(1);
            order.AdresId = 1;
            order.Invoice = invoice;
            db.Orders.Add(order);
             await db.SaveChangesAsync();
            return order;
        }

        public async Task<OrderV> GetOrderV(int id)
        {
            OrderV vsh = await db.OrderVs.FindAsync(id);
             return vsh;
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
        public async Task<int> SaveGoodl(int id, OrderProductView det)
        {
            OrderProduct products = new OrderProduct();
            products.OrderProductId = det.OrderProductId;
            products.GoodId = det.GoodId;
            products.OrderId = id;
            products.Quant = det.Quant;
            if (products.OrderProductId == 0)
                db.OrderProducts.Add(products);
            else
                db.Entry(products).State = EntityState.Modified;
            int iddet = await db.SaveChangesAsync();
            //Order order = 
                await SaveStep(id, 1);
            return iddet;
        }
    }
}
