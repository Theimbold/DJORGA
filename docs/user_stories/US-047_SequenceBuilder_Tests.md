# User Story US-047: Sequence Builder Tests

## Status: Offen
**Epic:** [E-026: Test Coverage](../epics/E-026_Test_Coverage.md)

## Beschreibung
**Als** Entwickler  
**möchte ich** umfassende Tests für den Sequenz-Builder-Algorithmus,  
**um** Edge Cases und Grenzwerte abzusichern, die in der Produktion zu
unerwartetem Verhalten führen könnten.

## Akzeptanzkriterien
- [ ] Happy-Path: Aus einem Pool von 10 Tracks wird eine Sequenz der Länge 5 gebaut.
- [ ] Edge Case: Pool ist leer → Ergebnis enthält nur den Starter-Track.
- [ ] Edge Case: Pool kleiner als gewünschte Länge → Sequenz endet früher.
- [ ] Edge Case: Kein Track überschreitet die Mindest-Score-Schwelle (0.2) →
      Sequenz bricht nach dem Starter-Track ab.
- [ ] Korrektheit: Kein Track erscheint zweimal in der Sequenz.

## Linked Implementation
- **Neu:** `DJORGA.Tests/Application/Services/SequenceBuilderServiceTests.cs`
  (Name abhängig von E-027 Umbenennung)
