using MyApp.Application.Interfaces.External;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Application.Services;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Application.UseCases.Library
{
    /// <summary>
    /// UseCase zum Exportieren einer Smart Collection als Rekordbox XML.
    /// </summary>
    public class ExportSmartCollectionUseCase
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IRekordboxExportService _exportService;
        private readonly RuleEvaluatorService _ruleEvaluator;

        public ExportSmartCollectionUseCase(
            ITrackRepository trackRepository, 
            IRekordboxExportService exportService,
            RuleEvaluatorService ruleEvaluator)
        {
            _trackRepository = trackRepository;
            _exportService = exportService;
            _ruleEvaluator = ruleEvaluator;
        }

        public async Task ExecuteAsync(SmartCollection collection, string targetPath)
        {
            // 1. Alle Tracks laden
            var allTracks = await _trackRepository.GetAllAsync();
            
            // 2. Regeln anwenden
            var filteredTracks = _ruleEvaluator.ApplyRules(allTracks.AsQueryable(), collection.Rules, collection.MatchAllRules).ToList();

            // 3. In temporäres Playlist-Objekt für den Export wandeln
            var playlist = new Playlist
            {
                Name = collection.Name,
                Items = filteredTracks
            };

            // 4. Exportieren
            await _exportService.ExportPlaylistsAsync(new List<Playlist> { playlist }, targetPath);
        }
    }
}
