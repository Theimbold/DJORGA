using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces.UI;
using MyApp.Desktop.ViewModels;
using System;

namespace MyApp.Desktop.Services
{
    /// <summary>
    /// Implementierung des Navigations-Services für die Desktop-Anwendung.
    /// </summary>
    public class DesktopNavigationService : INavigationService
    {
        private readonly Func<MainViewModel> _mainViewModelFactory;

        public DesktopNavigationService(Func<MainViewModel> mainViewModelFactory)
        {
            _mainViewModelFactory = mainViewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : class
        {
            var mainVm = _mainViewModelFactory();
            var targetVm = App.Current?.Services?.GetRequiredService<TViewModel>();
            
            if (targetVm is ViewModelBase vm)
            {
                mainVm.CurrentPage = vm;
            }
        }
    }
}
