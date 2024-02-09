
using InsaClub.Models;

namespace InsaClub.ViewModels;

public class UserDetailViewModel
{
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid StudyLevelId { get; set; }
        public StudyLevel StudyLevel { get; set; }

        public string? ProfileImageUrl { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Club> ClubsIn { get; set; }
        public ICollection<Event> Events { get; set; }
        // public ICollection<Event> EventsIn { get; set; }
        
        public string? Bio { get; set; }



}