using System;
using DJORGA.Domain.ValueObjects;

namespace DJORGA.Domain.Entities
{
    /// <summary>
    /// Repräsentiert einen Track in der Musikbibliothek.
    /// Reine Datenklasse ohne UI-Abhängigkeiten (Clean Architecture — Dependency Rule).
    /// UI-Binding-Logik gehört in TrackViewModel im Desktop-Layer.
    /// </summary>
    public class Track
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public TrackMood Mood { get; set; } = TrackMood.None;
        public TrackTimeContext TimeContext { get; set; } = TrackTimeContext.None;
        public string CoverArtPath { get; set; } = string.Empty;
        public bool IsAnalyzed { get; set; }
        public double Bpm { get; set; }
        public string Key { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime ImportedAt { get; set; } = DateTime.UtcNow;

        /// <summary>Domänenregel: Ein Track ist valide wenn Titel und BPM vorhanden sind.</summary>
        public bool IsValid() => !string.IsNullOrWhiteSpace(Title) && Bpm > 0;
    }
}
