# ADR 002: Wechsel zu CommunityToolkit.Mvvm

## Status
Vorgeschlagen

## Kontext
Das aktuelle UI-Projekt verwendet `Avalonia.ReactiveUI`. ReactiveUI ist mächtig, hat aber eine steile Lernkurve und führt oft zu komplexem, schwer debuggbarem Code durch implizite Reaktivität.

## Entscheidung
Wir nutzen für die neue UI-Struktur das **Microsoft CommunityToolkit.Mvvm**.

## Gründe
- **Performance:** Nutzt Source Generator für Boilerplate-Code (ObservableProperty, RelayCommand).
- **Einfachheit:** Klarere, imperative Struktur, die besser zum Clean Architecture Ansatz passt.
- **Standardisierung:** Ist der aktuelle Industriestandard für moderne .NET-Desktop-Anwendungen.
- **Leichtgewichtiger:** Weniger Laufzeit-Overhead im Vergleich zu den komplexen Rx-Pipelines von ReactiveUI.

## Konsequenzen
- Bestehende ViewModels in `RekordboxAi` müssen bei der Migration zu `MyApp.Desktop` umgeschrieben werden.
- Die Abhängigkeit `Avalonia.ReactiveUI` wird mittelfristig entfernt.
