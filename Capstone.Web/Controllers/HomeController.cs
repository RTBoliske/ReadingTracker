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

        public ActionResult Register()
        {
            return View("Register");
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            ActionResult result = null;

            if (!ModelState.IsValid)
            {
                result = View("Login", model);
            }

            Users user = _db.GetUser(model.Username, model.Password);
            // user does not exist or password is wrong
            if (user == null | user.Password == null)
            {
                ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                result = View("Login", model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Username, true);
                Session["User"] = user; 
            }

            if (((Users)Session["User"]).RoleID == 2)
            {
                result = RedirectToAction("ParentActivity", "Home");
            }
            else if (((Users)Session["User"]).RoleID == 3)
            {
                result = RedirectToAction("ChildActivity", "Home"); //unsure if we want/need a second view for child
            }

            return result;
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            Users user = _db.GetUser(model.Username, model.Password);
            // user does not exist or password is wrong
            if (user == null | user.Password == null)
            {
                ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                return View("Register", model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Username, true);
                Session["User"] = user;
            }
            if ((int)Session["UserRole"] == 2)
            {
                return RedirectToAction("ParentActivity", "Home"); 
            }
            else if (((Users)Session["User"]).RoleID == 3)
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

        public ActionResult AddFamilyMember()
        {
            return View("AddFamilyMember");
        }
    }
}