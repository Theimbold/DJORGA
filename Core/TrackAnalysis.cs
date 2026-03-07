using System;
using System.Collections.Generic;

namespace Core
{
    public sealed class TrackAnalysis
    {
        public string TrackId { get; init; } = string.Empty;
        public int Energy { get; init; }            // 1..5
        public int Danceability { get; init; }      // 1..5
        public int Darkness { get; init; }          // 1..5
        public int Warmth { get; init; }            // 1..5
        public string Mood { get; init; } = string.Empty;
        public string SetPhase { get; init; } = string.Empty; // warmup / peak / closing
        public string CamelotKey { get; init; } = string.Empty;
    }
}