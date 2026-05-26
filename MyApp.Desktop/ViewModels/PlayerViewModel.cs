using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces.External;
using MyApp.Application.Interfaces.UI;
using MyApp.Domain.Entities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Desktop.ViewModels
{
    public partial class PlayerViewModel : ViewModelBase
    {
        private readonly IAudioPlayerService _audioPlayer;
        private readonly IWaveformService _waveformService;
        private readonly IAppStateService _appStateService;
        private readonly DispatcherTimer _progressTimer;
        private bool _isUpdatingFromTimer = false;

        [ObservableProperty]
        private Track? _track;

        [ObservableProperty]
        private bool _isPlaying;

        [ObservableProperty]
        private double _position;

        [ObservableProperty]
        private double _duration;

        [ObservableProperty]
        private ObservableCollection<FrequencyPeak> _waveformPeaks = new();

        public PlayerViewModel(
            IAudioPlayerService audioPlayer, 
            IWaveformService waveformService,
            IAppStateService appStateService)
        {
            _audioPlayer = audioPlayer;
            _waveformService = waveformService;
            _appStateService = appStateService;

            _appStateService.StateChanged += OnAppStateChanged;
            
            PlayCommand = new RelayCommand(Play);
            PauseCommand = new RelayCommand(Pause);

            _progressTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            _progressTimer.Tick += (s, e) => 
            {
                _isUpdatingFromTimer = true;
                Position = _audioPlayer.Position.TotalSeconds;
                _isUpdatingFromTimer = false;
            };
        }

        public IRelayCommand PlayCommand { get; }
        public IRelayCommand PauseCommand { get; }

        /// <summary>
        /// Reagiert auf Änderungen der Position (z.B. durch den Slider).
        /// </summary>
        partial void OnPositionChanged(double value)
        {
            // Nur wenn die Änderung NICHT vom Timer kommt (also vom User am Slider)
            if (!_isUpdatingFromTimer)
            {
                _audioPlayer.Position = TimeSpan.FromSeconds(value);
            }
        }

        private void Play()
        {
            _audioPlayer.Play();
            IsPlaying = true;
            _progressTimer.Start();
        }

        private void Pause()
        {
            _audioPlayer.Pause();
            IsPlaying = false;
            _progressTimer.Stop();
        }

        private async void OnAppStateChanged()
        {
            if (_appStateService.CurrentTrack != null && _appStateService.CurrentTrack != Track)
            {
                await LoadTrackAsync(_appStateService.CurrentTrack);
            }
        }

        private async Task LoadTrackAsync(Track track)
        {
            _progressTimer.Stop();
            _audioPlayer.Stop();
            
            _isUpdatingFromTimer = true;
            Position = 0;
            _isUpdatingFromTimer = false;
            
            Track = track;

            await _audioPlayer.LoadAsync(track.FilePath);
            Duration = _audioPlayer.Duration.TotalSeconds;
            
            var peaks = await _waveformService.GetPeaksAsync(track.Id.ToString(), track.FilePath, 1000);
            float max = peaks.Any() ? peaks.Max(p => p.Total) : 0;
            WaveformPeaks.Clear();
            foreach (var p in peaks)
            {
                if (max > 0)
                {
                    WaveformPeaks.Add(new FrequencyPeak(p.Low / max, p.Mid / max, p.High / max));
                }
                else
                {
                    WaveformPeaks.Add(new FrequencyPeak(0, 0, 0));
                }
            }
            
            Play();
        }
    }
}
