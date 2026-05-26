using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;
using MyApp.Domain.ValueObjects;
using System;

namespace MyApp.Application.Services
{
    /// <summary>
    /// Berechnet die Kompatibilität von Tracks basierend auf Camelot Key, BPM und Energie.
    /// </summary>
    public class HarmonicScoringService : IHarmonicScorer
    {
        private const double WeightKey = 0.30;
        private const double WeightBpm = 0.30;
        private const double WeightDna = 0.40;

        public double CalculateScore(Track from, Track to)
        {
            return CalculateBreakdown(from, to).TotalScore;
        }

        public ScoreBreakdown CalculateBreakdown(Track from, Track to)
        {
            double keyScore = CalculateKeyScore(from.Key, to.Key);
            double bpmScore = CalculateBpmScore(from.Bpm, to.Bpm);
            double dnaScore = CalculateDnaScore(from, to);

            double total = (keyScore * WeightKey) + (bpmScore * WeightBpm) + (dnaScore * WeightDna);

            return new ScoreBreakdown
            {
                TotalScore = total,
                KeyScore = keyScore,
                BpmScore = bpmScore,
                DnaScore = dnaScore
            };
        }

        private double CalculateDnaScore(Track from, Track to)
        {
            // Wenn beide Felder identisch sind (und nicht 'None'), perfekter Match
            if (from.Mood == to.Mood && from.TimeContext == to.TimeContext && from.Mood != TrackMood.None)
                return 1.0;

            // Ein Feld passt
            if ((from.Mood == to.Mood && from.Mood != TrackMood.None) || 
                (from.TimeContext == to.TimeContext && from.TimeContext != TrackTimeContext.None))
                return 0.7;

            // Beide gesetzt, aber keine Übereinstimmung
            if (from.Mood != TrackMood.None && to.Mood != TrackMood.None)
                return 0.3;

            return 0.1; // Wenig Info oder kein Match
        }

        private double CalculateKeyScore(string keyA, string keyB)
        {
            try
            {
                var camelotA = CamelotKey.Parse(keyA);
                var camelotB = CamelotKey.Parse(keyB);

                if (camelotA == camelotB) return 1.0;
                if (camelotA.IsCompatibleWith(camelotB)) return 0.8;

                return 0.2; // Nicht direkt kompatibel
            }
            catch
            {
                return 0.1; // Fehler beim Parsing (z.B. unbekanntes Format)
            }
        }

        private double CalculateBpmScore(double bpmA, double bpmB)
        {
            if (bpmA <= 0 || bpmB <= 0) return 0.0;

            double diff = Math.Abs(bpmA - bpmB);
            double percentDiff = (diff / bpmA) * 100;

            return percentDiff switch
            {
                <= 1.0 => 1.0,  // < 1% Abweichung
                <= 3.0 => 0.7,  // < 3% Abweichung
                <= 6.0 => 0.3,  // < 6% Abweichung
                _ => 0.0        // Zu weit auseinander
            };
        }
    }
}
