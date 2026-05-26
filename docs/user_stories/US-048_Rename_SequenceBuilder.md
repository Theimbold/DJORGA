# User Story US-048: Service umbenennen und Algorithmus dokumentieren

## Status: Offen
**Epic:** [E-027: Sequence Builder Klarheit](../epics/E-027_Sequence_Builder_Clarity.md)

## Beschreibung
**Als** Entwickler  
**möchte ich**, dass der `AiPlaylistBuilderService` einen Namen trägt, der den
tatsächlichen Algorithmus beschreibt,  
**um** falsche Erwartungen über KI-Fähigkeiten zu vermeiden.

## Akzeptanzkriterien
- [ ] `AiPlaylistBuilderService` → `HarmonicSequenceBuilderService`.
- [ ] `IAiPlaylistBuilder` → `ISequenceBuilder`.
- [ ] XML-Dokumentation der Klasse beschreibt den Greedy-Algorithmus präzise.
- [ ] UI-Label im `AIBuilderViewModel` / `AIBuilderView` wird angepasst.
- [ ] DI-Registrierung ist aktualisiert.
- [ ] Alle Tests referenzieren den neuen Namen.

## Linked Implementation
- **Umbenannt:** `DJORGA.Application/Services/AiPlaylistBuilderService.cs`
- **Umbenannt:** `DJORGA.Application/Interfaces/Services/IAiPlaylistBuilder.cs`
- **Geändert:** `DJORGA.Desktop/ViewModels/AIBuilderViewModel.cs`
- **Geändert:** `DJORGA.Desktop/Views/AIBuilderView.axaml`
