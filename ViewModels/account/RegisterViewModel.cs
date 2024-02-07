using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InsaClub.Attributes;
using InsaClub.Models;

namespace InsaClub.ViewModels
{
    
    public class RegisterViewModel : EditProfileViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Display(Name = "Study level")]
        [Required(ErrorMessage = "Study level is required")]
        [ValidateStudyLevelRegisterAttribute(ErrorMessage = "Invalid combination of study level and speciality.")]

        public StudyLevel StudyLevel { get; set; }

        [Display(Name = "Profile image")]
        public string? ProfileImageUrl { get; set; }
        public IFormFile? Image { get; set; }



        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 8 and 15 characters and contain at least one lowercase letter, one uppercase letter, one digit and one special character")]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
