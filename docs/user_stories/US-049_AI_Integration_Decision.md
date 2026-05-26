# User Story US-049: Entscheidung über echte KI-Integration

## Status: Offen
**Epic:** [E-027: Sequence Builder Klarheit](../epics/E-027_Sequence_Builder_Clarity.md)

## Beschreibung
**Als** Produktverantwortlicher  
**möchte ich** eine dokumentierte Architekturentscheidung darüber, ob und wann
eine echte LLM-Integration in den Sequence Builder kommt,  
**um** den Fahrplan für das Produkt klar zu kommunizieren.

## Akzeptanzkriterien
- [ ] Ein ADR (`docs/adr/004_sequence_builder_strategy.md`) dokumentiert die
      gewählte Option (Greedy-only, LLM-optional, oder LLM-primary).
- [ ] Das ADR beschreibt Kontext, Optionen, Entscheidung und Konsequenzen.
- [ ] Falls LLM-Integration geplant: Ein `ISequenceBuilderProvider`-Interface
      ist als Erweiterungspunkt im Architektur-Dokument skizziert.

## Linked Implementation
- **Neu:** `docs/adr/004_sequence_builder_strategy.md`
