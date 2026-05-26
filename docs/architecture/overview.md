# Architecture Overview

DJORGA is built using **Clean Architecture** principles to ensure separation of concerns, testability, and maintainability.

## Layers

### 1. Domain (MyApp.Domain)
The core of the application. It contains entities, value objects, and domain logic. It has **zero dependencies** on other layers.
- **Entities:** `Track`, `Playlist`, `SmartCollection`.
- **Logic:** Harmonic compatibility rules, BPM calculation.

### 2. Application (MyApp.Application)
Contains business-specific use cases and interfaces for infrastructure services.
- **Use Cases:** `ImportRekordboxXml`, `BuildAiPlaylist`, `UpdateTrackMetadata`.
- **Interfaces:** `ITrackRepository`, `IAiPlaylistBuilder`, `IMetadataService`.
- **DTOs:** `ScoredTrack`, `AiPlaylistRequest`.

### 3. Infrastructure (MyApp.Infrastructure)
Implements the interfaces defined in the Application layer using specific technologies.
- **Persistence:** EF Core with SQLite.
- **External:** TagLib# for metadata, NAudio for audio engine.
- **Services:** `RekordboxXmlReader`, `WaveformGenerator`.

### 4. Desktop (MyApp.Desktop)
The presentation layer using Avalonia UI and the MVVM pattern.
- **ViewModels:** `LibraryViewModel`, `AIBuilderViewModel`.
- **Views:** Avalonia `.axaml` files.
- **Services:** `NavigationService`, `FilePickerService`.

## Data Flow
1. **User Action:** User clicks "Import" in `LibraryView`.
2. **ViewModel:** `LibraryViewModel` calls `ImportRekordboxXmlUseCase.ExecuteAsync()`.
3. **Application:** The Use Case uses `IRekordboxXmlReader` (Interface) to read data and `ITrackRepository` (Interface) to save it.
4. **Infrastructure:** `RekordboxXmlReader` (Implementation) parses the file; `SqliteTrackRepository` (Implementation) saves to the database.

## Key Design Patterns
- **MVVM:** Clean separation between UI and logic in the Desktop layer.
- **Dependency Injection:** Microsoft.Extensions.DependencyInjection is used to wire up the layers.
- **Repository Pattern:** Decouples the Application layer from specific persistence technology.
- **Use Case / Interactor:** Encapsulates business actions.

---

## 4+1 Architektur-Sichten (Kruchten)

Die detaillierte Architekturdokumentation folgt dem **4+1-Sichten-Modell** nach
Philippe Kruchten. Jede Sicht beleuchtet das System aus einer anderen Perspektive:

| Sicht | Dokument | Beschreibung |
|:---|:---|:---|
| Logische Sicht | [01_logical_view.md](views/01_logical_view.md) | Schichten, Klassen, Beziehungen |
| Prozess-Sicht | [02_process_view.md](views/02_process_view.md) | Laufzeitverhalten, async Abläufe |
| Implementierungs-Sicht | [03_implementation_view.md](views/03_implementation_view.md) | Projektstruktur, Module, Build |
| Physische Sicht | [04_physical_view.md](views/04_physical_view.md) | Deployment, Dateisystem, externe Systeme |
| Szenarien (+1) | [05_scenarios_view.md](views/05_scenarios_view.md) | Key Use Cases als Querschnitt |
