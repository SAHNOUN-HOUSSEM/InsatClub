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
        public async Task<Event?> GetEventById(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Event?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Events.AsNoTracking().Include(r => r.Club).FirstOrDefaultAsync(r => r.Id == id);
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

        public bool JoinEvent(int eventId, string userId)
        {
            var @event = _context.Events.Find(eventId);
            var user = _context.Users.Find(userId);
            if (@event == null || user == null)
            {
                return false;
            }

            var memberEvent = new MemberEvent
            {
                EventId = eventId,
                Event = @event,
                UserId = userId,
                User = user
            };

            _context.Add(memberEvent);
            return Save();
        }

        public bool LeaveEvent(int eventId, string userId)
        {
            var memberEvent = _context.MemberEvents.FirstOrDefault(r => r.EventId == eventId && r.UserId == userId);
            if (memberEvent == null)
            {
                return false;
            }

            _context.Remove(memberEvent);
            return Save();
        }

        public bool GetJoinedUsers(int eventId, string userId)
        {
            var m = _context.MemberEvents.Any(r => r.EventId == eventId && r.UserId == userId);
            return m ? true : false;
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