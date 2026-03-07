using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace MyApp.Desktop.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
    }

    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _title = "DJORGA - AI DJ Organizer";

        [ObservableProperty]
        private ViewModelBase? _currentPage;

        public MainViewModel()
        {
            // Initialisierung der Standardseite über den DI-Container
            // Wir greifen auf den Bootstrapper zu, um das LibraryViewModel aufzulösen
            var libraryVm = App.Current?.Services?.GetRequiredService<LibraryViewModel>();
            if (libraryVm != null)
            {
                CurrentPage = libraryVm;
                libraryVm.LoadTracksCommand.Execute(null);
            }
        }
    }
}
