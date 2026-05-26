using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces.External;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Services
{
    /// <summary>
    /// Implementiert die Hintergrund-Analyse von Tracks mittels einer Queue.
    /// </summary>
    public class BackgroundAnalysisService : IBackgroundAnalysisService
    {
        private readonly IMetadataService _metadataService;
        private readonly ICoverCacheService _coverCacheService;
        private readonly IWaveformService _waveformService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ConcurrentQueue<Track> _queue = new();
        private CancellationTokenSource? _cts;
        private Task? _workerTask;

        public event Action<Track>? TrackAnalyzed;

        public int QueueCount => _queue.Count;
        public bool IsRunning => _workerTask != null && !_workerTask.IsCompleted;

        public BackgroundAnalysisService(
            IMetadataService metadataService, 
            ICoverCacheService coverCacheService,
            IWaveformService waveformService,
            IServiceScopeFactory scopeFactory)
        {
            _metadataService = metadataService;
            _coverCacheService = coverCacheService;
            _waveformService = waveformService;
            _scopeFactory = scopeFactory;
        }

        public void EnqueueTracks(IEnumerable<Track> tracks)
        {
            foreach (var track in tracks.Where(t => !t.IsAnalyzed))
            {
                // Schneller Check ob bereits in Queue (Iteriert zwar, aber ConcurrentQueue hat kein Contains)
                if (!_queue.Any(t => t.Id == track.Id))
                {
                    _queue.Enqueue(track);
                }
            }
        }

        public void Start()
        {
            if (IsRunning) return;

            _cts = new CancellationTokenSource();
            _workerTask = Task.Run(() => ProcessQueueAsync(_cts.Token));
        }

        public void Stop()
        {
            _cts?.Cancel();
        }

        private async Task ProcessQueueAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                if (_queue.TryDequeue(out var track))
                {
                    try
                    {
                        // 1. Metadaten extrahieren (BPM, Key aus Tags)
                        var metadata = await _metadataService.ExtractMetadataAsync(track.FilePath);
                        
                        // 2. Metadaten in Track übernehmen
                        track.Bpm = metadata.Bpm > 0 ? metadata.Bpm.Value : track.Bpm;
                        track.Key = !string.IsNullOrEmpty(metadata.Key) ? metadata.Key : track.Key;
                        track.Title = string.IsNullOrEmpty(track.Title) && !string.IsNullOrEmpty(metadata.Title) ? metadata.Title : track.Title;
                        track.Artist = string.IsNullOrEmpty(track.Artist) && !string.IsNullOrEmpty(metadata.Artist) ? metadata.Artist : track.Artist;
                        track.Genre = string.IsNullOrEmpty(track.Genre) && !string.IsNullOrEmpty(metadata.Genre) ? metadata.Genre : track.Genre;

                        // 3. Cover-Art cachen
                        if (metadata.CoverData != null && metadata.CoverData.Length > 0)
                        {
                            track.CoverArtPath = await _coverCacheService.CacheCoverAsync(track.Id.ToString(), metadata.CoverData);
                        }

                        // 4. Waveform Peaks generieren (wird automatisch gecached)
                        await _waveformService.GetPeaksAsync(track.Id.ToString(), track.FilePath);
                        
                        track.IsAnalyzed = true;

                        // 5. In Repository speichern (Scope für DB-Context)
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var repo = scope.ServiceProvider.GetRequiredService<ITrackRepository>();
                            await repo.UpdateAsync(track);
                        }

                        // 6. Event feuern
                        TrackAnalyzed?.Invoke(track);
                    }
                    catch (Exception)
                    {
                        // TODO: Logging
                    }
                }
                else
                {
                    // Warteschlange leer - kurz warten
                    await Task.Delay(1000, ct);
                }
            }
        }
    }
}
}
