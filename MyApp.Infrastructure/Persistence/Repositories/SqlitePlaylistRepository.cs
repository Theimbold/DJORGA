using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Persistence.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// SQLite-Implementierung des Playlist-Repositories.
    /// </summary>
    public class SqlitePlaylistRepository : IPlaylistRepository
    {
        private readonly AppDbContext _context;

        public SqlitePlaylistRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Playlist?> GetByIdAsync(Guid id)
        {
            return await _context.Playlists
                .Include(p => p.Items)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync()
        {
            return await _context.Playlists
                .Include(p => p.Items)
                .ToListAsync();
        }

        public async Task AddAsync(Playlist playlist)
        {
            if (playlist == null) throw new ArgumentNullException(nameof(playlist));
            await _context.Playlists.AddAsync(playlist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Playlist playlist)
        {
            if (playlist == null) throw new ArgumentNullException(nameof(playlist));
            _context.Playlists.Update(playlist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();
            }
        }
    }
}
