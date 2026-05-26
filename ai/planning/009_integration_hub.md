# Epic 009: Integration Hub & XML Roundtrip

## Status
In Arbeit

## Beschreibung
Implementierung eines zentralen Hubs für den Datenaustausch mit Rekordbox. Fokus liegt auf der Benutzerführung beim Import (Assistent) und der Möglichkeit, Daten (Playlists) zurück in das Rekordbox-Format zu exportieren.

## Features & Tasks

### Feature 1: Integration Assistant (Import Hilfe)
- [x] **Task 044:** Implementierung eines geführten Import-Wizards (UI-Komponente).
- [x] **Task 045:** Auto-Detection Logik für Standard-Rekordbox Pfade auf Windows.

### Feature 2: XML Export Service (Roundtrip)
- [ ] **Task 046:** Definition des `IRekordboxExportService` im Application-Layer.
- [ ] **Task 047:** Implementierung des `RekordboxXmlWriter` im Infrastructure-Layer (Generierung valider Rekordbox XML-Strukturen).

### Feature 3: Export UI
- [ ] **Task 048:** Gestaltung des Export-Bereichs (Auswahl der Playlists, Zielpfad-Wahl).
- [ ] **Task 049:** "How-To" Integration (Anleitung für den Re-Import in Rekordbox).

## UX-Leitlinien
- Der Nutzer soll sich niemals fragen: "Wo ist meine Datei?" oder "Was mache ich jetzt mit dem Export?".
- Klare visuelle Trennung zwischen "Daten holen" und "Daten bringen".
