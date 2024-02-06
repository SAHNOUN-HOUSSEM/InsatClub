using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InsaClub.Models;

namespace InsaClub.ViewModels
{
    public class ValidateStudyLevelAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var registerViewModel = (RegisterViewModel)validationContext.ObjectInstance;
            if (registerViewModel.StudyLevel == null)
            {
                return new ValidationResult("StudyLevel is required.");
            }


            if (registerViewModel.StudyLevel.Level == EStudyLevel.A1 &&
                (registerViewModel.StudyLevel.Speciality != ESpeciality.MPI && registerViewModel.StudyLevel.Speciality != ESpeciality.CBA))
            {
                return new ValidationResult("The first year is only allowed for MPI and CBA specialities.");
            }

            if (registerViewModel.StudyLevel.Level != EStudyLevel.A1 &&
                (registerViewModel.StudyLevel.Speciality == ESpeciality.MPI || registerViewModel.StudyLevel.Speciality == ESpeciality.CBA))
            {
                return new ValidationResult("The first year is only allowed for MPI and CBA specialities.");
            }

            return ValidationResult.Success;
        }
    }
    public class RegisterViewModel
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
        [ValidateStudyLevel(ErrorMessage = "Invalid combination of study level and speciality.")]

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
