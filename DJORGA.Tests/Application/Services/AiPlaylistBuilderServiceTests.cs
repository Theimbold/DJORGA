using DJORGA.Application.Interfaces.Services;
using DJORGA.Application.Services;
using DJORGA.Domain.Entities;
using System;
using System.Linq;
using Xunit;

namespace DJORGA.Tests.Application.Services
{
    public class AiPlaylistBuilderServiceTests
    {
        private static Track MakeTrack(string title) =>
            new Track { Title = title, Bpm = 128, Key = "8A" };

        private static AiPlaylistBuilderService BuildService(Func<Track, Track, double> scoreFunc) =>
            new(new StubScorer(scoreFunc));

        [Fact]
        public void CreateSequence_HappyPath_Returns5Tracks()
        {
            var svc = BuildService((_, _) => 0.9);
            var starter = MakeTrack("Start");
            var pool = Enumerable.Range(1, 10).Select(i => MakeTrack($"T{i}")).ToList();

            var result = svc.CreateSequence(starter, pool, 5).ToList();

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void CreateSequence_EmptyPool_ReturnsOnlyStarter()
        {
            var svc = BuildService((_, _) => 0.9);
            var starter = MakeTrack("Start");

            var result = svc.CreateSequence(starter, [], 5).ToList();

            Assert.Single(result);
            Assert.Equal(starter.Id, result[0].Track.Id);
        }

        [Fact]
        public void CreateSequence_PoolSmallerThanLength_UsesEntirePool()
        {
            var svc = BuildService((_, _) => 0.9);
            var starter = MakeTrack("Start");
            var pool = Enumerable.Range(1, 3).Select(i => MakeTrack($"T{i}")).ToList();

            var result = svc.CreateSequence(starter, pool, 10).ToList();

            Assert.Equal(4, result.Count); // starter + all 3 from pool
        }

        [Fact]
        public void CreateSequence_NoBestScoreAboveThreshold_ReturnsOnlyStarter()
        {
            var svc = BuildService((_, _) => 0.1); // below min threshold of 0.2
            var starter = MakeTrack("Start");
            var pool = Enumerable.Range(1, 5).Select(i => MakeTrack($"T{i}")).ToList();

            var result = svc.CreateSequence(starter, pool, 5).ToList();

            Assert.Single(result);
            Assert.Equal(starter.Id, result[0].Track.Id);
        }

        [Fact]
        public void CreateSequence_NoTrackAppearsMoreThanOnce()
        {
            var svc = BuildService((_, _) => 0.9);
            var starter = MakeTrack("Start");
            var pool = Enumerable.Range(1, 5).Select(i => MakeTrack($"T{i}")).ToList();

            var result = svc.CreateSequence(starter, pool, 6).ToList();

            var ids = result.Select(r => r.Track.Id).ToList();
            Assert.Equal(ids.Distinct().Count(), ids.Count);
        }

        private sealed class StubScorer : IHarmonicScorer
        {
            private readonly Func<Track, Track, double> _score;
            public StubScorer(Func<Track, Track, double> score) => _score = score;

            public double CalculateScore(Track from, Track to) => _score(from, to);

            public ScoreBreakdown CalculateBreakdown(Track from, Track to)
            {
                var s = _score(from, to);
                return new ScoreBreakdown { TotalScore = s, KeyScore = s, BpmScore = s, DnaScore = s };
            }
        }
    }
}
