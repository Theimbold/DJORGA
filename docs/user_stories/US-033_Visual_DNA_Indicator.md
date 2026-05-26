# User Story US-033: Visueller DNA-Indikator in der Library

## Status: Abgeschlossen
**Epic:** [E-019: Contextual DNA System](../epics/E-019_Contextual_DNA.md)

## Beschreibung
**Als** DJ
**möchte ich** in der Trackliste sofort an der Farbe erkennen, welche Stimmung ein Track hat,
**um** während des Sets ohne Lesen den nächsten passenden Track zu finden.

## Akzeptanzkriterien
- [x] Anzeige eines vertikalen Farbbalkens pro Track-Zeile.
- [x] Die Farbe entspricht exakt dem Mood-Farbschema des Pickers.
- [x] Ein Tooltip zeigt die Stimmung im Klartext an.
- [x] Performance: Das Rendering erfolgt effizient über das DataGrid-Template.

## Linked Implementation
- **View:** `MyApp.Desktop.Views.LibraryView.axaml`
- **Converter:** `MyApp.Desktop.Converters.MoodToColorConverter`
- **Asset:** Mood-Farbschema in `MoodToColorConverter`
