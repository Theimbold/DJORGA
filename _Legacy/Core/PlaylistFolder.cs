using System.Collections.Generic;

namespace Core
{
    public class PlaylistFolder
    {
        public string Name { get; set; }
        public List<Playlist> Playlists { get; set; } = new();
    }
}