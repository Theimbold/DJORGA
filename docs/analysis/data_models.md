# Datenmodelle (IST-Zustand)

## Entitäten

### Track
Repräsentiert ein einzelnes Musikstück.
- `Title` (string): Name des Tracks.
- `Artist` (string): Interpret.
- `Bpm` (double): Beats per Minute.
- `Key` (string): Tonart-Information.

### Playlist
Repräsentiert eine Sammlung von Tracks.
- `Name` (string): Bezeichnung der Playlist.
- `Tracks` (List<Track>): Enthaltene Musikstücke.

### TrackAnalysis
Zusätzliche Metadaten für die KI-Bewertung.
- (Bestand: Siehe `Core/TrackAnalysis.cs`)

### HarmonicLink
Beschreibt die Beziehung zwischen zwei Tracks.
- (Bestand: Siehe `Core/HarmonicLink.cs`)
