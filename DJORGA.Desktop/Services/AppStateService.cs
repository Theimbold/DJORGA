using DJORGA.Application.Interfaces.UI;
using DJORGA.Domain.Entities;
using System;

namespace DJORGA.Desktop.Services
{
    public class AppStateService : IAppStateService
    {
        private bool _isLibraryEmpty = true;
        private bool _isPlayerVisible = false;
        private Track? _currentTrack;

        public bool IsLibraryEmpty
        {
            get => _isLibraryEmpty;
            set { if (_isLibraryEmpty != value) { _isLibraryEmpty = value; StateChanged?.Invoke(); } }
        }

        public bool IsPlayerVisible
        {
            get => _isPlayerVisible;
            set { if (_isPlayerVisible != value) { _isPlayerVisible = value; StateChanged?.Invoke(); } }
        }

        public Track? CurrentTrack
        {
            get => _currentTrack;
            set 
            { 
                if (_currentTrack != value) 
                { 
                    _currentTrack = value; 
                    IsPlayerVisible = value != null;
                    StateChanged?.Invoke(); 
                } 
            }
        }

        public event Action? StateChanged;
    }
}
