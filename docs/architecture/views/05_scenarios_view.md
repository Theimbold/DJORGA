# Szenarien-Sicht (+1) — DJORGA

> **Kruchten 4+1:** Die Szenarien-Sicht dient als Klammer über alle anderen
> Sichten. Schlüssel-Use-Cases zeigen, wie logische, prozessuale, implementierungs-
> und physische Elemente im echten Betrieb zusammenspielen.
> Zielgruppe: Alle Stakeholder.

---

## Szenario 1: Erst-Import der Rekordbox-Bibliothek

**User Journey:** [UJ-001 — Erst-Setup & Musik-Import](../../docs/user_journeys/UJ-002_Set_Preparation.md)

**Beschreibung:** Der DJ exportiert `rekordbox.xml` aus Rekordbox und importiert
ihn in DJORGA. 1.000+ Tracks werden in die lokale SQLite-Datenbank übernommen.

| Sicht | Beteiligte Elemente |
|:---|:---|
| **Logisch** | `ImportRekordboxXmlUseCase`, `RekordboxXmlReader`, `SqliteTrackRepository`, `Track` |
| **Prozess** | Async-Import auf Background-Thread; `BulkTracksAddedEvent` → `LibraryViewModel` |
| **Implementierung** | `DJORGA.Application/UseCases/`, `DJORGA.Infrastructure/ApiClients/` |
| **Physisch** | `rekordbox.xml` (Quelle, lokal), `djorga.db` (Ziel, lokal) |

**Ablauf (vereinfacht):**
1. Nutzer wählt XML-Datei über `AvaloniaFilePickerService`.
2. `ImportRekordboxXmlUseCase` liest via `IRekordboxXmlReader`.
3. Tracks werden via `ITrackRepository.AddRangeAsync()` in SQLite gespeichert.
4. `BulkTracksAddedEvent` triggert Update in `LibraryViewModel`.
5. Tracks erscheinen in der Library-Ansicht.

---

## Szenario 2: Set-Vorbereitung mit DNA und Sequence Builder

**User Journey:** [UJ-002 — Den perfekten Mix vorbereiten](../../docs/user_journeys/UJ-002_Set_Preparation.md)

**Beschreibung:** Der DJ weist Tracks eine DNA (Mood + TimeContext) zu, wählt
einen Starter-Track und lässt eine 10-Track-Sequenz für einen Peak-Time-Set generieren.

| Sicht | Beteiligte Elemente |
|:---|:---|
| **Logisch** | `Track`, `TrackMood`, `TrackTimeContext`, `HarmonicSequenceBuilderService`, `HarmonicScoringService`, `ScoringWeights` |
| **Prozess** | Synchroner Greedy-Algorithmus; Ergebnis als `IEnumerable<ScoredTrack>` |
| **Implementierung** | `DJORGA.Application/Services/`, `DJORGA.Desktop/ViewModels/AIBuilderViewModel.cs` |
| **Physisch** | Alle Daten lokal aus `djorga.db`; kein Netzwerkzugriff (MVP) |

**Ablauf (vereinfacht):**
1. DJ weist Tracks im Edit-Dialog DNA via `DnaPickerControl` zu.
2. Im Set-Builder wählt DJ Starter-Track und gewünschte Länge.
3. `HarmonicSequenceBuilderService.CreateSequence()` berechnet Greedy-Sequenz.
4. Jeder Schritt wird via `HarmonicScoringService.CalculateBreakdown()` bewertet.
5. Ergebnis: Liste von `ScoredTrack` mit Key-, BPM- und DNA-Score.

---

## Szenario 3: Smart Collection filtern

**User Journey:** UJ-003 — Library Management & Aufräumen

**Beschreibung:** Der DJ erstellt eine Smart Collection „Peak Time Techno" mit
Regeln: Genre = Techno, BPM > 128, Mood = Energetic.

| Sicht | Beteiligte Elemente |
|:---|:---|
| **Logisch** | `SmartCollection`, `FilterRule`, `RuleEvaluatorService`, `ITrackRepository` |
| **Prozess** | LINQ-Expression-Kompilierung zur Laufzeit; Query direkt gegen SQLite |
| **Implementierung** | `DJORGA.Application/Services/RuleEvaluatorService.cs`, `DJORGA.Desktop/ViewModels/SmartCollectionEditorViewModel.cs` |
| **Physisch** | Regelauswertung in-memory (LINQ) oder als SQL (EF Core Query Translation) |

**Ablauf (vereinfacht):**
1. DJ öffnet den Smart Collection Editor.
2. DJ fügt Regeln hinzu: `Genre Contains "Techno"`, `Bpm > 128`, `Mood = Energetic`.
3. `RuleEvaluatorService.ApplyRules()` kompiliert LINQ-Expressions.
4. `ITrackRepository` führt Query gegen SQLite aus.
5. Ergebnis: gefilterte Track-Liste erscheint dynamisch.

---

## Querbezüge zwischen den Sichten

| Element | Logisch | Prozess | Implementierung | Physisch |
|:---|:---|:---|:---|:---|
| `Track` | Domain Entity | Datenmutation via Events | `DJORGA.Domain/Entities/Track.cs` | `djorga.db` |
| Import | UseCase + Reader | Async Background Thread | `UseCases/`, `Infrastructure/` | `rekordbox.xml` → `djorga.db` |
| Waveform | `WaveformControl` | Hintergrund-Analyse | `Controls/`, `Infrastructure/` | `Cover-Art-Cache` |
| Sequenz | Builder + Scorer | Sync, greedy | `Application/Services/` | nur lokal |

---

*Zugehörige Dokumente: [01_logical_view.md](01_logical_view.md),
[02_process_view.md](02_process_view.md),
[03_implementation_view.md](03_implementation_view.md),
[04_physical_view.md](04_physical_view.md)*
