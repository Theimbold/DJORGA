using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using Avalonia.Markup.Xaml;
using MyApp.Desktop.ViewModels;
using MyApp.Desktop.Views.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Desktop.Views
{
    public partial class LibraryView : UserControl
    {
        public LibraryView()
        {
            InitializeComponent();
            
            var dataGrid = this.FindControl<DataGrid>("TracksGrid");
            if (dataGrid != null)
            {
                dataGrid.SelectionChanged += (s, e) =>
                {
                    if (DataContext is LibraryViewModel vm)
                    {
                        vm.SelectedTracks.Clear();
                        foreach (var item in dataGrid.SelectedItems)
                        {
                            if (item is MyApp.Domain.Entities.Track track)
                                vm.SelectedTracks.Add(track);
                        }
                    }
                };

                // Drag Start
                dataGrid.PointerPressed += async (s, e) =>
                {
                    if (e.GetCurrentPoint(dataGrid).Properties.IsLeftButtonPressed)
                    {
                        var dragData = new DataObject();
                        if (DataContext is LibraryViewModel vm && vm.SelectedTracks.Any())
                        {
                            dragData.Set("SelectedTracks", vm.SelectedTracks.ToList());
                            await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Copy);
                        }
                    }
                };
            }

            var playlistsListBox = this.FindControl<ListBox>("PlaylistsListBox");
            if (playlistsListBox != null)
            {
                playlistsListBox.AddHandler(DragDrop.DragOverEvent, (s, e) =>
                {
                    e.DragEffects &= DragDropEffects.Copy;
                    if (!e.Data.Contains("SelectedTracks"))
                        e.DragEffects = DragDropEffects.None;
                });

                playlistsListBox.AddHandler(DragDrop.DropEvent, async (s, e) =>
                {
                    if (e.Data.Contains("SelectedTracks"))
                    {
                        var tracks = e.Data.Get("SelectedTracks") as IEnumerable<MyApp.Domain.Entities.Track>;
                        if (tracks != null && DataContext is LibraryViewModel vm)
                        {
                            // Wir müssen herausfinden, auf welche Playlist gedroppt wurde
                            var visual = e.Source as Avalonia.Visual;
                            var listBoxItem = visual?.FindAncestorOfType<ListBoxItem>();
                            if (listBoxItem?.Content is MyApp.Domain.Entities.Playlist targetPlaylist)
                            {
                                await vm.AddTracksToPlaylistAsync(targetPlaylist, tracks);
                            }
                        }
                    }
                });
            }
        }

        protected override void OnDataContextChanged(System.EventArgs e)
        {
            base.OnDataContextChanged(e);
            if (DataContext is LibraryViewModel vm)
            {
                vm.RequestEditTrack += async track => await ShowEditDialogAsync(vm, track);
                vm.RequestConfirmation += async confirmVm => await ShowConfirmationDialogAsync(vm, confirmVm);
                vm.RequestCollectionEditor += async collection => await ShowCollectionEditorAsync(vm, collection);
                vm.RequestPlaylistEditor += async playlist => 
                {
                    playlist.Name += " (Edited)"; // Platzhalter für Dialog
                    await vm.SavePlaylistAsync(playlist);
                };
            }
        }

        private async Task ShowCollectionEditorAsync(LibraryViewModel vm, MyApp.Domain.Entities.SmartCollection collection)
        {
            var dialog = new SmartCollectionEditorDialog();
            var dialogVm = new SmartCollectionEditorViewModel(collection);
            dialog.DataContext = dialogVm;

            var window = VisualRoot as Window;
            if (window != null)
            {
                var result = await dialog.ShowDialog<MyApp.Domain.Entities.SmartCollection?>(window);
                if (result != null)
                {
                    await vm.SaveCollectionAsync(result);
                }
            }
        }

        private async Task ShowConfirmationDialogAsync(LibraryViewModel vm, ConfirmationViewModel confirmVm)
        {
            var dialog = new ConfirmationDialog();
            dialog.DataContext = confirmVm;

            var window = VisualRoot as Window;
            if (window != null)
            {
                var result = await dialog.ShowDialog<ConfirmationResult>(window);
                if (result != null && result.Confirmed)
                {
                    if (confirmVm.Tag is IEnumerable<MyApp.Domain.Entities.Track> targets)
                    {
                        await vm.FinalizeDeleteAsync(targets, result.SecondaryOptionActive);
                    }
                    else if (confirmVm.Tag is MyApp.Domain.Entities.Playlist targetPlaylist)
                    {
                        await vm.FinalizeDeletePlaylistAsync(targetPlaylist);
                    }
                }
            }
        }

        private async Task ShowEditDialogAsync(LibraryViewModel vm, MyApp.Domain.Entities.Track track)
        {
            var dialog = new EditTrackDialog();
            var dialogVm = new EditTrackViewModel(track);
            dialog.DataContext = dialogVm;

            var window = VisualRoot as Window;
            if (window != null)
            {
                var result = await dialog.ShowDialog<MyApp.Domain.Entities.Track?>(window);
                if (result != null)
                {
                    if (vm.SelectedTracks.Count > 1)
                    {
                        await vm.ApplyBatchChangesAsync(vm.SelectedTracks.ToList(), result);
                    }
                    else
                    {
                        await vm.ApplyChangesAsync(result);
                    }
                }
            }
        }
    }
}
