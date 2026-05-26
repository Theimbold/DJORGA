using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces.Services
{
    public struct ScoreBreakdown
    {
        public double TotalScore { get; set; }
        public double KeyScore { get; set; }
        public double BpmScore { get; set; }
        public double DnaScore { get; set; }

        public double KeyWeight => 0.30;
        public double BpmWeight => 0.30;
        public double DnaWeight => 0.40;
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
