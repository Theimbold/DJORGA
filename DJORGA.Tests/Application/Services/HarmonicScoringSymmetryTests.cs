using DJORGA.Application.Services;
using DJORGA.Domain.Entities;
using DJORGA.Domain.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace DJORGA.Tests.Application.Services
{
    public class HarmonicScoringSymmetryTests
    {
        private static HarmonicScoringService Build(ScoringWeights? weights = null) =>
            new(weights ?? new ScoringWeights(), NullLogger<HarmonicScoringService>.Instance);

        [Fact]
        public void CalculateScore_IsSymmetric_SameBpm()
        {
            var svc = Build();
            var t1 = new Track { Key = "8A", Bpm = 128, Mood = TrackMood.Energetic };
            var t2 = new Track { Key = "9A", Bpm = 124, Mood = TrackMood.Hypnotic };

            Assert.Equal(svc.CalculateScore(t1, t2), svc.CalculateScore(t2, t1), 10);
        }

        [Fact]
        public void CalculateScore_IsSymmetric_DifferentBpm()
        {
            var svc = Build();
            var t1 = new Track { Key = "1B", Bpm = 100 };
            var t2 = new Track { Key = "1B", Bpm = 140 };

            Assert.Equal(svc.CalculateScore(t1, t2), svc.CalculateScore(t2, t1), 10);
        }

        [Fact]
        public void CalculateScore_CustomWeights_OnlyKeyContributes()
        {
            var weights = new ScoringWeights { KeyWeight = 1.0, BpmWeight = 0.0, DnaWeight = 0.0 };
            var svc = Build(weights);

            // Perfect key match, terrible BPM + DNA — score should equal key score (1.0)
            var t1 = new Track { Key = "5A", Bpm = 80, Mood = TrackMood.Melancholic };
            var t2 = new Track { Key = "5A", Bpm = 200, Mood = TrackMood.Energetic };

            Assert.Equal(1.0, svc.CalculateScore(t1, t2), 10);
        }

        [Fact]
        public void CalculateScore_CustomWeights_OnlyBpmContributes()
        {
            var weights = new ScoringWeights { KeyWeight = 0.0, BpmWeight = 1.0, DnaWeight = 0.0 };
            var svc = Build(weights);

            // Identical BPM → BPM score 1.0; key mismatch and DNA mismatch don't count
            var t1 = new Track { Key = "1A", Bpm = 128, Mood = TrackMood.Melancholic };
            var t2 = new Track { Key = "7B", Bpm = 128, Mood = TrackMood.Energetic };

            Assert.Equal(1.0, svc.CalculateScore(t1, t2), 10);
        }
    }
}
