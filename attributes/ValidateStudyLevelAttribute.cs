using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InsaClub.Models;
using InsaClub.ViewModels;

namespace InsaClub.Attributes
{
    public


        class ValidateStudyLevelAttribute : ValidationAttribute
    {
        protected
            override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            var registerViewModel = (EditProfileViewModel)validationContext.ObjectInstance;

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
}
