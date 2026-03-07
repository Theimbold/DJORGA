# Task 016: Service Registration

## Feature
Dependency Injection & Bootstrapping

## Aufgabe
Registrierung aller Backend-Komponenten im DI-Container, damit ViewModels darauf zugreifen können.

## Schritte
1. Registrierung des `AppDbContext` (mit SQLite-Konfiguration).
2. Registrierung der Repositories (`ITrackRepository`, `IPlaylistRepository`) als Scoped oder Singleton.
3. Registrierung des `IRekordboxService` und des `ImportRekordboxXmlUseCase`.
4. Registrierung der ViewModels (`MainViewModel`, `LibraryViewModel`).

## Definition of Done
- Alle benötigten Services sind im Container hinterlegt.
- Testweise Auflösung eines UseCases im Bootstrapper funktioniert ohne Exception.
