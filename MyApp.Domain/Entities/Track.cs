using System;

namespace MyApp.Domain.Entities
{
    /// <summary>
    /// Repräsentiert ein einzelnes Musikstück in der DJ-Bibliothek.
    /// </summary>
    public class Track
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public double Bpm { get; set; }
        public string Key { get; set; } = string.Empty;
        public string CamelotKey { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public DateTime ImportedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Validiert die Grunddaten des Tracks.
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Title) && Bpm > 0;
        }
    }
}
