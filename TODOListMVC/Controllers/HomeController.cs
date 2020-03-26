using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TODOListMVC.Services;

namespace TODOListMVC.Controllers
{
    public class HomeController : Controller
    {
        ITaskData db;


        public HomeController(ITaskData db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }

    }
}