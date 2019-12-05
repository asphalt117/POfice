using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.ModelView;
using System.Web.Mvc;
using System;
using System.Data.Entity;
using Domain.Engine;
using System.Threading.Tasks;
using System.Data;

namespace Domain.Repository
{
    public class OrderRepositoryOld
    {
        private AbzContext db = new AbzContext();
        private OrderView sv = new OrderView();
        private PersonRepository personRepo = new PersonRepository();

        public async Task<List<OrderV>> GetOrder(int id,int invoice)
        {
            List<OrderV> orders = await db.OrderVs.Where(d => d.CustId == id && 
                                                d.StatusId> 0 && d.Invoice == invoice).OrderByDescending(x => x.DateExec).ToListAsync();
            return orders;
        }
        public async Task<int> Delete(int id)
        {
            Order ord = await db.Orders.FindAsync(id);
            db.Orders.Remove(ord);
            await db.SaveChangesAsync();
            return id;
        }
        public async Task<OrderView> GetChange(int id)
        {
            OrderV vsh = await db.OrderVs.FindAsync(id);
                
            sv.OrderId = vsh.OrderId;
            sv.CustId = vsh.CustId;
            sv.Good = vsh.Good;
            sv.Unit = vsh.Unit;
            sv.AdresId = vsh.AdresId;
            sv.Adres = vsh.Adres;
            sv.ContractId = vsh.ContractId;
            sv.Contract = vsh.Contract;
            sv.Centr = vsh.Centr;
            sv.Dat = vsh.DateExec;
            sv.CDat = DateToString.CDat(vsh.DateExec);
            sv.Note = vsh.Note;
            sv.Status = vsh.Status;
            sv.PersonId = vsh.PersonId;
            sv.Invoice = vsh.Invoice;
            sv.RelatedOrderId = vsh.RelatedOrderId;
            sv = await GetSelectList(sv);
            sv.isOnlinePay = vsh.isOnlinePay;
            sv.email = vsh.email;
            sv.Products = db.OrderProductViews.Where(o => o.OrderId == sv.OrderId).ToList();
            sv.OrderDrivs=db.OrderDrivs.Where(o => o.OrderId == sv.OrderId).ToList();
            sv.Smena = vsh.Smena;
            sv.SmenaID = vsh.SmenaID;
            return sv;
        }
        public async Task<OrderView> GetCopy(int id,int invoice)
        {
            sv = await GetChange(id);
            sv.OrderId = 0;
            sv.Dat = DateTime.Now;
            sv.CDat = DateToString.CDat(sv.Dat);
            sv.Invoice = invoice;
            sv.RelatedOrderId = id;
            int Orderid =await Save(sv);
            sv.OrderId = Orderid;

            //Разобраться. Повторяется!!!
            foreach (var item in sv.Products)
            {
                OrderProduct products = new OrderProduct();
                products.OrderProductId = item.OrderProductId;
                products.GoodId = item.GoodId;
                products.OrderId = Orderid;
                products.Quant = item.Quant;
                db.OrderProducts.Add(products);
            }
            await db.SaveChangesAsync();
            
            return await GetChange(Orderid);
        }
        public async Task<OrderView> GetCreate(int custid, int invoice, string email, int? contractId)
        //public async Task<OrderView> GetCreate(int custid, int invoice, string email)
        {
            sv.CustId = custid;
            sv.AdresId = 1;
            //sv.PersonId = 0;
            sv = await GetSelectList(sv);
            sv.Dat = DateTime.Now.AddDays(1);
            sv.CDat = DateToString.CDat(sv.Dat);
            sv.Invoice = invoice; 
            sv.email = email;  //???
            if (contractId == null)
                sv.ContractId = 0;
            else
                sv.ContractId = (int)contractId;

            sv.Products = new List<OrderProductView>();
            sv.OrderId = await Save(sv,sv.Products,email);

            return sv;
        }

        public async Task<OrderView> GetNew(AbzHash abzHash, int invoice)
        //public async Task<Order> GetNew(AbzHash abzHash,int invoice)
        {
            Order order = new Order();
            order.CustId = (int)abzHash.CustID;
            order.ContractId = abzHash.ContractID;
            order.email = MyCrypto.DeShifrovka(abzHash.Email);
            order.insDate   = DateTime.Now;
            order.DateExec  = DateTime.Now.AddDays(1);
            order.Dat       = DateTime.Now.AddDays(1);
            order.AdresId = 1;
            order.Invoice = invoice;
            db.Orders.Add(order);
            await db.SaveChangesAsync();

            return await GetChange(order.OrderId);
            //return order;
        }
        private async Task<OrderView> GetSelectList(OrderView order)
        {
            int CustId = order.CustId;
            Adres adres = await db.Adreses.FindAsync(order.AdresId);
            sv.Adres = adres.txt;
            Contract contract = await db.Contracts.FindAsync(order.ContractId);
            sv.Contract = contract.Num;            
            Person person = await db.Persons.FindAsync(order.PersonId);
            sv.Person = person.Name;
            sv.SelectAdres = new SelectList(await db.Adreses.Where(a => a.CustId == CustId | a.CustId == null).OrderBy(x=>x.txt).ToListAsync(), "AdresId", "Txt", sv.AdresId);
            sv.SelectContract = new SelectList(await db.Contracts.Where(a => a.CustID == CustId | a.CustID == 0).ToListAsync(), "ContractId", "Num", sv.ContractId);
            sv.SelectPerson = new SelectList( await personRepo.GetPerson(order.CustId), "PersonId", "Name", sv.PersonId);
            sv.SelectSmena = new SelectList(await db.Smenas.ToListAsync());
            return sv;
        }

        public async Task<int> Save(OrderView sv)
        {
            //Проверить что все заполнено, потом изменять статус
            Order order = new Order();
            order.OrderId = sv.OrderId;
            order.CustId = sv.CustId;
            order.AdresId = sv.AdresId;
            order.Centr = sv.Centr;
            order.ContractId = sv.ContractId;
            if (sv.CDat == null)
                order.DateExec = DateTime.Now;
            else 
                order.DateExec = StringToDate.Date(sv.CDat);
            order.PersonId = sv.PersonId;
            order.note = sv.Note;
            order.insDate = DateTime.Now;
            order.isOnlinePay = sv.isOnlinePay;
            order.StatusId = sv.StatusId;
            order.Invoice = sv.Invoice;
            order.RelatedOrderId = sv.RelatedOrderId;

            order.email = sv.email;

            if (order.OrderId == 0)
                db.Orders.Add(order);
            else
                db.Entry(order).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task<Order> SaveBooking (int ord,int status)
        {
            //Проверить что все заполнено, потом изменять статус
            Order order = db.Orders.Find(ord);
            order.StatusId = status;
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return order;
        }
        public async Task<int> Save(OrderView sv, List<OrderProductView> det,string email)
        {
            sv.email = email;
            int id = await Save(sv);
            await SaveDetails(id, det);
            return id;
        }
        public async Task<int> SaveDetails(int id, List<OrderProductView> det)
        {
            foreach (var item in det)
            {
                OrderProduct products = new OrderProduct();
                products.OrderProductId = item.OrderProductId;
                products.GoodId = item.GoodId;
                products.OrderId = id;
                products.Quant = item.Quant;
                if (products.OrderProductId == 0)
                    db.OrderProducts.Add(products);
                else
                    db.Entry(products).State = EntityState.Modified;
            }
            int iddet = await db.SaveChangesAsync();
            return iddet;
        }
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
