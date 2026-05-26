# User Story US-036: Build und Test validieren

## Status: Offen
**Epic:** [E-021: Namespace-Migration Abschluss](../epics/E-021_Namespace_Migration.md)

## Beschreibung
**Als** Entwickler  
**möchte ich**, dass `dotnet build` und `dotnet test` nach der Migration
fehlerfrei durchlaufen,  
**um** sicherzustellen, dass die Codebasis wieder in einem stabilen,
auslieferbaren Zustand ist.

## Akzeptanzkriterien
- [ ] `dotnet restore` — keine Fehler.
- [ ] `dotnet build --configuration Release` — keine Fehler, keine Namespace-Warnungen.
- [ ] `dotnet test` — alle bestehenden Tests bestehen (grün).
- [ ] Keine verbleibenden TODO-Markierungen aus der Namespace-Migration.
