using InsaClub.Data.Enum;
using InsaClub.Models;

namespace InsaClub.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();

        Task<IEnumerable<Club>> GetSliceAsync(int offset, int size);


        Task<IEnumerable<Club>> GetClubsByCategoryAndSliceAsync(ClubCategory category, int offset, int size);



        Task<Club?> GetByIdAsync(int id);

        Task<Club?> GetByIdAsyncNoTracking(int id);


        Task<int> GetCountAsync();

        Task<int> GetCountByCategoryAsync(ClubCategory category);

        Task<IEnumerable<Club>> GetClubsByUserIdAsync(string userId);

        bool Add(Club club);

        bool Update(Club club);

        bool Delete(Club club);

        bool Save();
    }
}