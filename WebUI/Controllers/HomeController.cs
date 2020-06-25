﻿using System.Collections.Generic;
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
using System.Drawing;

namespace WebUI.Controllers
{
    public class HomeController : BaseController
    {
        private int ContractID;
        private CustRepository repo = new CustRepository();
        private IEnumerable<Contract> contracts;
        private Contract contract;

        [Authorize]
        public async Task<ActionResult> Index(int SelectedCustId = -1, int SelectedContractId = -1)
        {

            HttpCookie aspnet = Request.Cookies[".AspNet.ApplicationCookie"];
            HttpCookie token = Request.Cookies["__RequestVerificationToken"];

            if (aspnet == null || token == null)
            {
                //Response.Cookies.Clear();
#region Чистка всех печенек
                if (HttpContext.CurrentHandler != null)
                {
                    int cookieCount = HttpContext.Request.Cookies.Count;

                    for (var i = 0; i < cookieCount; i++)
                    {
                        var cookie = HttpContext.Request.Cookies[i];
                        if (cookie != null)
                        {
                            var expiredCookie = new HttpCookie(cookie.Name)
                            {
                                Expires = DateTime.Now.AddDays(-1),
                                Domain = cookie.Domain
                            };

                            HttpContext.Response.Cookies.Add(expiredCookie);
                        }
                    }

                    HttpContext.Request.Cookies.Clear();
                }
#endregion
                return RedirectToAction("Login", "Account");
            }

            if (abzHash == null)
                return RedirectToAction("Logout", "Account");

            string usr = User.Identity.Name;

            int state = CalcState(SelectedCustId, SelectedContractId);

            switch (state)
            {
                case 3:
                    ContractID = SelectedContractId;
                    break;

                case 4:
                    CustID = SelectedCustId;
                    contract = repo.GetContract(CustID);
                    if (contract == null)
                        ContractID = 0;
                    else
                        ContractID = contract.ContractID;
                    break;

                case 5:
                    //1й вход после логина
                    CustID = repo.GetCustEmail(usr);
                    contract = repo.GetContract(CustID);
                    if (contract == null)
                        ContractID = 0;
                    else
                        ContractID = contract.ContractID;
                    break;
            }
            Cust cust = db.Custs.Find(CustID);            
            try
            {
                abzHash.CustID = CustID;
            }
            catch
            {
                //return Red--irectToAction("Logout", "Account");
            }

            contracts = repo.GetContracts(CustID);

            try
            {
                abzHash.ContractID = ContractID;
            }
            catch
            {
                //return RedirectToAction("Logout", "Account");
            }

            if (abzHash != null)
            {
                UpdateHash(abzHash);
            }
            else 
            {
                //return RedirectToAction("Logout", "Account");
            }

            ViewData["Contract"] = new SelectList(contracts, "ContractID", "Num", ContractID);

            //не верно для админа, однако работает?
            IEnumerable<OrgView> orgView = repo.GetCust(usr);
            ViewData["Cust"] = new SelectList(orgView, "ID", "Txt", CustID);
            ViewBag.MenuItem = "recv";

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



        //public ActionResult MainMenu(string menuItem)
        //{
        //    ViewBag.MenuItem = menuItem;
        //    return PartialView();
        //}

        //public ActionResult HeadMenu()
        //{
        //    BalanceRepository bl = new BalanceRepository();

        //    ViewBag.cust = Cust.SmalName;

        //    ViewBag.sm = bl.GetBalance(CustID, (int)abzHash.ContractID);
        //    contract = db.Contracts.Find((int)abzHash.ContractID);
        //    if (contract != null)
        //        ViewBag.Contract = "Договор № " + contract.Num;

        //    return PartialView();
        //}

        private int CalcState(int SelectedCustId, int SelectedContractId)
        {
            string cst = GetCookie("custid");
            if (!String.IsNullOrWhiteSpace(cst))
                CustID = Convert.ToInt32(cst);
            else
                CustID = 0;

            string dog = GetCookie("contractid");
            if (!String.IsNullOrWhiteSpace(dog))
                ContractID = Convert.ToInt32(dog);
            else
                ContractID = 0;

            int state = 0;
            if (SelectedCustId > 0)
            {
                //Сравнить с куками. Что поменялось.
                if (SelectedCustId == CustID)
                {
                    //Значит поменялся контракт
                    //Кука на cust не нужна
                    if (SelectedContractId == ContractID)
                    {
                        //Значит не поменялся контракт
                        //Кука на dog не нужна
                        //Раз попали сюда, ничего не изменилось
                        state = 2;
                    }
                    else
                    {
                        //Значит поменялся контракт
                        //Кука на dog  нужна
                        state = 3;
                    }
                }
                else
                {
                    //Значит поменялся cust
                    //Кука на cust  нужна
                    //Получить новый список контрактов
                    //Кука на dog  нужна
                    state = 4;
                }
            }
            else
            {
                if (CustID == 0)
                {
                    //1й вход после логина
                    //Получить заказчика.
                    //Кука на cust  нужна
                    //Получить контракт.
                    //Кука на dog  нужна
                    state = 5;
                }
                else
                {
                    if (CustID > 0 && ContractID > 0)
                    {
                        //Работаем по кукам
                        state = 6;
                    }
                }
            }
            return state;
        }
    }
}