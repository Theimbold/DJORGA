# Informationsarchitektur (Domain-Modell)

Definition der zentralen Entitäten für den `MyApp.Domain` Layer.

## Kern-Entitäten

### 1. Track
Basis-Datensatz eines Musikstücks.
- `Guid Id` (Primary Key)
- `string Title`
- `string Artist`
- `string Album`
- `double Bpm`
- `string Key` (Original Key)
- `string CamelotKey` (Umgerechnet für Harmonic Mixing)
- `TimeSpan Duration`
- `string FilePath`
- `DateTime ImportedAt`

### 2. Playlist
Sammlung von Tracks mit Metadaten.
- `Guid Id`
- `string Name`
- `List<Track> Items`
- `bool IsAiGenerated`
- `DateTime CreatedAt`

### 3. HarmonicRelation
Berechnete Beziehung zwischen zwei Tracks.
- `Guid SourceTrackId`
- `Guid TargetTrackId`
- `double CompatibilityScore` (0.0 - 1.0)
- `string RelationType` (z.B. "Same Key", "Perfect Fifth", "Relative Major/Minor")

## Value Objects (geplant)
- **BpmRange:** Repräsentiert minimale und maximale BPM für Filter.
- **KeyCompatibility:** Logik für das Camelot Wheel.
