﻿using Microsoft.EntityFrameworkCore;
using InsaClub.Data;
using InsaClub.Data.Enum;
using InsaClub.Interfaces;
using InsaClub.Models;

namespace InsaClub.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Club club)
        {
            _context.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }



        public async Task<IEnumerable<Club>> GetSliceAsync(int offset, int size)
        {
            return await _context.Clubs.Skip(offset).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<Club>> GetClubsByCategoryAndSliceAsync(ClubCategory category, int offset, int size)
        {
            return await _context.Clubs
                .Where(c => c.ClubCategory == category)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetCountByCategoryAsync(ClubCategory category)
        {
            return await _context.Clubs.CountAsync(c => c.ClubCategory == category);
        }

        public async Task<Club?> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(c => c.User).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Club?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<User> AddMemberToClub(int clubId, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var club = await _context.Clubs.Include(c => c.Members).FirstOrDefaultAsync(c => c.Id == clubId);
            if (club == null || user == null)
            {
                return null;
            }
            var membership = new MemberClub { ClubId = clubId, UserId = userId };
            club.Members.Add(membership);
            var saved = _context.SaveChanges();
            return user;
        }


        public async Task<IEnumerable<Club>> GetClubsByUserIdAsync(string userId)
        {
            return await _context.Clubs.Where(c => c.UserId == userId).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Clubs.CountAsync();
        }




    }
}