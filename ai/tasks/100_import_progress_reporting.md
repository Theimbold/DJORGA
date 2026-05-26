# Task 100: Fortschrittsanzeige für den Import-Prozess

## Ziel
Dem Nutzer während eines großen Rekordbox-Imports (z.B. > 10.000 Tracks) ein visuelles Feedback über den aktuellen Fortschritt geben.

## Details
- `IProgress<double>? progress` an `IRekordboxService.ParseLibraryAsync` und `ImportRekordboxXmlUseCase.ExecuteAsync` übergeben.
- `LibraryViewModel` um eine `Progress` Property erweitern.
- In `ExecuteAsync` die Fortschritts-Updates berechnen (Parsing + Deduplizierung + Saving).

## Fortschritt
- [x] Interface `IRekordboxService` angepasst.
- [x] Implementierung `RekordboxXmlService` angepasst (Reporting im XML-Loop).
- [x] UseCase `ImportRekordboxXmlUseCase` angepasst (Weiterleitung des Progress).
- [x] `LibraryViewModel` zeigt den Fortschritt in der UI an.
