using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class RoleManagerController : Controller
    {
        AMSModel db = new AMSModel();
        // GET: RoleManager
        [Authorize]
        public ActionResult Index()
        {
            var ursid = Convert.ToString(Session["Usr_ID"]);
            var rolename = (from n in db.UserRoles
                            where n.UserID.ToString() == ursid
                            select n.Role).FirstOrDefault();
            ViewBag.RoleName = rolename;

            var UserList = new List<SelectListItem>();
            var userList = GetUserList();
            foreach (var item in userList)
            {
                UserList.Add(new SelectListItem { Text = item.Name + " | " + item.Email, Value = item.ID.ToString() });
            }
            ViewBag.UserList = UserList;
            var mode = new List<SelectListItem>();
            {
                mode.Add(new SelectListItem { Text = "Admin", Value = "Admin" });
                mode.Add(new SelectListItem { Text = "Suppliers", Value = "Suppliers" });
                mode.Add(new SelectListItem { Text = "Agent", Value = "Agent" });
                //mode.Add(new SelectListItem { Text = "Bank", Value = "Bank" });
            };
            ViewBag.Role = mode;
            var adminmode = new List<SelectListItem>();
            {
                //adminmode.Add(new SelectListItem { Text = "Admin", Value = "Admin" });
                adminmode.Add(new SelectListItem { Text = "Suppliers", Value = "Suppliers" });
                adminmode.Add(new SelectListItem { Text = "Agent", Value = "Agent" });
                //mode.Add(new SelectListItem { Text = "Bank", Value = "Bank" });
            };
            ViewBag.adminRole = adminmode;
            return View();
        }
        private List<user> GetUserList()
        {
            List<user> UserList = db.users.ToList();

            return UserList;
        }
        public ActionResult GetRoleList(int ID)
        {
            List<UserRole> RoleList = db.UserRoles.Where(x => x.UserID == ID).ToList();
            return Json(RoleList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Index(UserRole model)
        {
            var search_role = (from n in db.UserRoles where n.Role == model.Role && n.UserID== model.UserID select n).FirstOrDefault();
            if (search_role != null)
            {
                ViewBag.notification = "This User in this role Already Exist!!";
                ModelState.Clear();
                return View("Index","RoleManager");
            }
            else
            {
                var mod = (from n in db.users where n.ID == model.UserID select n).FirstOrDefault();
                mod.Role = model.Role;
                
                db.UserRoles.Add(model);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index","RoleManager");
            }
        }
    }
}