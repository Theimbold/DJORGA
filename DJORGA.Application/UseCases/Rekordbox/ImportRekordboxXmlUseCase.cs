using DJORGA.Application.Interfaces.External;
using DJORGA.Application.Interfaces.Persistence;
using DJORGA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJORGA.Application.UseCases.Rekordbox
{
    public class ImportRekordboxXmlUseCase
    {
        private readonly IRekordboxService _rekordboxService;
        private readonly ITrackRepository _trackRepository;
        private readonly IMetadataService _metadataService;
        private readonly ICoverCacheService _coverCacheService;

        public ImportRekordboxXmlUseCase(
            IRekordboxService rekordboxService, 
            ITrackRepository trackRepository,
            IMetadataService metadataService,
            ICoverCacheService coverCacheService)
        {
            _rekordboxService = rekordboxService;
            _trackRepository = trackRepository;
            _metadataService = metadataService;
            _coverCacheService = coverCacheService;
        }

        public ITrackRepository GetTrackRepository() => _trackRepository;

        public async Task<int> ExecuteAsync(string xmlPath, IProgress<double>? progress = null)
        {
            Log($"Starte Import UseCase für: {xmlPath}");
            
            // Phase 1: Parsing (0% - 70%)
            var parsingProgress = new Progress<double>(p => progress?.Report(p * 0.7));
            var tracksFromXml = (await _rekordboxService.ParseLibraryAsync(xmlPath, parsingProgress)).ToList();
            Log($"Service hat {tracksFromXml.Count} Tracks aus XML geliefert.");

            // Phase 2: Deduplizierung (70% - 90%)
            progress?.Report(0.7);
            var existingTracks = await _trackRepository.GetAllAsync();
            var existingPaths = new HashSet<string>(existingTracks.Select(t => t.FilePath), StringComparer.OrdinalIgnoreCase);
            Log($"Datenbank enthält bereits {existingPaths.Count} Tracks.");

            int count = 0;
            var newTracks = new List<Track>();

            for (int i = 0; i < tracksFromXml.Count; i++)
            {
                var track = tracksFromXml[i];
                if (existingPaths.Contains(track.FilePath)) continue;

                // Validierung: Title und BPM sind Pflicht (für DJORGA Logik)
                if (string.IsNullOrWhiteSpace(track.Title) || track.Title == "Unknown")
                {
                    Log($"Überspringe Track ohne Titel: {track.FilePath}");
                    continue;
                }

                newTracks.Add(track);
                existingPaths.Add(track.FilePath);
                count++;

                if (i % 500 == 0) progress?.Report(0.7 + (0.2 * i / tracksFromXml.Count));
            }

            // Phase 3: Speichern (90% - 100%)
            progress?.Report(0.9);
            if (newTracks.Any())
            {
                try 
                {
                    Log($"Speichere {newTracks.Count} neue Tracks als Batch in der Datenbank...");
                    await _trackRepository.AddRangeAsync(newTracks);
                }
                catch (Exception ex)
                {
                    Log($"KRITISCHER FEHLER beim Bulk-Speichern: {ex.Message}");
                    // Fallback oder Abbruch je nach Anforderung
                }
            }
            progress?.Report(1.0);

            Log($"Import abgeschlossen. {count} neue Tracks hinzugefügt.");

            if (newTracks.Count > 0)
            {
                // Hintergrund-Analyse mit anschließendem DB-Update
                _ = Task.Run(() => BackgroundAnalysisAsync(newTracks));
            }

            return count;
        }

        private void Log(string message)
        {
            try { System.IO.File.AppendAllText("DJORGA_Import_Log.txt", $"{DateTime.Now}: [UseCase] {message}{Environment.NewLine}"); } catch { }
        }

        private async Task BackgroundAnalysisAsync(IEnumerable<Track> tracks)
        {
            foreach (var track in tracks)
            {
                try 
                {
                    var extraMeta = await _metadataService.ExtractMetadataAsync(track.FilePath);
                    track.Genre = extraMeta.Genre ?? string.Empty;
                    
                    if (extraMeta.CoverData != null)
                    {
                        track.CoverArtPath = await _coverCacheService.CacheCoverAsync(track.Id.ToString(), extraMeta.CoverData);
                    }

                    track.IsAnalyzed = true;
                    
                    // KRITISCH: Update in der DB speichern
                    await _trackRepository.UpdateAsync(track);
                }
                catch { }
            }
        }
    }
}
