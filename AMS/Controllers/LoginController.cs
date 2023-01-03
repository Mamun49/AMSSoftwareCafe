using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AMS.Controllers
{
   
    public class LoginController : Controller
    {
        // GET: Login
        AMSModel  db = new AMSModel();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(user model)
        {
            //HttpCookie cookie = new HttpCookie("softcafe");
            //if (model.Rememberme == true)
            //{
            //    cookie["mail"] = model.Email;
            //    cookie["password"] = model.Pass;
            //    cookie.Expires = DateTime.Now.AddDays(6);
            //    HttpContext.Response.Cookies.Add(cookie);
            //}
            //else
            //{
            //    cookie.Expires = DateTime.Now.AddDays(-1);
            //    HttpContext.Response.Cookies.Add(cookie);
            //}

            var checkLogin = db.users.Where(x => x.Email.Equals(model.Email) && x.Pass.Equals(model.Pass)).FirstOrDefault();
            if (checkLogin != null)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);

                Session["UserMail"] = model.Email.ToString();
                Session["UserName"] = checkLogin.Name.ToString();
                //Session["Emp_ID"] = Convert.ToInt64(checkLogin.Emp_ID);
                ModelState.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.Clear();
                ViewBag.Notification = "Wrong Username or password";
            }

            return View();

        }
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}