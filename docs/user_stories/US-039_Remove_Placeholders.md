# User Story US-039: Leere Platzhalter-Dateien entfernen

## Status: Offen
**Epic:** [E-023: Codebase Hygiene](../epics/E-023_Codebase_Hygiene.md)

## Beschreibung
**Als** Entwickler  
**möchte ich**, dass automatisch generierte, leere Platzhalter-Dateien aus
dem Projekt entfernt werden,  
**um** die Codebasis übersichtlich zu halten und neue Teammitglieder nicht
zu verwirren.

## Akzeptanzkriterien
- [ ] `DJORGA.Domain/Class1.cs` ist gelöscht.
- [ ] `DJORGA.Application/Class1.cs` ist gelöscht.
- [ ] `dotnet build` weiterhin fehlerfrei.

## Linked Implementation
- **Gelöscht:** `DJORGA.Domain/Class1.cs`
- **Gelöscht:** `DJORGA.Application/Class1.cs`
