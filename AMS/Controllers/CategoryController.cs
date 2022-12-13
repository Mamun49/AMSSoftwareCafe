using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class CategoryController : Controller
    {
        AMSModel db = new AMSModel();
        // GET: Category
        public ActionResult Index()
        {
            List<Category> CatList = db.Categories.ToList();
            ViewBag.CatList = CatList;
            return View();
        }
        

        [HttpPost]
        public ActionResult Index(Category model)
        {
            var search_cat = (from n in db.Categories where n.CatName == model.CatName select n).FirstOrDefault();
            if (search_cat != null)
            {
                ViewBag.notification = "Already Exist!!";
                ModelState.Clear();
                return View();
            }
            else
            {
                db.Categories.Add(model);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
            }

        }
        public ActionResult CatEdit(int CID)
        {
            return View(db.Categories.Where(x => x.CID.Equals(CID)).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult CatEdit(int CID, Category model)
        {
            try
            {
                var mod = (from n in db.Categories where n.CID == CID select n).FirstOrDefault();
                mod.CatName = model.CatName;
                db.SaveChanges();

                return RedirectToAction("Index", "Category");

            }
            catch
            {
                return View();
            }
        }
    public ActionResult CatDelete(int CID)
        {
            return View(db.Categories.Where(x => x.CID.Equals(CID)).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult CatDelete(int CID, Category model)
        {
            try
            {
                model = db.Categories.Where(x => x.CID.Equals(CID)).FirstOrDefault();
                db.Categories.Remove(model);
                db.SaveChanges();

                return RedirectToAction("Index", "Category");
            }
            catch
            {
                return View();
            }
        }

    }
}