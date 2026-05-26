# Task 022: Navigation Service (Clean Architecture)

## Feature
The Shell Layout

## Aufgabe
Implementierung eines `INavigationService`, um die Navigation aus den ViewModels heraus zu steuern, ohne Abhängigkeiten zur UI zu schaffen.

## Schritte
1. Definition von `INavigationService` in `MyApp.Application/Interfaces/UI`.
2. Implementierung des Services in `MyApp.Desktop` (Verknüpfung mit dem `MainViewModel`).
3. Registrierung im DI-Container.
4. Refactoring des `MainViewModel`, um den Service für den Seitenwechsel zu nutzen.

## Definition of Done
- Navigation erfolgt entkoppelt über das Interface.
- `MainViewModel` steuert `CurrentPage` über den Service.
- Architektur-Integrität (Clean Architecture) ist gewahrt.
