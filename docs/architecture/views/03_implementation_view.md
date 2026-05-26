# Implementierungs-Sicht (Implementation View) — DJORGA

> **Kruchten 4+1:** Die Implementierungs-Sicht beschreibt die statische
> Organisation des Quellcodes: Projektstruktur, Module, Abhängigkeiten und
> Build-Prozess. Zielgruppe: Entwickler, DevOps.

---

## 1. Solution-Struktur

```
DJORGA.sln
├── DJORGA.Domain/              ← Kern-Schicht (keine externen Abhängigkeiten)
│   ├── Entities/               ← Track, Playlist, SmartCollection
│   ├── ValueObjects/           ← CamelotKey, FilterRule, TrackMood, ScoringWeights
│   └── Rules/                  ← (reserviert für Domain-Regeln)
│
├── DJORGA.Application/         ← Use Cases & Interfaces
│   ├── UseCases/               ← ImportRekordboxXmlUseCase, UpdateTrackMetadataUseCase
│   ├── Services/               ← HarmonicSequenceBuilder, HarmonicScoring, RuleEvaluator
│   ├── Interfaces/             ← ITrackRepository, ISequenceBuilder, IMetadataService
│   └── DTOs/                   ← ScoredTrack, TrackMetadata, FrequencyPeak, MapGraph
│
├── DJORGA.Infrastructure/      ← Framework-Implementierungen
│   ├── Persistence/            ← AppDbContext, EF Core Migrations
│   ├── Repositories/           ← SqliteTrackRepository
│   └── ApiClients/             ← RekordboxXmlReader, TagLibMetadataService, NAudio
│
├── DJORGA.Desktop/             ← Avalonia UI (Presentation Layer)
│   ├── ViewModels/             ← MainViewModel, LibraryViewModel, TrackViewModel, ...
│   ├── Views/                  ← *.axaml Dateien
│   ├── Controls/               ← DnaPickerControl, WaveformControl
│   ├── Converters/             ← MoodToColorConverter, BitmapAssetValueConverter
│   ├── Services/               ← NavigationService, FilePickerService, AppStateService
│   └── Assets/                 ← Icons, Fonts
│
├── DJORGA.Api/                 ← (Zweck zu klären — siehe E-023 / ADR-003)
│
└── DJORGA.Tests/               ← xUnit Test-Projekt
    ├── Domain/
    │   └── ValueObjects/       ← CamelotKeyTests
    └── Application/
        └── Services/           ← HarmonicScoringServiceTests, RuleEvaluatorTests
```

---

## 2. Externe Abhängigkeiten (NuGet)

| Paket | Version | Verwendung | Layer |
|:---|:---|:---|:---|
| Avalonia | 11.x | UI-Framework | Desktop |
| CommunityToolkit.Mvvm | 8.x | MVVM-Boilerplate, ObservableProperty | Desktop |
| Microsoft.EntityFrameworkCore.Sqlite | 8.x | Datenbankzugriff | Infrastructure |
| TagLibSharp | 2.x | Datei-Metadaten (MP3, FLAC, …) | Infrastructure |
| NAudio | 2.x | Audio-Engine, Waveform-Analyse | Infrastructure |
| SkiaSharp | 2.x | Hardware-beschleunigtes Waveform-Rendering | Desktop |
| Microsoft.Extensions.DependencyInjection | 8.x | DI-Container | Desktop |
| Microsoft.Extensions.Logging | 8.x | Strukturiertes Logging *(E-025)* | Application |
| xunit | 2.x | Unit Testing | Tests |

---

## 3. Build-Prozess

```bash
# Abhängigkeiten wiederherstellen
dotnet restore

# Kompilieren (Release)
dotnet build --configuration Release

# Tests ausführen
dotnet test

# Anwendung starten
dotnet run --project DJORGA.Desktop
```

**Ziel-Framework:** .NET 8 (`net8.0`)

---

## 4. Wichtige Konventionen

- **Namespaces** folgen der Ordnerstruktur: `DJORGA.<Layer>.<Ordner>.<Klasse>`.
- **Interfaces** beginnen mit `I` und liegen im `Interfaces/`-Unterordner des jeweiligen Layers.
- **DTOs** sind reine Datencontainer ohne Logik (nur Properties).
- **ViewModels** enden auf `ViewModel` und erben von `ViewModelBase`.
- **Tests** spiegeln die Produktionsstruktur: `DJORGA.Tests/Application/Services/`
  entspricht `DJORGA.Application/Services/`.

---

*Zugehörige Dokumente: [NAMING_MIGRATION.md](../../technical/NAMING_MIGRATION.md), [BUILD_NOTES.md](../../technical/BUILD_NOTES.md)*
