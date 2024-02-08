using InsaClub.Models;

namespace InsaClub.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task<bool> IsMemberOf(string userId, int clubId);

        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();
    }
}
