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
        public DbSet<UserModel> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>()
            .HasRequired<UserModel>(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey<int?>(t => t.UserId);
        }
    }
}