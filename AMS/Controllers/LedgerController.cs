using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class LedgerController : Controller
    {
        // GET: Ledger
        AMSModel db = new AMSModel();
        public ActionResult Index()
        {
            var ItemList = new List<SelectListItem>();
            var itemList = GetItemList();
            foreach (var item in itemList)
            {
                ItemList.Add(new SelectListItem { Text = item.ItemCode.ToString() + " | " + item.ItemName , Value = item.ID.ToString() });
            }
            ViewBag.ITMList = ItemList;
            var StrList = new List<SelectListItem>();
            var strList = GetStoreList();
            foreach (var item in strList)
            {
                StrList.Add(new SelectListItem { Text = item.StoreID.ToString() + " | " + item.StoreName , Value = item.StoreID.ToString() });
            }
            ViewBag.StoreList = StrList;
            return View();
        }
        private List<STK_Item> GetItemList()
        {
            List<STK_Item> ItemList = db.STK_Items.ToList();

            return ItemList;
        }
        private List<Store_tbl> GetStoreList()
        {
            List<Store_tbl> StrList = db.Store_tbls.ToList();

            return StrList;
        }

        
        public ActionResult GetSizeList(int Itemid)
        {
            //var Itm = Itemid.ToString();
            //List<STK_Trans> sizedd = db.STK_Trans.Where(x => x.ITEMID == Itemid).ToList();
           var sizedd = (from n in db.STK_Trans where n.ITEMID == Itemid select n.SIZE).Distinct();
            return Json(sizedd, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetColorList(int Itemid)
        {
            //var Itm = Itemid.ToString();
            List<STK_Trans> sizedd = db.STK_Trans.Where(x => x.ITEMID == Itemid).ToList();
            var colordd = (from n in db.STK_Trans where n.ITEMID == Itemid select n.COLOR).Distinct();
            return Json(colordd, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemLedger(ledgerModel model)
        {
            var stid = model.storeid.ToString();
            var storename = (from n in db.Store_tbls where n.StoreID == model.storeid select n).FirstOrDefault();
            var strename = storename.StoreName;
            ViewBag.StoreName = strename;
            var strCode = storename.StoreID;
            ViewBag.StoreCode = strCode;
            var itemlist = (from n in db.STK_Items where n.ID == model.ItemID select n).FirstOrDefault();
            var Itemname = itemlist.ItemName;
            ViewBag.ItemName = Itemname;
            var Itemcode = itemlist.ItemCode;
            ViewBag.ItemCode = Itemcode;

            //var stto = model.ItemID.ToString();
            var datefrom = Convert.ToDateTime(model.datefrom);
            var dateto = Convert.ToDateTime(model.dateto);
            var data = from u in db.STK_Trans
                        where u.ITEMID == model.ItemID &&
                        u.SIZE == model.Size &&
                        u.COLOR == model.Color &&
                        u.TRANSTP != "Issue" &&
                        u.STOREFR == stid &&
                       (u.TRANSDT >= datefrom &&
                        u.TRANSDT <= dateto) select u;
            ViewBag.Data = data;
            return View();
        }
        
    }
}