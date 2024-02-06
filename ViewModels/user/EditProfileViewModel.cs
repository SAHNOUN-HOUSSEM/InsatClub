using System.ComponentModel.DataAnnotations;
using InsaClub.Attributes;
using InsaClub.Models;

namespace InsaClub.ViewModels
{
    public class EditProfileViewModel
    {
        public string  EmailAddress { get; set; }
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

        public string? Bio { get; set; }

    }
}