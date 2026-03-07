# Task 033: TagLib# Implementation

## Feature
Multi-Format Metadata Service

## Aufgabe
Konkrete Implementierung der Metadaten-Extraktion für alle gängigen Audioformate.

## Schritte
1. Hinzufügen des NuGet-Pakets `TagLibSharp` zu `MyApp.Infrastructure`.
2. Implementierung des `TagLibMetadataService`.
3. Mapping von Tags:
   - Title, Artist, Album, Genre
   - BPM, Key (falls in Tags vorhanden)
   - Duration
4. Fehlerbehandlung für beschädigte oder fehlende Tags.

## Definition of Done
- Service liest Metadaten aus .mp3, .flac, .wav und .aiff.
- Korrektes Mapping auf die Domänen-Entitäten.
