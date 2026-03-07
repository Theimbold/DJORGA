# Task 010: EF Core & SQLite Setup

## Feature
Database Setup (EF Core)

## Aufgabe
Vorbereitung des Infrastructure-Layers für die Datenbank-Anbindung. Installation der notwendigen Pakete und Erstellung der Context-Klasse.

## Schritte
1. Hinzufügen der NuGet-Pakete zu `MyApp.Infrastructure`:
   - `Microsoft.EntityFrameworkCore.Sqlite`
   - `Microsoft.EntityFrameworkCore.Design`
2. Erstellen des Verzeichnisses `MyApp.Infrastructure/Persistence/EntityFramework`.
3. Erstellen der Klasse `AppDbContext`, die von `DbContext` erbt.
4. Definieren der `DbSet<Track>` und `DbSet<Playlist>` Properties.

## Definition of Done
- NuGet-Pakete sind erfolgreich referenziert.
- `AppDbContext` ist im korrekten Namespace vorhanden.
- Projekt `MyApp.Infrastructure` lässt sich ohne Fehler bauen.
