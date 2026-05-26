using DJORGA.Application.Services;
using DJORGA.Domain.Entities;
using DJORGA.Domain.ValueObjects;
using DJORGA.Tests.Helpers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DJORGA.Tests.Application.Services
{
    public class RuleEvaluatorLoggingTests
    {
        [Fact]
        public void ApplyRules_InvalidPropertyName_LogsWarningAndSkipsRule()
        {
            var logger = new CapturingLogger<RuleEvaluatorService>();
            var svc = new RuleEvaluatorService(logger);

            var tracks = new List<Track>
            {
                new Track { Title = "T1" },
                new Track { Title = "T2" }
            }.AsQueryable();

            var rules = new List<FilterRule>
            {
                new FilterRule { PropertyName = "NonExistentProperty", Operator = FilterOperator.Equals, Value = "x" }
            };

            var result = svc.ApplyRules(tracks, rules, true).ToList();

            // Broken rule is skipped — all tracks pass through
            Assert.Equal(2, result.Count);
            Assert.Contains(logger.Entries, e => e.Level == LogLevel.Warning);
        }
    }
}
