# Task 085: SkiaSharp Render Logik für Wellenformen

## Ziel
Implementierung der `ICustomDrawOperation` in `WaveformControl.cs`, um die `FrequencyPeaks` via SkiaSharp auf den Canvas zu zeichnen.

## Details
- `WaveformDrawOperation` Klasse implementieren.
- Zugriff auf `ISkiaSharpApiLeaseFeature`.
- Zeichnen der Peaks basierend auf `Bounds.Width` und `Peaks.Count`.
- Initial: Einfache Balken (Mono-Color) zum Testen.

## Fortschritt
- [x] `WaveformDrawOperation` implementiert.
- [x] `Render` Methode in `WaveformControl` überschrieben.
- [x] SkiaSharp-Lease funktioniert.
