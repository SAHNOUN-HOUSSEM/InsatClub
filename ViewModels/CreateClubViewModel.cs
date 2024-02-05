using InsaClub.Data.Enum;
using InsaClub.Models;

namespace InsaClub.ViewModels
{
    public class CreateClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public ClubCategory ClubCategory { get; set; }
        public string UserId { get; set; }
    }
}
