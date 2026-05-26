# User Story US-040: DJORGA.Api Projektentscheidung dokumentieren

## Status: Offen
**Epic:** [E-023: Codebase Hygiene](../epics/E-023_Codebase_Hygiene.md)

## Beschreibung
**Als** Entwickler  
**möchte ich** eine klare, dokumentierte Entscheidung über den Zweck oder
die Entfernung des `DJORGA.Api`-Projekts,  
**um** Unklarheiten über die Systemgrenzen zu beseitigen.

## Akzeptanzkriterien
- [ ] Eine ADR-Datei (`docs/adr/003_api_project.md`) dokumentiert die Entscheidung:
      entweder Beibehaltung mit konkretem Zweck oder Entfernung.
- [ ] Falls behalten: `DJORGA.Api` hat ein `README.md` mit Zweck und Roadmap.
- [ ] Falls entfernt: Das Projekt ist aus der `.sln` ausgetragen und der Ordner gelöscht.

## Linked Implementation
- **Neu oder geändert:** `docs/adr/003_api_project.md`
- **Optional geändert:** `DJORGA.Api/`
