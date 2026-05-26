using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces.UI;
using System;

namespace MyApp.Desktop.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly IAppStateService _appStateService;

        [ObservableProperty]
        private string _title = "DJORGA - AI DJ Organizer";

        [ObservableProperty]
        private ViewModelBase? _currentPage;

        [ObservableProperty]
        private SidebarViewModel _sidebar;

        [ObservableProperty]
        private PlayerViewModel _player;

        [ObservableProperty]
        private bool _isSidebarVisible;

        [ObservableProperty]
        private bool _isPlayerVisible;

        public MainViewModel(SidebarViewModel sidebar, PlayerViewModel player, IAppStateService appStateService)
        {
            _sidebar = sidebar;
            _player = player;
            _appStateService = appStateService;

            _appStateService.StateChanged += UpdateViewStates;
            UpdateViewStates();
        }

        private void UpdateViewStates()
        {
            IsSidebarVisible = !_appStateService.IsLibraryEmpty;
            IsPlayerVisible = _appStateService.IsPlayerVisible;

            if (_appStateService.IsLibraryEmpty)
            {
                if (CurrentPage is not OnboardingViewModel && CurrentPage is not ImportWizardViewModel)
                {
                    CurrentPage = App.Current?.Services?.GetRequiredService<OnboardingViewModel>();
                }
            }
            else
            {
                if (CurrentPage is OnboardingViewModel || CurrentPage is ImportWizardViewModel || CurrentPage == null)
                {
                    // Automatischer Wechsel zur Library - diese aktualisiert sich nun selbst via Events
                    CurrentPage = App.Current?.Services?.GetRequiredService<LibraryViewModel>();
                }
            }
        }
    }
}
