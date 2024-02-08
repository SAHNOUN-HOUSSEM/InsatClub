using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InsaClub.Models;

namespace InsaClub.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MemberClub>()
                .HasKey(mc => new { mc.ClubId, mc.UserId });
            builder.Entity<MemberClub>()
                .HasOne(mc => mc.Club)
                .WithMany(c => c.Members)
                .HasForeignKey(mc => mc.ClubId);
            builder.Entity<MemberClub>()
                .HasOne(mc => mc.User)
                .WithMany(u => u.ClubsIn)
                .HasForeignKey(mc => mc.UserId);

            builder.Entity<MemberEvent>()
                .HasKey(me => new { me.EventId, me.UserId });
            builder.Entity<MemberEvent>()
                .HasOne(me => me.Event)
                .WithMany(e => e.Members)
                .HasForeignKey(me => me.EventId);
            builder.Entity<MemberEvent>()
                .HasOne(me => me.User)
                .WithMany(u => u.EventsIn)
                .HasForeignKey(me => me.UserId);
            base.OnModelCreating(builder);




        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<MemberClub> MemberClubs { get; set; }
        public DbSet<MemberEvent> MemberEvents { get; set; }
    }
}
