# Task 017: MainViewModel & Shell Setup

## Feature
MVVM Core & Navigation

## Aufgabe
Implementierung des Haupt-ViewModels basierend auf dem CommunityToolkit.Mvvm und Vorbereitung für das Content-Routing.

## Schritte
1. Erstellen von `MainViewModel.cs` erbt von `ObservableObject`.
2. Hinzufügen einer Property `CurrentPage` (ViewModelBase), um die aktive Ansicht zu steuern.
3. Implementierung eines Commands zum Wechseln der Ansichten.
4. Anpassung der `MainWindow.axaml` zur Anzeige des `CurrentPage` ViewModels via ViewLocator oder DataTemplates.

## Definition of Done
- `MainViewModel` nutzt Source Generator für Properties.
- Grundstruktur für das View-Switching steht.
- `MainWindow` zeigt (testweise) ein Placeholder-ViewModel an.
