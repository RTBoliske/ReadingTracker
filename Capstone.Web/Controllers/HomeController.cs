using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {

        private IDatabaseSvc _db = null;
        public HomeController(IDatabaseSvc db)
        {
            _db = db;
        }

        // GET: Home
        public ActionResult Index()
        {
            var user = _db.GetAllUsers();
            
            return View("Index", user);
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            Users user = _db.GetUser(model.Username, model.Password);
            // user does not exist or password is wrong
            if (user == null)
            {
                ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                return View("Login", model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Username, true);
                Session["UserRole"] = user.RoleID; //not properly loading up user to current Session
            }
            if ((int)Session["UserRole"] == 2)
            {
                return RedirectToAction("ParentActivity", "Home"); //need to create this view
            }
            else
            {
                return RedirectToAction("ChildActivity", "Home"); //unsure if we want/need a second view for child
            }
        }

        public ActionResult ParentActivity()
        {
            return View("ParentActivity");
        }

        public ActionResult ChildActivity()
        {
            return View("ChildActivity");
        }
    }
}