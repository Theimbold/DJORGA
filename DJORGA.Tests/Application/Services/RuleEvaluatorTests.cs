using DJORGA.Application.Services;
using DJORGA.Domain.Entities;
using DJORGA.Domain.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DJORGA.Tests.Application.Services
{
    public class RuleEvaluatorTests
    {
        private readonly RuleEvaluatorService _service =
            new(NullLogger<RuleEvaluatorService>.Instance);

        [Fact]
        public void ApplyRules_MoodFilter_ShouldReturnMatchingTracks()
        {
            // Arrange
            var tracks = new List<Track>
            {
                new Track { Title = "T1", Mood = TrackMood.Melancholic },
                new Track { Title = "T2", Mood = TrackMood.Energetic },
                new Track { Title = "T3", Mood = TrackMood.Melancholic }
            }.AsQueryable();

            var rules = new List<FilterRule>
            {
                new FilterRule { PropertyName = "Mood", Operator = FilterOperator.Equals, Value = "Melancholic" }
            };

            // Act
            var result = _service.ApplyRules(tracks, rules, true).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, t => Assert.Equal(TrackMood.Melancholic, t.Mood));
        }

        [Fact]
        public void ApplyRules_TimeContextFilter_ShouldReturnMatchingTracks()
        {
            // Arrange
            var tracks = new List<Track>
            {
                new Track { Title = "T1", TimeContext = TrackTimeContext.Sunset },
                new Track { Title = "T2", TimeContext = TrackTimeContext.PeakTime }
            }.AsQueryable();

            var rules = new List<FilterRule>
            {
                new FilterRule { PropertyName = "TimeContext", Operator = FilterOperator.Equals, Value = "Sunset" }
            };

            // Act
            var result = _service.ApplyRules(tracks, rules, true).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal(TrackTimeContext.Sunset, result[0].TimeContext);
        }

        [Fact]
        public void ApplyRules_ComplexDnaFilter_ShouldWorkWithMatchAll()
        {
            // Arrange
            var tracks = new List<Track>
            {
                new Track { Title = "Correct", Mood = TrackMood.Melancholic, TimeContext = TrackTimeContext.Sunset },
                new Track { Title = "Wrong Mood", Mood = TrackMood.Energetic, TimeContext = TrackTimeContext.Sunset },
                new Track { Title = "Wrong Time", Mood = TrackMood.Melancholic, TimeContext = TrackTimeContext.PeakTime }
            }.AsQueryable();

            var rules = new List<FilterRule>
            {
                new FilterRule { PropertyName = "Mood", Operator = FilterOperator.Equals, Value = "Melancholic" },
                new FilterRule { PropertyName = "TimeContext", Operator = FilterOperator.Equals, Value = "Sunset" }
            };

            // Act
            var result = _service.ApplyRules(tracks, rules, true).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Correct", result[0].Title);
        }

        [Fact]
        public void ApplyRules_CaseInsensitiveEnumParsing_ShouldWork()
        {
            // Arrange
            var tracks = new List<Track>
            {
                new Track { Title = "T1", Mood = TrackMood.DarkSinister }
            }.AsQueryable();

            var rules = new List<FilterRule>
            {
                // lowercase "darksinister" instead of "DarkSinister"
                new FilterRule { PropertyName = "Mood", Operator = FilterOperator.Equals, Value = "darksinister" }
            };

            // Act
            var result = _service.ApplyRules(tracks, rules, true).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal(TrackMood.DarkSinister, result[0].Mood);
        }
    }
}
