# Informationsarchitektur (Domain-Modell) - Update V2

## Kern-Entitäten

### 1. Track
Erweitert um Metadaten für High-End UX.
- `Guid Id`
- `string Title`
- `string Artist`
- `string Album`
- **`string Genre`** (Neu)
- `double Bpm`
- `string Key`
- `string CamelotKey`
- `TimeSpan Duration`
- `string FilePath`
- **`string CoverArtPath`** (Neu: Link zum lokalen Image-Cache)
- **`bool IsAnalyzed`** (Neu: Status für Waveform/Tag-Extraktion)
- `DateTime ImportedAt`

### 2. Playlist
(Unverändert)

## Geplante Services (Application Layer)
- **IMetadataService:** Extrahiert Tags und Covers aus Files via TagLib#.
- **IAudioStreamingService:** Verwaltet Buffer und Playback-State.
- **ICoverCacheService:** Skaliert und speichert Cover-Bilder.
