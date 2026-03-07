using System;
using System.Collections.Generic;

namespace MyApp.Domain.Entities
{
    /// <summary>
    /// Repräsentiert eine Sammlung von Musikstücken (Playlist).
    /// </summary>
    public class Playlist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public List<Track> Items { get; set; } = new List<Track>();
        public bool IsAiGenerated { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fügt einen Track zur Playlist hinzu.
        /// </summary>
        public void AddTrack(Track track)
        {
            if (track != null && !Items.Contains(track))
            {
                Items.Add(track);
            }
        }
    }
}
