using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;

namespace MyApp.Application.DTOs
{
    /// <summary>
    /// Kombiniert einen Track mit seiner Scoring-Aufschlüsselung für den AI Builder.
    /// </summary>
    public class ScoredTrack
    {
        public Track Track { get; set; } = null!;
        public ScoreBreakdown Breakdown { get; set; }
    }
}
