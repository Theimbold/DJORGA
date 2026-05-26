# Task 099: Umstellung des ImportRekordboxXmlUseCase auf Batch-Processing

## Ziel
Sicherstellen, dass neue Tracks aus dem Rekordbox-XML gesammelt und in einem einzelnen Datenbank-Aufruf gespeichert werden, anstatt jeden Track einzeln zu sichern.

## Details
- `newTracks` Liste im `ExecuteAsync` erstellen.
- Alle zu importierenden Tracks in `newTracks` sammeln.
- `await _trackRepository.AddRangeAsync(newTracks)` am Ende des Loops aufrufen.

## Fortschritt
- [x] UseCase nutzt `AddRangeAsync` für Batch-Inserts.
- [x] Redundante Einzel-Updates während der Initial-Phase entfernt.
