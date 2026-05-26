using DJORGA.Application.Services;
using DJORGA.Domain.Entities;
using DJORGA.Domain.ValueObjects;
using DJORGA.Tests.Helpers;
using Microsoft.Extensions.Logging;
using System.Linq;
using Xunit;

namespace DJORGA.Tests.Application.Services
{
    public class HarmonicScoringLoggingTests
    {
        [Fact]
        public void CalculateScore_InvalidKeyFormat_LogsWarning()
        {
            var logger = new CapturingLogger<HarmonicScoringService>();
            var svc = new HarmonicScoringService(new ScoringWeights(), logger);

            var t1 = new Track { Key = "NOTAKEY", Bpm = 120 };
            var t2 = new Track { Key = "8A", Bpm = 120 };

            _ = svc.CalculateScore(t1, t2); // should not throw

            Assert.Contains(logger.Entries, e => e.Level == LogLevel.Warning);
        }

        [Fact]
        public void CalculateScore_InvalidKeyFormat_ReturnsFiniteScore()
        {
            var logger = new CapturingLogger<HarmonicScoringService>();
            var svc = new HarmonicScoringService(new ScoringWeights(), logger);

            var t1 = new Track { Key = "??", Bpm = 120 };
            var t2 = new Track { Key = "??", Bpm = 120 };

            var score = svc.CalculateScore(t1, t2);

            Assert.True(score >= 0.0 && score <= 1.0);
        }
    }
}
