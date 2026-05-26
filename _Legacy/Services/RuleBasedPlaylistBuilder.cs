using Core;
using System.Collections.Generic;

namespace Services
{
    public class RuleBasedPlaylistBuilder
    {
        public Playlist BuildPlaylist(IEnumerable<Track> tracks)
        {
            // Logic to build a playlist based on rules
            return new Playlist { Name = "Rule-Based Playlist", Tracks = new List<Track>(tracks) };
        }
    }
}