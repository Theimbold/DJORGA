# Epic 006: Rekordbox Import-Workflow

## Status
Abgeschlossen

## Beschreibung
Implementierung des vollständigen Import-Prozesses für Rekordbox XML-Dateien. Fokus liegt auf einer intuitiven Benutzerführung (File-Selection) und sofortigem Feedback nach dem Import.

## Features & Tasks

### Feature 1: File Selection Service
- [x] **Task 026:** Definition des `IFilePickerService` (Abstraktion für UI-Dialoge).
- [x] **Task 027:** Implementierung des `AvaloniaFilePickerService` im Desktop-Layer.

### Feature 2: Import Integration
- [x] **Task 028:** Erweiterung des `LibraryViewModel` um das `ImportCommand`.
- [x] **Task 029:** Verknüpfung mit dem `ImportRekordboxXmlUseCase` aus dem Application-Layer.

### Feature 3: UI Feedback
- [x] **Task 030:** Aktualisierung der `LibraryView` (Import Button im Header).
- [x] **Task 031:** Implementierung einer Benachrichtigung bei Erfolg (Anzahl importierter Tracks).

## UX-Leitlinien
- Der Nutzer soll mit maximal zwei Klicks zum Import gelangen.
- Während des Imports wird ein Fortschrittsbalken angezeigt.
- Automatische Aktualisierung der Tabelle nach Abschluss.
