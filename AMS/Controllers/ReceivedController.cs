using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    [Authorize]
    public class ReceivedController : Controller
    {
        AMSModel db = new AMSModel();
        // GET: Received
        public ActionResult Index()
        {
            var Random = new Random();
            var num = Random.Next(0, 10000);
            var memo = "Inv" + "-" + Convert.ToString(num) + "/RCV-" + DateTime.Now.Year;
            ViewBag.Memo = memo;
            var StoreList = new List<SelectListItem>();
            var storeList = GetStoreList();
            foreach (var item in storeList)
            {
                StoreList.Add(new SelectListItem { Text = item.StoreID.ToString() + " | " + item.StoreName, Value = item.StoreID.ToString() });
            }
            ViewBag.StoreList = StoreList;

            var ItemList = new List<SelectListItem>();
            var itemList = GetItemList();
            foreach (var item in itemList)
            {
                ItemList.Add(new SelectListItem { Text = +item.ID + " | " + item.ItemCode.ToString() + " | " + item.ItemName, Value = item.ID.ToString() });
            }
            ViewBag.ItemList = ItemList;

            var SuppList = new List<SelectListItem>();
            var suppList = GetSuppList();
            foreach (var item in suppList)
            {
                SuppList.Add(new SelectListItem { Text = item.PSName, Value = item.ID.ToString() });
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
        private List<STK_Item> GetItemList()
        {
            List<STK_Item> ItemList = db.STK_Items.ToList();

            return ItemList;
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
        public ActionResult SaveProduct(string TRANSDT, string TRANSNO, string STOREFR, string STORETO, string TRANSYY, int TotalAmount, STK_Trans[] order)
        {
            string result = "Error! Order Is Not Complete!";

            foreach (var item in order)
            {
                STK_Trans obj = new STK_Trans();
                var Itemname = (from n in db.STK_Items where n.ID == item.ITEMID select n.ItemName).FirstOrDefault();
                obj.TRANSDT = Convert.ToDateTime(TRANSDT);
                obj.TRANSYY = TRANSYY;
                obj.TRANSNO = TRANSNO;
                obj.STORETO = STORETO;
                obj.STOREFR = STOREFR;
                obj.TRANSTP = "Recevied";
                obj.InsBy = Convert.ToString(Session["UserMail"]); ;
                obj.InsDate = DateTime.Now;
                obj.ITEMSL = Itemname;
                obj.ITEMID = item.ITEMID;
                obj.SIZE = item.SIZE;
                obj.COLOR = item.COLOR;
                obj.QTY = item.QTY;
                obj.RATE = item.RATE;
                obj.AMOUNT = item.AMOUNT;

                db.STK_Trans.Add(obj);

            }


            STK_TRANSMST add = new STK_TRANSMST();
            add.InsBy = Convert.ToString(Session["UserMail"]); ;
            add.InsDate = DateTime.Now;
            add.StoreFrom = STOREFR;
            add.StoreTo = STORETO;
            add.TransNo = TRANSNO;
            add.TransTP = "Recevied";
            add.TransYear = TRANSYY;
            add.TransDate = Convert.ToDateTime(TRANSDT);
            add.TotalAmount = TotalAmount;

            db.STK_TRANSMSTs.Add(add);
            db.SaveChanges();


            result = "Success! Order Is Complete!";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
    }
}