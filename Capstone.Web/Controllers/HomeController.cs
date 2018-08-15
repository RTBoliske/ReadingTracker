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

            User user = _db.GetUser(model.Username, model.Password);
            
            if (user == null || user.Password == null)
            {
                ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                result = View("Login", model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Username, true);
                Session["User"] = user;

                if (((User)Session["User"]).RoleID == 2)
                {
                    result = RedirectToAction("ParentActivity", "Home");
                }
                else if (((User)Session["User"]).RoleID == 3)
                {
                    result = RedirectToAction("ChildActivity", "Home"); //unsure if we want/need a second view for child
                }
            }

           

            return result;
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            ActionResult result = null;

            if (!ModelState.IsValid)
            {
                result = View("Register", model);
            }

            PasswordHash ph = new PasswordHash(model.Password);

            User user = new User();
            user.ID = model.ID;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Username = model.Username;
            user.Password = ph.Hash;
            user.FamilyName = model.FamilyName;
            user.FamilyID = model.FamilyID;
            user.Salt = ph.Salt;
            user.RoleID = model.RoleID;

            Family family = new Family();
            family.FamilyName = model.FamilyName;

            int familyID = _db.CreateFamily(family);
            user.FamilyID = familyID;

            user = _db.CreateUser(user);

            // user does not exist or password is wrong
            if (user == null || user.Password == null) //Question 1
            {
                ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                result = View("Register", model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Username, true);
                Session["User"] = user;
            }
            if ((int)Session["UserRole"] == 2)
            {
                result = RedirectToAction("ParentActivity", "Home"); 
            }
            else if (((User)Session["User"]).RoleID == 3)
            {
                result = RedirectToAction("ChildActivity", "Home"); //unsure if we want/need a second view for child
            }

            return result;
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