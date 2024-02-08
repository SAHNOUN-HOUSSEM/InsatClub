using Microsoft.EntityFrameworkCore;
using InsaClub.Data;
using InsaClub.Interfaces;
using InsaClub.Models;

namespace InsaClub.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.Include(u => u.StudyLevel).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> IsMemberOf(string userId, int clubId)
        {
            var membership = await _context.MemberClubs.Where(m => m.ClubId == clubId && m.UserId == userId).FirstOrDefaultAsync();
            return membership != null;
        }



        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}