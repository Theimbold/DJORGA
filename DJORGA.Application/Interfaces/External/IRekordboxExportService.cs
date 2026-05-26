using DJORGA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DJORGA.Application.Interfaces.External
{
    /// <summary>
    /// Service zum Exportieren von Daten in das Rekordbox XML-Format.
    /// </summary>
    public interface IRekordboxExportService
    {
        /// <summary>
        /// Exportiert die angegebenen Playlists und deren Tracks in eine XML-Datei.
        /// </summary>
        /// <param name="playlists">Die zu exportierenden Playlists.</param>
        /// <param name="targetPath">Der Zielpfad für die XML-Datei.</param>
        Task ExportPlaylistsAsync(IEnumerable<Playlist> playlists, string targetPath);
    }
}
