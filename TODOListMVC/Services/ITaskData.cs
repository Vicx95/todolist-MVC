using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOListMVC.Models;

namespace TODOListMVC.Services
{
    public interface ITaskData
    {
        IEnumerable<TaskModel> GetAll();
        TaskModel Get(int id);
        void Add(TaskModel taskModel);
        void Update(TaskModel taskModel);
        void Delete(int id);
    }
}
