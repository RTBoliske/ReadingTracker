﻿using System;
using System.Collections.Generic;
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

        public ActionResult Register()
        {
            return View("Register");
        }

        public ActionResult FamilyActivity()
        {
            return View("FamilyActivity");
        }

        public ActionResult UserActivity()
        {
            List<User> userList = new List<User>();
            userList = _db.GetAllUsersFromFamilyID(((User)Session["User"]).FamilyID);
            UserActivityViewModel model = new UserActivityViewModel();
            model.UserList = userList;
            
            return View("UserActivity", model);
        }

        public ActionResult AddFamilyMember()
        {

            List<User> userList = new List<User>();
            userList = _db.GetAllUsersFromFamilyID(((User)Session["User"]).FamilyID);
            AddFamilyMemberViewModel model = new AddFamilyMemberViewModel();
            if(TempData.ContainsKey("AddSuccessState"))
            {
                model.AddSuccessState = (AddFamilyMemberViewModel.SuccessState)TempData["AddSuccessState"];
            }
            else
            {
                model.AddSuccessState = AddFamilyMemberViewModel.SuccessState.None;
            }
            model.FamilyMembersList = userList;
            return View("AddFamilyMember", model);
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
                User user = _db.GetUser(model.Username);
                PasswordHash ph = new PasswordHash(model.Password, user.Salt);
                
                if (user == null)
                {
                    ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                    result = View("Index", model);
                }
                else if (ph.Hash != user.Password)
                {
                    ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                    result = View("Index", model);
                }
                else if (ph.Hash == user.Password)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    Session["User"] = user;

                    if (((User)Session["User"]).RoleID == 2 || ((User)Session["User"]).RoleID == 3)
                    {
                        result = RedirectToAction("UserActivity", "Home");
                    }
                    else
                    {
                        //page not found
                        //need to return Admin view
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

                if (((User)Session["User"]).RoleID == 2 || ((User)Session["User"]).RoleID == 3)
                {
                    result = RedirectToAction("UserActivity", "Home");
                }

            }

            return result;
        }

        [HttpPost]
        public ActionResult AddFamilyMember(AddFamilyMemberViewModel model)
        {
            ActionResult result = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception();

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
                    user.FamilyID = ((User)Session["User"]).FamilyID;
                    user.FamilyName = _db.GetFamilyFromFamilyID(user.FamilyID);
                    user.Salt = ph.Salt;
                    user.RoleID = model.RoleID;

                    user = _db.CreateFamilyMember(user);

                    if (((User)Session["User"]).RoleID == 2)
                    {
                        TempData["AddSuccessState"] = AddFamilyMemberViewModel.SuccessState.Success;
                        result = RedirectToAction("AddFamilyMember", "Home");
                    }
                }
            }
            catch(Exception)
            {
                TempData["AddSuccessState"] = AddFamilyMemberViewModel.SuccessState.Failed;
                result = RedirectToAction("AddFamilyMember", "Home");
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
                book.UserID = model.UserID;
                book.FamilyID = ((User)Session["User"]).FamilyID;
                book.Title = model.Title;
                book.Author = model.Author;
                book.ISBN = model.ISBN;
                book.Type = model.Type;

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

        [HttpPost]
        public ActionResult AddReadingLog(UserActivityViewModel model)
        {
            ActionResult result = null;

            if (!ModelState.IsValid)
            {
                result = View("UserActivity", model);
            }
            else
            {

                ReadingLog log = new ReadingLog();
                log.UserID = model.UserID;
                log.FamilyID = model.FamilyID; //use Session??
                log.MinutesRead = model.MinutesRead;
                log.Complete = model.Complete;
                //date gets added in DAL

                log = _db.CreateReadingLog(log);

                // book does not exist or ISBN is wrong
                if (log.ID == 0)
                {
                    ModelState.AddModelError("invalid-credentials", "The reading log was not successfully created.");
                    result = View("UserActivity", model);
                }
                else
                {
                    Session["Log"] = log; //not sure if needed... yet?
                }
                if (((User)Session["User"]).RoleID == 2)
                {
                    result = RedirectToAction("UserActivity", "Home");
                }
                else if (((User)Session["User"]).RoleID == 3)
                {
                    result = RedirectToAction("UserActivity", "Home");
                }
            }
            return result;
        }
    }
}