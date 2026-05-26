# Prozess-Sicht (Process View) — DJORGA

> **Kruchten 4+1:** Die Prozess-Sicht beschreibt das dynamische Verhalten des
> Systems zur Laufzeit: Prozesse, Threads, Nebenläufigkeit und wichtige
> Interaktionsabläufe. Zielgruppe: Entwickler, Tester.

---

## 1. Laufzeitkontext

DJORGA läuft als **Single-Process-Desktopanwendung** auf dem Rechner des DJs.
Es gibt keinen Server-Prozess. Die wesentlichen Nebenläufigkeitspunkte sind:

- **UI-Thread (Avalonia Dispatcher):** Alle View-Updates müssen auf dem UI-Thread erfolgen.
- **Background Worker Threads:** Import, Waveform-Analyse und Datenbankoperationen
  laufen async/await, um den UI-Thread nicht zu blockieren.

---

## 2. Ablauf: Rekordbox XML Import

Dieser Ablauf ist der primäre Einstiegspunkt neuer Nutzer.

```
Nutzer                LibraryViewModel      ImportUseCase        RekordboxXmlReader    SqliteRepository
  │                        │                     │                       │                    │
  │── [Klick Import] ────►│                     │                       │                    │
  │                        │── ExecuteAsync() ──►│                       │                    │
  │                        │                     │── ReadAsync(path) ───►│                    │
  │                        │                     │◄── IEnumerable<Track>─┤                    │
  │                        │                     │── AddRangeAsync() ────────────────────────►│
  │                        │                     │◄── Saved ─────────────────────────────────┤
  │                        │◄── BulkTracksAddedEvent ─────────────────────────────────────────
  │                        │── Update ObservableCollection ─────────────────────────────────►│
  │◄── UI aktualisiert ────│                     │                       │                    │
```

---

## 3. Ablauf: Waveform-Analyse (Background Service)

```
BackgroundAnalysisService          NAudioWaveformGenerator         PlayerViewModel
  │                                         │                            │
  │── [Track ohne Waveform erkannt] ───────►│                            │
  │                                         │── AnalyzeAsync(filePath)   │
  │                                         │   (läuft auf Thread Pool)  │
  │                                         │── Peaks berechnen          │
  │◄── FrequencyPeak[] zurück ──────────────│                            │
  │── Track.IsAnalyzed = true               │                            │
  │── DB aktualisieren                      │                            │
  │── WaveformReadyEvent ──────────────────────────────────────────────►│
  │                                                                       │── WaveformControl neu rendern
```

---

## 4. Ablauf: Set-Sequenz bauen

```
Nutzer          AIBuilderViewModel      HarmonicSequenceBuilder      HarmonicScoringService
  │                   │                          │                            │
  │── [Build-Klick]──►│                          │                            │
  │                   │── CreateSequence() ─────►│                            │
  │                   │                          │── CalculateBreakdown() ───►│ (für jeden Track im Pool)
  │                   │                          │◄── ScoreBreakdown ─────────│
  │                   │                          │── Greedy: BestMatch wählen │
  │                   │◄── IEnumerable<ScoredTrack> ────────────────────────  │
  │◄── Liste anzeigen─│                          │                            │
```

---

## 5. Reaktives Daten-Sync-Modell

DJORGA verwendet ein Event-basiertes Modell für UI-Updates nach
Datenmutationen. ViewModels lauschen auf Domain-Events und aktualisieren
ihre `ObservableCollection` entsprechend.

| Event | Auslöser | Empfänger |
|:---|:---|:---|
| `BulkTracksAddedEvent` | Import Use Case | `LibraryViewModel` |
| `TrackUpdatedEvent` | UpdateMetadata Use Case | `LibraryViewModel`, `PlayerViewModel` |
| `WaveformReadyEvent` | BackgroundAnalysisService | `PlayerViewModel` |

---

## 6. Threading-Regeln

1. Alle `async`-Methoden verwenden `ConfigureAwait(false)` in der
   Application- und Infrastructure-Schicht.
2. UI-Updates (Mutations auf `ObservableCollection`) werden über
   `Dispatcher.UIThread.InvokeAsync()` zurück auf den UI-Thread geleitet.
3. Kein direkter Thread-Zugriff auf den `AppDbContext` aus mehreren Threads.

---

*Zugehörige User Journeys: [UJ-001](../../docs/user_journeys/), [UJ-002](../../docs/user_journeys/)*
