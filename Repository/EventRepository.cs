using Microsoft.EntityFrameworkCore;
using InsaClub.Data;
using InsaClub.Data.Enum;
using InsaClub.Interfaces;
using InsaClub.Models;

namespace InsaClub.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Event @event)
        {
            _context.Add(@event);
            return Save();
        }

        public bool Delete(Event @event)
        {
            _context.Remove(@event);
            return Save();
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _context.Events.ToListAsync();
        }

        // public async Task<IEnumerable<Event>> GetAllEventsByCity(string city)
        // {
        //     return await _context.Events.Where(c => c.Address.City.Contains(city)).ToListAsync();
        // }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.Include(r => r.Club).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Event?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Events.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Events.CountAsync();
        }

        public async Task<int> GetCountByCategoryAsync(EventCategory category)
        {
            return await _context.Events.CountAsync(r => r.EventCategory == category);
        }

        public async Task<IEnumerable<Event>> GetSliceAsync(int offset, int size)
        {
            return await _context.Events.Skip(offset).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByCategoryAndSliceAsync(EventCategory category, int offset, int size)
        {
            return await _context.Events
                .Where(r => r.EventCategory == category)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Event @event)
        {
            _context.Update(@event);
            return Save();
        }
    }
}