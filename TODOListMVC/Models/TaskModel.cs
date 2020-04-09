using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TODOListMVC.Models
{
   
    public class TaskModel
    {
        public TaskModel()
        {
            Status = TaskStatus.Active;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public TaskStatus Status { get; set; }


        public int? UserId { get; set; }
        public UserModel User { get; set; }

    }
}