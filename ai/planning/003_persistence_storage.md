# Epic 003: Persistenz & Datenhaltung (Infrastructure)

## Status
Abgeschlossen

## Beschreibung
Implementierung der Datenhaltungsschicht unter Verwendung von SQLite und Entity Framework Core. Ziel ist die dauerhafte Speicherung von Tracks und Playlists gemäß den im Application-Layer definierten Interfaces.

## Features & Tasks

### Feature 1: Database Setup (EF Core)
- [x] **Task 010:** Installation der NuGet-Pakete und Erstellung des `AppDbContext`.
- [x] **Task 011:** Konfiguration der Entity-Mappings (Fluent API) für `Track` und `Playlist`.

### Feature 2: Repository Implementation
- [x] **Task 012:** Implementierung des `SqliteTrackRepository` im Infrastructure-Layer.
- [x] **Task 013:** Implementierung des `SqlitePlaylistRepository` im Infrastructure-Layer.

### Feature 3: Migration & Initialization
- [x] **Task 014:** Erstellen der Initialen Migration und Logik zum automatischen DB-Setup beim Start.

## Abhängigkeiten
- Basiert auf Epic 001 (Domain Entities) und Task 005 (Repository Interfaces).
