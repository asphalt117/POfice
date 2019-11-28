using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Repository;
using Domain.ModelView;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebUI.Models;
using System;

namespace WebUI.Controllers
{
    public class CustsController : Controller
    {
        private AbzContext db = new AbzContext();
        private int CustID;
        private int ContractID;
        private CustRepository repo = new CustRepository();
        private Cust cust;
        private IEnumerable<Contract> contracts;

        private MyMenu ChangeControl(MyMenu menu,int custid,int contractid)
        {
            Contract _cont;
            CustID = custid;
            cust = db.Custs.Find(custid);
            contracts = repo.GetContracts(CustID);
            menu.CustId = CustID;
            menu.Cust = cust.SmalName;
            if (contractid == 0)
            {
                //Новый вход или котрагент
                _cont = repo.GetContract(CustID);
                if (_cont == null)
                    ContractID = 0;
                else
                    ContractID = _cont.ContractID;

                menu.ContractId = ContractID;
            }
            else
            {
                ContractID = contractid;
                menu.ContractId = ContractID;
            }

            return menu;
        }
        public ActionResult Index(int SelectedCustId = 0, int SelectedContractId = -1)
        {
            //Определить контрагента и договор            
            string qq = User.Identity.Name;
            string cst = GetCookie("Cust");
            string dog = GetCookie("Dog");

            if (!String.IsNullOrWhiteSpace(cst) && !String.IsNullOrWhiteSpace(dog))
            {
                //Назначаем и быбираем контрагента и договор   
                CustID = Convert.ToInt32(cst);
                ContractID = Convert.ToInt32(dog);
            }
            else
            {
                //Получить данные по юзеру
                string usr = GetCookie("MyAuth");

                //cust = await db.Custs.FindAsync(user.CustID);
            }


            MyMenu menu = (MyMenu)Session["Menu"];
            if (SelectedContractId > 0 && menu.ContractId!= SelectedContractId)
            {
                menu = ChangeControl(menu, SelectedCustId, SelectedContractId);
                ViewData["Contract"] = new SelectList(contracts, "ContractID", "Num", ContractID);
            }
            else
            {
                if (SelectedCustId>0 &&  SelectedCustId != menu.CustId)
                    menu = ChangeControl(menu, SelectedCustId, 0);
                else 
                    menu = ChangeControl(menu, menu.CustId, 0);

                if (ContractID > 0)
                    ViewData["Contract"] = new SelectList(contracts, "ContractID", "Num", ContractID);
                else
                    ViewData["Contract"] = null;
            }
            BalanceRepository bl = new BalanceRepository();
            menu.sm = bl.GetBalance(CustID, ContractID);

            menu.ChangeSelected(1, 1);
            Session["Menu"] = menu;

            //не верно для админа, однако работает?
            IEnumerable<OrgView> orgView = repo.GetCust(menu.UserId); 
            ViewData["Cust"] = new SelectList(orgView, "ID", "Txt", CustID);

            return View("Index", cust);
        }
 
        public ActionResult HeadMenu()
        {
            MyMenu menu = (MyMenu)Session["Menu"];
            if (menu != null)
            {
                Contract contract;
               
                contract = db.Contracts.Find(menu.ContractId);
                
                ViewBag.cust = menu.Cust;
                ViewBag.sm = menu.sm;
                if (contract != null)
                    ViewBag.Contract = "Договор № " +contract.Num;
            }
            return PartialView();
        }

        public ActionResult MainMenu(string menuItem)
        {
            MyMenu menu;
            menu = (MyMenu)Session["Menu"];

            if (menu != null)
            {
                ViewBag.cust = menu.Cust;
                ViewBag.sm = menu.sm;
            }
            else
            {
                ViewBag.cust = "";
                menu = new MyMenu(null, null);
                Session["Menu"] = menu;
            }
            IEnumerable<Menu> SubMenu = null;
            foreach (var mn in menu.NavMenu)
            {
                if (mn.Selected)
                {
                    SubMenu = mn.SubMenus;
                }
            }

            menu.SubMenu = SubMenu;

            return PartialView(menu);
        }


        public string GetCookie(string cookieName)
        {
            string cookieValue = "";
            HttpCookie cookie = Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookieValue = cookie.Value;
            }
            return cookieValue;
        }

        public void SetCookie(string cookieName, string cookieValue)
        {
            string StrValue = cookieValue;
            Response.Cookies[cookieName].Value = StrValue;
            Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(2);
        }

        public void DeleteCookie(string cookieName)
        {
            Response.Cookies[cookieName].Value = string.Empty;
            Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1);
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