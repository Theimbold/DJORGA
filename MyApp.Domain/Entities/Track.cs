using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyApp.Domain.ValueObjects;

namespace MyApp.Domain.Entities
{
    /// <summary>
    /// Repräsentiert einen Track. Implementiert INotifyPropertyChanged für UI-Updates.
    /// </summary>
    public class Track : INotifyPropertyChanged
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        
        private string _genre = string.Empty;
        public string Genre 
        { 
            get => _genre; 
            set { _genre = value; OnPropertyChanged(); } 
        }

        private TrackMood _mood = TrackMood.None;
        public TrackMood Mood 
        { 
            get => _mood; 
            set { _mood = value; OnPropertyChanged(); } 
        }

        private TrackTimeContext _timeContext = TrackTimeContext.None;
        public TrackTimeContext TimeContext 
        { 
            get => _timeContext; 
            set { _timeContext = value; OnPropertyChanged(); } 
        }

        private string _coverArtPath = string.Empty;
        public string CoverArtPath 
        { 
            get => _coverArtPath; 
            set { _coverArtPath = value; OnPropertyChanged(); } 
        }

        private bool _isAnalyzed;
        public bool IsAnalyzed 
        { 
            get => _isAnalyzed; 
            set { _isAnalyzed = value; OnPropertyChanged(); } 
        }

        public double Bpm { get; set; }
        public string Key { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime ImportedAt { get; set; } = DateTime.UtcNow;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public bool IsValid() => !string.IsNullOrWhiteSpace(Title) && Bpm > 0;
    }
}
