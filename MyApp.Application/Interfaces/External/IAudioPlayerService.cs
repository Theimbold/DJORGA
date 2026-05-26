using System;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.External
{
    /// <summary>
    /// Service zur Steuerung der Audio-Wiedergabe.
    /// </summary>
    public interface IAudioPlayerService : IDisposable
    {
        Task LoadAsync(string filePath);
        void Play();
        void Pause();
        void Stop();
        
        TimeSpan Position { get; set; }
        TimeSpan Duration { get; }
        float Volume { get; set; }
        
        bool IsPlaying { get; }
        
        event Action? PlaybackEnded;
        event Action<string>? PlaybackError;
    }
}
