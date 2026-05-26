# Task 090: Höhere Peak-Auflösung in NAudioWaveformService

## Ziel
Erhöhung der Standardanzahl der generierten Peaks auf ca. 1500-2000, um auf großen Monitoren eine scharfe Darstellung zu ermöglichen.

## Details
- `NAudioWaveformService.cs` anpassen.
- Default-Wert für `peakCount` in `GetPeaksAsync` (Interface & Implementierung) erhöhen.
- Caching-Logik validieren (PeaksV2 Ordner bereits vorhanden).

## Fortschritt
- [x] `IWaveformService` Signatur geprüft.
- [x] Default-PeakCount in `NAudioWaveformService` erhöht.
- [x] Test-Lauf: Generierung bleibt performant.
