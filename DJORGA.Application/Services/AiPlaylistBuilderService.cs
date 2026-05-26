using DJORGA.Application.Interfaces.Services;
using DJORGA.Application.DTOs;
using DJORGA.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DJORGA.Application.Services
{
    /// <summary>
    /// Implementiert einen Greedy-Algorithmus zur Erstellung einer harmonischen Playlist.
    /// </summary>
    public class AiPlaylistBuilderService : IAiPlaylistBuilder
    {
        private readonly IHarmonicScorer _scorer;

        public AiPlaylistBuilderService(IHarmonicScorer scorer)
        {
            _scorer = scorer;
        }

        public IEnumerable<ScoredTrack> CreateSequence(Track startTrack, IEnumerable<Track> pool, int length)
        {
            var result = new List<ScoredTrack> 
            { 
                new ScoredTrack { Track = startTrack, Breakdown = new ScoreBreakdown { TotalScore = 1.0, KeyScore = 1.0, BpmScore = 1.0, DnaScore = 1.0 } } 
            };

            var remaining = pool.Where(t => t.Id != startTrack.Id).ToList();

            Track current = startTrack;

            for (int i = 0; i < length - 1; i++)
            {
                if (!remaining.Any()) break;

                // Finde den Track mit dem besten Score zum aktuellen Track
                var bestMatch = remaining
                    .Select(next => new { Track = next, Breakdown = _scorer.CalculateBreakdown(current, next) })
                    .OrderByDescending(x => x.Breakdown.TotalScore)
                    .FirstOrDefault();

                if (bestMatch == null || bestMatch.Breakdown.TotalScore < 0.2) // Mindest-Schwelle
                    break;

                result.Add(new ScoredTrack { Track = bestMatch.Track, Breakdown = bestMatch.Breakdown });
                remaining.Remove(bestMatch.Track);
                current = bestMatch.Track;
            }

            return result;
        }
    }
}
