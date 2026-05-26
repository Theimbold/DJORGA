# Epic E-023: Codebase Hygiene

## Status: Offen
**Typ:** Technische Schuld (Aufräumen)
**Phase:** 2 – Architektur-Integrität

## Hintergrund
Zwei konkrete Hygiene-Probleme wurden identifiziert:

**Problem 1: Leere Platzhalter-Dateien**
`DJORGA.Domain/Class1.cs` und `DJORGA.Application/Class1.cs` sind automatisch
generierte Platzhalter, die nie entfernt wurden. Sie haben keinen Inhalt,
verwirren neue Entwickler und verschmutzen die Projektstruktur.

**Problem 2: Unklarer Zweck von `DJORGA.Api`**
Es existiert ein `DJORGA.Api`-Projekt (ASP.NET Core), obwohl DJORGA laut MVP
eine reine Desktop-Anwendung ohne Cloud- oder Netzwerkfunktion ist.
Dies muss als bewusste Architekturentscheidung dokumentiert oder das Projekt
entfernt werden.

## Ziel
Der Codebase-Zustand spiegelt den tatsächlichen Zweck wider: keine toten
Dateien, jedes Projekt hat einen klar dokumentierten Existenzgrund.

## Verknüpfte User Stories
- [US-039: Leere Platzhalter-Dateien entfernen](../user_stories/US-039_Remove_Placeholders.md)
- [US-040: DJORGA.Api Entscheidung dokumentieren](../user_stories/US-040_Api_Project_Decision.md)

## Akzeptanzkriterien
- [ ] `Class1.cs` in `DJORGA.Domain` und `DJORGA.Application` sind gelöscht.
- [ ] Entweder: `DJORGA.Api` ist entfernt und die Entscheidung in einem ADR festgehalten.
- [ ] Oder: `DJORGA.Api` hat einen dokumentierten Zweck (z.B. zukünftiger Plugin-Endpunkt)
      und ein `README.md` in seinem Verzeichnis.
- [ ] `dotnet build` weiterhin fehlerfrei.

## Abhängigkeiten
- Erfordert: E-021
