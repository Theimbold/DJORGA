# Epic 015: Library Management & Metadata Power-User Tools

## Status: Abgeschlossen
DJORGA verfügt nun über vollständige Management-Funktionen für die Musikbibliothek.

## Phasen & Subtasks

### Phase 1: Das Fundament (Data Access)
- [x] **Task 074:** Erweiterung von `ITrackRepository` um `UpdateAsync` und `DeleteAsync`.
- [x] **Task 075:** Implementierung im `SqliteTrackRepository`.
- [x] **Task 076:** Repository Search & Event Logic.

### Phase 2: Die Brücke (Application Logic)
- [x] **Task 077:** `UpdateMetadataService` (Synchronisation mit physischer Datei via TagLib#).
- [x] **Task 078:** `DeleteTrackUseCase` (Sicherheits-Logik).

### Phase 3: Die Interaktion (UI)
- [x] **Task 079:** Context Menu in der `LibraryView` implementiert.
- [x] **Task 080:** Edit-Dialog für Track-Metadaten (Corporate Design 2.0).
- [x] **Task 081:** Lösch-Bestätigungsdialog inkl. Datei-Option.

### Phase 4: Power-Tools
- [x] **Task 082:** Multi-Selection Support für die Track-Liste.
- [x] **Task 083:** Batch-Edit Funktionalität implementiert.

## Definition of Done
- Tracks können gelöscht und bearbeitet werden.
- Änderungen sind permanent in der DB UND in der Datei gespeichert.
- Batch-Processing für große Mengen an Tracks aktiv.
