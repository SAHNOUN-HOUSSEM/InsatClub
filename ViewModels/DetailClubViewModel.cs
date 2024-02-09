using InsaClub.Models;

namespace InsaClub.ViewModels
{
    public class DetailClubViewModel
    {
        public Club Club { get; set; }
        public ICollection<User> Members { get; set; }

    }
}
