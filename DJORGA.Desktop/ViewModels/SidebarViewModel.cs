using CommunityToolkit.Mvvm.ComponentModel;
using DJORGA.Application.Interfaces.UI;

namespace DJORGA.Desktop.ViewModels
{
    public partial class SidebarViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int _selectedIndex = 1;

        public SidebarViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        partial void OnSelectedIndexChanged(int value)
        {
            switch (value)
            {
                case 0:
                    _navigationService.NavigateTo<DashboardViewModel>();
                    break;
                case 1:
                    _navigationService.NavigateTo<LibraryViewModel>();
                    break;
                case 2:
                    _navigationService.NavigateTo<AIBuilderViewModel>();
                    break;
                case 3:
                    _navigationService.NavigateTo<HarmonicMapViewModel>();
                    break;
                case 4:
                    _navigationService.NavigateTo<SettingsViewModel>();
                    break;
            }
        }
    }
}
