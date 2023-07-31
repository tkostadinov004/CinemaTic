using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels.Users
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        [Required(ErrorMessage = "Enter your old password!")]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [Required(ErrorMessage = "Enter a new password!")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(NewPassword), ErrorMessage = "The two passwords do not match!")]
        [Required(ErrorMessage = "Enter a new password!")]
        public string ConfirmPassword { get; set; }
    }
}
