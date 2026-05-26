# DJORGA MVP Scope

## Product Positioning
**DJORGA: The AI-powered intelligence layer for DJ libraries.**

DJORGA is NOT a Rekordbox replacement. It is a companion tool that adds a layer of intelligence and advanced organization to existing music libraries.

## Target User
The "Preparation-Heavy" DJ who spends significant time organizing crates, analyzing transitions, and planning sets. This DJ values structured data and wants to leverage AI to find new connections in their library.

## Problem Statement
DJs often have thousands of tracks but struggle to find the "perfect next track" or build cohesive sets for specific contexts (e.g., "3 AM Peak Time Hypnotic Techno"). Existing tools provide basic filtering but lack deep contextual reasoning and intelligent sequence generation.

## MVP Features

### 1. Rekordbox XML Import
- Support for importing tracks, playlists, and basic metadata from `rekordbox.xml`.
- Incremental updates (add new tracks from XML).

### 2. Track Library View
- Clean, searchable list of all tracks.
- Detailed inspection of track metadata.

### 3. Metadata & Contextual DNA
- Support for standard tags (Artist, Title, BPM, Key, Genre).
- Custom DNA fields: **Mood**, **Time Context**, **Energy/Role**.
- Manual editing of DNA fields.

### 4. Harmonic & BPM Compatibility
- Camelot key system support.
- Compatibility scoring based on musical theory and BPM drift.

### 5. Smart Collections
- Rule-based filtering (e.g., "Genre is Techno AND BPM > 125").
- Dynamic updates as track metadata changes.

### 6. AI Playlist/Set Builder
- Generate sequences of tracks based on a starting track and constraints.
- Reasoning over structured metadata (Key, BPM, DNA).
- Scoring and explanation for AI-suggested transitions.

### 7. Local Persistence
- SQLite database for all library and metadata storage.
- No mandatory cloud connection.

## Non-Goals for MVP
- **Live Performance:** No "deck" interface, no real-time mixing.
- **Advanced Audio Analysis:** No auto-detection of BPM/Key from raw audio (rely on existing tags or Rekordbox).
- **Streaming Services:** No Spotify/Tidal integration.
- **Cloud Sync:** Multi-device sync is out of scope.
- **Waveform Polish:** Waveforms are for visualization, not frame-perfect beatmatching.

## User Flows
1. **Initial Setup:** User imports `rekordbox.xml` -> Tracks appear in Library.
2. **Organization:** User tags a batch of tracks with "Peak Time" and "Hypnotic".
3. **Planning:** User selects a "Seed" track -> Opens AI Builder -> Requests a 10-track sequence -> Reviews suggestions.
4. **Refinement:** User adjusts smart collection rules to create a specialized crate.

## Success Criteria
- Successful import of a library with 1000+ tracks.
- Accurate harmonic compatibility suggestions.
- Coherent AI-generated playlist suggestions based on input metadata.
- Performance: Library view remains responsive with large datasets.
