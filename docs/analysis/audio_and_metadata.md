# High-Performance Audio & Metadata Engine (Analyse)

## 1. Streaming & Playback Performance
Um bei Bibliotheken > 400 GB (4000+ Files à 100MB) Streaming-Geschwindigkeit zu erreichen, wird folgende Architektur implementiert:

### Technischer Ansatz
- **NAudio / BASS.NET:** Hardwarenahe Audio-Bibliotheken für Low-Latency.
- **Circular Buffering:** Laden von nur 1-2 MB Initial-Buffer für sofortigen Playback-Start.
- **Background Caching:** Vorberechnen von Peak-Daten (Waveform) beim Import, um Dateizugriffe beim Browsen zu vermeiden.

## 2. Multi-Format Metadaten Extraktion
Das System muss Metadaten direkt aus den Binärdateien lesen, unabhängig vom Rekordbox-Import.

### Unterstützte Formate
- **Verlustfrei:** .wav, .aiff, .aif, .flac
- **Komprimiert:** .mp3, .m4a, .aac

### Kern-Metadaten (Priorität)
1. **Cover Art:** Extraktion des eingebetteten Bildes (Front Cover).
2. **Titel & Interpret:** Sauberes Mapping auch bei unstrukturierten Tags.
3. **Genre:** Konsolidierung von Tags für den AI-Builder.
4. **Technisch:** Präzise BPM- und Key-Werte direkt aus dem Header (falls XML ungenau).

## 3. Speicher-Strategie
- **File System Cache:** Cover-Arts werden in `/AppData/Local/DJORGA/Covers/` als .jpg/webp gespeichert.
- **Database:** Die SQLite-Tabelle `Tracks` wird um `CoverArtPath` und `Genre` erweitert.
