using DJORGA.Application.Interfaces.Services;
using DJORGA.Domain.Entities;

namespace DJORGA.Application.DTOs
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
