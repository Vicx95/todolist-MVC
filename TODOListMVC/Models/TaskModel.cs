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
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
    }
}