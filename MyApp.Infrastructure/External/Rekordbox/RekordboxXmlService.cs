using MyApp.Application.Interfaces.External;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;

namespace MyApp.Infrastructure.External.Rekordbox
{
    /// <summary>
    /// Implementierung des Rekordbox-Imports mittels XML-Dateien.
    /// </summary>
    public class RekordboxXmlService : IRekordboxService
    {
        public Task<IEnumerable<Track>> ParseLibraryAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Pfad darf nicht leer sein.", nameof(filePath));

            var tracks = new List<Track>();

            try
            {
                XDocument doc = XDocument.Load(filePath);
                
                // Parsing der COLLECTION Sektion
                var trackElements = doc.Descendants("TRACK");

                foreach (var element in trackElements)
                {
                    var track = new Track
                    {
                        Title = (string?)element.Attribute("Name") ?? "Unknown",
                        Artist = (string?)element.Attribute("Artist") ?? "Unknown",
                        Album = (string?)element.Attribute("Album") ?? string.Empty,
                        Bpm = double.TryParse((string?)element.Attribute("AverageBpm"), out var bpm) ? bpm : 0,
                        Key = (string?)element.Attribute("Tonality") ?? string.Empty,
                        FilePath = (string?)element.Attribute("Location") ?? string.Empty,
                        Duration = TimeSpan.FromSeconds(double.TryParse((string?)element.Attribute("TotalTime"), out var sec) ? sec : 0)
                    };

                    // Camelot-Key Konvertierung (Beispielhaft, kann später verfeinert werden)
                    track.CamelotKey = track.Key; 

                    tracks.Add(track);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler beim Lesen der Rekordbox XML: {ex.Message}", ex);
            }

            return Task.FromResult(tracks.AsEnumerable());
        }

        public Task<IEnumerable<Playlist>> ParsePlaylistsAsync(string filePath)
        {
            var playlists = new List<Playlist>();

            try
            {
                XDocument doc = XDocument.Load(filePath);
                var playlistElements = doc.Descendants("NODE").Where(n => (string?)n.Attribute("Type") == "1");

                foreach (var element in playlistElements)
                {
                    var playlist = new Playlist
                    {
                        Name = (string?)element.Attribute("Name") ?? "Unnamed Playlist"
                    };

                    // Tracks der Playlist extrahieren
                    var trackKeys = element.Descendants("TRACK").Select(t => (string?)t.Attribute("Key"));
                    // Hinweis: Die Verknüpfung der Tracks erfolgt später im UseCase über die Track-IDs.
                    
                    playlists.Add(playlist);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler beim Lesen der Rekordbox Playlists: {ex.Message}", ex);
            }

            return Task.FromResult(playlists.AsEnumerable());
        }
    }
}
