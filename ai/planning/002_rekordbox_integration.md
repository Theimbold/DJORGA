# Epic 002: Rekordbox Integration (Infrastructure)

## Status
Abgeschlossen

## Beschreibung
Migration und Refactoring der Rekordbox XML Import-Logik. Ziel ist es, Musikdaten aus Rekordbox-Exporten einzulesen und in die neuen Domänen-Entitäten zu transformieren.

## Features & Tasks

### Feature 1: External Service Abstraction
- [x] **Task 006:** Definition des `IRekordboxService` Interfaces im Application-Layer.

### Feature 2: Infrastructure Implementation
- [x] **Task 007:** Implementierung des `RekordboxXmlService` im Infrastructure-Layer (Migration der Alt-Logik).
- [x] **Task 008:** Implementierung der XML-Parsing-Logik für Tracks und Playlists.

### Feature 3: Use Case Integration
- [x] **Task 009:** Erstellen des `ImportRekordboxXmlUseCase` im Application-Layer.

### Feature 4: Safety & Data Integrity
- [x] **Task 065:** Automatisches Backup der Quell-XML vor jedem Importvorgang.
- [x] **Task 066:** Schema-Validierung (XSD) vor dem Einlesen, um Korruption zu verhindern.
- [x] **Task 067:** Read-Only Lock: Sicherstellen, dass der Import-Stream die Datei niemals im Schreibmodus öffnet.

## Abhängigkeiten
- Basiert auf Epic 001 (Core Domain vorhanden).
