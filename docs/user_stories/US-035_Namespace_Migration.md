# User Story US-035: Namespaces vollständig migrieren

## Status: Offen
**Epic:** [E-021: Namespace-Migration Abschluss](../epics/E-021_Namespace_Migration.md)

## Beschreibung
**Als** Entwickler  
**möchte ich**, dass alle Quelldateien in `DJORGA.Desktop`, `DJORGA.Infrastructure`
und `DJORGA.Tests` den Namespace `DJORGA.*` verwenden,  
**um** Namespace-Konflikte zu beseitigen und einen einheitlichen, professionellen
Codebase-Namen zu haben.

## Akzeptanzkriterien
- [ ] Globale Suche nach `namespace MyApp` ergibt 0 Treffer.
- [ ] Globale Suche nach `using MyApp` ergibt 0 Treffer.
- [ ] Alle `.axaml`-Dateien verwenden `clr-namespace:DJORGA.*`.
- [ ] `NAMING_MIGRATION.md` zeigt alle Checkboxen als abgeschlossen.

## Linked Implementation
- **Betroffen:** `DJORGA.Desktop/ViewModels/*.cs`, `DJORGA.Desktop/Views/*.axaml`
- **Betroffen:** `DJORGA.Infrastructure/**/*.cs`
- **Betroffen:** `DJORGA.Tests/**/*.cs`
