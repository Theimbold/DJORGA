using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DJORGA.Application.Interfaces.Persistence;
using DJORGA.Application.Interfaces.Services;
using DJORGA.Application.Interfaces.External;
using DJORGA.Application.DTOs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DJORGA.Desktop.ViewModels
{
    public partial class AIBuilderViewModel : ViewModelBase
    {
        private readonly IAiPlaylistBuilder _playlistBuilder;
        private readonly ITrackRepository _trackRepository;
        private readonly IAudioPlayerService _audioPlayer;

        [ObservableProperty] private bool _isLoading;
        [ObservableProperty] private TrackViewModel? _selectedStartTrack;
        [ObservableProperty] private int _playlistLength = 10;

        public ObservableCollection<TrackViewModel> AvailableTracks { get; } = new();
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
            PlayTrackCommand = new AsyncRelayCommand<TrackViewModel>(PlayTrackAsync);

            _ = LoadTracksAsync();
        }

        public IAsyncRelayCommand LoadTracksCommand { get; }
        public IRelayCommand GenerateCommand { get; }
        public IAsyncRelayCommand<TrackViewModel> PlayTrackCommand { get; }

        private async Task LoadTracksAsync()
        {
            IsLoading = true;
            AvailableTracks.Clear();
            var tracks = await _trackRepository.GetAllAsync();
            foreach (var track in tracks)
                AvailableTracks.Add(new TrackViewModel(track));
            IsLoading = false;
        }

        private void GenerateSequence()
        {
            if (SelectedStartTrack == null) return;

            // Domain-Models für den Builder-Service extrahieren
            var pool = AvailableTracks.Select(tvm => tvm.Model);
            var sequence = _playlistBuilder.CreateSequence(SelectedStartTrack.Model, pool, PlaylistLength);

            GeneratedSequence.Clear();
            foreach (var scoredTrack in sequence)
                GeneratedSequence.Add(scoredTrack);
        }

        private bool CanGenerate() => SelectedStartTrack != null && !IsLoading;

        private async Task PlayTrackAsync(TrackViewModel? tvm)
        {
            if (tvm == null) return;
            await _audioPlayer.LoadAsync(tvm.FilePath);
            _audioPlayer.Play();
        }

        partial void OnSelectedStartTrackChanged(TrackViewModel? value)
        {
            GenerateCommand.NotifyCanExecuteChanged();
        }
    }
}
