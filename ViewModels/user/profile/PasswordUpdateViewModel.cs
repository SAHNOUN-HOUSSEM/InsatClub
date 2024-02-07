
using System.ComponentModel.DataAnnotations;
using InsaClub.Models;

namespace InsaClub.ViewModels;

public class PasswordUpdateViewModel 
{
        [Display(Name = "Old password")]
        [Required(ErrorMessage = "Old password is required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 8 and 15 characters and contain at least one lowercase letter, one uppercase letter, one digit and one special character")]
        public string NewPassword { get; set; }
        [Display(Name = "Confirm new  password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password do not match")]
        public string ConfirmNewPassword { get; set; }
        
 
}