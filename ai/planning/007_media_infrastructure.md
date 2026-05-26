# Epic 007: Media Infrastructure & Metadata

## Status
Abgeschlossen

## Beschreibung
Implementierung der High-Performance Media-Engine zur Extraktion von Metadaten (.aiff, .flac, .wav) und Cover-Arts. Fokus liegt auf einer effizienten Speicher-Strategie für Visuals und der Erweiterung des Datenmodells.

## Features & Tasks

### Feature 1: Multi-Format Metadata Service
- [x] **Task 032:** Definition des `IMetadataService` im Application-Layer.
- [x] **Task 033:** Implementierung des `TagLibMetadataService` im Infrastructure-Layer (Unterstützung für .aiff, .flac, .wav, .mp3).

### Feature 2: Visual Asset Management (Cover Art)
- [x] **Task 034:** Implementierung des `ICoverCacheService` (Extraktion, Skalierung via SkiaSharp und lokales Caching).
- [x] **Task 035:** Konfiguration des lokalen Speicherpfads (`/AppData/Local/DJORGA/Covers`).

### Feature 3: Data Model & Integration
- [x] **Task 036:** EF Core Migration: Update der `Track`-Tabelle um `Genre`, `CoverArtPath` und `IsAnalyzed`.
- [x] **Task 037:** Erweiterung des `ImportRekordboxXmlUseCase` um die automatische Metadaten-Vervollständigung.

## UX-Leitlinien
- Cover-Arts sollen beim Scrollen in der Bibliothek verzögerungsfrei erscheinen (Lazy Loading von Thumbnails).
- Metadaten-Extraktion erfolgt asynchron im Hintergrund, um den Import-Prozess nicht zu blockieren.
