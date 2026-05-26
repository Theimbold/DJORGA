# DJORGA

AI-powered intelligence layer for DJ music libraries.

## Vision
DJORGA is a modern desktop application for DJ music-library management with Rekordbox integration and AI-assisted playlist/set building. It helps DJs understand, organize, and prepare their existing libraries by providing an intelligence layer on top of their structured track metadata.

## Core Definition
**DJORGA is an intelligence layer, not a performance tool.** 
The goal is to help DJs prepare and organize, not to replace Rekordbox or become a full DJ performance system.

## Key Features (MVP)
- **Rekordbox Integration:** Import tracks and metadata from Rekordbox XML.
- **Library Management:** View and inspect track metadata in a clean interface.
- **Contextual DNA:** Assign and visualize mood, time context, and energy.
- **Harmonic Intelligence:** Camelot key and BPM based compatibility scoring.
- **Smart Collections:** Filter tracks using rule-based logic.
- **AI Playlist Builder:** Generate set ideas and track sequences using structured metadata and AI reasoning.
- **Local Persistence:** All data stays on your machine in a SQLite database.

## Architecture
DJORGA follows **Clean Architecture** principles:
- **Domain:** Core entities (Track, Playlist, SmartCollection) and business logic.
- **Application:** Use cases, interfaces, and service logic (Scoring, AI Builder).
- **Infrastructure:** Implementation of external services (EF Core, TagLib, NAudio).
- **Desktop:** Avalonia UI (MVVM) for the desktop experience.

## Tech Stack
- **Frontend:** Avalonia UI (v11), CommunityToolkit.Mvvm
- **Backend:** .NET 8
- **Database:** Entity Framework Core with SQLite
- **Media:** TagLib# (Metadata), NAudio (Audio/Waveform), SkiaSharp (Rendering)

## Getting Started

### Prerequisites
- .NET 8 SDK

### Build and Run
```bash
dotnet restore
dotnet build
dotnet run --project DJORGA.Desktop
```

### Running Tests
```bash
dotnet test
```

## Documentation
- [Master Work Order](docs/project/GEMINI_WORK_ORDER.md) - The original strategic roadmap for this phase.
- [MVP Scope](docs/MVP.md) - Definition of the MVP boundary and product goals.
- [Architecture Overview](docs/architecture/overview.md) - System design and layer responsibilities.
- [Documentation Index](docs/README.md)
- [Build Notes](docs/technical/BUILD_NOTES.md)

---
*Status: MVP Development phase.*
