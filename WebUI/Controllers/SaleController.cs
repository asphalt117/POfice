﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using WebUI.Filter;

namespace WebUI.Controllers
{
    [MyAuthAttribute]
    public class SaleController : BaseController
    {
        //[Authorize]
        public async Task<ActionResult> Index()
        {
            ViewBag.MenuItem = "sale";
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
            ViewBag.MenuItem = "load";
            List<Sale> sale = await db.Sales.Where(p => p.CustId == Cust.CustId && p.Status==1).ToListAsync();
            return View( sale);
        }
    }
}