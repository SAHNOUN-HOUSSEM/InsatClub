using InsaClub.Data.Enum;
using InsaClub.Models;

namespace InsaClub.Interfaces
{
    public interface IRaceRepository
    {
        Task<int> GetCountAsync();

        Task<int> GetCountByCategoryAsync(EventCategory category);

        Task<Event?> GetByIdAsync(int id);

        Task<Event?> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Event>> GetAll();

        Task<IEnumerable<Event>> GetAllRacesByCity(string city);

        Task<IEnumerable<Event>> GetSliceAsync(int offset, int size);

        Task<IEnumerable<Event>> GetRacesByCategoryAndSliceAsync(EventCategory category, int offset, int size);

        bool Add(Event race);

        bool Update(Event race);

        bool Delete(Event race);

        bool Save();
    }
}