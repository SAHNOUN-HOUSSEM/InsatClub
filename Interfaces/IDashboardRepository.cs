using InsaClub.Models;

namespace InsaClub.Interfaces
{
    public interface IDashboardRepository
    {
        // Task<List<Event>> GetAllUserEvents();
        Task<List<Club>> GetAllUserClubs();
        Task<User> GetUserById(string id);
        Task<User> GetByIdNoTracking(string id);
        bool Update(User user);
        bool Save();
    }
}
