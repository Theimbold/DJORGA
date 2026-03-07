# Task 026: File Picker Service Abstraction

## Feature
File Selection Service

## Aufgabe
Erstellung eines Interfaces, um Datei-Auswahl-Dialoge plattformunabhängig aus den ViewModels heraus aufrufen zu können.

## Schritte
1. Erstellen von `IFilePickerService` in `MyApp.Application/Interfaces/UI`.
2. Definition der Methode `Task<string?> OpenFileAsync(string title, string[] extensions)`.

## Definition of Done
- Interface ist im Application-Layer vorhanden.
- Keine Abhängigkeit zu Avalonia-Namespaces im Interface.
