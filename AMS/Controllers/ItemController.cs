using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        AMSModel db = new AMSModel();
        // GET: Item
        public ActionResult Index()
        {
            var Itemlist = (from n in db.STK_Items orderby n.ID descending select n).ToList(); 
            return View(Itemlist);
        }
        private List<Category> GetCategories()
        {
            List<Category> CatList = db.Categories.ToList();
            return CatList;
        }
        public ActionResult AddItem()
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
        public ActionResult GetCategoryList(int CID)
        {
            List<SubCategory> Subcat = db.SubCategories.Where(x => x.CID == CID).ToList();
            return Json(Subcat, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddItem(STK_Item model)
        {
            model.InsertTime = DateTime.Now;
            model.InsertBy = Convert.ToString(Session["UserMail"]); ;
            model.UpdateTime = null;
            model.UpdateBy = null;



            db.STK_Items.Add(model);
            db.SaveChanges();


            ModelState.Clear();

            return RedirectToAction("Index");
        }
        public ActionResult ItemEdit(int ID)
        {
            var CatList = new List<SelectListItem>();
            var CategoryList = GetCategories();
            foreach (var item in CategoryList)
            {
                CatList.Add(new SelectListItem { Text = item.CatName, Value = item.CID.ToString() });
            }
            ViewBag.CatList = CatList;
            return View(db.STK_Items.Where(x => x.ID.Equals(ID)).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult ItemEdit(STK_Item model, int ID)
        {
            try
            {
                var mod = (from n in db.STK_Items where n.ID == ID select n).FirstOrDefault();
                mod.CID = model.CID;
                mod.SCID = model.SCID;
                mod.ItemName = model.ItemName;
                mod.ItemCode = model.ItemCode;
                mod.Brand = model.Brand;
                mod.Discount = model.Discount;
                mod.DiscountTp = model.DiscountTp;
                mod.MinStock = model.MinStock;
                mod.Remarks = model.Remarks;
                mod.Unit = model.Unit;
                mod.UpdateBy = Convert.ToString(Session["UserMail"]);;
                mod.UpdateTime = DateTime.Now;
                
                db.SaveChanges();

                return RedirectToAction("Index", "Item");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult ItemDelete(int ID)
        {
            return View(db.STK_Items.Where(x => x.ID.Equals(ID)).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult ItemDelete(STK_Item model, int ID)
        {
            try
            {
                model = db.STK_Items.Where(x => x.ID.Equals(ID)).FirstOrDefault();
                db.STK_Items.Remove(model);
                db.SaveChanges();

                return RedirectToAction("Index", "Item");
            }
            catch
            {
                return View();
            }
        }
    }
}