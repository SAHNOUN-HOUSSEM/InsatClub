

namespace InsaClub.Models
{
    public class MemberClub
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
