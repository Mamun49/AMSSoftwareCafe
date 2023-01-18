using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    [Authorize]
    public class PartySearchController : Controller
    {
        AMSModel db = new AMSModel();
        // GET: PartySearch
        public ActionResult Index()
        {
            var StrList = new List<SelectListItem>();
            var strList = GetPartyList();
            foreach (var item in strList)
            {
                StrList.Add(new SelectListItem { Text = item.ID.ToString() + " | " + item.PSName, Value = item.ID.ToString() });
            }
            ViewBag.PartyList = StrList;
            return View();

        }
        private List<PS_tbl> GetPartyList()
        {
            List<PS_tbl> StrList = db.PS_Tbls.ToList();

            return StrList;
        }
        public ActionResult GetPartyLedger(ledgerModel model)
        {
            var stid = model.storeid.ToString();
            var suppname = (from n in db.PS_Tbls where n.ID == model.PSid select n).FirstOrDefault();
            var strename = suppname.PSName;
            ViewBag.PartyName = strename;
            var dateTime = DateTime.Now.ToString("M/d/yyyy"); 
            
            ViewBag.Date = dateTime;

            var datefrom = Convert.ToDateTime(model.datefrom);
            var dateto = Convert.ToDateTime(model.dateto);
            var data = (from u in db.STK_Trans
                        where 
                       u.PSID == model.PSid &&
                      (u.TRANSDT >= datefrom &&
                       u.TRANSDT <= dateto)
                        select u).ToList();
                       

            ViewBag.Data = data;
            return View();
        }
    }
}