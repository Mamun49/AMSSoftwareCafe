using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.Security;

namespace AMS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            AMSModel db = new AMSModel();
            int totalitem = (from n in db.STK_Stocks
                             where n.StockQty > 5
                             select n.ItemID).Count();
            ViewBag.totaliteminred = totalitem;
            int itemcount = (from n in db.STK_Items select n.ID).Count();
            ViewBag.ItemCount = itemcount;
            int issueitem = (from n in db.STK_Trans where n.TRANSTP == "Issue" select n.ID).Count();
            ViewBag.IssueItem = issueitem;
            int sumissue = (from n in db.STK_Trans where n.TRANSTP == "Issue" select n.AMOUNT).Sum();
            ViewBag.SumIssue = sumissue;
            var itemresult = (from stock in db.STK_Stocks
                          where stock.StockQty >5
                          select new { stock.ItemID, stock.Size, stock.Color }).ToList();
            ViewBag.warningitem = itemresult;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Maintainence()
        {
            if (Session["UserMail"] != null)
            {


                return View();
            }
            else
            {

                return RedirectToAction("SessionOut", "Home");
            }
        }

        public ActionResult Test()
        {
            

            return View();
        }public ActionResult SessionOut()
        {
            

            return View();
        }
    }
}