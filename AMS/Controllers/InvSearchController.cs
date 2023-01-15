using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class InvSearchController : Controller
    {
        AMSModel db = new AMSModel();
        // GET: Purchase
        public ActionResult Index(string Invid)
        {
            return View();
        }



        public JsonResult EditSave(int ID, string ITEMSL, string SIZE, string COLOR, int QTY, int RATE, int AMOUNT, string INV)
        {
            var mod = (from n in db.STK_Trans where n.ID == ID select n).FirstOrDefault();
            mod.ITEMSL = ITEMSL;
            mod.SIZE = SIZE;
            mod.COLOR = COLOR;
            mod.QTY = QTY;
            mod.RATE = RATE;
            mod.AMOUNT = AMOUNT;
            mod.UpdateBy = Convert.ToString(Session["UserMail"]); ;
            mod.UpdateDate = DateTime.Now;
            db.SaveChanges();

            var totalamount = (from n in db.STK_Trans where n.TRANSNO == INV select n.AMOUNT).Sum();
            var master = (from n in db.STK_TRANSMSTs where n.TransNo == INV select n).FirstOrDefault();
            master.TotalAmount = totalamount;
            master.UpdateBy = Convert.ToString(Session["UserMail"]); ;
            master.UpdateDate = DateTime.Now;
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);

        }


        public ActionResult InvoiceSearch(string date)
        {

            var datetime = Convert.ToDateTime(date);
            var inv = (from u in db.STK_Trans
                       where u.TRANSDT == datetime
                       select u.TRANSNO).Distinct();

            return Json(inv, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPurchaseRecord(string Invid)
        {

            List<STK_Trans> List = db.STK_Trans.Where(x => x.TRANSNO == Invid).ToList();
            return Json(List, JsonRequestBehavior.AllowGet);
        }

    }
}