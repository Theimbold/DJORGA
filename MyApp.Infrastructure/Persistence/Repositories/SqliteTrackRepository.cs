using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Persistence.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Persistence.Repositories
{
    public class SqliteTrackRepository : ITrackRepository
    {
        private readonly AppDbContext _context;

        public event Action<Track>? TrackAdded;
        public event Action<Track>? TrackUpdated;
        public event Action<IEnumerable<Track>>? BulkTracksAdded;

        public SqliteTrackRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Track?> GetByIdAsync(Guid id) => await _context.Tracks.FindAsync(id);
        public async Task<IEnumerable<Track>> GetAllAsync() => await _context.Tracks.ToListAsync();

        public async Task AddAsync(Track track) 
        { 
            await _context.Tracks.AddAsync(track); 
            await _context.SaveChangesAsync(); 
            TrackAdded?.Invoke(track);
        }

        public async Task AddRangeAsync(IEnumerable<Track> tracks)
        {
            await _context.Tracks.AddRangeAsync(tracks);
            await _context.SaveChangesAsync();
            BulkTracksAdded?.Invoke(tracks);
        }
        
        public async Task UpdateAsync(Track track) 
        { 
            _context.Tracks.Update(track); 
            await _context.SaveChangesAsync(); 
            TrackUpdated?.Invoke(track);
        }

        public async Task DeleteAsync(Guid id)
        {
            var track = await _context.Tracks.FindAsync(id);
            if (track != null) { _context.Tracks.Remove(track); await _context.SaveChangesAsync(); }
        }

        public async Task<IEnumerable<Track>> GetUnanalyzedAsync()
        {
            return await _context.Tracks
                .Where(t => !t.IsAnalyzed)
                .ToListAsync();
        }

        public async Task<IEnumerable<Track>> GetOrphansAsync()
        {
            // Tracks ohne PlaylistId-Schatteneigenschaft
            return await _context.Tracks
                .Where(t => EF.Property<Guid?>(t, "PlaylistId") == null)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync() => await _context.Tracks.CountAsync();

        public async Task<IEnumerable<Track>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return await GetAllAsync();

            var q = query.ToLower();
            return await _context.Tracks
                .Where(t => t.Title.ToLower().Contains(q) || 
                           t.Artist.ToLower().Contains(q) || 
                           t.Genre.ToLower().Contains(q) ||
                           t.Album.ToLower().Contains(q))
                .ToListAsync();
        }
    }
}
