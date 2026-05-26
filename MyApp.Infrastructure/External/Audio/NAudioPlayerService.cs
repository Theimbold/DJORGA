using MyApp.Application.Interfaces.External;
using NAudio.Wave;
using System;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.External.Audio
{
    /// <summary>
    /// Implementierung des Audio-Players unter Verwendung von NAudio.
    /// Nutzt Streaming, um auch große Dateien (100MB+) sofort zu starten.
    /// </summary>
    public class NAudioPlayerService : IAudioPlayerService
    {
        private IWavePlayer? _outputDevice;
        private AudioFileReader? _audioFile;
        private bool _isPlaying;

        public event Action? PlaybackEnded;
        public event Action<string>? PlaybackError;

        public TimeSpan Position 
        { 
            get => _audioFile?.CurrentTime ?? TimeSpan.Zero;
            set { if (_audioFile != null) _audioFile.CurrentTime = value; }
        }

        public TimeSpan Duration => _audioFile?.TotalTime ?? TimeSpan.Zero;

        public float Volume 
        { 
            get => _audioFile?.Volume ?? 1.0f;
            set { if (_audioFile != null) _audioFile.Volume = value; }
        }

        public bool IsPlaying => _isPlaying;

        public async Task LoadAsync(string filePath)
        {
            try 
            {
                await Task.Run(() => 
                {
                    Stop();
                    Cleanup();

                    _audioFile = new AudioFileReader(filePath);
                    _outputDevice = new WaveOutEvent();
                    _outputDevice.Init(_audioFile);
                    _outputDevice.PlaybackStopped += OnPlaybackStopped;
                });
            }
            catch (Exception ex)
            {
                PlaybackError?.Invoke($"Fehler beim Laden der Datei: {ex.Message}");
            }
        }

        public void Play()
        {
            if (_outputDevice != null && !_isPlaying)
            {
                _outputDevice.Play();
                _isPlaying = true;
            }
        }

        public void Pause()
        {
            if (_outputDevice != null && _isPlaying)
            {
                _outputDevice.Pause();
                _isPlaying = false;
            }
        }

        public void Stop()
        {
            _outputDevice?.Stop();
            if (_audioFile != null) _audioFile.Position = 0;
            _isPlaying = false;
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            _isPlaying = false;
            if (e.Exception == null)
                PlaybackEnded?.Invoke();
            else
                PlaybackError?.Invoke(e.Exception.Message);
        }

        private void Cleanup()
        {
            _outputDevice?.Dispose();
            _outputDevice = null;
            _audioFile?.Dispose();
            _audioFile = null;
        }

        public void Dispose()
        {
            Cleanup();
        }
    }
}
