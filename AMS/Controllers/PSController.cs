using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class PSController : Controller
    {
        AMSModel db = new AMSModel();
        // GET: PS
        public ActionResult Index()
        {
            var mode = new List<SelectListItem>();
            {
                mode.Add(new SelectListItem { Text = "Suppliers", Value = "Suppliers" });
                mode.Add(new SelectListItem { Text = "Party", Value = "Party" });
               
            };
            ViewBag.list = mode;
            List<PS_tbl> PSList = db.PS_Tbls.ToList();
            ViewBag.PSList = PSList;
            return View();
        }
        [HttpPost]
        public ActionResult Index(PS_tbl model)
        {
           
                
                db.PS_Tbls.Add(model);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
            
        }
        public ActionResult PSEdit(int ID)
        {
            var mode = new List<SelectListItem>();
            {
                mode.Add(new SelectListItem { Text = "Suppliers", Value = "Suppliers" });
                mode.Add(new SelectListItem { Text = "Party", Value = "Party" });

            };
            ViewBag.list = mode;
            return View(db.PS_Tbls.Where(x => x.ID.Equals(ID)).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult PSEdit(int ID, PS_tbl model)
        {
            try
            {
                var mod = (from n in db.PS_Tbls where n.ID == ID select n).FirstOrDefault();
                mod.PSName = model.PSName;
                mod.Mobile = model.Mobile;
                mod.Address = model.Address;
                mod.Remarks = model.Remarks;
                mod.Type = model.Type;
                db.SaveChanges();

                return RedirectToAction("Index", "PS");

            }
            catch
            {
                return View();
            }

        }
        public ActionResult PSDelete(int ID)
        {
            return View(db.PS_Tbls.Where(x => x.ID.Equals(ID)).FirstOrDefault());

        }
        [HttpPost]
        public ActionResult PSDelete(int ID, PS_tbl model)
        {
            try
            {
                model = db.PS_Tbls.Where(x => x.ID.Equals(ID)).FirstOrDefault();
                db.PS_Tbls.Remove(model);
                db.SaveChanges();

                return RedirectToAction("Index", "PS");
            }
            catch
            {
                return View();
            }
        }
    }
}