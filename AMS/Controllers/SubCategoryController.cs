using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class SubCategoryController : Controller
    {
        AMSModel db = new AMSModel();
        // GET: SubCategory
        public ActionResult Index()
        {
            var CatList = new List<SelectListItem>();
            var CategoryList = GetCategories();
            foreach (var item in CategoryList)
            {
                CatList.Add(new SelectListItem { Text = item.CatName, Value = item.CID.ToString() });
            }
            ViewBag.CatList = CatList;
            return View();
        }
        private List<Category> GetCategories()
        {
            List<Category> CatList = db.Categories.ToList();
            return CatList;
        }
        [HttpPost]
        public ActionResult Index(SubCategory model)
        {
            var search_cat = (from n in db.SubCategories where n.SubCatName == model.SubCatName select n).FirstOrDefault();
            if (search_cat != null)
            {
                ViewBag.notification = "This Sub Category Already Exist!!";
                ModelState.Clear();
                return View();
            }
            else
            {
                db.SubCategories.Add(model);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
            }
        }
        public ActionResult GetCategoryList(int CID)
        {
            List<SubCategory> Subcat = db.SubCategories.Where(x => x.CID == CID).ToList();
            return Json(Subcat, JsonRequestBehavior.AllowGet);   
        }
    }
}