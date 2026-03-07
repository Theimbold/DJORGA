using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Persistence.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// SQLite-Implementierung des Track-Repositories.
    /// </summary>
    public class SqliteTrackRepository : ITrackRepository
    {
        private readonly AppDbContext _context;

        public SqliteTrackRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Track?> GetByIdAsync(Guid id)
        {
            return await _context.Tracks.FindAsync(id);
        }

        public async Task<IEnumerable<Track>> GetAllAsync()
        {
            return await _context.Tracks.ToListAsync();
        }

        public async Task AddAsync(Track track)
        {
            if (track == null) throw new ArgumentNullException(nameof(track));
            await _context.Tracks.AddAsync(track);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var track = await _context.Tracks.FindAsync(id);
            if (track != null)
            {
                _context.Tracks.Remove(track);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Track>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return await GetAllAsync();

            query = query.ToLower();
            return await _context.Tracks
                .Where(t => t.Title.ToLower().Contains(query) || t.Artist.ToLower().Contains(query))
                .ToListAsync();
        }
    }
}
