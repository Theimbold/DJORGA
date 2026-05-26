# Epic E-019: Contextual DNA System

## Status: Abgeschlossen
**Typ:** Feature-Erweiterung (Klassifizierung & Visualisierung)

## Ziel
Einführung eines zweidimensionalen, funktionalen Klassifizierungssystems (8 Moods x 8 Tageszeiten), um das klassische 5-Sterne-Rating durch tiefergehende musikalische DNA zu ersetzen.

## Kern-Konzept
Jeder Track erhält eine "DNA", bestehend aus emotionaler Energie (Mood) und zeitlichem Kontext (Tageszeit). Diese Kombination ermöglicht präzise Playlist-Generierung und visuelle Identifikation in der Library.

## Verknüpfte User Stories
- [US-031: Musikalische Stimmung zuweisen](../user_stories/US-031_Mood_Assignment.md)
- [US-032: Tageszeit-Kontext zuweisen](../user_stories/US-032_TimeContext_Selection.md)
- [US-033: Visueller DNA-Indikator](../user_stories/US-033_Visual_DNA_Indicator.md)

## Phasen der Implementierung
1. **Domain:** Erstellung der Enums `TrackMood` und `TrackTimeContext`.
2. **Data:** Update der `Track` Entität und des `AppDbContext`.
3. **UI:** Entwicklung des `DnaPickerControl` (8x8 Grid) und Integration in den `EditTrackDialog`.
4. **Visual:** Implementierung des DNA-Indikators (Farbbalken) in der `LibraryView`.
5. **Logic:** Erweiterung des `RuleEvaluatorService` zur Filterung nach DNA.

## Linked Implementation
- **Domain Entities:** `MyApp.Domain.Entities.Track`
- **UI Controls:** `MyApp.Desktop.Controls.DnaPickerControl`
- **Converters:** `MyApp.Desktop.Converters.MoodToColorConverter`
- **Database:** `MyApp.Infrastructure.Persistence.EntityFramework.AppDbContext`
