using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    [Authorize]
    public class StoreSearchController : Controller
    {
        AMSModel db = new AMSModel();
        // GET: StoreSearch
        public ActionResult Index()
        {
            var StrList = new List<SelectListItem>();
            var strList = GetStoreList();
            foreach (var item in strList)
            {
                StrList.Add(new SelectListItem { Text = item.StoreID.ToString() + " | " + item.StoreName, Value = item.StoreID.ToString() });
            }
            ViewBag.StoreList = StrList;
            return View();
        }
        private List<Store_tbl> GetStoreList()
        {
            List<Store_tbl> StrList = db.Store_tbls.ToList();

            return StrList;
        }
        public ActionResult GetStoreLedger(ledgerModel model)
        {
            var stid = model.storeid.ToString();
            var storename = (from n in db.Store_tbls where n.StoreID == model.storeid select n).FirstOrDefault();
            var strename = storename.StoreName;
            ViewBag.StoreName = strename;
            var strCode = storename.StoreID;
            ViewBag.StoreCode = strCode;
            var dateTime = DateTime.Now.ToString("M/d/yyyy");
            ViewBag.Date = dateTime;

            var datefrom = Convert.ToDateTime(model.datefrom);
            var dateto = Convert.ToDateTime(model.dateto);
            var data = (from u in db.STK_Trans
                        where u.TRANSTP == "Recevied" &&
                       u.STORETO == stid &&
                      (u.TRANSDT >= datefrom &&
                       u.TRANSDT <= dateto)
                        select u)
                       .Union(from u in db.STK_Trans
                              where u.TRANSTP == "Transfer" &&
                             u.STOREFR == stid &&
                            (u.TRANSDT >= datefrom &&
                             u.TRANSDT <= dateto)
                              select u)
                        .Union(from u in db.STK_Trans
                               where u.TRANSTP == "Sale" &&
                              u.STOREFR == stid &&
                             (u.TRANSDT >= datefrom &&
                              u.TRANSDT <= dateto)
                               select u).ToList();
                         
            ViewBag.Data = data;
            return View();
        }
    }
}