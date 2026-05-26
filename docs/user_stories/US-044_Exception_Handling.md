# User Story US-044: Silent Exception Handling bereinigen

## Status: Offen
**Epic:** [E-025: Error Handling & Logging](../epics/E-025_Error_Handling_Logging.md)

## Beschreibung
**Als** Entwickler  
**möchte ich**, dass kein `catch`-Block eine Exception still ignoriert,  
**um** Fehler im System sichtbar zu machen und Debugging zu ermöglichen.

## Akzeptanzkriterien
- [ ] `HarmonicScoringService.CalculateKeyScore()`: bei unbekanntem Key-Format
      wird `LogWarning` aufgerufen mit Key-Wert und Fehlerdetail.
- [ ] `RuleEvaluatorService.CreateExpression()`: bei fehlerhafter Regel wird
      `LogWarning` aufgerufen mit Regelname und Fehlerdetail.
- [ ] Kein `catch`-Block gibt einen Fallback-Wert zurück, ohne vorher geloggt
      zu haben.
- [ ] Unit Tests prüfen das Logging-Verhalten bei ungültigem Input
      (via `Mock<ILogger<T>>`).

## Linked Implementation
- **Geändert:** `DJORGA.Application/Services/HarmonicScoringService.cs`
- **Geändert:** `DJORGA.Application/Services/RuleEvaluatorService.cs`
