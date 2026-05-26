using DJORGA.Domain.Entities;

namespace DJORGA.Application.Interfaces.Services
{
    public struct ScoreBreakdown
    {
        public double TotalScore { get; set; }
        public double KeyScore { get; set; }
        public double BpmScore { get; set; }
        public double DnaScore { get; set; }
    }

    /// <summary>
    /// Definiert die Logik für das Scoring von Übergängen zwischen zwei Tracks.
    /// </summary>
    public interface IHarmonicScorer
    {
        /// <summary>
        /// Berechnet einen Score zwischen 0.0 (unpassend) und 1.0 (perfekt).
        /// </summary>
        double CalculateScore(Track from, Track to);

        /// <summary>
        /// Berechnet die detaillierte Aufschlüsselung des Scores.
        /// </summary>
        ScoreBreakdown CalculateBreakdown(Track from, Track to);
    }
}
