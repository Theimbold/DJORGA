# Task 006: Rekordbox Service Interface

## Feature
External Service Abstraction

## Aufgabe
Definition einer sauberen Schnittstelle im Application-Layer, um den Rekordbox-Import von der technischen Implementierung (XML-Parsing) zu entkoppeln.

## Schritte
1. Erstellen des Verzeichnisses `MyApp.Application/Interfaces/External`.
2. Erstellen des Interfaces `IRekordboxService`.
3. Definition der Methode `Task<IEnumerable<Track>> ParseLibraryAsync(string filePath)`.

## Definition of Done
- Interface ist im korrekten Namespace definiert.
- Nutzt die Domänen-Entität `Track`.
- Keine Abhängigkeit zu XML-Bibliotheken im Interface.
