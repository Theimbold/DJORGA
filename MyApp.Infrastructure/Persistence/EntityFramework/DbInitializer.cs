using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Persistence.EntityFramework
{
    /// <summary>
    /// Stellt sicher, dass die Datenbank initialisiert und bereit ist.
    /// </summary>
    public static class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            // Erstellt die Datenbank und Tabellen, falls sie nicht existieren.
            await context.Database.EnsureCreatedAsync();

            // Default Smart Collections für DNA System
            if (!await context.SmartCollections.AnyAsync())
            {
                var scenarios = new List<SmartCollection>
                {
                    new SmartCollection 
                    { 
                        Name = "The Sunset Mix", 
                        Icon = "CloudSun", 
                        MatchAllRules = true,
                        Rules = new List<FilterRule> 
                        { 
                            new FilterRule { PropertyName = "TimeContext", Operator = FilterOperator.Equals, Value = "Sunset" },
                            new FilterRule { PropertyName = "Mood", Operator = FilterOperator.Equals, Value = "Melancholic" }
                        }
                    },
                    new SmartCollection 
                    { 
                        Name = "Peak Time Energy", 
                        Icon = "Zap", 
                        MatchAllRules = true,
                        Rules = new List<FilterRule> 
                        { 
                            new FilterRule { PropertyName = "TimeContext", Operator = FilterOperator.Equals, Value = "PeakTime" },
                            new FilterRule { PropertyName = "Mood", Operator = FilterOperator.Equals, Value = "Energetic" }
                        }
                    },
                    new SmartCollection 
                    { 
                        Name = "Hypnotic Afterhour", 
                        Icon = "Moon", 
                        MatchAllRules = true,
                        Rules = new List<FilterRule> 
                        { 
                            new FilterRule { PropertyName = "TimeContext", Operator = FilterOperator.Equals, Value = "Afterhour" },
                            new FilterRule { PropertyName = "Mood", Operator = FilterOperator.Equals, Value = "Hypnotic" }
                        }
                    }
                };

                await context.SmartCollections.AddRangeAsync(scenarios);
                await context.SaveChangesAsync();
            }
        }
    }
}
