using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Persistence
{
    /// <summary>
    /// Interface für den Zugriff auf Tracks.
    /// </summary>
    public interface ITrackRepository
    {
        Task<Track?> GetByIdAsync(Guid id);
        Task<IEnumerable<Track>> GetAllAsync();
        Task AddAsync(Track track);
        Task DeleteAsync(Guid id);
        
        /// <summary>
        /// Sucht nach Tracks basierend auf Metadaten.
        /// </summary>
        Task<IEnumerable<Track>> SearchAsync(string query);
    }
}
