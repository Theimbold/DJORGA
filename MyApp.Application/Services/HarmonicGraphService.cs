using MyApp.Application.DTOs;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;
using MyApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp.Application.Services
{
    /// <summary>
    /// Berechnet die Struktur der Harmonic Map (Knoten und Kanten).
    /// </summary>
    public class HarmonicGraphService
    {
        private readonly IHarmonicScorer _scorer;

        public HarmonicGraphService(IHarmonicScorer scorer)
        {
            _scorer = scorer;
        }

        public (List<MapNode> Nodes, List<MapEdge> Edges) BuildGraph(IEnumerable<Track> tracks)
        {
            var trackList = tracks.ToList();
            var nodes = trackList.Select(t => new MapNode
            {
                TrackId = t.Id,
                Title = t.Title,
                Artist = t.Artist,
                Key = t.Key,
                Color = GetColorForKey(t.Key)
            }).ToList();

            var edges = new List<MapEdge>();

            // Wir prüfen jeden Track gegen jeden anderen (N^2 Komplexität)
            // Bei sehr großen Bibliotheken (> 2000) müsste man hier räumliches Indexing nutzen.
            for (int i = 0; i < trackList.Count; i++)
            {
                for (int j = i + 1; j < trackList.Count; j++)
                {
                    var t1 = trackList[i];
                    var t2 = trackList[j];

                    double score = _scorer.CalculateScore(t1, t2);

                    // Nur starke harmonische Verbindungen anzeigen
                    if (score > 0.7)
                    {
                        edges.Add(new MapEdge
                        {
                            SourceId = t1.Id,
                            TargetId = t2.Id,
                            Strength = score
                        });
                    }
                }
            }

            return (nodes, edges);
        }

        private string GetColorForKey(string key)
        {
            try
            {
                var camelot = CamelotKey.Parse(key);
                // Farbpalette basierend auf dem Circle of Fifths (HSL-basiert)
                // Wir nutzen hier feste Farben für die Haupt-Keys
                return camelot.Number switch
                {
                    1 => camelot.Mode == 'A' ? "#00FF00" : "#80FF80", // 1A/1B (Green)
                    2 => "#00FF80",
                    3 => "#00FFFF",
                    4 => "#0080FF",
                    5 => "#0000FF",
                    6 => "#8000FF",
                    7 => "#FF00FF",
                    8 => "#FF0080",
                    9 => "#FF0000",
                    10 => "#FF8000",
                    11 => "#FFFF00",
                    12 => "#80FF00",
                    _ => "#FFFFFF"
                };
            }
            catch { return "#FFFFFF"; }
        }
    }
}
