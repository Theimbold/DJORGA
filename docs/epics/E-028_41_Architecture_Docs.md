# Epic E-028: 4+1 Architekturdokumentation

## Status: Offen
**Typ:** Dokumentation (Pflicht laut Projektstandard)
**Phase:** 5 – Klarheit & Dokumentation

## Hintergrund
Das Projekt schreibt die Verwendung der **4+1-Sichten-Architektur nach
Philippe Kruchten** vor. Derzeit existiert nur eine flache
`docs/architecture/overview.md`. Die fünf definierten Sichten fehlen.

Die 4+1-Sichten sind:
1. **Logische Sicht** — Klassenstruktur, Domänenmodell, Schichtenbeziehungen
2. **Prozess-Sicht** — Laufzeitverhalten, Nebenläufigkeit, wichtige Abläufe
3. **Implementierungs-Sicht** — Projektstruktur, Module, Build-Artefakte
4. **Physische Sicht** — Deployment-Topologie, Dateisystem, externe Systeme
5. **Szenarien (+1)** — Schlüssel-Use-Cases als Querverbindung durch alle Sichten

## Ziel
Für jede der fünf Sichten existiert eine strukturierte Markdown-Datei mit
Gliederung, Diagramm-Platzhalter und Erklärungstext. Die Dateien sind
bewusst als „lebende Dokumente" angelegt — Inhalt wird iterativ ergänzt.

## Verknüpfte User Stories
- [US-050: Logische Sicht dokumentieren](../user_stories/US-050_Logical_View.md)
- [US-051: Prozess-Sicht dokumentieren](../user_stories/US-051_Process_View.md)
- [US-052: Implementierungs-Sicht dokumentieren](../user_stories/US-052_Implementation_View.md)
- [US-053: Physische Sicht dokumentieren](../user_stories/US-053_Physical_View.md)
- [US-054: Szenarien (+1) dokumentieren](../user_stories/US-054_Scenarios_View.md)

## Dateistruktur (Ziel)
```
docs/architecture/
  overview.md              ← vorhanden (Überblick)
  views/
    01_logical_view.md     ← neu (US-050)
    02_process_view.md     ← neu (US-051)
    03_implementation_view.md ← neu (US-052)
    04_physical_view.md    ← neu (US-053)
    05_scenarios_view.md   ← neu (US-054)
```

## Akzeptanzkriterien
- [ ] Alle 5 Sichten-Dateien existieren unter `docs/architecture/views/`.
- [ ] Jede Datei enthält: Zweck der Sicht, Gliederung, Diagramm-Platzhalter
      und mindestens eine konkrete inhaltliche Aussage über DJORGA.
- [ ] `overview.md` verlinkt auf alle 5 Sichten-Dateien.

## Abhängigkeiten
- Kann parallel zu anderen Epics begonnen werden.
- Inhaltlich profitiert E-028 von abgeschlossenem E-022 (saubere Domain-Struktur).
