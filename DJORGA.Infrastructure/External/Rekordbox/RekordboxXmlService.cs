using DJORGA.Application.Interfaces.External;
using DJORGA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DJORGA.Infrastructure.External.Rekordbox
{
    public class RekordboxXmlService : IRekordboxService, IRekordboxExportService
    {
        private void Log(string message)
        {
            try { File.AppendAllText("DJORGA_Import_Log.txt", $"{DateTime.Now}: {message}{Environment.NewLine}"); } catch { }
        }

        public async Task<IEnumerable<Track>> ParseLibraryAsync(string filePath, IProgress<double>? progress = null)
        {
            Log($"Starte XML-Parsing für: {filePath}");
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                Log("Datei nicht gefunden oder Pfad leer.");
                return Enumerable.Empty<Track>();
            }

            try
            {
                // Task 065: Automatisches Backup erstellen
                string backupPath = CreateBackup(filePath);
                Log($"Backup erstellt unter: {backupPath}");
            }
            catch (Exception ex)
            {
                Log($"WARNUNG: Backup konnte nicht erstellt werden: {ex.Message}");
                // Wir fahren trotzdem fort, aber loggen die Warnung
            }

            return await Task.Run(() =>
            {
                try
                {
                    // Task 067: Read-Only Lock (FileShare.Read)
                    using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    
                    // Task 066: Validierung der XML-Struktur (vereinfacht: XDocument.Load übernimmt das)
                    XDocument doc = XDocument.Load(stream);
                    var trackElements = doc.Descendants("TRACK").ToList();
                    int totalTracks = trackElements.Count;
                    Log($"Anzahl gefundener TRACK-Elemente in XML: {totalTracks}");

                    var tracks = new List<Track>();
                    int processedCount = 0;

                    foreach (var element in trackElements)
                    {
                        processedCount++;
                        if (processedCount % 100 == 0 || processedCount == totalTracks)
                        {
                            progress?.Report((double)processedCount / totalTracks);
                        }

                        string? location = (string?)element.Attribute("Location");
                        if (string.IsNullOrEmpty(location)) 
                        {
                            // In Rekordbox XML gibt es TRACKs in COLLECTION (mit Location) 
                            // und TRACKs in PLAYLISTS (nur Referenz via Key).
                            // Wir überspringen die Referenzen ohne Log-Spam.
                            continue;
                        }

                        string localPath = string.Empty;
                        try
                        {
                            // Robustes Pfad-Handling via Uri-Klasse
                            if (Uri.TryCreate(location, UriKind.Absolute, out var uri))
                            {
                                localPath = Uri.UnescapeDataString(uri.LocalPath);
                                
                                // Windows Korrektur: /C:/Music -> C:\Music
                                if (localPath.StartsWith("/") && localPath.Length > 2 && localPath[2] == ':')
                                {
                                    localPath = localPath.Substring(1);
                                }
                                localPath = localPath.Replace('/', Path.DirectorySeparatorChar);
                            }
                            else
                            {
                                // Fallback für Pfade, die keine validen URIs sind
                                localPath = location;
                            }
                        }
                        catch (Exception pathEx)
                        {
                            Log($"Fehler beim Dekodieren des Pfads {location}: {pathEx.Message}");
                        }

                        var track = new Track
                        {
                            Title = (string?)element.Attribute("Name") ?? "Unknown",
                            Artist = (string?)element.Attribute("Artist") ?? "Unknown",
                            Bpm = double.TryParse((string?)element.Attribute("AverageBpm")?.Value.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var bpm) ? bpm : 0,
                            Key = (string?)element.Attribute("Tonality") ?? string.Empty,
                            FilePath = localPath,
                            Album = (string?)element.Attribute("Album") ?? string.Empty
                        };

                        tracks.Add(track);
                    }

                    Log($"Erfolgreich konvertierte Tracks: {tracks.Count}");
                    return tracks;
                }
                catch (Exception ex)
                {
                    Log($"KRITISCHER FEHLER beim XML-Laden: {ex.Message}");
                    return Enumerable.Empty<Track>();
                }
            });
        }

        private string CreateBackup(string sourcePath)
        {
            var backupDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backups");
            if (!Directory.Exists(backupDir))
            {
                Directory.CreateDirectory(backupDir);
            }

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var fileName = Path.GetFileNameWithoutExtension(sourcePath);
            var extension = Path.GetExtension(sourcePath);
            var backupPath = Path.Combine(backupDir, $"{fileName}_backup_{timestamp}{extension}");

            File.Copy(sourcePath, backupPath, true);
            return backupPath;
        }

        public Task<IEnumerable<Playlist>> ParsePlaylistsAsync(string filePath) => Task.FromResult(Enumerable.Empty<Playlist>());

        public async Task ExportPlaylistsAsync(IEnumerable<Playlist> playlists, string targetPath)
        {
            Log($"Starte Export von {playlists.Count()} Playlisten nach: {targetPath}");

            await Task.Run(() =>
            {
                try
                {
                    var root = new XElement("DJONTOLOGY", new XAttribute("Version", "1.0.0"));
                    var product = new XElement("PRODUCT", new XAttribute("Name", "rekordbox"), new XAttribute("Version", "6.0.0"), new XAttribute("Company", "Pioneer DJ"));
                    root.Add(product);

                    var collection = new XElement("COLLECTION", new XAttribute("Entries", playlists.SelectMany(p => p.Items).Count()));
                    var playlistsRoot = new XElement("PLAYLISTS");
                    var rootNode = new XElement("NODE", new XAttribute("Type", "0"), new XAttribute("Name", "ROOT"), new XAttribute("Count", playlists.Count()));
                    
                    int trackIdCounter = 1;
                    var trackMap = new Dictionary<string, int>();

                    foreach (var playlist in playlists)
                    {
                        var playlistNode = new XElement("NODE", 
                            new XAttribute("Name", playlist.Name), 
                            new XAttribute("Type", "1"), 
                            new XAttribute("KeyType", "0"), 
                            new XAttribute("Entries", playlist.Items.Count));

                        foreach (var track in playlist.Items)
                        {
                            if (track == null) continue;

                            // Pfad in Rekordbox Format wandeln
                            string rbPath = track.FilePath.Replace(Path.DirectorySeparatorChar, '/');
                            if (!rbPath.StartsWith("file://"))
                            {
                                rbPath = "file://localhost/" + rbPath;
                            }

                            if (!trackMap.TryGetValue(track.FilePath, out int id))
                            {
                                id = trackIdCounter++;
                                trackMap[track.FilePath] = id;

                                var trackEntry = new XElement("TRACK",
                                    new XAttribute("TrackID", id),
                                    new XAttribute("Name", track.Title),
                                    new XAttribute("Artist", track.Artist),
                                    new XAttribute("Album", track.Album),
                                    new XAttribute("Genre", track.Genre),
                                    new XAttribute("Kind", Path.GetExtension(track.FilePath).Replace(".", "").ToUpper() + " File"),
                                    new XAttribute("Size", "0"),
                                    new XAttribute("TotalTime", "0"),
                                    new XAttribute("DiscNumber", "0"),
                                    new XAttribute("TrackNumber", "0"),
                                    new XAttribute("Year", "0"),
                                    new XAttribute("AverageBpm", track.Bpm.ToString("F2")),
                                    new XAttribute("DateAdded", DateTime.Now.ToString("yyyy-MM-dd")),
                                    new XAttribute("BitRate", "0"),
                                    new XAttribute("SampleRate", "0"),
                                    new XAttribute("Comments", "Exported from DJORGA"),
                                    new XAttribute("PlayCount", "0"),
                                    new XAttribute("Rating", "0"),
                                    new XAttribute("Location", rbPath),
                                    new XAttribute("Remixer", ""),
                                    new XAttribute("Tonality", track.Key),
                                    new XAttribute("Label", ""),
                                    new XAttribute("Mixer", "")
                                );
                                collection.Add(trackEntry);
                            }

                            playlistNode.Add(new XElement("TRACK", new XAttribute("Key", id)));
                        }
                        rootNode.Add(playlistNode);
                    }

                    playlistsRoot.Add(rootNode);
                    root.Add(collection);
                    root.Add(playlistsRoot);

                    var doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), root);
                    doc.Save(targetPath);
                    Log("Export erfolgreich abgeschlossen.");
                }
                catch (Exception ex)
                {
                    Log($"FEHLER beim Export: {ex.Message}");
                    throw;
                }
            });
        }
    }
}
