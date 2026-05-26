# Task 097: Implementierung von AddRangeAsync in SqliteTrackRepository

## Ziel
Effizientes Speichern mehrerer Tracks durch einen einzigen Datenbank-Aufruf via EF Core.

## Details
- Implementierung von `AddRangeAsync(IEnumerable<Track> tracks)` in `SqliteTrackRepository.cs`.
- Nutzung von `_context.Tracks.AddRangeAsync(tracks)`.
- Aufruf von `await _context.SaveChangesAsync()`.
- Event-Handling optimieren.

## Fortschritt
- [x] Implementierung in Repository hinzugefügt.
- [x] Transaktions-Sicherheit geprüft.
