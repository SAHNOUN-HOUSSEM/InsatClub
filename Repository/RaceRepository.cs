using Microsoft.EntityFrameworkCore;
using InsaClub.Data;
using InsaClub.Data.Enum;
using InsaClub.Interfaces;
using InsaClub.Models;

namespace InsaClub.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Event race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Event race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetAllRacesByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Event?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Races.CountAsync();
        }

        public async Task<int> GetCountByCategoryAsync(EventCategory category)
        {
            return await _context.Races.CountAsync(r => r.EventCategory == category);
        }

        public async Task<IEnumerable<Event>> GetSliceAsync(int offset, int size)
        {
            return await _context.Races.Include(a => a.Address).Skip(offset).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetRacesByCategoryAndSliceAsync(EventCategory category, int offset, int size)
        {
            return await _context.Races
                .Where(r => r.EventCategory == category)
                .Include(a => a.Address)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Event race)
        {
            _context.Update(race);
            return Save();
        }
    }
}