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

            //sv.OrderId = vsh.OrderId;
            //sv.CustId = vsh.CustId;
            //sv.Good = vsh.Good;
            //sv.Unit = vsh.Unit;
            //sv.AdresId = vsh.AdresId;
            //sv.Adres = vsh.Adres;
            //sv.ContractId = vsh.ContractId;
            //sv.Contract = vsh.Contract;
            //sv.Centr = vsh.Centr;
            //sv.Dat = vsh.DateExec;
            //sv.CDat = DateToString.CDat(vsh.DateExec);
            //sv.Note = vsh.Note;
            //sv.Status = vsh.Status;
            //sv.PersonId = vsh.PersonId;
            //sv.Invoice = vsh.Invoice;
            //sv.RelatedOrderId = vsh.RelatedOrderId;
            //sv = await GetSelectList(sv);
            //sv.isOnlinePay = vsh.isOnlinePay;
            //sv.email = vsh.email;
            //sv.Products = db.OrderProductViews.Where(o => o.OrderId == sv.OrderId).ToList();
            //sv.OrderDrivs = db.OrderDrivs.Where(o => o.OrderId == sv.OrderId).ToList();
            //sv.Smena = vsh.Smena;
            //sv.SmenaID = vsh.SmenaID;
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
        //public async Task<int> SaveDetails(int id, List<OrderProductView> det)
        //{
        //    foreach (var item in det)
        //    {
        //        OrderProduct products = new OrderProduct();
        //        products.OrderProductId = item.OrderProductId;
        //        products.GoodId = item.GoodId;
        //        products.OrderId = id;
        //        products.Quant = item.Quant;
        //        if (products.OrderProductId == 0)
        //            db.OrderProducts.Add(products);
        //        else
        //            db.Entry(products).State = EntityState.Modified;
        //    }
        //    int iddet = await db.SaveChangesAsync();
        //    return iddet;
        //}
        public async Task<int> SaveDetail(int id, OrderProductView det)
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
            return iddet;
        }
    }
}
