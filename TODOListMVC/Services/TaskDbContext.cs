using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TODOListMVC.Models;

namespace TODOListMVC.Services
{
    public class TaskDbContext : DbContext 
    {
        public DbSet<TaskModel> Tasks { get; set; }
    }
}