# User Story US-041: BPM-Score symmetrisch machen

## Status: Offen
**Epic:** [E-024: Scoring-Logik Korrektheit](../epics/E-024_Scoring_Correctness.md)

## Beschreibung
**Als** Entwickler  
**möchte ich**, dass der BPM-Kompatibilitäts-Score für zwei Tracks unabhängig
von der Reihenfolge identisch ist,  
**um** mathematisch korrekte und faire Sequenz-Vergleiche zu gewährleisten.

## Akzeptanzkriterien
- [ ] `Score(trackA, trackB).BpmScore == Score(trackB, trackA).BpmScore` für
      alle Track-Paare.
- [ ] BPM-Differenz wird gegen den Durchschnitt `(bpmA + bpmB) / 2.0`
      normalisiert, nicht gegen `bpmA`.
- [ ] Unit Test beweist die Symmetrie mit konkreten BPM-Werten.

## Linked Implementation
- **Geändert:** `DJORGA.Application/Services/HarmonicScoringService.cs`
- **Geändert:** `DJORGA.Tests/Application/Services/HarmonicScoringServiceTests.cs`
