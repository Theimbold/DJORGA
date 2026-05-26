# Task 108: Batch-Tagging für DNA-Metadaten

## Ziel
Sicherstellen, dass die neuen Mood- und TimeContext-Werte auch über die Batch-Edit Funktion für mehrere Tracks gleichzeitig gesetzt werden können.

## Details
- `LibraryViewModel.ApplyBatchChangesAsync` aktualisieren.
- `Mood` und `TimeContext` vom Template-Track auf alle Ziel-Tracks übertragen.
- Persistenz via `UpdateTrackMetadataUseCase` sicherstellen.

## Fortschritt
- [x] Batch-Update Logik in `LibraryViewModel` erweitert.
- [x] Erfolgreiche Persistenz für Massenänderungen sichergestellt.
