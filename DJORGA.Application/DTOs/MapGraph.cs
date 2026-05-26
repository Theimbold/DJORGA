using System;

namespace DJORGA.Application.DTOs
{
    /// <summary>
    /// Repräsentiert einen Knoten in der Harmonic Map.
    /// </summary>
    public class MapNode
    {
        public Guid TrackId { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Artist { get; init; } = string.Empty;
        public string Key { get; init; } = string.Empty;
        
        // Position im 2D-Raum (wird vom Layout-Service berechnet)
        public double X { get; set; }
        public double Y { get; set; }
        
        // Visuelle Metriken
        public float Radius { get; set; } = 10f;
        public string Color { get; set; } = "#FFFFFF";
    }

    /// <summary>
    /// Repräsentiert eine Verbindung (Edge) zwischen zwei harmonisch kompatiblen Tracks.
    /// </summary>
    public class MapEdge
    {
        public Guid SourceId { get; init; }
        public Guid TargetId { get; init; }
        public double Strength { get; init; } // 0.0 bis 1.0 (basiert auf Harmonic Score)
    }
}
