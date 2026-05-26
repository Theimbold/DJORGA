# User Journey UJ-002: Den perfekten Mix vorbereiten (DNA & AI)

## Status: Implementiert
**Ziel:** Ein DJ möchte für ein spezifisches Event (z.B. Sunset-Set) passende Musik kuratieren.

## Beteiligte User Stories
- [US-031: Musikalische Stimmung zuweisen](../user_stories/US-031_Mood_Assignment.md)
- [US-032: Tageszeit-Kontext zuweisen](../user_stories/US-032_TimeContext_Selection.md)
- [US-026: Smart Collections nutzen](../user_stories/US-026_Smart_Collections.md)

## Der Ablauf

### 1. Tracks sichten & DNA vergeben
Der DJ hört durch neue Musik in der `LibraryView`. Bei jedem Track öffnet er den `EditTrackDialog` und wählt im 8x8 Grid die Kombination aus **Mood (z.B. Melancholic)** und **Time Context (z.B. Sunset)** aus.

### 2. Visuelle Bestätigung
Zurück in der Liste sieht der DJ sofort den blauen Farbbalken (Melancholic) am Zeilenanfang. Dies gibt ihm das Vertrauen, dass die Klassifizierung korrekt ist.

### 3. Automatisches Filtering
Der DJ wählt in der Seitenleiste die vordefinierte Smart Collection **"The Sunset Mix"**. Das System filtert sofort alle Tracks heraus, die exakt die DNA `Sunset + Melancholic` besitzen.

### 4. Playlist-Feinschliff
Diese Auswahl wird nun als Quelle für den AI Playlist Builder genutzt, um einen harmonischen Übergang zwischen den Tracks zu gewährleisten.

## Endresultat
Der DJ hat innerhalb weniger Minuten ein hochspezifisches Set vorbereitet, das sowohl emotional als auch musikalisch perfekt passt.

## Linked Implementation
- **Logic:** `MyApp.Application.Services.RuleEvaluatorService`
- **Seeding:** `MyApp.Infrastructure.Persistence.EntityFramework.DbInitializer` (Standard-Szenarien)
