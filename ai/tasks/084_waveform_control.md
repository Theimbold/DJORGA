# Task 084: WaveformControl Framework

## Ziel
Erstellung der Basis-Klasse `WaveformControl` in `MyApp.Desktop/Controls`, die von `Control` erbt und die notwendigen `StyledProperty` für `Peaks` und `Progress` bereitstellt.

## Details
- `Control` erben.
- `PeaksProperty` (`IEnumerable<FrequencyPeak>`).
- `ProgressProperty` (`double`).
- `InvalidateVisual` bei Property-Änderungen.

## Fortschritt
- [x] Datei `MyApp.Desktop/Controls/WaveformControl.cs` erstellt.
- [x] Properties definiert.
- [x] Standard-Style in `App.axaml` (oder global) falls nötig.
