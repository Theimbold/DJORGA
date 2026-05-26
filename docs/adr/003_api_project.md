# ADR-003: Status und Zweck von DJORGA.Api

**Datum:** 26. Mai 2026  
**Status:** Entschieden  
**Entscheider:** Projektteam

---

## Kontext

Im Repository existiert ein Projekt `DJORGA.Api` (ASP.NET Core Stub). Es
enthält ausschließlich eine leere `Program.cs` mit `Console.WriteLine("API Starting...")`,
zwei leere `README.md`-Platzhalter sowie keine Controller, keine Middleware
und keine Abhängigkeit auf `DJORGA.Application` oder `DJORGA.Domain`.

DJORGA ist laut MVP-Definition eine **reine Desktop-Anwendung** ohne
Netzwerkfunktionalität. Die Frage war: Wird `DJORGA.Api` entfernt oder
mit einem dokumentierten Zweck behalten?

## Optionen

### Option A: Sofort entfernen
- Projekt aus `.sln` austragen und Ordner löschen.
- Vorteil: Kein toter Code, minimale Codebasis.
- Nachteil: Zukünftige Szenarien (Plugin-System, Heimnetz-Sync, Web-UI)
  müssten das Projekt neu anlegen.

### Option B: Als Erweiterungspunkt behalten (deferred)
- Projekt bleibt in der Solution, aber explizit als **out-of-scope für MVP** markiert.
- Erhält ein dokumentiertes `README.md` mit möglichem Zweck.
- Vorteil: Architektonische Weiche für spätere Erweiterungen ist bereits gesetzt.
- Nachteil: Trägt minimal zur Unordnung bei, solange kein Inhalt da ist.

## Entscheidung

**Option B — Behalten als deferred Erweiterungspunkt.**

Begründung: Der Aufwand zum Behalten ist minimal (leerer Stub). Mögliche
zukünftige Verwendungsfälle sind realistisch:

- **Plugin-Endpunkt:** Externe Tools (z.B. Rekordbox-Live-Hook, Streaming-Dienste)
  könnten über eine lokale API mit DJORGA kommunizieren.
- **Lokales Heimnetz-Interface:** Ein DJ könnte seine Bibliothek über ein
  einfaches Web-UI im Heimnetz einsehen.
- **Automatisierungs-Schnittstelle:** CI/CD-artige Batch-Operationen auf
  der Bibliothek via REST.

Das Projekt darf jedoch **keinen Einfluss auf den MVP-Build** haben und
keine Abhängigkeiten zu `DJORGA.Application` oder `DJORGA.Domain` aufbauen,
solange es kein aktives Epic gibt.

## Konsequenzen

- `DJORGA.Api` bleibt in der Solution, erhält aber ein klärendes `README.md`.
- Das Projekt ist in allen Dokumenten explizit als `Status: Deferred (post-MVP)`
  gekennzeichnet.
- Kein weiterer Entwicklungsaufwand für `DJORGA.Api` bis ein dediziertes Epic
  (E-029+) angelegt wird.
- Die `Program.cs` bleibt unverändert als Stub.
