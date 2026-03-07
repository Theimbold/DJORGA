# Projektstruktur (IST-Zustand)

## Verzeichnisstruktur (Root)
Das Projekt ist aktuell in mehrere logische Bereiche unterteilt, wobei die `MyApp.*`-Struktur als Zielarchitektur bereits vorbereitet ist.

```text
/DJORGA
  /Core                 - Domänenmodelle (Bestand: Track.cs, Playlist.cs)
  /Infrastructure       - Externe Integrationen (Bestand: RekordboxXmlReader.cs)
  /Services             - Geschäftslogik (Bestand: AiPlaylistBuilder.cs, Scorer)
  /UI                   - UI-Komponenten (Avalonia .axaml Dateien)
  /RekordboxAi          - .NET 6 Avalonia Hauptprojekt (Einstiegspunkt UI)
  /DJORGA               - .NET 4.7.2 Legacy Konsolenprojekt (Einstiegspunkt Core)
  /MyApp.Api            - (Skelett) Zukünftiger API-Layer
  /MyApp.Application    - (Skelett) Zukünftiger Applikations-Layer (UseCases/Services)
  /MyApp.Domain         - (Skelett) Zukünftiger Domain-Layer (Entities/Rules)
  /MyApp.Infrastructure - (Skelett) Zukünftiger Infrastruktur-Layer (Persistence/APIs)
  /MyApp.Desktop        - (Skelett) Zukünftiger Desktop-Layer
  /MyApp.Tests          - (Skelett) Test-Projekt
```

## Solution-Dateien
- **MyApp.sln:** Aktuell leerer Platzhalter.
- **DJORGA.slnx:** Modernes Solution-Format, referenziert `DJORGA/DJORGA.csproj`.
