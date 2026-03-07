# Architektur (IST-Zustand)

## Übersicht
Das Projekt befindet sich in einem Übergangszustand zwischen einer klassischen monolithischen Struktur (Legacy) und einer modernen **Clean Architecture**.

## Layer-Analyse

### 1. UI Layer
- **RekordboxAi:** Aktuelles Hauptprojekt für die Oberfläche. Verwendet Avalonia.
- **UI Verzeichnis:** Enthält UI-Komponenten, die noch nicht vollständig in das `RekordboxAi` Projekt integriert zu sein scheinen (lokaler Filesystem-Stand vs. Projekt-Inklusion).

### 2. Application/Services Layer
- **Services Verzeichnis:** Beinhaltet Geschäftslogik wie den `AiPlaylistBuilder`. Diese ist aktuell als lose Klassenbibliothek-Struktur vorhanden, aber noch nicht in ein formalisiertes `MyApp.Application` Projekt überführt.

### 3. Domain Layer
- **Core Verzeichnis:** Enthält die fundamentalen Entitäten (`Track`, `Playlist`). Diese bilden das Herzstück des Systems.

### 4. Infrastructure Layer
- **Infrastructure Verzeichnis:** Beinhaltet technische Details wie `RekordboxXmlReader`. Diese hängen aktuell direkt von den Core-Entitäten ab.

## Modulare Abhängigkeiten
Die Abhängigkeiten sind aktuell noch unstrukturiert (Referenzierung teilweise über lose Dateien im Filesystem statt sauberer Projekt-Referenzen). Die `MyApp.*` Layer sind als Zielstruktur angelegt, aber noch nicht aktiv verknüpft.
