using DJORGA.Application.DTOs;
using DJORGA.Application.Interfaces.External;
using DJORGA.Application.Interfaces.Persistence;
using DJORGA.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DJORGA.Application.UseCases.Library
{
    /// <summary>
    /// UseCase zur Aktualisierung von Track-Metadaten in DB und physischer Datei.
    /// </summary>
    public class UpdateTrackMetadataUseCase
    {
        private readonly ITrackRepository _repository;
        private readonly IMetadataService _metadataService;

        public UpdateTrackMetadataUseCase(ITrackRepository repository, IMetadataService metadataService)
        {
            _repository = repository;
            _metadataService = metadataService;
        }

        public async Task ExecuteAsync(Track track)
        {
            if (track == null) throw new ArgumentNullException(nameof(track));

            // 1. In Datenbank speichern
            await _repository.UpdateAsync(track);

            // 2. In physische Datei schreiben (Sync)
            try
            {
                var dto = new TrackMetadata
                {
                    Title = track.Title,
                    Artist = track.Artist,
                    Album = track.Album,
                    Genre = track.Genre,
                    Bpm = track.Bpm,
                    Key = track.Key
                };

                await _metadataService.UpdateMetadataAsync(track.FilePath, dto);
            }
            catch (Exception ex)
            {
                // Hier könnten wir entscheiden, ob wir bei einem Datei-Fehler 
                // die DB-Änderung rückgängig machen oder nur loggen.
                // Für DJs ist die DB oft wichtiger als die Datei-Tags.
                // TODO: Logging
            }
        }
    }
}
