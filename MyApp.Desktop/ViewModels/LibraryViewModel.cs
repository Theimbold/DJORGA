using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Application.Interfaces.UI;
using MyApp.Application.Services;
using MyApp.Application.UseCases.Rekordbox;
using MyApp.Application.UseCases.Library;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Desktop.ViewModels
{
    public enum LibraryWorkflowMode
    {
        All,
        NeedsAnalysis,
        Orphans
    }

    public partial class LibraryViewModel : ViewModelBase
    {
        private readonly ITrackRepository _trackRepository;
        private readonly ISmartCollectionRepository _collectionRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IBackgroundAnalysisService _analysisService;
        private readonly IFilePickerService _filePickerService;
        private readonly IAppStateService _appStateService;
        private readonly ImportRekordboxXmlUseCase _importUseCase;
        private readonly UpdateTrackMetadataUseCase _updateUseCase;
        private readonly DeleteTrackUseCase _deleteUseCase;
        private readonly ExportSmartCollectionUseCase _exportUseCase;
        private readonly RuleEvaluatorService _ruleEvaluator;

        private List<Track> _allTracks = new();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string _statusMessage = "Ready";

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private double _importProgress;

        [ObservableProperty]
        private LibraryWorkflowMode _selectedWorkflowMode = LibraryWorkflowMode.All;

        [ObservableProperty]
        private int _needsAnalysisCount;

        [ObservableProperty]
        private int _orphansCount;

        public ObservableCollection<Track> Tracks { get; } = new();

        [ObservableProperty]
        private Track? _selectedTrack;

        [ObservableProperty]
        private SmartCollection? _selectedCollection;

        [ObservableProperty]
        private Playlist? _selectedPlaylist;

        public ObservableCollection<SmartCollection> SmartCollections { get; } = new();
        public ObservableCollection<Playlist> Playlists { get; } = new();
        public ObservableCollection<Track> SelectedTracks { get; } = new();

        public event Action<Track>? RequestEditTrack;
        public event Action<ConfirmationViewModel>? RequestConfirmation;
        public event Action<SmartCollection>? RequestCollectionEditor;
        public event Action<Playlist>? RequestPlaylistEditor;

        public LibraryViewModel(
            ITrackRepository trackRepository, 
            ISmartCollectionRepository collectionRepository,
            IPlaylistRepository playlistRepository,
            IBackgroundAnalysisService analysisService,
            IFilePickerService filePickerService,
            IAppStateService appStateService,
            ImportRekordboxXmlUseCase importUseCase,
            UpdateTrackMetadataUseCase updateUseCase,
            DeleteTrackUseCase deleteUseCase,
            ExportSmartCollectionUseCase exportUseCase,
            RuleEvaluatorService ruleEvaluator)
        {
            _trackRepository = trackRepository;
            _collectionRepository = collectionRepository;
            _playlistRepository = playlistRepository;
            _analysisService = analysisService;
            _filePickerService = filePickerService;
            _appStateService = appStateService;
            _importUseCase = importUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
            _exportUseCase = exportUseCase;
            _ruleEvaluator = ruleEvaluator;

            ImportCommand = new AsyncRelayCommand(ImportRekordboxAsync);
            ExportCommand = new AsyncRelayCommand(ExportCollectionAsync, () => SelectedCollection != null);
            PlayTrackCommand = new RelayCommand<Track>(PlayTrack);
            DeleteTrackCommand = new RelayCommand<Track>(OnRequestDelete);
            UpdateTrackCommand = new RelayCommand<Track>(OnRequestEdit);
            CreateCollectionCommand = new AsyncRelayCommand(CreateNewCollectionAsync);
            CreatePlaylistCommand = new AsyncRelayCommand(CreateNewPlaylistAsync);
            DeletePlaylistCommand = new RelayCommand<Playlist>(OnRequestDeletePlaylist);
            RenamePlaylistCommand = new RelayCommand<Playlist>(p => RequestPlaylistEditor?.Invoke(p));
            SetWorkflowModeCommand = new RelayCommand<LibraryWorkflowMode>(m => SelectedWorkflowMode = m);

            _trackRepository.TrackAdded += t => { _allTracks.Add(t); FilterTracks(); UpdateCounters(); _analysisService.EnqueueTracks(new[] { t }); };
            _trackRepository.TrackUpdated += t => { var existing = _allTracks.FirstOrDefault(x => x.Id == t.Id); if (existing != null) { _allTracks.Remove(existing); _allTracks.Add(t); FilterTracks(); } else { _ = LoadTracksAsync(); } UpdateCounters(); };
            _trackRepository.BulkTracksAdded += ts => { _allTracks.AddRange(ts); FilterTracks(); UpdateCounters(); _analysisService.EnqueueTracks(ts); };
            
            _analysisService.TrackAnalyzed += t => 
            {
                // UI aktualisieren, wenn ein Track fertig ist
                Dispatcher.UIThread.Post(() => {
                    UpdateCounters();
                    if (SelectedWorkflowMode == LibraryWorkflowMode.NeedsAnalysis) FilterTracks();
                    StatusMessage = $"Analysis progress: {_analysisService.QueueCount} remaining.";
                });
            };

            _analysisService.Start();
            
            _ = LoadTracksAsync();
            _ = LoadAllCollectionsAsync();
        }

        public IAsyncRelayCommand ImportCommand { get; }
        public IAsyncRelayCommand ExportCommand { get; }
        public IAsyncRelayCommand CreateCollectionCommand { get; }
        public IAsyncRelayCommand CreatePlaylistCommand { get; }
        public IRelayCommand<Playlist> DeletePlaylistCommand { get; }
        public IRelayCommand<Playlist> RenamePlaylistCommand { get; }
        public IRelayCommand<Track> PlayTrackCommand { get; }
        public IRelayCommand<Track> DeleteTrackCommand { get; }
        public IRelayCommand<Track> UpdateTrackCommand { get; }
        public IRelayCommand<LibraryWorkflowMode> SetWorkflowModeCommand { get; }

        partial void OnSelectedCollectionChanged(SmartCollection? value) 
        { 
            if (value != null) { SelectedPlaylist = null; SelectedWorkflowMode = LibraryWorkflowMode.All; }
            FilterTracks(); 
        }

        partial void OnSelectedPlaylistChanged(Playlist? value)
        {
            if (value != null) { SelectedCollection = null; SelectedWorkflowMode = LibraryWorkflowMode.All; }
            FilterTracks();
        }

        partial void OnSelectedWorkflowModeChanged(LibraryWorkflowMode value)
        {
            if (value != LibraryWorkflowMode.All) { SelectedCollection = null; SelectedPlaylist = null; }
            FilterTracks();
        }
        
        partial void OnSearchTextChanged(string value) 
        {
            if (value == "REFRESH") { _ = LoadTracksAsync(); return; }
            FilterTracks();
        }

        private void FilterTracks()
        {
            var filtered = _allTracks.AsQueryable();

            // 1. Workflow Mode Filter
            if (SelectedWorkflowMode == LibraryWorkflowMode.NeedsAnalysis)
                filtered = filtered.Where(t => !t.IsAnalyzed);
            else if (SelectedWorkflowMode == LibraryWorkflowMode.Orphans)
                filtered = filtered.Where(t => !Playlists.Any(p => p.Items.Any(i => i.Id == t.Id)));

            // 2. Collection / Playlist Filter
            if (SelectedCollection != null)
                filtered = _ruleEvaluator.ApplyRules(filtered, SelectedCollection.Rules, SelectedCollection.MatchAllRules);
            else if (SelectedPlaylist != null)
                filtered = SelectedPlaylist.Items.AsQueryable();

            // 3. Search Filter
            if (!string.IsNullOrWhiteSpace(SearchText) && SearchText != "REFRESH")
            {
                var q = SearchText.ToLower();
                filtered = filtered.Where(t => 
                    (t.Title != null && t.Title.ToLower().Contains(q)) || 
                    (t.Artist != null && t.Artist.ToLower().Contains(q)) || 
                    (t.Genre != null && t.Genre.ToLower().Contains(q)));
            }

            var resultList = filtered.ToList();
            
            // UI Update in Chunks für maximale Performance
            Dispatcher.UIThread.Post(() => {
                Tracks.Clear();
                // Wenn Liste sehr groß ist, fügen wir sie in Batches hinzu (verhindert UI-Stottern)
                const int batchSize = 500;
                for (int i = 0; i < resultList.Count; i += batchSize)
                {
                    var batch = resultList.Skip(i).Take(batchSize);
                    foreach (var t in batch) Tracks.Add(t);
                }

                StatusMessage = SelectedPlaylist != null 
                    ? $"Playlist '{SelectedPlaylist.Name}': {Tracks.Count} tracks."
                    : $"Library: {Tracks.Count} tracks visible.";
            });
        }

        private async Task LoadTracksAsync()
        {
            IsLoading = true;
            try 
            {
                var result = await _trackRepository.GetAllAsync();
                _allTracks = result.ToList();
                FilterTracks();
                UpdateCounters();

                // Vorhandene, nicht analysierte Tracks in die Queue packen
                _analysisService.EnqueueTracks(_allTracks);
            }
            catch (Exception ex) { StatusMessage = $"Load Error: {ex.Message}"; }
            finally { IsLoading = false; }
        }

        private void UpdateCounters()
        {
            NeedsAnalysisCount = _allTracks.Count(t => !t.IsAnalyzed);
            // Orphan Berechnung ist etwas teurer, daher ggf. optimieren
            OrphansCount = _allTracks.Count(t => !Playlists.Any(p => p.Items.Any(i => i.Id == t.Id)));
        }

        private async Task LoadAllCollectionsAsync()
        {
            var collections = await _collectionRepository.GetAllAsync();
            SmartCollections.Clear();
            foreach (var col in collections) SmartCollections.Add(col);

            var playlists = await _playlistRepository.GetAllAsync();
            Playlists.Clear();
            foreach (var pl in playlists) Playlists.Add(pl);
        }

        public async Task AddTracksToPlaylistAsync(Playlist playlist, IEnumerable<Track> tracks)
        {
            foreach (var track in tracks)
            {
                playlist.AddTrack(track);
            }
            await _playlistRepository.UpdateAsync(playlist);
            if (SelectedPlaylist?.Id == playlist.Id) FilterTracks();
            StatusMessage = $"{tracks.Count()} tracks added to {playlist.Name}.";
        }

        private async Task CreateNewPlaylistAsync()
        {
            var newPl = new Playlist { Name = "New Playlist" };
            await _playlistRepository.AddAsync(newPl);
            await LoadAllCollectionsAsync();
            SelectedPlaylist = Playlists.FirstOrDefault(p => p.Id == newPl.Id);
        }

        private async Task ImportRekordboxAsync()
        {
            var filePath = await _filePickerService.OpenFileAsync("Select Rekordbox XML", new[] { "xml" });
            if (string.IsNullOrEmpty(filePath)) return;

            IsLoading = true;
            ImportProgress = 0;
            StatusMessage = "Importing tracks...";
            
            var progress = new Progress<double>(p => {
                ImportProgress = p * 100;
                StatusMessage = $"Importing: {ImportProgress:F0}%";
            });

            try { 
                await _importUseCase.ExecuteAsync(filePath, progress); 
                // await LoadTracksAsync(); // Entfällt, da BulkTracksAdded effizient aktualisiert
                _appStateService.IsLibraryEmpty = false;
                StatusMessage = "Import finished.";
            }
            catch (Exception ex) { StatusMessage = $"Import Error: {ex.Message}"; }
            finally { 
                IsLoading = false; 
                ImportProgress = 0;
            }
        }

        // Hilfsmethoden
        private void PlayTrack(Track? track) { if (track != null) _appStateService.CurrentTrack = track; }
        private void OnRequestEdit(Track? track) { var targets = SelectedTracks.Any() ? SelectedTracks.ToList() : (track != null ? new List<Track> { track } : new List<Track>()); if (targets.Any()) RequestEditTrack?.Invoke(targets[0]); }
        public async Task ApplyChangesAsync(Track track) { await _updateUseCase.ExecuteAsync(track); await LoadTracksAsync(); }
        public async Task ApplyBatchChangesAsync(IEnumerable<Track> targets, Track template) 
        { 
            foreach (var t in targets) 
            { 
                t.Genre = template.Genre; 
                t.Album = template.Album; 
                t.Bpm = template.Bpm; 
                t.Key = template.Key; 
                t.Mood = template.Mood;
                t.TimeContext = template.TimeContext;
                await _updateUseCase.ExecuteAsync(t); 
            } 
            await LoadTracksAsync(); 
        }
        private void OnRequestDelete(Track? track) { var targets = SelectedTracks.Any() ? SelectedTracks.ToList() : (track != null ? new List<Track> { track } : new List<Track>()); if (!targets.Any()) return; var vm = new ConfirmationViewModel("Delete Tracks", $"Remove {targets.Count} tracks?") { ConfirmButtonText = "Delete", Tag = targets }; RequestConfirmation?.Invoke(vm); }
        public async Task FinalizeDeleteAsync(IEnumerable<Track> tracks, bool deletePhysical) { foreach (var t in tracks) await _deleteUseCase.ExecuteAsync(t.Id, deletePhysical); await LoadTracksAsync(); }
        
        private void OnRequestDeletePlaylist(Playlist? playlist)
        {
            if (playlist == null) return;
            var vm = new ConfirmationViewModel("Delete Playlist", $"Delete playlist '{playlist.Name}'? Tracks will remain in the library.")
            {
                ConfirmButtonText = "Delete",
                Tag = playlist
            };
            RequestConfirmation?.Invoke(vm);
        }

        public async Task FinalizeDeletePlaylistAsync(Playlist playlist)
        {
            await _playlistRepository.DeleteAsync(playlist.Id);
            if (SelectedPlaylist?.Id == playlist.Id) SelectedPlaylist = null;
            await LoadAllCollectionsAsync();
            StatusMessage = $"Playlist '{playlist.Name}' deleted.";
        }

        public async Task SavePlaylistAsync(Playlist playlist)
        {
            await _playlistRepository.UpdateAsync(playlist);
            await LoadAllCollectionsAsync();
            StatusMessage = $"Playlist '{playlist.Name}' updated.";
        }

        private async Task CreateNewCollectionAsync() { var newCol = new SmartCollection { Name = "New Collection", Icon = "Star" }; RequestCollectionEditor?.Invoke(newCol); }
        public async Task SaveCollectionAsync(SmartCollection collection) { var existing = await _collectionRepository.GetByIdAsync(collection.Id); if (existing == null) await _collectionRepository.AddAsync(collection); else await _collectionRepository.UpdateAsync(collection); await LoadCollectionsAsync(); }
        private async Task ExportCollectionAsync() { if (SelectedCollection == null) return; var path = await _filePickerService.SaveFileAsync($"Export {SelectedCollection.Name}", "xml"); if (string.IsNullOrEmpty(path)) return; IsLoading = true; try { await _exportUseCase.ExecuteAsync(SelectedCollection, path); StatusMessage = "Export successful."; } catch (Exception ex) { StatusMessage = $"Export Error: {ex.Message}"; } finally { IsLoading = false; } }
    }
}
