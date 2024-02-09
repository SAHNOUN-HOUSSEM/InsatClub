using System.ComponentModel.DataAnnotations;
using InsaClub.Data.Enum;
using InsaClub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InsaClub.ViewModels
{
    public class CreateEventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "EventCategory is required.")]
        public EventCategory EventCategory { get; set; }

        [Required(ErrorMessage = "ClubId is required.")]    
        public int ClubId { get; set; }
        public IEnumerable<SelectListItem> Clubs { get; set; }
    }
}
