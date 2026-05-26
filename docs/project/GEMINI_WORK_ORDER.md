# GEMINI CLI WORK ORDER: DJORGA Architecture, Documentation, and MVP Cleanup

> **Source:** Integrated from external user directive (Downloads/GEMINI_DJORGA_MVP_WORKPLAN.md)
> **Status:** ACTIVE - Current Master Roadmap

You are Gemini CLI working inside the `Theimbold/DJORGA` repository.

Your job is to put DJORGA into a focused, maintainable MVP state. Do not randomly add features. First stabilize architecture, documentation, naming, build quality, and the product direction.

## 0. Project context

DJORGA is a .NET 8 / Avalonia desktop application for DJ music-library management with Rekordbox integration and AI-assisted playlist/set building.

Current repo structure includes:

- `MyApp.Domain` — domain entities and value objects such as Track, Playlist, SmartCollection, CamelotKey, mood/time context rules.
- `MyApp.Application` — use cases, DTOs, service interfaces, harmonic scoring, smart rules, AI playlist builder service.
- `MyApp.Infrastructure` — EF Core SQLite persistence, TagLib metadata, NAudio playback/waveform, Rekordbox XML import/export.
- `MyApp.Desktop` — Avalonia UI, view models, views, controls, services.
- `MyApp.Api` — minimal API placeholder.
- `MyApp.Tests` — domain/application tests.
- `ai/` — planning and task notes.
- `docs/` — architecture docs, ADRs, story map, user stories.

The product is currently broad: Rekordbox companion, audio player, waveform renderer, metadata tool, smart collections, AI playlist builder, contextual DNA system. For the MVP, focus is required.

## 1. Product decision

Define DJORGA as:

> AI-powered intelligence layer for DJ libraries.

The MVP should not try to replace Rekordbox or become a full DJ performance system. The MVP should help DJs understand, organize, and prepare their existing libraries.

### MVP promise

A DJ can import a Rekordbox XML or local library, inspect enriched track metadata, assign/see contextual DNA, create smart collections, and ask AI for useful set/playlist ideas based on structured library data.

### Killer feature

AI-assisted contextual set building using structured DJ metadata.

Example prompts the app should eventually support:

- “Build a 90-minute dark hypnotic techno set from 124 to 128 BPM.”
- “Find tracks that transition smoothly after this one.”
- “Create a warmup crate for a 1am warehouse slot.”
- “Show me emotionally similar tracks with compatible Camelot keys.”

## 2. Important architectural rule

Gemini / AI should not be used as the primary raw-audio analyzer.

AI should reason over structured data:

```json
{
  "title": "...",
  "artist": "...",
  "bpm": 124,
  "camelotKey": "8A",
  "energy": 0.82,
  "mood": "Dark",
  "timeContext": "PeakTime",
  "tags": ["hypnotic", "rolling"],
  "playCount": 3
}
```

Audio/DSP analysis should remain a separate service boundary. For MVP, keep existing TagLib/NAudio/SkiaSharp capabilities. Document a future `AudioAnalysisService` boundary for Essentia/librosa/Python/ONNX, but do not overbuild it now.

## 3. Main goals for this work phase

### Goal A — Make the repo understandable

1. Create or update a strong root `README.md`.
2. Document what DJORGA is and is not.
3. Document MVP scope.
4. Add setup/build/run/test instructions.
5. Add a short architecture diagram in text form.
6. Explain current modules.

### Goal B — Normalize naming

The solution still uses `MyApp.*` names. This hurts readability.

Preferred namespace/project direction:

- `DJORGA.Domain`
- `DJORGA.Application`
- `DJORGA.Infrastructure`
- `DJORGA.Desktop`
- `DJORGA.Api`
- `DJORGA.Tests`

If renaming all projects is safe, do it carefully and update solution/project references. If it is too risky in one pass, create a clear migration plan in `docs/technical/NAMING_MIGRATION.md` and avoid partial broken renames.

