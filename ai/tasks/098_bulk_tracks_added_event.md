# Task 098: Einführung eines BulkTracksAdded Events

## Ziel
Sicherstellen, dass die UI nicht durch zu viele Einzel-Events ("TrackAdded") während eines Massenimports blockiert wird.

## Details
- `event Action<IEnumerable<Track>>? BulkTracksAdded;` im `ITrackRepository` Interface definieren.
- In `SqliteTrackRepository` feuern, sobald `AddRangeAsync` erfolgreich war.
- Verhindern, dass `TrackAdded` für jeden einzelnen Track innerhalb von `AddRangeAsync` gefeuert wird.

## Fortschritt
- [x] Event im Interface registriert.
- [x] Repository feuert das Event nach `SaveChangesAsync`.
