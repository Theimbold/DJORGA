# Architektur-Dokumentation (Stand V1.0)

## 1. Übersicht
DJORGA basiert auf der **Clean Architecture**. Das System ist in vier entkoppelte Layer unterteilt, um maximale Testbarkeit und Performance zu gewährleisten.

## 2. Layer-Struktur

### MyApp.Domain (Kern)
- **Entities:** `Track`, `Playlist` (Implementieren `INotifyPropertyChanged` für reaktive UI).
- **Value Objects:** `BpmRange`, `KeyCompatibility` (Camelot Wheel Logik).
- **Regeln:** Domänenspezifische Validierung und Logik.

### MyApp.Application (Logik)
- **Interfaces:** Abstraktionen für Repositories (`ITrackRepository`), Audio (`IAudioPlayerService`), Metadata (`IMetadataService`) und UI (`INavigationService`, `IFilePickerService`).
- **Use Cases:** `ImportRekordboxXmlUseCase` (Orchestrierung von Import, DB-Speicherung und Hintergrund-Analyse).
- **DTOs:** `TrackMetadata` für den Datenaustausch.

### MyApp.Infrastructure (Technik)
- **Persistence:** SQLite mit EF Core 8.0.13. Automatisches DB-Initialisierungs-System.
- **External Services:** 
  - `RekordboxXmlService`: Parsing und Generierung von Rekordbox XML (Roundtrip).
  - `TagLibMetadataService`: Extraktion von Tags aus .aiff, .flac, .wav, .mp3.
  - `LocalCoverCacheService`: Bildskalierung via SkiaSharp und Caching in AppData.
  - `NAudioPlayerService`: High-Performance Audio-Streaming (Streaming-Ready für 100MB+ Files).
  - `NAudioWaveformService`: Peak-Extraktion und Binär-Caching für Wellenformen.

### MyApp.Desktop (UI)
- **Framework:** Avalonia UI 11.0.
- **Pattern:** MVVM mit CommunityToolkit.Mvvm (Source Generators).
- **UX-Features:** Kontextuelle Sichtbarkeit (Onboarding vs. Library), reaktive Echtzeit-Updates ohne Refresh-Button, CrossFade Transitions.

## 3. Datenfluss (Reaktiver Kreislauf)
`Import/Analyse` → `Repository` → `Event (TrackAdded)` → `ViewModel` → `UI (Auto-Update)`
