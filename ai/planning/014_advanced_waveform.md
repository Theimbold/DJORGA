# Epic 014: Advanced Waveform (Frequenz-basiert)

## Status: Abgeschlossen
Modernisierung der Wellenform-Visualisierung durch native SkiaSharp-Renderlogik für höhere Performance und Detailtreue (Multi-Color).

## Kern-Konzept
Ersetzung der `ItemsControl`-basierten Wellenform durch ein Custom Control (`WaveformControl`), das direkt via SkiaSharp zeichnet. Dies erlaubt die Darstellung von tausenden Peaks ohne Performance-Einbußen und ermöglicht komplexe Frequenz-Überlagerungen (Layered 3-Band).

## Phasen & Subtasks

### Phase 1: Custom Control & Skia-Integration
- [x] **Task 084:** Erstellung des `WaveformControl` in `MyApp.Desktop/Controls`.
- [x] **Task 085:** Implementierung der `ICustomDrawOperation` für SkiaSharp-Rendering.
- [x] **Task 086:** Anbindung an `PlayerViewModel.WaveformPeaks`.

### Phase 2: Multi-Color & Layering Logic
- [x] **Task 087:** Implementierung der 3-Band Visualisierung (Low/Mid/High) mit Alpha-Blending.
- [x] **Task 088:** Farbschema-Anpassung (Rekordbox-Style: Red/Green/Blue).
- [x] **Task 089:** Implementierung der Peak-Glättung (Moving Average).

### Phase 3: Performance & Integration
- [x] **Task 090:** Erhöhung der Standard-Peak-Anzahl auf 1000-2000 für scharfe Darstellung.
- [x] **Task 091:** Integration in `PlayerView.axaml` (Ersetzen des `ItemsControl`).
- [x] **Task 092:** Dynamische Skalierung (Native SkiaSharp-Abwicklung).

## Definition of Done
- Wellenform wird flüssig (60 FPS) via SkiaSharp gerendert.
- Frequenzbänder sind klar voneinander unterscheidbar (Farbe/Ebene).
- Waveform skaliert dynamisch mit der Breite des Players.
- Performance bleibt stabil auch bei 1000+ Peaks.
