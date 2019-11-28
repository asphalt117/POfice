using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
namespace WebUI.Controllers
{
    public class SaleController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            List<GraphSale> graph = await db.GraphSales.Where(p => p.CustId== Cust.CustId).ToListAsync();          
                
            return View(graph);
        }
        public async Task<ActionResult> Details(int id)
        {
            List<Sale> sale = await db.Sales.Where(p => p.GraphSaleId == id).ToListAsync();
            return View(sale);
        }
        public async Task<ActionResult> Loadering()
        {
            
            List<Sale> sale = await db.Sales.Where(p => p.CustId == Cust.CustId && p.Status==2).ToListAsync();
            return View( sale);
        }
    }
}