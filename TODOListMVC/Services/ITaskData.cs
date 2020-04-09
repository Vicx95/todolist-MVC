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
        UserModel GetVerifyCode(string id);
        UserModel GetUserByEmail(string email);
        void Add(TaskModel taskModel);
        void Add(UserModel userModel);
        void Update(TaskModel taskModel);
        void UpdateEmailVerifyStatus(UserModel userModel);
        void Delete(int id);

    }
}
