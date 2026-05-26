using DJORGA.Application.Services;
using DJORGA.Domain.Entities;
using DJORGA.Domain.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace DJORGA.Tests.Application.Services
{
    public class HarmonicScoringServiceTests
    {
        private readonly HarmonicScoringService _service =
            new(new ScoringWeights(), NullLogger<HarmonicScoringService>.Instance);

        [Fact]
        public void CalculateScore_IdenticalTracks_FullDna_ShouldReturnPerfectScore()
        {
            var t1 = new Track { Key = "8A", Bpm = 124, Mood = TrackMood.Energetic, TimeContext = TrackTimeContext.PeakTime };
            var t2 = new Track { Key = "8A", Bpm = 124, Mood = TrackMood.Energetic, TimeContext = TrackTimeContext.PeakTime };

            var score = _service.CalculateScore(t1, t2);
            Assert.Equal(1.0, score, 2);
        }

        [Fact]
        public void CalculateScore_CompatibleKey_CloseBpm_MatchingMood_ShouldReturnGoodScore()
        {
            var t1 = new Track { Key = "8A", Bpm = 124, Mood = TrackMood.Hypnotic };
            var t2 = new Track { Key = "9A", Bpm = 125, Mood = TrackMood.Hypnotic }; 

            var score = _service.CalculateScore(t1, t2);
            // Key: 0.8*0.3=0.24, BPM: 1.0*0.3=0.3, DNA (Mood): 0.7*0.4=0.28 => 0.82
            Assert.True(score > 0.8);
        }

        [Fact]
        public void CalculateScore_DifferentDNA_ShouldLowerScore()
        {
            var t1 = new Track { Key = "8A", Bpm = 124, Mood = TrackMood.Melancholic };
            var t2 = new Track { Key = "8A", Bpm = 124, Mood = TrackMood.Energetic }; 

            var score = _service.CalculateScore(t1, t2);
            // Key: 1.0*0.3=0.3, BPM: 1.0*0.3=0.3, DNA (Both set, no match): 0.3*0.4=0.12 => 0.72
            Assert.True(score < 0.8);
            Assert.True(score > 0.7);
        }
    }
}
