using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TODOListMVC.Models;

namespace TODOListMVC.Services
{
    public class SqlTasks : ITaskData
    {
        private readonly TaskDbContext db;

        public SqlTasks(TaskDbContext db)
        {
            this.db = db;
        }

        public void Add(TaskModel taskModel)
        {
            db.Tasks.Add(taskModel);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
        }

        public TaskModel Get(int id)
        {
            return db.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TaskModel> GetAll()
        {
            return from t in db.Tasks
                   orderby t.Status
                   select t;
        }

        public void Update(TaskModel taskModel)
        {
            var existing = db.Entry(taskModel);
            existing.State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}