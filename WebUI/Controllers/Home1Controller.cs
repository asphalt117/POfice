using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using WebUI.Models;
using Domain.Entities;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private int CustID;
        private int ContractID;

        [Authorize]
        //public ActionResult Index()
        public async Task<ActionResult> Index(int SelectedId = 0, int SelectedContractId = -1)

        {
            MyMenu menu;
            Cust cust = new Cust();
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();
            ApplicationSignInManager SignInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            ApplicationUser user = await userManager.FindByEmailAsync(User.Identity.Name);

            /// 1й вход 
            if (Session["Menu"] == null)
            {
                if (user != null)
                    roles = await userManager.GetRolesAsync(user.Id);
                CustID = user.CustID;
                ContractID = -1;
            }


            return RedirectToAction("Index", "Custs");
        }
    }
}