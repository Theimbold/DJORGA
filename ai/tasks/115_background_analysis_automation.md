# Task 115: Background Analysis Service Automation

## Ziel
Vollständige Automatisierung der Hintergrund-Analyse für Tracks, die noch nicht analysiert wurden. Dies umfasst Metadaten, Cover-Art und Waveform-Generierung.

## Details
- [ ] `BackgroundAnalysisService` um `ICoverCacheService` und `IWaveformService` erweitern.
- [ ] `ProcessQueueAsync` implementieren:
    - Metadaten extrahieren.
    - Cover-Art in lokalen Cache speichern.
    - Waveform-Peaks generieren und im Cache ablegen.
    - `IsAnalyzed` auf `true` setzen und Track speichern.
- [ ] Fehlerbehandlung verbessern (Logging oder Fehlermarkierung).
- [ ] UI-Feedback im `LibraryViewModel` sicherstellen (bereits teilweise vorhanden).

## Fortschritt
- [ ] Service Erweiterung.
- [ ] Analyse-Logik (Metadata + Cover + Waveform).
- [ ] Integrationstest / Verifizierung.
