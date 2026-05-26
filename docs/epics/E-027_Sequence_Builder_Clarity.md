# Epic E-027: Sequence Builder Klarheit

## Status: Offen
**Typ:** Konzeptuelle Korrektheit & Ehrlichkeit
**Phase:** 5 – Klarheit & Dokumentation

## Hintergrund
Der `AiPlaylistBuilderService` heißt „AI", implementiert aber einen
**Greedy-Nearest-Neighbor-Algorithmus**: Bei jedem Schritt wird der
Track mit dem höchsten Kompatibilitäts-Score zum aktuellen Track gewählt.
Es gibt keine lernende Komponente, kein LLM, keine Begründungslogik.

Der Name „AI Playlist Builder" weckt Erwartungen, die das System nicht
erfüllt. Das ist ein Problem in der Kommunikation gegenüber Nutzern und
eine Unehrlichkeit im Code.

## Ziel
Entweder wird der Service ehrlich umbenannt **oder** eine echte
KI-Komponente wird als optionaler Provider integriert. Die Entscheidung
ist als ADR zu dokumentieren.

## Verknüpfte User Stories
- [US-048: Service umbenennen und Algorithmus dokumentieren](../user_stories/US-048_Rename_SequenceBuilder.md)
- [US-049: Entscheidung über echte KI-Integration](../user_stories/US-049_AI_Integration_Decision.md)

## Entscheidungsoptionen

### Option A: Umbenennen (Minimal)
`AiPlaylistBuilderService` → `HarmonicSequenceBuilderService`  
`IAiPlaylistBuilder` → `ISequenceBuilder`  
UI-Label: „Harmonischer Set-Planer" statt „AI Playlist Builder"  
Aufwand: Klein. Risiko: Keins.

### Option B: Echte KI-Integration (Erweiterung)
Einen optionalen `ISequenceBuilder`-Provider, der das Anthropic Claude API
verwendet, um Track-Sequenzen mit natürlichsprachiger Begründung zu erzeugen.
Der Greedy-Algorithmus bleibt als Fallback, wenn kein API-Key konfiguriert ist.  
Aufwand: Mittel-Groß. Risiko: API-Abhängigkeit, Kosten.

## Akzeptanzkriterien
- [ ] Eine ADR-Datei dokumentiert die gewählte Option und ihre Begründung.
- [ ] Der Klassenname im Code stimmt mit dem UI-Label überein.
- [ ] Alle Referenzen (Tests, ViewModels, DI-Registrierung) sind aktualisiert.

## Abhängigkeiten
- Erfordert: E-021
- Empfohlen vor: E-026 (damit Tests die finalen Namen verwenden)
