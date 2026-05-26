using DJORGA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DJORGA.Application.Interfaces.External
{
    /// <summary>
    /// Interface für den Import von Musikdaten aus Rekordbox (XML).
    /// </summary>
    public interface IRekordboxService
    {
        /// <summary>
        /// Liest die gesamte Rekordbox-Bibliothek aus einer XML-Datei ein.
        /// </summary>
        /// <param name="filePath">Pfad zur Rekordbox XML-Datei.</param>
        /// <param name="progress">Optionales Progress-Objekt für den Fortschritt (0.0 bis 1.0).</param>
        /// <returns>Eine Liste der eingelesenen Tracks.</returns>
        Task<IEnumerable<Track>> ParseLibraryAsync(string filePath, IProgress<double>? progress = null);

        /// <summary>
        /// Liest spezifische Playlists aus der Rekordbox XML-Datei ein.
        /// </summary>
        Task<IEnumerable<Playlist>> ParsePlaylistsAsync(string filePath);
    }
}
