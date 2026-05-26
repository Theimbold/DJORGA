# User Story US-046: Repository Integration Tests

## Status: Offen
**Epic:** [E-026: Test Coverage](../epics/E-026_Test_Coverage.md)

## Beschreibung
**Als** Entwickler  
**möchte ich** Integrationstests für den `SqliteTrackRepository`,  
**um** sicherzustellen, dass Persistenzoperationen (Speichern, Laden, Suchen)
korrekt mit der SQLite-Datenbank funktionieren.

## Akzeptanzkriterien
- [ ] Test-Datenbank wird in-memory oder in einem Temp-Verzeichnis erstellt.
- [ ] `AddAsync` + `GetAllAsync`: gespeicherter Track kann wieder geladen werden.
- [ ] `AddRangeAsync`: Batch-Insert von 100 Tracks funktioniert korrekt.
- [ ] Datenbank wird nach jedem Test zurückgesetzt (Test-Isolation).

## Linked Implementation
- **Neu:** `DJORGA.Tests/Infrastructure/Repositories/SqliteTrackRepositoryTests.cs`
