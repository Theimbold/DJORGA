# Projekt: DJORGA

## Vision
DJORGA ist eine moderne Desktop-Anwendung zur Verwaltung von DJ-Musikbibliotheken, die KI-gestützte Playlist-Erstellung und Rekordbox-Integration bietet. Das System zielt darauf ab, DJ-Workflows durch intelligente Automatisierung und eine klare, plattformübergreifend einheitliche Benutzeroberfläche zu optimieren.

## Ziel des Projekts
Transformation des Prototyps in eine High-Performance Anwendung auf Basis der Clean Architecture mit Fokus auf High-End UX und Audio-Streaming.

## Technologien
- **Frontend:** Avalonia UI (v11.0.0), CommunityToolkit.Mvvm.
- **Backend:** .NET 8, EF Core (SQLite).
- **Media:** TagLib# (Metadata), SkiaSharp (Images), NAudio (Audio Engine).

## Architektur & IA
- **Clean Architecture:** Strenge Layer-Trennung.
- **Kontextuelle Sichtbarkeit:** UI-Elemente (Player, Listen) erscheinen erst, wenn Daten vorhanden sind.
- **Performance:** Asynchrones Streaming und Peak-Caching für große Dateien (100MB+).

## Hauptfeatures
- **Rekordbox-Integration:** XML Import.
- **Media Engine:** Multi-Format Metadaten & Cover-Extraction.
- **High-Performance Player:** Interaktive Waveform, lückenloses Streaming.
- **Corporate Design:** Abgerundete Formen, Schatten-basierte Tiefe, keine harten Konturen.
