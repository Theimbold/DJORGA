using DJORGA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DJORGA.Application.Interfaces.Persistence
{
    public interface ITrackRepository
    {
        Task<Track?> GetByIdAsync(Guid id);
        Task<IEnumerable<Track>> GetAllAsync();
        Task AddAsync(Track track);
        Task AddRangeAsync(IEnumerable<Track> tracks); // Bulk Insert
        Task UpdateAsync(Track track);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Track>> SearchAsync(string query);
        Task<IEnumerable<Track>> GetUnanalyzedAsync();
        Task<IEnumerable<Track>> GetOrphansAsync();
        Task<int> GetCountAsync();

        // Neue Events für reaktive UI
        event Action<Track>? TrackAdded;
        event Action<Track>? TrackUpdated;
        event Action<IEnumerable<Track>>? BulkTracksAdded; // Bulk-Update Event
    }
}
