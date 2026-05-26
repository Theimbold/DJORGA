using CommunityToolkit.Mvvm.ComponentModel;
using DJORGA.Domain.Entities;
using DJORGA.Domain.ValueObjects;
using System;

namespace DJORGA.Desktop.ViewModels
{
    /// <summary>
    /// UI-Wrapper für die Track-Domain-Entity.
    /// Kapselt INotifyPropertyChanged-Logik im Desktop-Layer (Clean Architecture).
    /// Bindet an AXAML-Views; das zugrunde liegende Domain-Model bleibt UI-frei.
    /// </summary>
    public partial class TrackViewModel : ViewModelBase
    {
        /// <summary>Das zugrunde liegende Domain-Model.</summary>
        public Track Model { get; }

        /// <summary>Unveränderliche ID — direkt aus dem Model.</summary>
        public Guid Id => Model.Id;

        [ObservableProperty] private string _title = string.Empty;
        [ObservableProperty] private string _artist = string.Empty;
        [ObservableProperty] private string _album = string.Empty;
        [ObservableProperty] private string _genre = string.Empty;
        [ObservableProperty] private double _bpm;
        [ObservableProperty] private string _key = string.Empty;
        [ObservableProperty] private string _filePath = string.Empty;
        [ObservableProperty] private string _coverArtPath = string.Empty;
        [ObservableProperty] private bool _isAnalyzed;
        [ObservableProperty] private TrackMood _mood;
        [ObservableProperty] private TrackTimeContext _timeContext;

        public TrackViewModel(Track track)
        {
            Model = track;
            _title = track.Title;
            _artist = track.Artist;
            _album = track.Album;
            _genre = track.Genre;
            _bpm = track.Bpm;
            _key = track.Key;
            _filePath = track.FilePath;
            _coverArtPath = track.CoverArtPath;
            _isAnalyzed = track.IsAnalyzed;
            _mood = track.Mood;
            _timeContext = track.TimeContext;
        }

        /// <summary>
        /// Schreibt alle ViewModel-Properties zurück in das Domain-Model.
        /// Vor der Übergabe an einen Use Case aufrufen.
        /// </summary>
        public void SyncToModel()
        {
            Model.Title = Title;
            Model.Artist = Artist;
            Model.Album = Album;
            Model.Genre = Genre;
            Model.Bpm = Bpm;
            Model.Key = Key;
            Model.FilePath = FilePath;
            Model.CoverArtPath = CoverArtPath;
            Model.IsAnalyzed = IsAnalyzed;
            Model.Mood = Mood;
            Model.TimeContext = TimeContext;
        }

        /// <summary>
        /// Aktualisiert die ViewModel-Properties aus einem aktualisierten Domain-Model.
        /// Aufzurufen wenn das Repository eine Änderung meldet.
        /// </summary>
        public void UpdateFrom(Track track)
        {
            Title = track.Title;
            Artist = track.Artist;
            Album = track.Album;
            Genre = track.Genre;
            Bpm = track.Bpm;
            Key = track.Key;
            FilePath = track.FilePath;
            CoverArtPath = track.CoverArtPath;
            IsAnalyzed = track.IsAnalyzed;
            Mood = track.Mood;
            TimeContext = track.TimeContext;
        }
    }
}
