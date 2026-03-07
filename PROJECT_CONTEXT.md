# Projekt: DJORGA

## Vision
DJORGA ist eine moderne Desktop-Anwendung zur Verwaltung von DJ-Musikbibliotheken, die KI-gestützte Playlist-Erstellung und Rekordbox-Integration bietet. Das System zielt darauf ab, DJ-Workflows durch intelligente Automatisierung und eine klare, plattformübergreifende Benutzeroberfläche zu optimieren.

## Ziel des Projekts
Ziel ist die Transformation des bestehenden Prototyps in eine robuste, skalierbare Anwendung auf Basis der **Clean Architecture**. Dies umfasst:
- Vollständige Integration von Rekordbox-Daten.
- KI-gestützte Analyse und Playlist-Generierung.
- Eine moderne, performante Avalonia-UI.
- Saubere Trennung von Geschäftslogik, Daten und UI.

## Technologien
- **Frontend:** Avalonia UI (v11.0.0), ReactiveUI.
- **Backend:** .NET 6 (UI), .NET Framework 4.7.2 (Legacy Core).
- **Konzepte:** Clean Architecture, MVVM, Dependency Injection (geplant).
- **Tools:** Gemini CLI, PlantUML, Git.

## Architektur
Das Projekt befindet sich in der Umstellungsphase auf **Clean Architecture**.
Die aktuelle Struktur sieht folgende Layer vor:
- **UI:** `RekordboxAi` (Avalonia) / `MyApp.Desktop` (Ziel).
- **Application:** `MyApp.Application` (Skelett).
- **Domain:** `MyApp.Domain` / `Core` (Bestand).
- **Infrastructure:** `MyApp.Infrastructure` / `Infrastructure` (Bestand).

## MVP Vision
Der erste MVP soll folgende Features enthalten:
1. Rekordbox XML Import.
2. Bibliotheks-Übersicht.
3. Basis KI-Playlist-Builder.
4. Navigation zwischen Dashboard und Bibliothek.
5. Lokale Datenspeicherung.

## Hauptfeatures
- **Rekordbox-Integration:** Einlesen und Verarbeiten von Rekordbox XML-Dateien.
- **Harmonic Linking:** Analyse von BPM und Tonarten für optimale Übergänge.
- **KI-Playlist-Builder:** Automatisierte Erstellung von Playlists basierend auf vordefinierten Regeln und KI-Scoring.
- **Graph-Visualisierung:** Darstellung von harmonischen Beziehungen zwischen Tracks.
