# Epic E-024: Scoring-Logik Korrektheit

## Status: Abgeschlossen
**Typ:** Fachlicher Fehler & Erweiterbarkeit
**Phase:** 3 – Korrektheit & Robustheit

## Hintergrund
Der `HarmonicScoringService` enthält zwei konkrete Probleme:

**Problem 1: Asymmetrischer BPM-Score**
Die BPM-Differenz wird berechnet als `(diff / bpmA) * 100`. Das bedeutet: Der
Score für Track A→B ist ein anderer als für Track B→A (da durch unterschiedliche
`bpmA`-Werte dividiert wird). Für einen Sequenz-Builder, der Tracks transitiv
vergleicht, ist das mathematisch falsch. Der Mittelwert beider BPMs sollte als
Divisor verwendet werden.

**Problem 2: Hartcodierte Gewichte (Magic Numbers)**
Die Gewichte `WeightKey = 0.30`, `WeightBpm = 0.30`, `WeightDna = 0.40` sind
private Konstanten, die nicht konfigurierbar sind. Ein DJ, der einen
Warm-Up-Set plant, gewichtet Energie (DNA) anders als einer, der einen
harmonischen Journey-Mix plant. Die Gewichte müssen injizierbar sein.

## Ziel
Der Scoring-Service liefert für alle Track-Paare konsistente, symmetrische
Ergebnisse und ist über eine Konfigurationsklasse steuerbar.

## Verknüpfte User Stories
- [US-041: BPM-Score symmetrisch machen](../user_stories/US-041_Symmetric_BPM_Score.md)
- [US-042: Scoring-Gewichte konfigurierbar machen](../user_stories/US-042_Configurable_Weights.md)

## Technische Umsetzung (Leitlinien)
1. BPM-Score: Divisor auf `(bpmA + bpmB) / 2.0` ändern.
2. `ScoringWeights`-Klasse (Value Object) im Domain oder Application Layer
   einführen: `KeyWeight`, `BpmWeight`, `DnaWeight`.
3. `HarmonicScoringService` erhält `ScoringWeights` per Dependency Injection.
4. Default-Werte bleiben `0.30 / 0.30 / 0.40`.

## Akzeptanzkriterien
- [x] `Score(A, B) == Score(B, A)` für alle Track-Paare (Unit Test).
- [x] `ScoringWeights` ist eine eigenständige, injizierbare Klasse.
- [x] Bestehende Tests laufen weiterhin durch.
- [x] Neue Unit Tests decken Symmetrie und Gewichtungs-Konfiguration ab.

## Abhängigkeiten
- Erfordert: E-021, E-022
