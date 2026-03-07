# Navigationsarchitektur & Kontextuelle IA

## UI-Zustände (Zustandsmaschine)

### 1. Initial State (Empty)
- **Sichtbar:** Begrüßungs-Screen, Import-Zentrum.
- **Versteckt:** Player-Bar, Sidebar-Navigation (Library/Builder), Main DataGrid.
- **Fokus:** Klare Handlungsaufforderung zum ersten Import.

### 2. Loaded State (Library Active)
- **Sichtbar:** Sidebar, Suchfunktion, Track-Tabelle.
- **Versteckt:** Player-Bar (solange kein Track gewählt).
- **Übergang:** Animiertes Einblenden der Sidebar nach erfolgreichem Import.

### 3. Playback State (Now Playing)
- **Sichtbar:** Alle Elemente aus State 2 + **Player-Bar im Footer**.
- **Fokus:** Wellenform, großes Cover-Art links, Title/Artist zentral.

## Player-Komponenten (Priorität)
1. **Waveform:** Echtzeit-Visualisierung basierend auf vorgerenderten Peak-Daten.
2. **Visuals:** Dominantes Cover-Art mit starkem Schatten (Corporate Design).
3. **Controls:** Minimalistisch (Play/Pause, Cue, Progress).
