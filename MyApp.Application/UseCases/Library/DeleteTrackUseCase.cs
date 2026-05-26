using MyApp.Application.Interfaces.Persistence;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyApp.Application.UseCases.Library
{
    /// <summary>
    /// UseCase zum Entfernen eines Tracks.
    /// </summary>
    public class DeleteTrackUseCase
    {
        private readonly ITrackRepository _repository;

        public DeleteTrackUseCase(ITrackRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Löscht einen Track aus der Datenbank und optional vom Datenträger.
        /// </summary>
        public async Task ExecuteAsync(Guid trackId, bool deletePhysicalFile = false)
        {
            var track = await _repository.GetByIdAsync(trackId);
            if (track == null) return;

            // 1. Aus Datenbank entfernen
            await _repository.DeleteAsync(trackId);

            // 2. Optional: Physische Datei löschen
            if (deletePhysicalFile && File.Exists(track.FilePath))
            {
                try
                {
                    File.Delete(track.FilePath);
                }
                catch
                {
                    // TODO: Logging
                    // Fehler beim Dateilöschen sollte den DB-Erfolg nicht verhindern.
                }
            }
        }
    }
}
