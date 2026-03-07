# Task 002: Projekt-Referenzen & Layer-Konfiguration

## Feature
Solution Setup

## Aufgabe
Einrichten der Abhängigkeiten zwischen den Projekten gemäß Clean Architecture Regeln.

## Schritte
1. `MyApp.Application` referenziert `MyApp.Domain`.
2. `MyApp.Infrastructure` referenziert `MyApp.Application`.
3. `MyApp.Desktop` referenziert `MyApp.Application` und `MyApp.Infrastructure` (für DI-Registrierung).
4. Validierung, dass `MyApp.Domain` keine externen Abhängigkeiten besitzt.

## Definition of Done
- Projekt-Referenzen sind korrekt in den `.csproj` Dateien eingetragen.
- Keine zirkulären Abhängigkeiten.
- Architektur-Integrität ist gewahrt.
