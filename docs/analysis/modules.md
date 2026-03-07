# Module & Komponenten (IST-Zustand)

## Kernkomponenten

| Modul | Beschreibung | Standort |
| :--- | :--- | :--- |
| **RekordboxAi** | Hauptanwendung (Avalonia UI), Orchestrierung des Programms. | `/RekordboxAi` |
| **AiPlaylistBuilder** | Logik zur Erstellung von Playlists basierend auf Tracks. | `/Services` |
| **RekordboxXmlReader**| Integrationstool zum Auslesen der Rekordbox-Datenbank. | `/Infrastructure` |
| **HarmonicLinkScorer**| Mathematische Logik zur Bewertung der harmonischen Kompatibilität. | `/Services` |
| **Track/Playlist** | Zentrale Datenmodelle der Domäne. | `/Core` |

## UI Komponenten
- **HarmonicGraphView:** Visualisierung der Track-Beziehungen.
- **LibraryView:** Tabellarische Ansicht der Musikbibliothek.
- **TrackDetailsView:** Detailansicht eines einzelnen Musikstücks.
- **MainWindow:** Der Hauptcontainer der App.
