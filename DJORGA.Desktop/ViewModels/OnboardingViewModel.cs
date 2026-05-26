using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DJORGA.Application.Interfaces.UI;
using System.Threading.Tasks;

namespace DJORGA.Desktop.ViewModels
{
    /// <summary>
    /// ViewModel für den Begrüßungs-Bildschirm.
    /// </summary>
    public partial class OnboardingViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public OnboardingViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            StartImportCommand = new RelayCommand(OnStartImport);
        }

        public IRelayCommand StartImportCommand { get; }

        private void OnStartImport()
        {
            // Wechselt zum geführten Import-Assistenten
            _navigationService.NavigateTo<ImportWizardViewModel>();
        }
    }
}
