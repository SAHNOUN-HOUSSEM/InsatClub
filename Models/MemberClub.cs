using System.Text.Json.Serialization;


namespace InsaClub.Models
{
    public class MemberClub
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        [JsonIgnore]
        public Club Club { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
}
