using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class StoreController : Controller
    {
        AMSModel db = new AMSModel();
        // GET: Store
        public ActionResult Index()
        {
            var Storelist = (from n in db.Store_tbls orderby n.StID descending select n).ToList();
            return View(Storelist);
        }
        public ActionResult AddStore()
        {
            var userList = new List<SelectListItem>();
            var UserList = GetUserList();
            foreach (var item in UserList)
            {
                userList.Add(new SelectListItem { Text = item.ID.ToString() + " | " + item.Name + " | " + item.Email, Value = item.ID.ToString() });
            }
            ViewBag.CusList = userList;
            return View();
        }
        private List<user> GetUserList()
        {
            List<user> userList = db.users.Where(x=>x.Role=="Agent").ToList();
            
            return userList;
        }
        [HttpPost]
        public ActionResult AddStore(Store_tbl model)
        {
            model.InsTime = DateTime.Now;
            model.InsBy = "Admin";
            model.UpdateTime = null;
            model.UpdateBy = null;

            db.Store_tbls.Add(model);
            db.SaveChanges();
             ModelState.Clear();

            return RedirectToAction("Index");
        }
        public ActionResult StoreEdit(int StID)
        {
            return View(db.Store_tbls.Where(x => x.StID.Equals(StID)).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult StoreEdit(int StID, Store_tbl model)
        {
            try
            {
                var mod = (from n in db.Store_tbls where n.StID == StID select n).FirstOrDefault();
                mod.StoreName = model.StoreName;
                mod.StoreID = model.StoreID;
                mod.AgentID = model.AgentID;
                mod.Remarks = model.Remarks;
                
                mod.UpdateBy = "Agent";
                mod.UpdateTime = DateTime.Now;

                db.SaveChanges();

                return RedirectToAction("Index", "Store");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult StoreDelete(int StID)
        {
            return View(db.Store_tbls.Where(x => x.StID.Equals(StID)).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult StoreDelete(int StID, Store_tbl model)
        {
            try
            {
                model = db.Store_tbls.Where(x => x.StID.Equals(StID)).FirstOrDefault();
                db.Store_tbls.Remove(model);
                db.SaveChanges();

                return RedirectToAction("Index", "Store");
            }
            catch
            {
                return View();
            }
        }
    }
}