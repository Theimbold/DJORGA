# Task 012: SqliteTrackRepository Implementierung

## Feature
Repository Implementation

## Aufgabe
Konkrete Implementierung des `ITrackRepository` Interfaces unter Verwendung des `AppDbContext`.

## Schritte
1. Erstellen der Klasse `SqliteTrackRepository` im Verzeichnis `MyApp.Infrastructure/Persistence/Repositories`.
2. Implementierung der CRUD-Methoden (`AddAsync`, `GetByIdAsync`, `GetAllAsync`, `DeleteAsync`).
3. Implementierung der `SearchAsync` Methode (einfache LIKE-Abfrage auf Title/Artist).

## Definition of Done
- Klasse implementiert `ITrackRepository` vollständig.
- Nutzt asynchrone DB-Zugriffe (Async/Await).
- Alle Methoden sind funktionsfähig und fehlerbehandelt.
