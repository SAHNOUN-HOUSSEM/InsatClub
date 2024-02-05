using InsaClub.Models;

namespace InsaClub.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Club>? Clubs { get; set; }
        public HomeUserCreateViewModel Register { get; set; } = new HomeUserCreateViewModel();
    }
}