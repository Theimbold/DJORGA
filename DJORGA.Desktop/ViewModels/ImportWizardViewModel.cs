using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DJORGA.Application.Interfaces.External;
using DJORGA.Application.Interfaces.UI;
using DJORGA.Application.UseCases.Rekordbox;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DJORGA.Desktop.ViewModels
{
    /// <summary>
    /// ViewModel für den geführten Import-Assistenten.
    /// </summary>
    public partial class ImportWizardViewModel : ViewModelBase
    {
        private readonly IRekordboxPathService _pathService;
        private readonly IFilePickerService _filePickerService;
        private readonly ImportRekordboxXmlUseCase _importUseCase;
        private readonly INavigationService _navigationService;
        private readonly IAppStateService _appStateService;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string _statusMessage = string.Empty;

        public ObservableCollection<string> DetectedPaths { get; } = new();

        public ImportWizardViewModel(
            IRekordboxPathService pathService,
            IFilePickerService filePickerService,
            ImportRekordboxXmlUseCase importUseCase,
            INavigationService navigationService,
            IAppStateService appStateService)
        {
            _pathService = pathService;
            _filePickerService = filePickerService;
            _importUseCase = importUseCase;
            _navigationService = navigationService;
            _appStateService = appStateService;

            ImportCommand = new AsyncRelayCommand<string>(ImportFileAsync);
            BrowseCommand = new AsyncRelayCommand(BrowseManuallyAsync);

            LoadDetectedPaths();
        }

        public IAsyncRelayCommand<string> ImportCommand { get; }
        public IAsyncRelayCommand BrowseCommand { get; }

        private void LoadDetectedPaths()
        {
            var paths = _pathService.GetDetectedXmlPaths();
            foreach (var path in paths) DetectedPaths.Add(path);
        }

        private async Task ImportFileAsync(string? path)
        {
            if (string.IsNullOrEmpty(path)) return;
            await ExecuteImport(path);
        }

        private async Task BrowseManuallyAsync()
        {
            var path = await _filePickerService.OpenFileAsync("Select Rekordbox XML", new[] { "xml" });
            if (!string.IsNullOrEmpty(path)) await ExecuteImport(path);
        }

        private async Task ExecuteImport(string path)
        {
            IsLoading = true;
            StatusMessage = "Analyzing and importing your library...";
            try
            {
                int count = await _importUseCase.ExecuteAsync(path);
                
                var allTracks = await _importUseCase.GetTrackRepository().GetAllAsync();
                if (allTracks.Any())
                {
                    _appStateService.IsLibraryEmpty = false;
                    _navigationService.NavigateTo<LibraryViewModel>();
                }
                else
                {
                    StatusMessage = "No tracks imported. Please check your XML file and log.";
                }
            }
            catch (System.Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally { IsLoading = false; }
        }
    }
}
