//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Threading.Tasks;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using Domain.Entities;
//using Domain.Repository;
//using Domain.ModelView;
//using WebUI.Models;
//using System;
//using System.Drawing;
//using System.Collections;
//using System.Linq;

//namespace WebUI.Controllers
//{
//    public class HomeController : BaseController
//    {
//        private int ContractID;
//        private CustRepository repo = new CustRepository();
//        private IEnumerable<Contract> contracts;
//        private Contract contract;

//      //  [Authorize]
//        public async Task<ActionResult> Index(int SelectedCustId = -1, int SelectedContractId = -1)
//        {
//           string usr = "Admin";

//           ContractID = 138;
//           CustID = repo.GetCustEmail(usr);
//           Cust cust = db.Custs.Find(CustID);
//            abzHash = new AbzHash();
//           abzHash.CustID = CustID;
//           contracts = repo.GetContracts(CustID);
//           abzHash.ContractID = ContractID;

//            ViewData["Contract"] = new SelectList(contracts, "ContractID", "Num", ContractID);

//            //не верно для админа, однако работает?
//            IEnumerable<OrgView> orgView = repo.GetCust(usr);
//            ViewData["Cust"] = new SelectList(orgView, "ID", "Txt", CustID);
//            ViewBag.MenuItem = "recv";

//            BalanceRepository bl = new BalanceRepository();
//            ViewBag.customer = cust.SmalName;
//            ViewBag.balance = bl.GetBalance(CustID, ContractID).ToString();
//            ViewBag.contractn = "";
//            Contract contractcc = db.Contracts.Find(ContractID);
//            if (contractcc != null)
//            {
//                ViewBag.contractn = "Договор № " + contractcc.Num;
//            }

//            SetCookie("custid", CustID.ToString());
//            SetCookie("contractid", ContractID.ToString());
//            SetCookie("customer", ViewBag.customer);
//            SetCookie("balance", ViewBag.balance);
//            SetCookie("contract", ViewBag.contractn);

//            return View("Index", cust);
//        }
    
   
//    }
//}





using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Repository;
using Domain.ModelView;
using System;
using Domain.Engine;
using WebUI.Filter;
using Domain.Entities.Account;

namespace WebUI.Controllers
{
    public class HomeController : BaseController
    {
        private int ContractID;
        private CustRepository repo = new CustRepository();
        private IEnumerable<Contract> contracts;
        private Contract contract;
        private AbzHashRepo hashRepo = new AbzHashRepo();
        private AbzContext db = new AbzContext();

        [MyAuthAttribute]
        public ActionResult Index(int SelectedCustId = -1, int SelectedContractId = -1)
        {
            string auth = GetCookie("Auth");
            AbzHash abzHash = hashRepo.GetHash(auth);
            string usr = abzHash.Email;
            if ((SelectedCustId > 0) && ((int)abzHash.CustID != SelectedCustId))
            {
                abzHash.CustID = SelectedCustId;
                contract = repo.GetContract((int)abzHash.CustID);
                if (contract == null)
                    ContractID = 0;
                else
                    ContractID = contract.ContractID;
                abzHash.ContractID = ContractID;
                hashRepo.UpdateHash(abzHash);
            }
            else
            {
                if ((SelectedContractId > 0) && ((int)abzHash.ContractID != SelectedContractId))
                {
                    abzHash.ContractID = SelectedContractId;
                    hashRepo.UpdateHash(abzHash);
                }
            }
            contract = repo.GetContract((int)abzHash.CustID);
            if (contract == null)
                ContractID = 0;
            else
                ContractID = contract.ContractID;

            Cust cust = db.Custs.Find((int)abzHash.CustID);
            contracts = repo.GetContracts((int)abzHash.CustID);
            ViewData["Contract"] = new SelectList(contracts, "ContractID", "Num", ContractID);

            IEnumerable<OrgView> orgView = repo.GetCust(usr);
            ViewData["Cust"] = new SelectList(orgView, "ID", "Txt", abzHash.CustID);
            ViewBag.MenuItem = "recv";
            ViewBag.User = usr;


            BalanceRepository bl = new BalanceRepository();

            ViewBag.customer = cust.SmalName;
            ViewBag.balance = bl.GetBalance(CustID, (int)abzHash.ContractID).ToString();
            ViewBag.contractn = "";
            Contract contractcc = db.Contracts.Find((int)abzHash.ContractID);
            if (contractcc != null)
            {
                ViewBag.contractn = "Договор № " + contractcc.Num;
            }

            SetCookie("custid", CustID.ToString());
            SetCookie("contractid", abzHash.ContractID.ToString());
            SetCookie("customer", ViewBag.customer);
            SetCookie("balance", ViewBag.balance);
            SetCookie("contract", ViewBag.contractn);



            return View("Index", cust);
        }
    }
}