using InsaClub.Data.Enum;
using InsaClub.Models;

namespace InsaClub.ViewModels
{
    public class CreateEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public EventCategory EventCategory { get; set; }
        public string ClubId { get; set; }
    }
}
