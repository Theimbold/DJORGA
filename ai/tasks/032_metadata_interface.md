# Task 032: Metadata Service Interface

## Feature
Multi-Format Metadata Service

## Aufgabe
Definition der Schnittstelle für den Zugriff auf Datei-Metadaten, entkoppelt von spezifischen Bibliotheken wie TagLib#.

## Schritte
1. Erstellen von `IMetadataService` in `MyApp.Application/Interfaces/External`.
2. Definition der Methode `Task<TrackMetadata> ExtractMetadataAsync(string filePath)`.
3. Erstellung eines DTOs `TrackMetadata` im Application-Layer.

## Definition of Done
- Interface ist im Application-Layer vorhanden.
- Unterstützt asynchrone Extraktion.
- Keine Abhängigkeit zu TagLib# im Interface.
