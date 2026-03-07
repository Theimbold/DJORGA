# Task 046: Rekordbox Export Service Interface

## Feature
XML Export Service (Roundtrip)

## Aufgabe
Definition der Schnittstelle, um DJORGA-Playlists zurück in das Rekordbox-kompatible XML-Format zu überführen.

## Schritte
1. Erstellen von `IRekordboxExportService` in `MyApp.Application/Interfaces/External`.
2. Definition der Methode `Task ExportPlaylistsAsync(IEnumerable<Playlist> playlists, string targetPath)`.

## Definition of Done
- Interface ist im Application-Layer vorhanden.
- Entkoppelt von der XML-Serialisierungs-Logik.
