# User Story US-031: Musikalische Stimmung zuweisen (Mood)

## Status: Abgeschlossen
**Epic:** [E-019: Contextual DNA System](../epics/E-019_Contextual_DNA.md)

## Beschreibung
**Als** DJ
**möchte ich** jedem Track eine von 8 spezifischen Stimmungen (Moods) zuweisen können,
**um** meine Musikbibliothek funktional nach emotionaler Energie zu ordnen.

## Akzeptanzkriterien
- [x] Auswahl aus 8 vordefinierten Moods (Melancholic, Energetic, etc.).
- [x] Zuweisung erfolgt über ein grafisches 8x8 Grid.
- [x] Die Stimmung wird permanent in der Datenbank gespeichert.
- [x] Unterstützung von Batch-Editing für mehrere Tracks.

## Linked Implementation
- **Domain:** `MyApp.Domain.ValueObjects.TrackMood`
- **UI Control:** `MyApp.Desktop.Controls.DnaPickerControl`
- **ViewModel:** `MyApp.Desktop.ViewModels.EditTrackViewModel`
