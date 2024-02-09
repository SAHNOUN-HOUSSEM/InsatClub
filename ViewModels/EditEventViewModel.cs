using System.ComponentModel.DataAnnotations;
using InsaClub.Data.Enum;

namespace InsaClub.ViewModels
{
    public class EditEventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; }
        public string? URL { get; set; }

        [Required(ErrorMessage = "EventCategory is required.")]
        public EventCategory EventCategory { get; set; }
    }
}
