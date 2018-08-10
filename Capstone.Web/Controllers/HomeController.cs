using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var roles = _db.GetRoleItems();
            var user = _db.GetUserItem("admin");
            bool itWorks = false;
            //itWorks = DbManager.VerifyPassword("password", user.Salt);
            
            return View("Index", itWorks);
        }
    }
}