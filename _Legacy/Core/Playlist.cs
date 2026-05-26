using System.Collections.Generic;

namespace Core
{
    public class Playlist
    {
        public string Name { get; set; }
        public List<Track> Tracks { get; set; } = new();
    }
}