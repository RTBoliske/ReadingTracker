using System;
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

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }

        public ActionResult FamilyActivity(int? id)
        {
            List<User> userList = new List<User>();
            userList = _db.GetAllUsersFromFamilyID(((User)Session["User"]).FamilyID);
            UserActivityViewModel model = new UserActivityViewModel(_db);
            model.UserList = userList;
            return View("FamilyActivity", model);
        }

        public ActionResult UserActivity(int? id)
        {
            List<User> userList = new List<User>();
            userList = _db.GetAllUsersFromFamilyID(((User)Session["User"]).FamilyID);
            UserActivityViewModel model = new UserActivityViewModel(_db);
            model.UserList = userList;
            if (id.HasValue)
            {
                model.UserID = id.Value;
                //loading up the list of Prizes & Progress
                User user = _db.GetUserByID(model.UserID);
                model.PrizeList = _db.GetPrizesByUser(user);
                //loading active/inactive books
                model.ActiveBooks = _db.GetActiveBooks(user.FamilyID);
                model.InactiveBooks = _db.GetInactiveBooks(model.UserID);
                //loading reading log
                model.ReadingLogs = _db.GetReadingLog(model.UserID);
            } else
            {
                model.UserID = (Session["User"] as User).ID;
                //loading up the list of Prizes & Progress
                User user = _db.GetUserByID(model.UserID);
                model.PrizeList = _db.GetPrizesByUser(user);
                //Loading active/inactive books
                model.ActiveBooks = _db.GetActiveBooks(user.FamilyID);
                model.InactiveBooks = _db.GetInactiveBooks(model.UserID);
                //reading logs
                model.ReadingLogs = _db.GetReadingLog(model.UserID);
            }
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

        public ActionResult AddPrize(int? id)
        {

            PrizeViewModel model = new PrizeViewModel();

            if (id.HasValue)
            {
                Prize prize = _db.GetPrizeById(id.Value);

                model.PrizeId = prize.ID;
                model.Milestone = prize.Milestone;
                model.UserType = prize.UserType;
                model.isActive = prize.isActive;
                model.MaxNumPrizes = prize.MaxNumPrizes;
                model.StartDate = prize.StartDate;
                model.EndDate = prize.EndDate;
                model.FamilyId = prize.FamilyID;
                model.Title = prize.Title;
            }

            //need to add method to call allPrizes
            model.PrizeList = _db.GetPrizes(((User)Session["User"]).FamilyID);
            if (TempData.ContainsKey("AddSuccessState"))
            {
                model.AddSuccessState = (PrizeViewModel.SuccessState)TempData["AddSuccessState"];
            }
            else
            {
                model.AddSuccessState = PrizeViewModel.SuccessState.None;
            }
            return View("AddPrize", model);
        }
        
        public ActionResult AddBook()
        {
            List<Book> bookList = new List<Book>();
            bookList = _db.GetAllBooksByFamilyID(((User)Session["User"]).FamilyID);
            AddBookViewModel model = new AddBookViewModel();
            if (TempData.ContainsKey("AddSuccessState"))
            {
                model.AddSuccessState = (AddBookViewModel.SuccessState)TempData["AddSuccessState"];
            }
            else
            {
                model.AddSuccessState = AddBookViewModel.SuccessState.None;
            }
            model.BookList = bookList;
            return View("AddBook", model);
        }

        //public ActionResult UpdatePrize(int id)
        //{
        //    Prize prize = _db.GetPrizeById(id);

        //    PrizeViewModel pvm = new PrizeViewModel();
        //    pvm.PrizeId = prize.ID;
        //    pvm.Milestone = prize.Milestone;
            
        //    return View("AddPrize", pvm);
        //}


        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            ActionResult result = null;

            if (!ModelState.IsValid || model.Password == null || model.Username == null)
            {
                result = View("Register", model);
            }
            else
            {
                User user = _db.GetUserByUsername(model.Username);

                if (user.Username == null || user.Password == null)
                {
                    ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                    result = View("Index", model);
                }
                else
                {
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
            try
            {


                if (!ModelState.IsValid)
                {
                    result = View("AddBook", model);
                }
                else
                {

                    Book book = new Book();
                    book.ID = model.ID;
                    book.FamilyID = ((User)Session["User"]).FamilyID;
                    book.Title = model.Title;
                    book.Author = model.Author;
                    book.ISBN = model.ISBN;

                    book = _db.CreateBook(book);

                    if (((User)Session["User"]).RoleID == 2)
                    {
                        TempData["AddSuccessState"] = AddBookViewModel.SuccessState.Success;
                        result = RedirectToAction("AddBook", "Home");
                    }
                    // book does not exist or ISBN is wrong
                    //if (book == null || book.ISBN == null)
                    //{
                    //    ModelState.AddModelError("invalid-credentials", "An invalid book title or ISBN was provided");
                    //    result = View("AddBook", model);
                    //}
                    //else
                    //{
                    //    Session["Book"] = book; //not sure if needed... yet?
                    //}
                    //if (((User)Session["User"]).RoleID == 2)
                    //{
                    //    result = RedirectToAction("UserActivity", "Home");
                    //}
                    //else if (((User)Session["User"]).RoleID == 3)
                    //{
                    //    result = RedirectToAction("UserActivity", "Home");
                    //}
                }
            }
            catch(Exception)
            {
                TempData["AddSuccessState"] = AddBookViewModel.SuccessState.Failed;
                result = RedirectToAction("AddBook", "Home");
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
                model.MinutesRead = model.MinutesRead + (model.HoursRead * 60);

                ReadingLog log = new ReadingLog();
                log.UserID = model.UserID;
                log.BookID = model.BookID;
                log.MinutesRead = model.MinutesRead;
                log.Status = model.Status;
                log.Type = model.Type;
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

        [HttpPost]
        public ActionResult AddPrize(Prize model)
        {
            ActionResult result = null;
            try {
                if (!ModelState.IsValid)
                {
                    result = View("AddPrize", model);
                }
                else
                {

                    Prize prize = new Prize();
                    prize.ID = model.ID;
                    prize.FamilyID = ((User)Session["User"]).FamilyID;
                    prize.UserType = model.UserType;
                    prize.Milestone = model.Milestone;
                    prize.MaxNumPrizes = model.MaxNumPrizes;
                    prize.isActive = model.isActive;
                    prize.StartDate = model.StartDate;
                    prize.EndDate = model.EndDate;
                    prize.Title = model.Title;


                    prize = _db.AddPrize(prize);

                   
                    if (prize == null)
                    {
                        ModelState.AddModelError("invalid-credentials", "An invalid prize was attempted");
                        result = View("AddPrize", model);
                    }
                    
                    if (((User)Session["User"]).RoleID == 2)
                    {
                        TempData["AddSuccessState"] = AddFamilyMemberViewModel.SuccessState.Success;
                        result = RedirectToAction("AddPrize", "Home");
                    }
                    else if (((User)Session["User"]).RoleID == 3)
                    {
                        result = RedirectToAction("AddPrize", "Home");
                    }
                }
            }
            catch (Exception)
            {
                TempData["AddSuccessState"] = AddFamilyMemberViewModel.SuccessState.Failed;
                throw;
            }
            return result;
        }

        [HttpPost]
        public ActionResult EditPrize(PrizeViewModel model)
        {
            ActionResult result = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    result = View("AddPrize", model);
                }
                else
                {

                    Prize prize = new Prize();
                    prize.ID = model.PrizeId;
                    prize.FamilyID = ((User)Session["User"]).FamilyID;
                    prize.UserType = model.UserType;
                    prize.Milestone = model.Milestone;
                    prize.MaxNumPrizes = model.MaxNumPrizes;
                    prize.isActive = model.isActive;
                    prize.StartDate = model.StartDate;
                    prize.EndDate = model.EndDate;
                    prize.Title = model.Title;


                    _db.EditPrize(prize);

                    result =RedirectToAction("AddPrize",model);
                    if (prize == null)
                    {
                        ModelState.AddModelError("invalid-credentials", "An invalid prize was attempted");
                        result = View("AddPrize", model);
                    }
                    
                    if (((User)Session["User"]).RoleID == 2)
                    {
                        TempData["AddSuccessState"] = AddFamilyMemberViewModel.SuccessState.Success;
                        result = RedirectToAction("AddPrize", model);
                    }
                    else if (((User)Session["User"]).RoleID == 3)
                    {
                        result = RedirectToAction("AddPrize", "Home");
                    }
                }
            }
            catch (Exception)
            {
                TempData["AddSuccessState"] = AddFamilyMemberViewModel.SuccessState.Failed;
                throw;
            }
            return result;
        }


    }


}
