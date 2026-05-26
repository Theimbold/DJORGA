using DJORGA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DJORGA.Application.Interfaces.Services
{
    /// <summary>
    /// Dienst zur Hintergrund-Analyse von Tracks (BPM, Key, Metadaten).
    /// </summary>
    public interface IBackgroundAnalysisService
    {
        /// <summary>
        /// Fügt Tracks zur Analyse-Warteschlange hinzu.
        /// </summary>
        void EnqueueTracks(IEnumerable<Track> tracks);

        /// <summary>
        /// Startet den Analyse-Prozess.
        /// </summary>
        void Start();

        /// <summary>
        /// Stoppt den Analyse-Prozess.
        /// </summary>
        void Stop();

        /// <summary>
        /// Wird ausgelöst, wenn ein Track erfolgreich analysiert wurde.
        /// </summary>
        event Action<Track>? TrackAnalyzed;

        /// <summary>
        /// Gibt die Anzahl der noch zu analysierenden Tracks zurück.
        /// </summary>
        int QueueCount { get; }

        /// <summary>
        /// Gibt an, ob der Dienst gerade arbeitet.
        /// </summary>
        bool IsRunning { get; }
    }
}
