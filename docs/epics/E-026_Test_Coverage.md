# Epic E-026: Test Coverage

## Status: Abgeschlossen
**Typ:** Qualitätssicherung
**Phase:** 4 – Qualitätssicherung

## Hintergrund
Aktuell existieren genau drei Testdateien:
- `HarmonicScoringServiceTests.cs`
- `RuleEvaluatorTests.cs`
- `CamelotKeyTests.cs`

Nicht getestet sind: Use Cases, Repositories, Import-Workflow, ViewModels,
der Sequence Builder, der Background Analysis Service. Das entspricht nicht
den Qualitätsanforderungen nach Sommerville, der systematisches Testen als
integralen Bestandteil des Software Engineering Prozesses betrachtet.

## Ziel
Die kritischen Schichten Application und Domain haben eine angemessene
Testabdeckung. Priorität liegt auf Use Cases und dem Sequence Builder,
da diese die Kernfachlichkeit des Systems darstellen.

## Verknüpfte User Stories
- [US-045: Use Case Tests](../user_stories/US-045_UseCase_Tests.md)
- [US-046: Repository Tests](../user_stories/US-046_Repository_Tests.md)
- [US-047: Sequence Builder Tests](../user_stories/US-047_SequenceBuilder_Tests.md)

## Priorisierte Test-Bereiche

| Priorität | Komponente | Begründung |
|:---|:---|:---|
| 1 | `AiPlaylistBuilderService` (Sequence Builder) | Kernlogik, wurde in E-027 umbenannt |
| 2 | `HarmonicScoringService` — Symmetrie | Neuer Bug aus E-024 |
| 3 | Use Cases (`ImportRekordboxXmlUseCase`, `UpdateTrackMetadataUseCase`) | Geschäftskritisch |
| 4 | `RuleEvaluatorService` — Edge Cases | Komplexe Expression-Logik |
| 5 | `SqliteTrackRepository` (Integration Test) | Persistenz-Korrektheit |

## Akzeptanzkriterien
- [x] Alle Use Cases haben mindestens einen Happy-Path- und einen
      Error-Path-Test.
- [x] Der Sequence Builder hat Tests für: leerer Pool, Pool kleiner als
      gewünschte Länge, Mindest-Score-Schwelle unterschritten.
- [x] `dotnet test` läuft ohne Fehler durch.
- [x] Testprojekt referenziert alle notwendigen Layer korrekt.

## Abhängigkeiten
- Erfordert: E-021, E-022, E-024, E-025
- Stark empfohlen: E-027 (damit Tests die finalen Klassennamen verwenden)
