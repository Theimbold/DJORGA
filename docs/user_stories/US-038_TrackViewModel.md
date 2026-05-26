# User Story US-038: TrackViewModel für UI-Binding einführen

## Status: Offen
**Epic:** [E-022: Domain Layer Bereinigung](../epics/E-022_Domain_Layer_Cleanup.md)

## Beschreibung
**Als** Entwickler  
**möchte ich** eine dedizierte `TrackViewModel`-Klasse im Desktop-Layer,  
**um** alle UI-Binding-Logik (INotifyPropertyChanged, Observable Properties)
dort zu kapseln, ohne die Domain zu belasten.

## Akzeptanzkriterien
- [ ] `TrackViewModel` existiert in `DJORGA.Desktop/ViewModels/`.
- [ ] `TrackViewModel` verwendet `[ObservableProperty]` via CommunityToolkit.Mvvm.
- [ ] `TrackViewModel` wraps ein `Track`-Domain-Objekt (Referenz oder Mapping).
- [ ] `LibraryViewModel` nutzt `ObservableCollection<TrackViewModel>`.
- [ ] Alle Views binden auf `TrackViewModel`-Properties.
- [ ] Keine View oder ViewModel greift direkt auf `Track.PropertyChanged` zu.

## Linked Implementation
- **Neu:** `DJORGA.Desktop/ViewModels/TrackViewModel.cs`
- **Geändert:** `DJORGA.Desktop/ViewModels/LibraryViewModel.cs`
- **Geändert:** `DJORGA.Desktop/Views/LibraryView.axaml`
