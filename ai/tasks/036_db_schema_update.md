# Task 036: Database Schema Update (Migration)

## Feature
Data Model & Integration

## Aufgabe
Erweiterung der Datenbank-Struktur, um die neuen Metadaten dauerhaft zu speichern.

## Schritte
1. Aktualisierung der `Track`-Entität in `MyApp.Domain` (Properties hinzufügen).
2. Konfiguration der neuen Felder in `AppDbContext` (Fluent API).
3. (Optional) Erstellen einer Migration oder Nutzung von `EnsureCreated` für den Prototyp-Status.

## Definition of Done
- Die SQLite-Datenbank enthält Spalten für Genre und CoverArtPath.
- Bestehende Daten bleiben (wenn möglich) erhalten oder Schema wird sauber neu initialisiert.
