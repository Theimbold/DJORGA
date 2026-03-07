# Task 011: Entity Mapping (Fluent API)

## Feature
Database Setup (EF Core)

## Aufgabe
Konfiguration der Datenbank-Mappings, um die Domänen-Entitäten (`Track`, `Playlist`) sauber auf SQLite-Tabellen abzubilden.

## Schritte
1. Überschreiben der `OnModelCreating` Methode im `AppDbContext`.
2. Konfiguration der Primärschlüssel (Guids).
3. Festlegen von Pflichtfeldern (z.B. Title) und Längenbeschränkungen.
4. Konfiguration der One-to-Many Beziehung zwischen `Playlist` und `Track`.

## Definition of Done
- Fluent API Konfiguration ist vollständig.
- Relationen zwischen Tracks und Playlists sind korrekt definiert.
- Keine Verletzung der Clean Architecture (Domain-Entitäten bleiben rein).
