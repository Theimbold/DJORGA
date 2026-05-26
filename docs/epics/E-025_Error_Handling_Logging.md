# Epic E-025: Error Handling & Structured Logging

## Status: Abgeschlossen
**Typ:** Robustheit & Wartbarkeit
**Phase:** 3 – Korrektheit & Robustheit

## Hintergrund
Mehrere Services verschlucken Exceptions still:

- `HarmonicScoringService.CalculateKeyScore()`: `catch { return 0.1; }`
  → Ein unbekanntes Key-Format wird lautlos als „fast inkompatibel" bewertet.
- `RuleEvaluatorService.CreateExpression()`: `catch { return null; }`
  → Eine fehlerhafte Filterregel wird ignoriert, ohne dass der Benutzer
  oder der Entwickler es erfährt.

Außerdem gibt es im gesamten Projekt kein einheitliches Logging-System.
Fehler, Warnungen und Debug-Informationen werden entweder gar nicht
festgehalten oder nur über `Console.WriteLine` ausgegeben.

## Ziel
Fehler sind sichtbar, nachvollziehbar und angemessen behandelt.
`Microsoft.Extensions.Logging` (bereits im .NET 8 Stack verfügbar) wird
als Standard eingeführt.

## Verknüpfte User Stories
- [US-043: Structured Logging einführen](../user_stories/US-043_Structured_Logging.md)
- [US-044: Silent Exception Handling bereinigen](../user_stories/US-044_Exception_Handling.md)

## Technische Umsetzung (Leitlinien)
1. `ILogger<T>` per DI in alle Application- und Infrastructure-Services injizieren.
2. `catch`-Blöcke, die silent sind, werden auf `_logger.LogWarning(...)` oder
   `_logger.LogError(...)` umgestellt.
3. Für den Benutzer relevante Fehler (z.B. ungültige Filterregel) werden über
   das bestehende Notification-System in der UI angezeigt.
4. Kritische Fehler (z.B. Datenbankfehler beim Import) werden als `LogError`
   mit vollem Exception-Stack geloggt.

## Akzeptanzkriterien
- [x] `ILogger<T>` ist in `HarmonicScoringService` und `RuleEvaluatorService`
      injiziert und aktiv genutzt.
- [x] Kein `catch`-Block gibt `null` oder einen Fallback-Wert zurück,
      ohne vorher geloggt zu haben.
- [x] Logging ist im DI-Container registriert.
- [x] Unit Tests prüfen, dass bei ungültigem Input eine Warnung geloggt wird
      (via Mock-Logger).

## Abhängigkeiten
- Erfordert: E-021
