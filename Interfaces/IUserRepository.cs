using InsaClub.Models;

namespace InsaClub.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        // Task<ICollection<Club>> GetClubsOfUser(string userId)
        Task<bool> IsMemberOf(string userId, int clubId);
        Task<User> RemoveMemberFromClub(int clubId, string userId);
        Task<ICollection<User>> GetMembersOfClub(int clubId);
        Task<ICollection<Club>> GetClubsOfUser(string userId);
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();
    }
}
