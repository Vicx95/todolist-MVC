using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TODOListMVC.Models
{
    
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "E-mail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password and password do not match")]
        public string ConfirmPassword { get; set; }

        public bool IsEmailVerified { get; set; }

        public System.Guid ActivationCode { get; set; }


        public ICollection<TaskModel> Tasks { get; set; }
    }

}