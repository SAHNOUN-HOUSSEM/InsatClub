using InsaClub.Data.Enum;
using InsaClub.Models;

namespace InsaClub.Interfaces
{
    public interface IEventRepository
    {
        Task<int> GetCountAsync();

        Task<int> GetCountByCategoryAsync(EventCategory category);

        Task<Event?> GetByIdAsync(int id);

        Task<Event?> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Event>> GetAll();

        // Task<IEnumerable<Event>> GetAllEventsByCity(string city);

        Task<IEnumerable<Event>> GetSliceAsync(int offset, int size);

        Task<IEnumerable<Event>> GetEventsByCategoryAndSliceAsync(EventCategory category, int offset, int size);

        bool JoinEvent(int eventId, string userId);

        bool Add(Event @event);

        bool Update(Event @event);

        bool Delete(Event @event);

        bool Save();
    }
}