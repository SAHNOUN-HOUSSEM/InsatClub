using InsaClub.Data.Enum;
using InsaClub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InsaClub.ViewModels
{
    public class CreateEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public EventCategory EventCategory { get; set; }
        public int ClubId { get; set; }
        public IEnumerable<SelectListItem> Clubs { get; set; }
    }
}
