﻿using InsaClub.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InsaClub.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public DateTime? StartTime { get; set; }
        public int? EntryFee { get; set; }
        public string? Website { get; set; }
        public string? Facebook { get; set; }
        public string? Contact { get; set; }
        public EventCategory EventCategory { get; set; }
        public  int? ClubId { get; set; }
        public Club? Club { get; set; }

        [JsonIgnore]
        public ICollection<MemberEvent> Members { get; set; }
    }
}
