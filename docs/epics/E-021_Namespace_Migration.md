# Epic E-021: Namespace-Migration Abschluss

## Status: Offen
**Typ:** Technische Schuld (Blocking)
**Phase:** 1 – Projektstabilisierung

## Priorität
**KRITISCH – BLOCKING.** Das Projekt ist in einem nicht-buildbaren Zustand.
Kein anderes Epic darf begonnen werden, bevor E-021 abgeschlossen ist.

## Hintergrund
Die Umbenennung von `MyApp.*` → `DJORGA.*` wurde gestartet (Ordner und `.csproj`
wurden umbenannt), aber nie fertiggestellt. Viele Quelldateien verwenden weiterhin
`namespace MyApp.*` und `using MyApp.*`. Das Ergebnis: `dotnet build` schlägt fehl.

Dokumentiert in: [`docs/technical/NAMING_MIGRATION.md`](../technical/NAMING_MIGRATION.md)

## Ziel
Alle Namespaces, `using`-Direktiven und XAML-Namespace-Deklarationen auf
`DJORGA.*` vereinheitlichen, sodass `dotnet build` und `dotnet test` ohne
Fehler durchlaufen.

## Verknüpfte User Stories
- [US-035: Namespaces vollständig migrieren](../user_stories/US-035_Namespace_Migration.md)
- [US-036: Build und Test validieren](../user_stories/US-036_Build_Validation.md)

## Betroffene Module (Ausstehend)
- `DJORGA.Desktop/` — ViewModels, Views, Services, Controls
- `DJORGA.Infrastructure/` — Repositories, Services, ApiClients
- `DJORGA.Tests/` — alle Testklassen
- `.axaml`-Dateien mit `xmlns:vm="clr-namespace:MyApp.*"`

## Akzeptanzkriterien
- [ ] `dotnet restore` läuft fehlerfrei durch.
- [ ] `dotnet build` ohne Fehler oder Warnungen bezüglich falscher Namespaces.
- [ ] `dotnet test` läuft durch und alle bestehenden Tests bestehen.
- [ ] `NAMING_MIGRATION.md` auf Status „Abgeschlossen" aktualisiert.

## Abhängigkeiten
- Blockiert: E-022, E-023, E-024, E-025, E-026, E-027
