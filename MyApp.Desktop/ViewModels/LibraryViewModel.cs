using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Domain.Entities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyApp.Desktop.ViewModels
{
    /// <summary>
    /// ViewModel für die Bibliotheks-Ansicht.
    /// </summary>
    public partial class LibraryViewModel : ViewModelBase
    {
        private readonly ITrackRepository _trackRepository;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private ObservableCollection<Track> _tracks = new();

        public LibraryViewModel(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
            LoadTracksCommand = new AsyncRelayCommand(LoadTracksAsync);
        }

        public IAsyncRelayCommand LoadTracksCommand { get; }

        /// <summary>
        /// Lädt alle Tracks aus dem Repository.
        /// </summary>
        private async Task LoadTracksAsync()
        {
            IsLoading = true;
            Tracks.Clear();
            
            var result = await _trackRepository.GetAllAsync();
            foreach (var track in result)
            {
                Tracks.Add(track);
            }
            
            IsLoading = false;
        }
    }
}
