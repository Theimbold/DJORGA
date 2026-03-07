# Task 015: Dependency Injection Setup

## Feature
Dependency Injection & Bootstrapping

## Aufgabe
Einrichten eines modernen DI-Containers in der Avalonia-Anwendung, um die Layer sauber zu entkoppeln.

## Schritte
1. Hinzufügen der NuGet-Pakete zu `MyApp.Desktop`:
   - `Microsoft.Extensions.DependencyInjection`
   - `CommunityToolkit.Mvvm`
2. Erstellen einer statischen Klasse `Bootstrapper` (oder Erweiterung der `App.axaml.cs`), um den `IServiceProvider` zu verwalten.
3. Konfiguration der `App.axaml.cs`, um das `MainWindow` über den DI-Container aufzulösen.

## Definition of Done
- NuGet-Pakete sind erfolgreich referenziert.
- DI-Container ist initialisiert.
- Die App startet weiterhin fehlerfrei.
