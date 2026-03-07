# ADR 001: Einführung der Clean Architecture & .NET 8 Upgrade

## Status
Vorgeschlagen

## Kontext
Das aktuelle Projekt `DJORGA` besteht aus einem Mix aus .NET Framework 4.7.2 und .NET 6.0. Die Geschäftslogik ist teilweise direkt in UI-Komponenten oder losen Services eingebettet. Dies erschwert die Testbarkeit und Wartbarkeit.

## Entscheidung
Wir stellen das gesamte System auf **.NET 8** um und implementieren eine strikte **Clean Architecture**.

### Layer-Struktur:
1. **MyApp.Domain:** Reine POCOs (Plain Old CLR Objects) und Business Rules. Keine Abhängigkeiten nach außen.
2. **MyApp.Application:** Use Cases, Interfaces für Repositories und Services. Hängt nur von Domain ab.
3. **MyApp.Infrastructure:** Implementierung der Repositories (z.B. SQLite/Entity Framework), API-Clients und File-System-Zugriffe (Rekordbox XML). Hängt von Application ab.
4. **MyApp.Desktop:** Avalonia UI Projekt. Verwendet ViewModels, die Use Cases aus dem Application-Layer aufrufen.

## Konsequenzen
- **Vorteil:** Hohe Testbarkeit (Unit-Tests für Logik ohne UI/DB möglich).
- **Vorteil:** Zukunftssicherheit durch aktuelles .NET 8 LTS Framework.
- **Nachteil:** Initialer Aufwand für das Refactoring der Legacy-Module (`Core`, `Infrastructure`).
- **Nachteil:** Notwendigkeit von Mapping zwischen Layer-Modellen (Entities vs. DTOs/ViewModels).
