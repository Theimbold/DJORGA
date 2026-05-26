using DJORGA.Application.DTOs;
using DJORGA.Application.Interfaces.External;
using System;
using System.Linq;
using System.Threading.Tasks;
using TagLib;

namespace DJORGA.Infrastructure.External.Metadata
{
    /// <summary>
    /// Implementierung der Metadaten-Extraktion unter Verwendung von TagLib#.
    /// Unterstützt .mp3, .flac, .wav, .aiff, .m4a etc.
    /// </summary>
    public class TagLibMetadataService : IMetadataService
    {
        public Task<TrackMetadata> ExtractMetadataAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Dateipfad darf nicht leer sein.", nameof(filePath));

            try
            {
                // TagLib# arbeitet synchron, wir kapseln es für das Interface in einen Task
                using var file = TagLib.File.Create(filePath);
                
                var tags = file.Tag;
                var properties = file.Properties;

                var metadata = new TrackMetadata
                {
                    Title = tags.Title,
                    Artist = tags.FirstPerformer ?? tags.FirstAlbumArtist,
                    Album = tags.Album,
                    Genre = tags.FirstGenre,
                    Bpm = tags.BeatsPerMinute > 0 ? tags.BeatsPerMinute : (double?)null,
                    Key = tags.InitialKey,
                    Duration = properties.Duration,
                    
                    // Cover-Art Extraktion
                    CoverData = tags.Pictures.FirstOrDefault()?.Data.Data,
                    MimeType = tags.Pictures.FirstOrDefault()?.MimeType
                };

                return Task.FromResult(metadata);
            }
            catch (Exception ex)
            {
                // Bei Fehlern (z.B. Dateiformat nicht unterstützt) leeres DTO zurückgeben
                // TODO: Logging implementieren
                return Task.FromResult(new TrackMetadata());
            }
        }

        public Task UpdateMetadataAsync(string filePath, TrackMetadata metadata)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Dateipfad darf nicht leer sein.", nameof(filePath));

            return Task.Run(() =>
            {
                try
                {
                    using var file = TagLib.File.Create(filePath);
                    
                    file.Tag.Title = metadata.Title;
                    file.Tag.Performers = new[] { metadata.Artist };
                    file.Tag.Album = metadata.Album;
                    file.Tag.Genres = new[] { metadata.Genre };
                    file.Tag.BeatsPerMinute = (uint)(metadata.Bpm ?? 0);
                    file.Tag.InitialKey = metadata.Key;

                    file.Save();
                }
                catch (Exception ex)
                {
                    // TODO: Logging
                    throw new IOException($"Fehler beim Schreiben der Metadaten in {filePath}: {ex.Message}", ex);
                }
            });
        }
    }
}
