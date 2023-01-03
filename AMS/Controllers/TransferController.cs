﻿using AMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class TransferController : Controller
    {
        AMSModel db = new AMSModel();
        // GET: Transfer
        public ActionResult Index()
        {
            var Random = new Random();
            var num = Random.Next(0, 10000);
            var memo = "Inv" +"-"+ Convert.ToString(num) + "/"+DateTime.Now.Year;
            ViewBag.Memo = memo;
            var StoreList = new List<SelectListItem>();
            var storeList = GetStoreList();
            foreach (var item in storeList)
            {
                StoreList.Add(new SelectListItem { Text = item.StoreID.ToString() + " | " + item.StoreName  , Value = item.StoreID.ToString() });
            }
            ViewBag.StoreList = StoreList;
            var SuppList = new List<SelectListItem>();
            var suppList = GetSuppList();
            foreach (var item in suppList)
            {
                SuppList.Add(new SelectListItem { Text = item.PSName   , Value = item.ID.ToString() });
            }
            ViewBag.SuppList = SuppList;
            var CatList = new List<SelectListItem>();
            var CategoryList = GetCategories();
            foreach (var item in CategoryList)
            {
                CatList.Add(new SelectListItem { Text = item.CatName, Value = item.CID.ToString() });
            }
            ViewBag.CatList = CatList;
            return View();
        }
        private List<Store_tbl> GetStoreList()
        {
            List<Store_tbl> StoreList = db.Store_tbls.ToList();

            return StoreList;
        }
        private List<PS_tbl> GetSuppList()
        {
            List<PS_tbl> SuppList = db.PS_Tbls.ToList();

            return SuppList;
        }
        private List<Category> GetCategories()
        {
            List<Category> CatList = db.Categories.ToList();
            return CatList;
        }
        public ActionResult GetCategoryList(int CID)
        {
            List<SubCategory> Subcat = db.SubCategories.Where(x => x.CID == CID).ToList();
            return Json(Subcat, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult SaveProduct(string TRANSDT, string TRANSNO, string STORETO, int PSID, string TRANSYY, int TotalAmount, STK_Trans[]  order)
        {
            string result = "Error! Order Is Not Complete!";
          
            foreach (var item in order)
            {
                STK_Trans obj = new STK_Trans();

                obj.TRANSDT = Convert.ToDateTime(TRANSDT);
                obj.TRANSYY = TRANSYY;
                obj.TRANSNO = TRANSNO;
                obj.STORETO = STORETO;
                obj.PSID = PSID;
                obj.TRANSTP = "Purchase";
                obj.InsBy = "admin";
                obj.InsDate = DateTime.Now;
                obj.ITEMSL = item.ITEMSL;
                obj.SIZE = item.SIZE;
                obj.COLOR = item.COLOR;
                obj.QTY = item.QTY;
                obj.RATE = item.RATE;
                obj.AMOUNT = item.AMOUNT;

                db.STK_Trans.Add(obj);
               
            }
           

            STK_TRANSMST add = new STK_TRANSMST();
            add.InsBy = "admin";
            add.InsDate = DateTime.Now;
            add.PSID = PSID;
            add.StoreTo = STORETO;
            add.TransNo = TRANSNO;
            add.TransTP = "Purchase";
            add.TransYear = TRANSYY;
            add.TransDate= Convert.ToDateTime(TRANSDT);
            add.TotalAmount = TotalAmount;

            db.STK_TRANSMSTs.Add(add);
            db.SaveChanges();


            result = "Success! Order Is Complete!";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
    }
}