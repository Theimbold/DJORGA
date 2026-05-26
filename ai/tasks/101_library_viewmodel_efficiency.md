# Task 101: Effiziente Library-Updates bei Bulk-Events

## Ziel
Sicherstellen, dass das `LibraryViewModel` nicht die gesamte Datenbank neu laden muss, wenn nur eine Teilmenge an Tracks per Bulk-Event hinzugefügt wird.

## Details
- `BulkTracksAdded` Event im `LibraryViewModel` abonnieren.
- Neue Tracks direkt zur internen Liste `_allTracks` hinzufügen.
- `FilterTracks()` aufrufen, um die UI-Liste zu aktualisieren, ohne die DB zu kontaktieren.

## Fortschritt
- [x] Handler für `BulkTracksAdded` in `LibraryViewModel` implementiert.
- [x] UI aktualisiert sich bei Bulk-Import ohne Full-Reload.
