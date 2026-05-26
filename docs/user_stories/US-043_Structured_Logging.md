# User Story US-043: Structured Logging einführen

## Status: Offen
**Epic:** [E-025: Error Handling & Logging](../epics/E-025_Error_Handling_Logging.md)

## Beschreibung
**Als** Entwickler  
**möchte ich** ein einheitliches Logging-System via `Microsoft.Extensions.Logging`,  
**um** Fehler, Warnungen und Debug-Informationen systemweit nachvollziehbar
zu erfassen.

## Akzeptanzkriterien
- [ ] `ILogger<T>` ist im DI-Container registriert.
- [ ] `HarmonicScoringService` nutzt `ILogger<HarmonicScoringService>`.
- [ ] `RuleEvaluatorService` nutzt `ILogger<RuleEvaluatorService>`.
- [ ] Alle Infrastructure-Services (Repository, XmlReader) nutzen Logging.
- [ ] Log-Ausgaben erscheinen im Debug-Output (Console Sink für Development).

## Linked Implementation
- **Geändert:** `DJORGA.Desktop/Program.cs` (Logger-Registrierung im DI-Container)
- **Geändert:** `DJORGA.Application/Services/HarmonicScoringService.cs`
- **Geändert:** `DJORGA.Application/Services/RuleEvaluatorService.cs`
