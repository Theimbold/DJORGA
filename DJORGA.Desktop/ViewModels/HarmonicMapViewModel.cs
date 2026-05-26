using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DJORGA.Application.DTOs;
using DJORGA.Application.Interfaces.Persistence;
using DJORGA.Application.Interfaces.UI;
using DJORGA.Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DJORGA.Desktop.ViewModels
{
    public partial class HarmonicMapViewModel : ViewModelBase
    {
        private readonly ITrackRepository _trackRepository;
        private readonly HarmonicGraphService _graphService;
        private readonly IAppStateService _appStateService;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private MapNode? _selectedNode;

        [ObservableProperty]
        private bool _isPopupVisible;

        public List<MapNode> Nodes { get; private set; } = new();
        public List<MapEdge> Edges { get; private set; } = new();

        public HarmonicMapViewModel(
            ITrackRepository trackRepository, 
            HarmonicGraphService graphService,
            IAppStateService appStateService)
        {
            _trackRepository = trackRepository;
            _graphService = graphService;
            _appStateService = appStateService;
            
            LoadMapCommand = new AsyncRelayCommand(LoadMapAsync);
            PlayTrackCommand = new AsyncRelayCommand(PlayTrackAsync);
            ClosePopupCommand = new RelayCommand(() => IsPopupVisible = false);
        }

        public IAsyncRelayCommand LoadMapCommand { get; }
        public IAsyncRelayCommand PlayTrackCommand { get; }
        public IRelayCommand ClosePopupCommand { get; }

        private async Task LoadMapAsync()
        {
            IsLoading = true;
            var tracks = await _trackRepository.GetAllAsync();
            var (nodes, edges) = _graphService.BuildGraph(tracks);

            CalculateCircularLayout(nodes);

            Nodes = nodes;
            Edges = edges;
            
            IsLoading = false;
            OnPropertyChanged(nameof(Nodes));
            OnPropertyChanged(nameof(Edges));
        }

        private async Task PlayTrackAsync()
        {
            if (SelectedNode == null) return;
            var track = await _trackRepository.GetByIdAsync(SelectedNode.TrackId);
            if (track != null)
            {
                _appStateService.CurrentTrack = track;
                IsPopupVisible = false;
            }
        }

        public void SelectNode(MapNode? node)
        {
            SelectedNode = node;
            IsPopupVisible = node != null;
        }

        private void CalculateCircularLayout(List<MapNode> nodes)
        {
            double centerX = 0;
            double centerY = 0;
            double baseRadius = 300;

            foreach (var node in nodes)
            {
                try
                {
                    var camelot = DJORGA.Domain.ValueObjects.CamelotKey.Parse(node.Key);
                    double angle = (camelot.Number - 1) * (360.0 / 12.0) * (Math.PI / 180.0);
                    double radius = camelot.Mode == 'A' ? baseRadius * 0.7 : baseRadius;
                    
                    var random = new Random(node.TrackId.GetHashCode());
                    radius += random.Next(-20, 20);
                    angle += random.NextDouble() * 0.1;

                    node.X = centerX + Math.Cos(angle) * radius;
                    node.Y = centerY + Math.Sin(angle) * radius;
                }
                catch
                {
                    node.X = centerX;
                    node.Y = centerY;
                }
            }
        }
    }
}
