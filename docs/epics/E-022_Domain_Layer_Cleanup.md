# Epic E-022: Domain Layer Bereinigung

## Status: Offen
**Typ:** Architekturverletzung
**Phase:** 2 – Architektur-Integrität

## Priorität
**HOCH.** Verletzt das fundamentale Prinzip der Clean Architecture (Dependency Rule).

## Hintergrund
Die Domain-Entity `Track` implementiert `INotifyPropertyChanged` — eine
UI-spezifische Schnittstelle aus `System.ComponentModel`. Das bedeutet: Die
innerste Schicht der Clean Architecture hat eine direkte Abhängigkeit auf ein
Konzept, das ausschließlich dem Presentation-Layer gehört.

Konsequenzen:
- Die Domain kann nicht ohne UI-Abhängigkeit getestet werden.
- Zukünftige Ports (z.B. Web, CLI) erben UI-Logik im Domain-Modell.
- Es verstößt gegen das Single Responsibility Principle: Eine Entität soll
  Geschäftslogik kapseln, nicht UI-Binding-Verhalten.

## Ziel
`Track` (und alle anderen Domain-Entities) sind reine Datencontainer mit
Geschäftslogik. Die UI-Binding-Verantwortung wandert in dedizierte
`ViewModel`-Klassen im Desktop-Layer.

## Verknüpfte User Stories
- [US-037: INotifyPropertyChanged aus Track entfernen](../user_stories/US-037_Remove_INPC_From_Track.md)
- [US-038: TrackViewModel für UI-Binding einführen](../user_stories/US-038_TrackViewModel.md)

## Technische Umsetzung (Leitlinien)
1. `INotifyPropertyChanged` und alle `OnPropertyChanged()`-Aufrufe aus `Track.cs` entfernen.
2. Properties in `Track` werden einfache Auto-Properties.
3. Ein `TrackViewModel` in `DJORGA.Desktop/ViewModels/` wraps `Track` und
   implementiert `INotifyPropertyChanged` via `CommunityToolkit.Mvvm`.
4. Alle View-Bindings werden auf `TrackViewModel` umgestellt.
5. `LibraryViewModel` verwaltet eine `ObservableCollection<TrackViewModel>`
   statt `ObservableCollection<Track>`.

## Akzeptanzkriterien
- [ ] `Track.cs` hat keine Abhängigkeit auf `System.ComponentModel`.
- [ ] `Track.cs` hat keine `event`-Deklarationen.
- [ ] `TrackViewModel` existiert und implementiert alle benötigten Bindings.
- [ ] Alle Views binden korrekt auf `TrackViewModel`.
- [ ] Alle bestehenden Tests laufen weiterhin durch.

## Abhängigkeiten
- Erfordert: E-021 (Projekt muss buildbar sein)
