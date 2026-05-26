using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Interfaces.External;
using MyApp.Application.DTOs;
using MyApp.Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Desktop.ViewModels
{
    public partial class AIBuilderViewModel : ViewModelBase
    {
        private readonly IAiPlaylistBuilder _playlistBuilder;
        private readonly ITrackRepository _trackRepository;
        private readonly IAudioPlayerService _audioPlayer;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private Track? _selectedStartTrack;

        [ObservableProperty]
        private int _playlistLength = 10;

        public ObservableCollection<Track> AvailableTracks { get; } = new();
        public ObservableCollection<ScoredTrack> GeneratedSequence { get; } = new();

        public AIBuilderViewModel(
            IAiPlaylistBuilder playlistBuilder, 
            ITrackRepository trackRepository,
            IAudioPlayerService audioPlayer)
        {
            _playlistBuilder = playlistBuilder;
            _trackRepository = trackRepository;
            _audioPlayer = audioPlayer;
            
            LoadTracksCommand = new AsyncRelayCommand(LoadTracksAsync);
            GenerateCommand = new RelayCommand(GenerateSequence, CanGenerate);
            PlayTrackCommand = new AsyncRelayCommand<Track>(PlayTrackAsync);

            _ = LoadTracksAsync();
        }

        public IAsyncRelayCommand LoadTracksCommand { get; }
        public IRelayCommand GenerateCommand { get; }
        public IAsyncRelayCommand<Track> PlayTrackCommand { get; }

        private async Task LoadTracksAsync()
        {
            IsLoading = true;
            AvailableTracks.Clear();
            var tracks = await _trackRepository.GetAllAsync();
            foreach (var track in tracks)
            {
                AvailableTracks.Add(track);
            }
            IsLoading = false;
        }

        private void GenerateSequence()
        {
            if (SelectedStartTrack == null) return;

            var sequence = _playlistBuilder.CreateSequence(SelectedStartTrack, AvailableTracks, PlaylistLength);
            
            GeneratedSequence.Clear();
            foreach (var scoredTrack in sequence)
            {
                GeneratedSequence.Add(scoredTrack);
            }
        }

        private bool CanGenerate() => SelectedStartTrack != null && !IsLoading;

        private async Task PlayTrackAsync(Track? track)
        {
            if (track == null) return;
            await _audioPlayer.LoadAsync(track.FilePath);
            _audioPlayer.Play();
        }

        partial void OnSelectedStartTrackChanged(Track? value)
        {
            GenerateCommand.NotifyCanExecuteChanged();
        }
    }
}
