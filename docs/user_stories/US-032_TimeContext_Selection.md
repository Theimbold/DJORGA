# User Story US-032: Tageszeit-Kontext zuweisen (TimeContext)

## Status: Abgeschlossen
**Epic:** [E-019: Contextual DNA System](../epics/E-019_Contextual_DNA.md)

## Beschreibung
**Als** DJ
**möchte ich** jedem Track einen optimalen Zeit-Kontext (z.B. Sunset, Peak Time) zuweisen,
**um** automatische Playlisten für spezifische Szenarien generieren zu lassen.

## Akzeptanzkriterien
- [x] Auswahl aus 8 vordefinierten Zeit-Szenarien (Sunrise bis Afterhour).
- [x] Auswahl erfolgt über die vertikale Achse im DNA-Grid.
- [x] Speicherung erfolgt in der SQLite Datenbank.
- [x] Anzeige des gewählten Kontexts in den Track-Details.

## Linked Implementation
- **Domain:** `MyApp.Domain.ValueObjects.TrackTimeContext`
- **UI Control:** `MyApp.Desktop.Controls.DnaPickerControl`
- **Persistence:** `MyApp.Infrastructure.Persistence.EntityFramework.AppDbContext`
