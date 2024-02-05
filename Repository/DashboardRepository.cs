using Microsoft.EntityFrameworkCore;
using InsaClub.Data;
using InsaClub.Interfaces;
using InsaClub.Models;

namespace InsaClub.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(r => r.User.Id == curUser);
            return userClubs.ToList();
        }

        // public async Task<List<Event>> GetAllUserEvents()
        // {
        //     var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        //     var userEvents = _context.Events.Where(e => e.User.Id == curUser);
        //     return userEvents.ToList();
        // }
        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(User user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
