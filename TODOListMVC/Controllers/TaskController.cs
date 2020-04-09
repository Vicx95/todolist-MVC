using System.Web.Mvc;
using TODOListMVC.Models;
using TODOListMVC.Services;

namespace TODOListMVC.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskData db;

        public TaskController(ITaskData db)
        {
            this.db = db;
        }

        // GET: Task
        [Authorize]
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = db.Get(id);
            if(model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(TaskModel task)
        {
            if(ModelState.IsValid)
            {
                db.Add(task);
                return RedirectToAction("Details", new { id = task.Id });
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Get(id);
            if(model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(TaskModel task)
        {
            if(ModelState.IsValid)
            {
                db.Update(task);
                return RedirectToAction("Details", new { id = task.Id });
            }
            return View(task);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = db.Get(id);
            if(model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }

    }
}