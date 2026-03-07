using Core;
using System;

namespace Services
{
    public static class HarmonicLinkScorer
    {
        public static double ScoreTransition(TrackAnalysis a, TrackAnalysis b, double? bpmA, double? bpmB)
        {
            double score = 0;

            if (a.CamelotKey == b.CamelotKey)
                score += 0.45;
            else if (AreNeighborKeys(a.CamelotKey, b.CamelotKey))
                score += 0.30;
            else if (AreRelativeKeys(a.CamelotKey, b.CamelotKey))
                score += 0.20;

            if (bpmA.HasValue && bpmB.HasValue)
            {
                var diff = Math.Abs(bpmA.Value - bpmB.Value);
                score += diff switch
                {
                    <= 1 => 0.25,
                    <= 3 => 0.15,
                    <= 5 => 0.08,
                    _ => 0
                };
            }

            var energyDiff = Math.Abs(a.Energy - b.Energy);
            score += energyDiff switch
            {
                0 => 0.20,
                1 => 0.12,
                2 => 0.05,
                _ => -0.05
            };

            if (a.Mood == b.Mood)
                score += 0.10;

            return Math.Clamp(score, 0, 1);
        }

        private static bool AreNeighborKeys(string keyA, string keyB)
        {
            // Implement logic to check if keys are neighbors
            return false;
        }

        private static bool AreRelativeKeys(string keyA, string keyB)
        {
            // Implement logic to check if keys are relative
            return false;
        }
    }
}