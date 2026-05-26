# User Story US-037: INotifyPropertyChanged aus Track entfernen

## Status: Offen
**Epic:** [E-022: Domain Layer Bereinigung](../epics/E-022_Domain_Layer_Cleanup.md)

## Beschreibung
**Als** Entwickler  
**möchte ich**, dass die `Track`-Entity keine UI-spezifischen Interfaces
implementiert,  
**um** die Reinheit der Domain-Schicht gemäß Clean Architecture sicherzustellen
und `Track` unabhängig von UI-Technologien testbar zu halten.

## Akzeptanzkriterien
- [ ] `Track.cs` importiert kein `System.ComponentModel` mehr.
- [ ] `Track.cs` enthält kein `INotifyPropertyChanged`, kein `event PropertyChangedEventHandler`,
      kein `OnPropertyChanged()`.
- [ ] `Track`-Properties sind einfache Auto-Properties.
- [ ] `Track.IsValid()` bleibt als Domänen-Methode erhalten.
- [ ] `dotnet build` weiterhin fehlerfrei.

## Linked Implementation
- **Datei:** `DJORGA.Domain/Entities/Track.cs`
