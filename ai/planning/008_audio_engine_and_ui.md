# Epic 008: Audio Engine & Kontextuelle UI

## Status
In Planung

## Beschreibung
Implementierung der zustandsbasierten UI (Empty vs. Loaded) und Aufbau der Audio-Streaming Engine inklusive Waveform-Visualisierung.

## Features & Tasks

### Feature 1: UI State Management
- [ ] **Task 038:** Implementierung des `AppStateService` zur Steuerung der Sichtbarkeit (IsLibraryLoaded, CurrentTrack).
- [ ] **Task 039:** Gestaltung des "Empty State" Screens (Onboarding).

### Feature 2: Audio Engine (Streaming)
- [ ] **Task 040:** Setup der `NAudio` Engine mit Circular Buffering für 100MB+ Files.
- [ ] **Task 041:** Implementierung der Peak-Extraktion (Waveform-Daten) beim Import.

### Feature 3: Player UI (High-End UX)
- [ ] **Task 042:** Implementierung der Player-Bar (Footer) mit Fokus auf Waveform und Cover-Art.
- [ ] **Task 043:** Animiertes Ein-/Ausblenden der Player-Bar.

## UX-Leitlinien
- Keine "toten" UI-Flächen: Zeige nur, was der Nutzer gerade braucht.
- Player muss innerhalb von 50ms auf "Play" reagieren (Streaming-Start).
