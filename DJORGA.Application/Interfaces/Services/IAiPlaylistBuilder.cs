using DJORGA.Domain.Entities;
using DJORGA.Application.DTOs;
using System.Collections.Generic;

namespace DJORGA.Application.Interfaces.Services
{
    /// <summary>
    /// Service zur automatischen Erstellung von harmonischen Playlists.
    /// </summary>
    public interface IAiPlaylistBuilder
    {
        /// <summary>
        /// Generiert eine Playlist-Sequenz aus einem Track-Pool.
        /// </summary>
        IEnumerable<ScoredTrack> CreateSequence(Track startTrack, IEnumerable<Track> pool, int length);
    }
}