### Goal C — Define the MVP boundary

Create `docs/MVP.md` with:

- Product positioning
- Target user
- Problem statement
- MVP features
- Explicit non-goals
- User flows
- Success criteria
- Next-phase ideas

Recommended MVP features:

1. Rekordbox XML import.
2. Track library view.
3. Metadata inspection/editing.
4. Camelot key and BPM based harmonic compatibility.
5. Contextual DNA fields: mood, time context, energy/role where already present.
6. Smart collections using rules.
7. AI playlist/set builder that consumes structured track metadata.
8. Local SQLite persistence.

Non-goals for MVP:

- Replacing Rekordbox.
- Full live DJ performance engine.
- Streaming integrations.
- Perfect audio analysis.
- Advanced waveform polish beyond current basics.
- Cloud-first user accounts.

### Goal D — Clean documentation sprawl

The `ai/planning`, `ai/tasks`, and `docs` folders contain many notes. Do not delete useful work blindly.

Create a documentation index:

- `docs/README.md`
- `ai/README.md`

Group docs into:

- Current product docs
- Architecture decisions
- Historical planning
- Implemented tasks
- Future backlog

Mark stale or historical docs clearly. Prefer adding headers like:

```md
> Status: Historical planning note. Do not treat as current MVP scope.
```

### Goal E — Improve build and test reliability

1. Run `dotnet restore`.
2. Run `dotnet build`.
3. Run `dotnet test`.
4. Fix obvious compile/test errors.
5. Do not introduce big new features until the solution builds.

If dependencies or environment prevent running something, document the exact blocker in `docs/technical/BUILD_NOTES.md`.

### Goal F — Review architecture for layer violations

Check:

- Domain must not depend on Application/Infrastructure/Desktop.
- Application defines interfaces and use cases.
- Infrastructure implements persistence/external services.
- Desktop depends on Application and Infrastructure only through DI where possible.
- UI logic should stay in ViewModels, not code-behind unless necessary for Avalonia control plumbing.

Create `docs/analysis/current_architecture_review.md` with:

- What is good
- Layer violations found
- Concrete fixes
- Risk level

Apply low-risk fixes immediately.

## 4. Code cleanup priorities

Work in this order:

### Step 1 — Baseline

- Inspect repo.
- Run restore/build/test.
- Record baseline status.
- Do not refactor before knowing whether build is green.

### Step 2 — Documentation foundation

Create/update:

- `README.md`
- `docs/MVP.md`
- `docs/README.md`
- `docs/architecture/overview.md` or update existing architecture docs
- `docs/technical/BUILD_NOTES.md`
- `ai/README.md`

### Step 3 — Naming / structure plan

Decide whether to rename `MyApp.*` now.

Safe full rename checklist:

- Project folders
- `.csproj` names
- `.sln` references
- Root namespaces
- `using MyApp...` directives
- DI registrations
- XAML namespaces
- Test references

If not doing now, add a migration plan.

### Step 4 — Thin MVP flow

Ensure the following flow is coherent in code and docs:

1. User imports Rekordbox XML.
2. Tracks are persisted to SQLite.
3. Library view displays tracks.
4. User can inspect/edit metadata/DNA.
5. Smart collection rules can filter tracks.
6. Harmonic scoring can suggest compatibility.
7. AI playlist builder receives a track list and returns ranked/scored suggestions.

Do not build a new UI from scratch. Clean existing pieces.

### Step 5 — AI boundary cleanup

Make sure the AI playlist builder interface is explicit and testable.

Recommended abstraction:

```csharp
public interface IAiPlaylistBuilder
{
    Task<IReadOnlyList<ScoredTrack>> BuildPlaylistAsync(
        AiPlaylistRequest request,
        IReadOnlyCollection<TrackMetadata> candidates,
        CancellationToken cancellationToken = default);
}
```

If similar types already exist, improve them rather than duplicating.

The AI implementation should:

