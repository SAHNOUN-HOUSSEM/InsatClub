using System.ComponentModel.DataAnnotations;
using InsaClub.Attributes;
using InsaClub.Models;

namespace InsaClub.ViewModels
{
    public class ProfileViewModel
    {
        public EditProfileViewModel EditProfileViewModel { get; set; }
        public PasswordUpdateViewModel PasswordUpdateViewModel { get; set; }
        public UpdatePhotoUpdateModel UpdatePhotoUpdateModel { get; set; }
        public string ActiveTab { get; set; }

    }
}