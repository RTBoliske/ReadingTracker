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
            return View("Index");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Register()
        {
            return View("Register");
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

        public ActionResult AddBook()
        {
            return View("AddBook");
        }

        
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            ActionResult result = null;

            if (!ModelState.IsValid)
            {
                result = View("Index", model);
            }
            else
            {

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
            else
            {

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
        public ActionResult AddFamilyMember(RegisterViewModel model)
        {
            ActionResult result = null;

            if (!ModelState.IsValid)
            {
                result = View("AddFamilyMember", model);
            }
            else
            {
                PasswordHash ph = new PasswordHash(model.Password);

                User user = new User();
                user.ID = model.ID;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Username = model.Username;
                user.Password = ph.Hash;
                user.FamilyName = _db.GetFamilyFromFamilyID(model.FamilyID);
                user.FamilyID = ((User)Session["User"]).FamilyID;
                user.Salt = ph.Salt;
                user.RoleID = model.RoleID;

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
        public ActionResult AddBook(Book model)
        {
            ActionResult result = null;

            if (!ModelState.IsValid)
            {
                result = View("AddBook", model);
            }
            else
            {

                Book book = new Book();
                book.ID = model.ID;
                book.Title = model.Title;
                book.ISBN = model.ISBN;
                book.Type = model.Type;
                //add in User ID via Session info?

                book = _db.CreateBook(book);

                // book does not exist or ISBN is wrong
                if (book == null || book.ISBN == null)
                {
                    ModelState.AddModelError("invalid-credentials", "An invalid book title or ISBN was provided");
                    result = View("AddBook", model);
                }
                else
                {
                    Session["Book"] = book; //not sure if needed... yet?
                }
                if (((User)Session["User"]).RoleID == 2)
                {
                    result = RedirectToAction("ParentActivity", "Home");
                }
                else if (((User)Session["User"]).RoleID == 3)
                {
                    result = RedirectToAction("ChildActivity", "Home");
                }
            }
            return result;
        }

        //[HttpPost]
        //public ActionResult AddReadingLog(ReadingLogViewModel model)
        //{
        //    ActionResult result = null;

        //    if (!ModelState.IsValid)
        //    {
        //        result = View("ReadingLog", model);
        //    }
        //    else
        //    {

        //        ReadingLog log = new ReadingLog();
        //        log.ID = model.ID;
        //        log.BookID = model.BookID;
        //        log.MinutesRead = model.MinutesRead;
        //        log.Type = model.Type;
        //        //add in User ID via Session info?

        //        book = _db.Create(book);

        //        // book does not exist or ISBN is wrong
        //        if (book == null || book.ISBN == null)
        //        {
        //            ModelState.AddModelError("invalid-credentials", "An invalid book title or ISBN was provided");
        //            result = View("AddBook", model);
        //        }
        //        else
        //        {
        //            Session["Book"] = book; //not sure if needed... yet?
        //        }
        //        if (((User)Session["User"]).RoleID == 2)
        //        {
        //            result = RedirectToAction("ParentActivity", "Home");
        //        }
        //        else if (((User)Session["User"]).RoleID == 3)
        //        {
        //            result = RedirectToAction("ChildActivity", "Home");
        //        }
        //    }
        //    return result;
        //}
    }
}