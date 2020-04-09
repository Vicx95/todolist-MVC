using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TODOListMVC.Services;

namespace TODOListMVC.Areas.user.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskData db;

        
        public HomeController(ITaskData db)
        {
            this.db = db;
        }

        // GET: user/Home
        [Authorize]
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }
    }
}