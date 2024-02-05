using Microsoft.AspNetCore.Identity;

namespace InsaClub.Models
{
    public class User : IdentityUser
    {
        
        public string? ProfileImageUrl { get; set; }
        public ICollection<Club> Clubs { get; set; }
    }
}
