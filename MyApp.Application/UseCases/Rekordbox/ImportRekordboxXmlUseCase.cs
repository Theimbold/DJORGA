using MyApp.Application.Interfaces.External;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Application.UseCases.Rekordbox
{
    /// <summary>
    /// Use Case zum Importieren einer Rekordbox XML-Bibliothek.
    /// </summary>
    public class ImportRekordboxXmlUseCase
    {
        private readonly IRekordboxService _rekordboxService;
        private readonly ITrackRepository _trackRepository;

        public ImportRekordboxXmlUseCase(IRekordboxService rekordboxService, ITrackRepository trackRepository)
        {
            _rekordboxService = rekordboxService;
            _trackRepository = trackRepository;
        }

        /// <summary>
        /// Führt den Import aus.
        /// </summary>
        /// <param name="xmlPath">Pfad zur Rekordbox XML-Datei.</param>
        /// <returns>Anzahl der erfolgreich importierten Tracks.</returns>
        public async Task<int> ExecuteAsync(string xmlPath)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
                throw new ArgumentException("XML-Pfad darf nicht leer sein.", nameof(xmlPath));

            // 1. XML parsen
            var tracks = await _rekordboxService.ParseLibraryAsync(xmlPath);
            int count = 0;

            // 2. Tracks validieren und speichern
            foreach (var track in tracks)
            {
                if (track.IsValid())
                {
                    // TODO: Hier könnte ein Dubletten-Check eingebaut werden
                    await _trackRepository.AddAsync(track);
                    count++;
                }
            }

            return count;
        }
    }
}
