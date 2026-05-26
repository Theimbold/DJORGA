# DJORGA Documentation Tree (Master Index)

## Vision & Context
- [Project Vision](../PROJECT_CONTEXT.md)
- [Architecture Overview](./analysis/architecture.md)
- [Current Features](./analysis/features.md)

## 1. Epics (E-xxx)
*Strukturelle Feature-Pakete.*

### Phase 1–19: MVP (Abgeschlossen)

| ID | Titel | Status | Stories |
|:---|:---|:---|:---|
| E-001 | Solution Setup & Clean Architecture | Abgeschlossen | US-001, US-002 |
| E-002 | Rekordbox XML Integration | Abgeschlossen | US-003, US-004, US-005 |
| E-003 | Persistence & SQLite Storage | Abgeschlossen | US-006, US-007 |
| E-004 | Library Management UI | Abgeschlossen | US-008, US-009, US-010 |
| E-005 | UX Navigation & Shell | Abgeschlossen | US-011 |
| E-006 | Advanced Import Workflow | Abgeschlossen | US-012, US-013 |
| E-007 | Media Infrastructure (Metadata/Cover) | Abgeschlossen | US-014, US-015 |
| E-008 | Audio Engine & Player | Abgeschlossen | US-016, US-017 |
| E-009 | Integration Hub (Export) | Abgeschlossen | US-018 |
| E-010 | Visual Excellence (UI Polishing) | Abgeschlossen | US-019 |
| E-011 | Reliability & Validation | Abgeschlossen | US-020 |
| E-012 | Reactive Data Sync | Abgeschlossen | US-021 |
| E-013 | AI Playlist Builder | Abgeschlossen | US-022 |
| E-014 | Advanced Waveform Rendering | Abgeschlossen | US-023 |
| E-015 | Metadata Power-User Tools | Abgeschlossen | US-024, US-025 |
| E-016 | Smart Collections & Rules | Abgeschlossen | US-026, US-027 |
| E-017 | Harmonic Map (Visual Key Analysis) | Abgeschlossen | US-028 |
| E-018 | Performance Batch Processing | Abgeschlossen | US-029, US-030 |
| E-019 | Contextual DNA System | Abgeschlossen | US-031, US-032, US-033 |

### Phase 20+: Post-MVP & Qualitätssicherung

> Die folgenden Epics sind nach einer strukturierten Code-Review entstanden. Sie
> adressieren technische Schulden, Architekturverletzungen und fehlende
> Dokumentation. Die Reihenfolge ist verbindlich — jede Phase baut auf der
> vorherigen auf.

| ID | Titel | Phase | Status | Stories | Blockt |
|:---|:---|:---|:---|:---|:---|
| E-020 | Drag & Drop Excellence | — | In Planung | US-034 | — |
| E-021 | [Namespace-Migration Abschluss](epics/E-021_Namespace_Migration.md) | 1 – Stabilisierung | Offen | US-035, US-036 | E-022–E-026 |
| E-022 | [Domain Layer Bereinigung](epics/E-022_Domain_Layer_Cleanup.md) | 2 – Architektur | Offen | US-037, US-038 | E-023–E-026 |
| E-023 | [Codebase Hygiene](epics/E-023_Codebase_Hygiene.md) | 2 – Architektur | Offen | US-039, US-040 | — |
| E-024 | [Scoring-Logik Korrektheit](epics/E-024_Scoring_Correctness.md) | 3 – Korrektheit | Abgeschlossen | US-041, US-042 | — |
| E-025 | [Error Handling & Logging](epics/E-025_Error_Handling_Logging.md) | 3 – Korrektheit | Abgeschlossen | US-043, US-044 | — |
| E-026 | [Test Coverage](epics/E-026_Test_Coverage.md) | 4 – Qualitätssicherung | Abgeschlossen | US-045, US-046, US-047 | — |
| E-027 | [Sequence Builder Klarheit](epics/E-027_Sequence_Builder_Clarity.md) | 5 – Klarheit | Offen | US-048, US-049 | — |
| E-028 | [4+1 Architekturdokumentation](epics/E-028_41_Architecture_Docs.md) | 5 – Klarheit | Offen | US-050, US-051, US-052, US-053, US-054 | — |

## 2. User Journeys (UJ-xxx)
*Prozessabläufe über mehrere Features.*

| ID | Titel | Beteiligte Stories |
|:---|:---|:---|
| UJ-001 | Erst-Setup & Musik-Import | US-003, US-012, US-029 |
| UJ-002 | Den perfekten Mix vorbereiten (DNA & AI) | US-031, US-022, US-026 |
| UJ-003 | Library Management & Aufräumen | US-024, US-025, US-010 |
| UJ-004 | Export für den Club | US-018, US-005 |

---
*Zuletzt aktualisiert: 26. Mai 2026*