- Accept structured metadata.
- Not read files directly.
- Be replaceable/mockable.
- Return explainable scores/reasons.

If Gemini CLI integration is not yet implemented in code, document it as an adapter:

- `GeminiCliPlaylistBuilder : IAiPlaylistBuilder`
- executes `gemini` as external process
- sends JSON prompt/context
- parses JSON response
- handles timeout/errors
- logs prompt/response safely without exposing personal data

### Step 6 — Tests

Add or improve tests for:

- `CamelotKey`
- `KeyCompatibility`
- `HarmonicScoringService`
- `RuleEvaluatorService`
- Smart collection filtering
- AI builder with mocked implementation

Do not require real audio files or Gemini CLI for tests.

## 5. Gemini CLI adapter specification

If implementing the Gemini CLI adapter, use a strict JSON protocol.

### Input payload

```json
{
  "goal": "Build a 60 minute warmup set",
  "constraints": {
    "minBpm": 122,
    "maxBpm": 126,
    "targetMood": ["dark", "hypnotic"],
    "avoidVocals": false,
    "maxTracks": 20
  },
  "tracks": [
    {
      "id": "...",
      "title": "...",
      "artist": "...",
      "bpm": 124.0,
      "camelotKey": "8A",
      "energy": 0.8,
      "mood": "Dark",
      "timeContext": "Warmup",
      "tags": ["rolling"]
    }
  ]
}
```

### Output payload

```json
{
  "playlistTitle": "Dark Hypnotic Warmup",
  "tracks": [
    {
      "id": "...",
      "position": 1,
      "score": 0.92,
      "reason": "Compatible key, low-to-mid energy, good opener"
    }
  ],
  "notes": "Energy rises gradually from 122 to 126 BPM."
}
```

### Hard requirements

- Validate JSON before using it.
- Time out external process calls.
- Log errors but keep the app usable.
- Provide a deterministic fallback builder when Gemini is unavailable.

## 6. UX direction

Keep UI focused on DJ preparation, not performance.

Primary navigation for MVP:

1. Dashboard
2. Library
3. Smart Collections
4. AI Builder
5. Harmonic Map
6. Settings

The first-run flow should guide the user to:

1. Import Rekordbox XML or choose a music folder.
2. Confirm track count.
3. Review library.
4. Generate first AI crate/set.

## 7. Technical cautions

- Do not commit local database files like `djorga.db` unless this is deliberate sample data. Prefer adding it to `.gitignore` if not needed.
- Do not delete `_Legacy` immediately; mark it as historical and move deletion to a later cleanup task.
- Avoid adding heavy Python/DSP infrastructure during this phase.
- Avoid creating a full API backend unless desktop actually needs it.
- Avoid making Gemini required for core app startup.
- Keep all AI features optional and failure-tolerant.

## 8. Deliverables expected from this work phase

At the end, produce:

1. A build/test status summary.
2. Updated root README.
3. MVP document.
4. Documentation index.
5. Architecture review.
6. Build notes.
7. Either completed `MyApp` -> `DJORGA` rename or a safe migration plan.
8. A prioritized backlog with small next tasks.
9. Any low-risk code fixes needed for build/test reliability.

## 9. Commit strategy

Use small commits:

1. `docs: define DJORGA MVP scope`
2. `docs: add architecture and build notes`
3. `chore: document AI planning archive`
4. `test: improve domain and rule tests`
5. `refactor: clarify AI playlist builder boundary`
6. `chore: add naming migration plan` or `refactor: rename projects to DJORGA namespaces`

Do not mix large rename work with behavioral changes.

## 10. Definition of done

This phase is done when a new developer can clone the repo and understand:

- What DJORGA is.
- What the MVP is.
- How to build/run/test it.
- Which docs are current vs historical.
- Where Domain/Application/Infrastructure/Desktop responsibilities live.
- How AI/Gemini is supposed to integrate.
- What the next implementation steps are.
