using System;

namespace MyApp.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object für extrahierte Datei-Metadaten.
    /// </summary>
    public record TrackMetadata
    {
        public string? Title { get; init; }
        public string? Artist { get; init; }
        public string? Album { get; init; }
        public string? Genre { get; init; }
        public double? Bpm { get; init; }
        public string? Key { get; init; }
        public TimeSpan Duration { get; init; }
        public byte[]? CoverData { get; init; }
        public string? MimeType { get; init; }
    }
}
