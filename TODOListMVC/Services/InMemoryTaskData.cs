using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TODOListMVC.Models;

namespace TODOListMVC.Services
{
    public class InMemoryTaskData : ITaskData
    {
         List<TaskModel> tasks;


        public InMemoryTaskData()
        {
            tasks = new List<TaskModel>()
            {
                new TaskModel{Id = 1, Title = "Jedzenie", Description = "przygotować coś", Status = TaskStatus.Active},
                new TaskModel{Id = 2, Title = "Trening", Description = "Iść poćwiczyć na siłownie", Status = TaskStatus.Active},
                new TaskModel{Id = 3, Title = "Pranie", Description = "Wstawić pranie", Status = TaskStatus.Completed},
                new TaskModel{Id = 4, Title = "Sprzątanie", Description = "Posprzątać całe mieszkanie", Status = TaskStatus.Completed}
            };
        }

        public void Add(TaskModel taskModel)
        {
            tasks.Add(taskModel);
            taskModel.Id = tasks.Max(t => t.Id) + 1;
        }

        public void Delete(int id)
        {
            var task = Get(id);
            if(task != null)
            {
                tasks.Remove(task);
            }
        }

        public TaskModel Get(int id)
        {
            return tasks.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<TaskModel> GetAll()
        {
            return tasks.OrderBy(t => t.Id);
        }

        public void Update(TaskModel taskModel)
        {
            var existing = Get(taskModel.Id);
            if(existing != null)
            {
                existing.Title = taskModel.Title;
                existing.Description = taskModel.Description;
                existing.Status = taskModel.Status;
            }
               
        }
    }
}