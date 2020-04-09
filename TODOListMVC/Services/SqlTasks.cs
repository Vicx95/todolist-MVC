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
        private UserModel currentUser;

        public SqlTasks(TaskDbContext db)
        {            
            this.db = db;
            currentUser = GetUserByEmail(HttpContext.Current.User.Identity.Name);
        }

        public void Add(TaskModel taskModel)
        {
            //string userEmail = HttpContext.Current.User.Identity.Name;
            //var currentUser = GetUserByEmail(userEmail);
            taskModel.UserId = currentUser.UserId;
            db.Tasks.Add(taskModel);
            db.SaveChanges();
        }

        public void Add(UserModel userModel)
        {
            db.Users.Add(userModel);
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
            return db.Tasks.Where(u => u.UserId == currentUser.UserId)
                .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TaskModel> GetAll()
        {
            return db.Tasks.Where(t => t.UserId == currentUser.UserId).OrderBy(t => t.Status);
        }

        public UserModel GetUserByEmail(string email)
        {
            return db.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public UserModel GetVerifyCode(string id)
        {
            return db.Users.Where(u => u.ActivationCode == new Guid(id)).FirstOrDefault();
        }

        public void Update(TaskModel taskModel)
        {
            var existing = db.Entry(taskModel);
            existing.Entity.UserId = currentUser.UserId;
            existing.State = EntityState.Modified;
            db.SaveChanges();
                           
        }

        public void UpdateEmailVerifyStatus(UserModel userModel)
        {
            var existing = db.Entry(userModel);
            existing.State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}