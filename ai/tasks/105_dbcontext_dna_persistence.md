# Task 105: AppDbContext Update & DNA Persistence

## Ziel
Sicherstellen, dass die neuen DNA-Felder (Mood & TimeContext) in der SQLite Datenbank gespeichert werden.

## Details
- `AppDbContext` Konfiguration prüfen (EF Core Enum-Mapping).
- Falls nötig, Default-Werte konfigurieren.
- (Optional) Migration erstellen, falls die Datenbank bereits existiert.

## Fortschritt
- [x] EF Core Mapping in `AppDbContext` geprüft.
- [x] Schema-Konsistenz mit `Mood` und `TimeContext` sichergestellt (Default-Werte ergänzt).
