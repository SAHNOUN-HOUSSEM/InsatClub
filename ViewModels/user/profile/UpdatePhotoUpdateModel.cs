
using System.ComponentModel.DataAnnotations;
using InsaClub.Models;

namespace InsaClub.ViewModels;

public class UpdatePhotoUpdateModel 
{
       public string? ProfileImageUrl { get; set; }
       public IFormFile? Image { get; set; }
        
 
}