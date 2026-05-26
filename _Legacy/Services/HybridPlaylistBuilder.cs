using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class HybridPlaylistBuilder
    {
        private readonly RuleBasedPlaylistBuilder _ruleBasedBuilder;
        private readonly AiPlaylistBuilder _aiBuilder;

        public HybridPlaylistBuilder(RuleBasedPlaylistBuilder ruleBasedBuilder, AiPlaylistBuilder aiBuilder)
        {
            _ruleBasedBuilder = ruleBasedBuilder;
            _aiBuilder = aiBuilder;
        }

        public Playlist BuildHybridPlaylist(IEnumerable<Track> tracks, IEnumerable<TrackAnalysis> analyses, string phase)
        {
            // Step 1: Filter tracks based on rules
            var filteredTracks = FilterTracksByRules(tracks, analyses, phase);

            // Step 2: Use AI to refine the playlist
            var refinedPlaylist = _aiBuilder.BuildPlaylist(filteredTracks);

            // Step 3: Optional LLM for labeling (placeholder for future integration)
            refinedPlaylist.Name = GeneratePlaylistName(phase);

            return refinedPlaylist;
        }

        private IEnumerable<Track> FilterTracksByRules(IEnumerable<Track> tracks, IEnumerable<TrackAnalysis> analyses, string phase)
        {
            return tracks.Where(track =>
            {
                var analysis = analyses.FirstOrDefault(a => a.TrackId == track.Id);
                if (analysis == null) return false;

                return phase switch
                {
                    "warmup" => analysis.Energy <= 2 && track.Bpm <= 120,
                    "peak" => analysis.Energy >= 4 && track.Bpm >= 125,
                    "closing" => analysis.Energy <= 3 && analysis.Mood.Contains("melodic"),
                    _ => false
                };
            });
        }

        private string GeneratePlaylistName(string phase)
        {
            return phase switch
            {
                "warmup" => "Warmup Set",
                "peak" => "Peak Time Set",
                "closing" => "Closing Set",
                _ => "Custom Playlist"
            };
        }
    }
}