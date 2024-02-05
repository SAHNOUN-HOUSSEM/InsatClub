using InsaClub.Data.Enum;

namespace InsaClub.ViewModels
{
    public class EditEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public EventCategory EventCategory { get; set; }
    }
}
