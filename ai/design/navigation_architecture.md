# Navigationsarchitektur (Zielmodell)

Das Hauptfenster (`MainWindow`) dient als Shell für das Content-Routing.

## Struktur des MainWindow
- **Sidebar:** Navigation zu den Hauptbereichen.
- **Content Area:** Dynamische Anzeige der Views (UserControls).
- **Player Bar:** (Optional im Footer) Aktueller Track und einfache Playback-Controls.

## Navigations-Bereiche
1. **Dashboard (Home)**
   - Statistiken der Bibliothek.
   - Letzte Aktivitäten / Importe.
   - Schnellstart für Playlist-Builder.

2. **Bibliothek (Library)**
   - **Track-Liste:** Tabellarische Ansicht aller importierten Tracks.
   - **Track-Details:** Editieren von Metadaten und Anzeige der Analyse-Ergebnisse.

3. **Playlist-Builder (AI Engine)**
   - Konfiguration der Regeln (BPM Range, Key-Kompatibilität).
   - Vorschau der generierten Playlists.
   - Export-Funktion.

4. **Visualisierung (Graph)**
   - Interaktive Darstellung der harmonischen Beziehungen (Harmonic Graph).

5. **Einstellungen (Settings)**
   - Pfade zu Rekordbox-Datenbanken.
   - KI-Parameter (Gewichtung von BPM vs. Key).
   - Theme-Einstellungen.
