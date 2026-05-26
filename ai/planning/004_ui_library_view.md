# Epic 004: UI-Grundgerüst & Bibliotheks-Ansicht (Desktop)

## Status
Abgeschlossen

## Beschreibung
Initialisierung der Benutzeroberfläche mit Avalonia und CommunityToolkit.Mvvm. Ziel ist ein funktionales Dashboard mit einer navigierbaren Bibliotheks-Ansicht, die Daten aus dem SQLite-Repository anzeigt.

## Features & Tasks

### Feature 1: Dependency Injection & Bootstrapping
- [x] **Task 015:** Setup von `Microsoft.Extensions.DependencyInjection` in `MyApp.Desktop`.
- [x] **Task 016:** Registrierung der Repositories, Services und Use Cases im DI-Container.

### Feature 2: MVVM Core & Navigation
- [x] **Task 017:** Implementierung des `MainViewModel` und der View-Switching Logik.
- [x] **Task 018:** Erstellung des `NavigationService` (Abstraktion im Application-Layer).

### Feature 3: Library View
- [x] **Task 019:** Implementierung des `LibraryViewModel` (Laden von Tracks aus dem Repository).
- [x] **Task 020:** Gestaltung der `LibraryView.axaml` (DataGrid zur Anzeige der Tracks).

## Abhängigkeiten
- Basiert auf Epic 001, 002 und 003 (Backend vollständig initialisiert).
- Erfordert ADR 002 (CommunityToolkit.Mvvm).
