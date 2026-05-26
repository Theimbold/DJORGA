using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DJORGA.Domain.Entities;
using DJORGA.Domain.ValueObjects;
using System;

namespace DJORGA.Desktop.ViewModels
{
    public partial class EditTrackViewModel : ViewModelBase
    {
        private readonly Track _originalTrack;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _artist;

        [ObservableProperty]
        private string _album;

        [ObservableProperty]
        private string _genre;

        [ObservableProperty]
        private double _bpm;

        [ObservableProperty]
        private string _key;

        [ObservableProperty]
        private TrackMood _mood;

        [ObservableProperty]
        private TrackTimeContext _timeContext;

        public event Action<Track?>? CloseRequested;

        public EditTrackViewModel(Track track)
        {
            _originalTrack = track;
            
            // Kopie der Daten zum Editieren
            _title = track.Title;
            _artist = track.Artist;
            _album = track.Album;
            _genre = track.Genre;
            _bpm = track.Bpm;
            _key = track.Key;
            _mood = track.Mood;
            _timeContext = track.TimeContext;

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        public IRelayCommand SaveCommand { get; }
        public IRelayCommand CancelCommand { get; }

        private void Save()
        {
            // Änderungen zurückschreiben (nur in das Objekt, noch nicht Persistenz)
            _originalTrack.Title = Title;
            _originalTrack.Artist = Artist;
            _originalTrack.Album = Album;
            _originalTrack.Genre = Genre;
            _originalTrack.Bpm = Bpm;
            _originalTrack.Key = Key;
            _originalTrack.Mood = Mood;
            _originalTrack.TimeContext = TimeContext;

            CloseRequested?.Invoke(_originalTrack);
        }

        private void Cancel()
        {
            CloseRequested?.Invoke(null);
        }
    }
}
