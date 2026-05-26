# Epic 019: Contextual DNA System

## Status: In Planung
Ersetzung des klassischen 5-Sterne-Ratings durch ein zweidimensionales, funktionales Klassifizierungssystem (8 Moods x 8 Tageszeiten).

## Kern-Konzept
Jeder Track erhält eine "DNA", bestehend aus emotionaler Energie (Mood) und zeitlichem Kontext (Tageszeit). In Kombination mit dem Genre ermöglicht dies eine vollautomatische, hochpräzise Playlist-Generierung und visuelle Identifikation von Tracks in der Library.

### Die Achsen (8x8 Matrix)
1. **Moods:** Melancholic, Hypnotic, Energetic, Aggressive, Uplifting, Dark/Sinister, Minimal/Stripped, Organic/Warm.
2. **Time Context:** Sunrise, Morning, Afternoon, Sunset, Warmup, Peak Time, Late Night, Afterhour.

## Phasen & Subtasks

### Phase 1: Domain & Data Modeling
- [ ] **Task 103:** Erstellung der Enums `TrackMood` und `TrackTimeContext` in `MyApp.Domain/ValueObjects`.
- [ ] **Task 104:** Erweiterung der `Track` Entität um die neuen Properties.
- [ ] **Task 105:** Update des `AppDbContext` und Datenbank-Migration für die neuen Felder.

### Phase 2: DNA Picker UI (Das Grid)
- [ ] **Task 106:** Entwicklung eines `DnaPickerControl` (8x8 Grid), das eine schnelle, visuelle Auswahl ermöglicht.
- [ ] **Task 107:** Integration des Pickers in den `EditTrackDialog`.
- [ ] **Task 108:** Implementierung von Batch-Tagging (Zuweisung von Mood/Time für mehrere Tracks gleichzeitig).

### Phase 3: Visual DNA (Liste)
- [ ] **Task 109:** Definition eines Farbschemas für Moods und Helligkeitsstufen für Tageszeiten.
- [ ] **Task 110:** Implementierung eines "DNA-Indikators" in der `LibraryView` (kleines Icon oder Farbbalken pro Track).

### Phase 4: Smart Context Playlists
- [ ] **Task 111:** Erweiterung des `RuleEvaluatorService` um Filter für Mood und Time Context.
- [ ] **Task 112:** Erstellung von Standard-Szenarien (z.B. "The Sunset Mix").

## Definition of Done
- Tracks können via 8x8 Matrix klassifiziert werden.
- Die Klassifizierung ist permanent in der SQLite DB gespeichert.
- Die Trackliste zeigt die "DNA" visuell (Farbe/Icon) an.
- Smart Collections unterstützen Filterung nach Mood und Tageszeit.
