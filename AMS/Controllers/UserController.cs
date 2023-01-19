using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        AMSModel db = new AMSModel();
        [Authorize(Roles ="SuperAdmin, Admin")]
        public ActionResult Index()
        {
            var Userlist = db.users.ToList();
            return View(Userlist);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult AddUser()
        {
            if (Session["UserMail"] != null)
            {


                return View();
            }
            else
            {

                return RedirectToAction("SessionOut", "Home");
            }
            //var mode = new List<SelectListItem>();
            //{
            //    mode.Add(new SelectListItem { Text = "Admin", Value = "Admin" });
            //    mode.Add(new SelectListItem { Text = "Agent", Value = "Agent" });
            //    mode.Add(new SelectListItem { Text = "Suppliers", Value = "Suppliers" });

            //};
            //ViewBag.Role = mode;
           
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public ActionResult AddUser(user model)
        {
            if (Session["UserMail"] != null)
            {
                var search_user = (from n in db.users where n.Email == model.Email select n).FirstOrDefault();


                if (search_user != null)
                {
                    ViewBag.Notification = "This User Name Already Exist!!";
                    ModelState.Clear();
                    return View();
                }

                else
                {
                    model.Role = "User";
                    model.CretDate = DateTime.Now;
                    model.CretBy = Convert.ToString(Session["UserMail"]); ;
                    model.UpdDate = null;
                    model.UpdBy = null;

                    model.Name = model.Name.ToUpper();
                    model.Email = model.Email.ToLower();


                    db.users.Add(model);
                    db.SaveChanges();


                    ModelState.Clear();

                    return RedirectToAction("Index");
                }


            }
            else
            {

                return RedirectToAction("SessionOut", "Home");
            }

            
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult UserEdt(int ID)
        {
            return View(db.users.Where(x => x.ID.Equals(ID)).FirstOrDefault());
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public ActionResult UserEdt(user model, int ID)
        {
            if (Session["UserMail"] != null)
            {

                try
                {
                    var mod = (from n in db.users where n.ID == ID select n).FirstOrDefault();
                    mod.Name = model.Name;
                    mod.Email = model.Email;
                    mod.Phone = model.Phone;
                    mod.Pass = model.Pass;
                    mod.Role = model.Role;
                    mod.UpdBy = Convert.ToString(Session["UserMail"]);
                    mod.UpdDate = DateTime.Now;


                    db.SaveChanges();

                    return RedirectToAction("Index", "User");

                }
                catch
                {
                    return View();
                }

            }
            else
            {

                return RedirectToAction("SessionOut", "Home");
            }
            
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult UserDlt(int ID)
        {
            return View(db.users.Where(x => x.ID.Equals(ID)).FirstOrDefault());
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public ActionResult UserDlt(user model, int ID)
        {
            try
            {
                model = db.users.Where(x => x.ID.Equals(ID)).FirstOrDefault();
                db.users.Remove(model);
                db.SaveChanges();

                return RedirectToAction("Index", "User");
            }
            catch
            {
                return View();
            }
        }
        
    }
}