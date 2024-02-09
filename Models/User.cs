using Microsoft.AspNetCore.Identity;
using InsaClub.Models;
using System.Text.Json.Serialization;

namespace InsaClub.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int StudyLevelId { get; set; }
        public StudyLevel StudyLevel { get; set; }
        public string? ProfileImageUrl { get; set; }
        [JsonIgnore]
        public ICollection<Club> Clubs { get; set; }

        public string? Bio { get; set; }
        public ICollection<MemberClub> ClubsIn { get; set; }
        public ICollection<MemberEvent> EventsIn { get; set; }
    }
}
