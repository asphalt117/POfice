using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Entities;
using Domain.ModelView;
using Domain.Repository;
using WebUI.Models;
using Microsoft.AspNet.Identity;

//Новый заказ
//При любом переходе (Прод., адрес, персон) сохраняю в сессию order, а детали в сессию detail
//
//Обнулять сессии. Иначе попадает в следующий заказ

namespace WebUI.Controllers
{
    public class OrderController : BaseController
    {
       //Nx!oE7x_
        private OrderRepository repo = new OrderRepository();
        private OrderView order;
        private OrderSes orderSes=new OrderSes();

        public ActionResult Order()
        {
            Menu.ChangeSelected(4, 1);
            Session["Menu"] = Menu;
            orderSes.OrderType = 0;
            orderSes.act = "Заказы";
            orderSes.Prefix = "Заказ";
            Session["OrdSes"] = orderSes;
            return RedirectToAction("Index");
        }

        public ActionResult Invoice()
        {
            Menu.ChangeSelected(4, 2);
            Session["Menu"] = Menu;
            orderSes.OrderType = 1;
            orderSes.act = "Счета";
            orderSes.Prefix = "Счет";
            Session["OrdSes"] = orderSes;
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Index()
        {
            Session["ChProducts"] = null;
            Session["Products"] = null;
            Session["trans"] = null;            
            orderSes = (OrderSes)Session["OrdSes"];
            List<OrderV> orders = await repo.GetOrder(Menu.CustId, orderSes.OrderType);
            ViewBag.Title = orderSes.act;
            return View(orders);
        }

        public async Task<ActionResult> Create()
        {
            orderSes = (OrderSes)Session["OrdSes"];
            string email= User.Identity.GetUserName();
            order = await repo.GetCreate(Menu.CustId, orderSes.OrderType, email);
            //order.email = User.Identity.GetUserName();
            orderSes.id=order.OrderId;
            orderSes.act = "Новый "+ orderSes.Prefix;
            Session["OrdSes"] = orderSes;
            return RedirectToAction("Booking");
        }
        public ActionResult Edit(int id)
        {
            orderSes = (OrderSes)Session["OrdSes"];
            orderSes.id = id;
            orderSes.act = "Редактировать " + orderSes.Prefix;
            Session["OrdSes"] = orderSes;
            
            return RedirectToAction("Booking");
        }
        public async Task<ActionResult> Copy(int id)
        {
            orderSes = (OrderSes)Session["OrdSes"];
            order = await repo.GetCopy(id, orderSes.OrderType);
            orderSes.id = order.OrderId;
            string cord = id.ToString();
            //orderSes.act = "Копия заказа №"+ cord+" Новый заказ";
            orderSes.act = "Новый " + orderSes.Prefix;
            Session["OrdSes"] = orderSes;
            return RedirectToAction("Booking");
        }
        public async Task<ActionResult> Delete(int id)
        {
            orderSes = (OrderSes)Session["OrdSes"];
            orderSes.id = id;
            orderSes.act = "Удаление заказа";
            Session["OrdSes"] = orderSes;
            order = await repo.GetChange(orderSes.id);
            ViewBag.Order = "Удаление заказа";
            return View(order);
        }
        [HttpPost]
        public async Task<ActionResult> Delete()
        {
            orderSes= (OrderSes)Session["OrdSes"];
            await repo.Delete(orderSes.id);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> CreateFrom(int id)
        {
            Menu.ChangeSelected(4, 2);
            Session["Menu"] = Menu;
            order = await repo.GetCopy(id, 1);

            order.Invoice = 1;
            orderSes.id = order.OrderId;

            orderSes.OrderType = 1;
            orderSes.act = "Счета";
            orderSes.Prefix = "Счет";

            Session["OrdSes"] = orderSes;
            return RedirectToAction("Booking");
        }
        public async Task<ActionResult> CreateFromInvoice(int id)
        {
            Menu.ChangeSelected(4, 1);
            Session["Menu"] = Menu;
            order = await repo.GetCopy(id, 0);

            order.Invoice = 0;
            orderSes.id = order.OrderId;

            orderSes.OrderType = 0;
            orderSes.act = "Заказ";
            orderSes.Prefix = "Заказ";

            Session["OrdSes"] = orderSes;
            return RedirectToAction("Booking");
        }
        public async Task<ActionResult> Booking()
        {            
            orderSes = (OrderSes)Session["OrdSes"];

            order = await repo.GetChange(orderSes.id);
            Session["Products"] = order.Products;
            ViewBag.Order = orderSes.act;
            if (orderSes.OrderType==0)
                ViewBag.Order = orderSes.act + " №  " + orderSes.id.ToString();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Booking(OrderView ord, string Gd, string Adr, string Prs, string Trans)
        {
            string email = User.Identity.GetUserName();
            List<OrderProductView> products = (List<OrderProductView>)Session["Products"];
            orderSes = (OrderSes)Session["OrdSes"];
            if (ModelState.IsValid && Gd == null && Adr == null && Prs == null && Trans==null && ord.StatusId == 0)
            {
                //Значит именно сохроанение
                if (products.Any())
                {
                    ord.StatusId = 1;
                    await repo.Save(ord, products, email);
                    order = await repo.GetChange(ord.OrderId);
                    order.Products = products;
                    ViewBag.Order = orderSes.Prefix + " отправлен";
                    if (orderSes.OrderType == 0)
                        ViewBag.Order = "Заказ №  " + orderSes.id.ToString() + " отправлен";

                    if (orderSes.OrderType == 1)
                        ViewBag.Order = "Счет №  " + orderSes.id.ToString() + " отправлен";

                    return View("Saved", order);
                }
                else
                {
                    ord.StatusId = 9;
                    await repo.Save(ord, products, email);

                    return View("NotSaved", order);
                }
            }

            //Уходя на другой объект, сохранить заполненное!
            //Проверить что все заполнено, потом изменять статус
            await repo.Save(ord, products, email);
            order = await repo.GetChange(ord.OrderId);
            order.Products = products;

            if (Gd != null)
                return RedirectToAction("Index", "Good", new { act = "Products", cont = "Order" });
            if (Adr != null)
                return RedirectToAction("NewAdres");
            if (Prs != null)
                return RedirectToAction("NewContact");
            if (Trans != null)
                return RedirectToAction("Transports");                

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Transports()
        {
            List<Transport> trans =  await db.Transports.Where(t => (Int32)t.CustID == Menu.CustId).OrderByDescending(t => t.Dat).ToListAsync();
            return View(trans);
        }
        public ActionResult CreateTec()
        {
            return View(new OrderDriv());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTec(TrustTecDet det)
        {
            if (ModelState.IsValid)
            {
                orderSes = (OrderSes)Session["OrdSes"];
                OrderDriv drv = new OrderDriv();
                drv.OrderId = orderSes.id;
                drv.Gn = det.Gn;
                drv.TecModel = det.TecModel;
                db.OrderDrivs.Add(drv);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Booking");
        }

        public async Task<ActionResult> NewDriv(string gn)
        {
            if (gn!=null)
            {
                //Session["trans"] = null;
               Transport trans = await db.Transports.Where(t => t.CustID == Menu.CustId).FirstOrDefaultAsync(o => o.Gn == gn);
                orderSes = (OrderSes)Session["OrdSes"];
                OrderDriv drv = new OrderDriv();
                drv.OrderId = orderSes.id;
                drv.Gn = gn;
                drv.TecModel = trans.TecModel;
                db.OrderDrivs.Add(drv);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Booking");
                //View(await db.Transports.Where(t => t.CustID == Menu.CustId).ToListAsync());
        }        

        [HttpPost]
        public async Task<ActionResult> DelTrans(string id)
        {
            orderSes = (OrderSes)Session["OrdSes"];
            OrderDriv drv = await db.OrderDrivs.Where(t=>t.OrderId== orderSes.id).FirstOrDefaultAsync(o => o.Gn == id);
            db.OrderDrivs.Remove(drv);
            await db.SaveChangesAsync();
            return RedirectToAction("Booking");
        }
        public async Task<ActionResult> DelProducts(int id)
        {
            Session["ChProducts"] = id;
            OrderProductView products = await db.OrderProductViews.FindAsync(id);
            return View(products);
        }
        [HttpPost]
        public async Task<ActionResult> DelProducts()
        {
            int id = (int)Session["ChProducts"];
            OrderProduct products = await db.OrderProducts.FindAsync(id);
            db.OrderProducts.Remove(products);
            await db.SaveChangesAsync();
            Session["ChProducts"] = null;
            return RedirectToAction("Booking");
        }

        public ActionResult ChProducts(int id,string act)
        {
            Session["ChProducts"] = id;
            return RedirectToAction("Index", "Good", new { act = "Products", cont = "Order" });
        }

        public async Task<ActionResult> Products(int id)
        {
            Good good = await db.Goods.FindAsync(id);
            OrderProductView svd = new OrderProductView();
            svd.GoodId = good.GoodID;
            svd.Good = good.txt;
            svd.Unit = good.Unit;
            svd.OrderId = id;
            if (Session["ChProducts"] != null)
            {
                int DetId = (int)Session["ChProducts"];
                Session["ChProducts"] = null;
                svd.OrderProductId = DetId;
                
            }
            return View(svd);
        }

        [HttpPost]
        public async Task<ActionResult> Products(OrderProductView svd)
        {
            orderSes = (OrderSes)Session["OrdSes"];
            if (Session["Products"] != null)
            {
                await repo.SaveDetail(orderSes.id, svd);
            }
            return RedirectToAction("Booking", "Order", new { id = orderSes.id });
        }
        
        public ActionResult NewContact()
        {
            Person person = new Person();
            person.CustId = Menu.CustId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> NewContact(Person person)
        {
            try
            {
                person.CustId = Menu.CustId;
                db.Persons.Add(person);
                await db.SaveChangesAsync();

                orderSes = (OrderSes)Session["OrdSes"];
                order = await repo.GetChange(orderSes.id);
                order.PersonId = person.PersonId;
                await repo.Save(order);
                return RedirectToAction("Booking");                
            }
            catch
            {
                return View();
            }
        }
        public ActionResult NewAdres()
        {
            Adres adres = new Adres();
            return View(adres);
        }
        [HttpPost]
        public async Task<ActionResult> NewAdres(Adres adres)
        {
            adres.CustId = Menu.CustId;
            db.Adreses.Add(adres);
            await db.SaveChangesAsync();
            orderSes = (OrderSes)Session["OrdSes"];
            order = await repo.GetChange(orderSes.id);
            order.AdresId = adres.AdresID;
            await repo.Save(order);

            return RedirectToAction("Booking");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
