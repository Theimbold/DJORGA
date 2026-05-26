# User Story US-042: Scoring-Gewichte konfigurierbar machen

## Status: Offen
**Epic:** [E-024: Scoring-Logik Korrektheit](../epics/E-024_Scoring_Correctness.md)

## Beschreibung
**Als** DJ  
**möchte ich** die Gewichtung von Key, BPM und DNA beim Sequenz-Scoring
anpassen können,  
**um** das System auf meinen persönlichen Mix-Stil abzustimmen (z.B. mehr
Gewicht auf harmonische Kompatibilität für Journey-Mixes).

## Akzeptanzkriterien
- [ ] Eine `ScoringWeights`-Klasse (Value Object) existiert mit Properties
      `KeyWeight`, `BpmWeight`, `DnaWeight`.
- [ ] Die Summe der drei Gewichte muss 1.0 ergeben (Validierung).
- [ ] `HarmonicScoringService` erhält `ScoringWeights` per DI.
- [ ] Default-Werte: `KeyWeight = 0.30`, `BpmWeight = 0.30`, `DnaWeight = 0.40`.
- [ ] Die Gewichte sind in der `SettingsViewModel` konfigurierbar (UI, optional für MVP2).

## Linked Implementation
- **Neu:** `DJORGA.Application/` oder `DJORGA.Domain/ValueObjects/ScoringWeights.cs`
- **Geändert:** `DJORGA.Application/Services/HarmonicScoringService.cs`
