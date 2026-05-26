using DJORGA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DJORGA.Application.Interfaces.Persistence
{
    /// <summary>
    /// Interface für den Zugriff auf Playlists.
    /// </summary>
    public interface IPlaylistRepository
    {
        Task<Playlist?> GetByIdAsync(Guid id);
        Task<IEnumerable<Playlist>> GetAllAsync();
        Task AddAsync(Playlist playlist);
        Task UpdateAsync(Playlist playlist);
        Task DeleteAsync(Guid id);
    }
}
