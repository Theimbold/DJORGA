# Epic 002: Rekordbox Integration (Infrastructure)

## Status
In Planung

## Beschreibung
Migration und Refactoring der Rekordbox XML Import-Logik. Ziel ist es, Musikdaten aus Rekordbox-Exporten einzulesen und in die neuen Domänen-Entitäten zu transformieren.

## Features & Tasks

### Feature 1: External Service Abstraction
- [ ] **Task 006:** Definition des `IRekordboxService` Interfaces im Application-Layer.

### Feature 2: Infrastructure Implementation
- [ ] **Task 007:** Implementierung des `RekordboxXmlService` im Infrastructure-Layer (Migration der Alt-Logik).
- [ ] **Task 008:** Implementierung der XML-Parsing-Logik für Tracks und Playlists.

### Feature 3: Use Case Integration
- [ ] **Task 009:** Erstellen des `ImportRekordboxXmlUseCase` im Application-Layer.

## Abhängigkeiten
- Basiert auf Epic 001 (Core Domain vorhanden).
