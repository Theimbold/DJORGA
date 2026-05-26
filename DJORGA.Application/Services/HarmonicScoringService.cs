using DJORGA.Application.Interfaces.Services;
using DJORGA.Domain.Entities;
using DJORGA.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;

namespace DJORGA.Application.Services
{
    public class HarmonicScoringService : IHarmonicScorer
    {
        private readonly ScoringWeights _weights;
        private readonly ILogger<HarmonicScoringService> _logger;

        public HarmonicScoringService(ScoringWeights weights, ILogger<HarmonicScoringService> logger)
        {
            _weights = weights;
            _logger = logger;
        }

        public double CalculateScore(Track from, Track to)
        {
            return CalculateBreakdown(from, to).TotalScore;
        }

        public ScoreBreakdown CalculateBreakdown(Track from, Track to)
        {
            double keyScore = CalculateKeyScore(from.Key, to.Key);
            double bpmScore = CalculateBpmScore(from.Bpm, to.Bpm);
            double dnaScore = CalculateDnaScore(from, to);

            double total = (keyScore * _weights.KeyWeight)
                         + (bpmScore * _weights.BpmWeight)
                         + (dnaScore * _weights.DnaWeight);

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
            if (from.Mood == to.Mood && from.TimeContext == to.TimeContext && from.Mood != TrackMood.None)
                return 1.0;

            if ((from.Mood == to.Mood && from.Mood != TrackMood.None) ||
                (from.TimeContext == to.TimeContext && from.TimeContext != TrackTimeContext.None))
                return 0.7;

            if (from.Mood != TrackMood.None && to.Mood != TrackMood.None)
                return 0.3;

            return 0.1;
        }

        private double CalculateKeyScore(string keyA, string keyB)
        {
            try
            {
                var camelotA = CamelotKey.Parse(keyA);
                var camelotB = CamelotKey.Parse(keyB);

                if (camelotA == camelotB) return 1.0;
                if (camelotA.IsCompatibleWith(camelotB)) return 0.8;

                return 0.2;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not parse Camelot key (keyA={KeyA}, keyB={KeyB}); defaulting key score to 0.1.", keyA, keyB);
                return 0.1;
            }
        }

        private double CalculateBpmScore(double bpmA, double bpmB)
        {
            if (bpmA <= 0 || bpmB <= 0) return 0.0;

            double diff = Math.Abs(bpmA - bpmB);
            double percentDiff = (diff / ((bpmA + bpmB) / 2.0)) * 100;

            return percentDiff switch
            {
                <= 1.0 => 1.0,
                <= 3.0 => 0.7,
                <= 6.0 => 0.3,
                _ => 0.0
            };
        }
    }
}
