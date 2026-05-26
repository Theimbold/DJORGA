# Epic 017: The Harmonic Map (Visual Graph)

## Status: In Arbeit
Innovative visuelle Navigation durch den Sound-Space implementiert.

## Phasen & Subtasks

### Phase 1: Logik & Daten (Application)
- [x] **Task 093:** `HarmonicGraphService` erstellt (Berechnung der Adjazenzmatrix).
- [x] **Task 094:** DTOs für `MapNode` und `MapEdge` definiert.

### Phase 2: UI Foundation (Avalonia)
- [x] **Task 095:** `HarmonicMapView` UserControl und ViewModel erstellt.
- [x] **Task 096:** Native SkiaSharp-Integration via `ICustomDrawOperation`.

### Phase 3: Layout & Rendering (Skia)
- [x] **Task 097:** Mathematische Anordnung der Knoten (Radial Camelot Layout).
- [x] **Task 098:** Visual Styles (Key-Farben, Linien, Glow).

### Phase 4: Interaction (UX)
- [x] **Task 099:** Zoom & Pan Mechanismen implementiert.
- [x] **Task 100:** Detail-Overlay bei Klick (In Arbeit).

## Definition of Done
- Die gesamte Bibliothek ist als interaktive Map sichtbar.
- Flüssige Performance bei großen Bibliotheken (> 3000 Tracks).
