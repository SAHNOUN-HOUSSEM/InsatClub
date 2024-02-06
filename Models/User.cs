using Microsoft.AspNetCore.Identity;
using InsaClub.Models;

namespace InsaClub.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid StudyLevelId { get; set; }
        public StudyLevel StudyLevel { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<Club> Clubs { get; set; }
    }
}
