# Task 087: Multi-Color & Layered 3-Band Rendering

## Ziel
Erweiterung der SkiaSharp-Logik um die Darstellung von Low, Mid und High Frequenzbändern mit Überlagerung.

## Details
- Drei verschiedene `SKPaint` Instanzen für die Frequenzbänder.
- Berechnung der Höhen basierend auf `FrequencyPeak.Low`, `Mid` und `High`.
- Alpha-Blending aktivieren, damit sich die Bänder optisch überlagern können.
- "Mirrored" Look (oben/unten spiegelsymmetrisch) optional oder als Standard.

## Fortschritt
- [x] Paints for Low, Mid, High erstellt.
- [x] Zeichnen-Logik auf 3-Band umgestellt.
- [x] Layering optisch ansprechend gestaltet.
