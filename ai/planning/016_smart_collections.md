# Epic 016: Smart Collections & Dynamic Filtering

## Status: Abgeschlossen
Dynamische, regelbasierte Sammlungen und Echtzeit-Filtering implementiert.

## Phasen & Subtasks

### Phase 1: Infrastruktur (Data Access)
- [x] **Task 084:** `SmartCollection` Domänen-Entität erstellt.
- [x] **Task 085:** `ISmartCollectionRepository` definiert und implementiert.
- [x] **Task 086:** Datenbank-Schema-Erweiterung (Migration via JSON-Converter).

### Phase 2: Rule Engine (Logik)
- [x] **Task 087:** `RuleEvaluatorService` zur Übersetzung von Kriterien in LINQ-Expressions.
- [x] **Task 088:** Validierung der Filter-Logik.

### Phase 3: User Experience (UI)
- [x] **Task 089:** Implementierung der "Quick Filter Bar" in der `LibraryView`.
- [x] **Task 090:** Sidebar-Integration für gespeicherte Smart Collections.

### Phase 4: Advanced Editor
- [x] **Task 091:** Dialog zur Erstellung und Bearbeitung von Smart Collection Regeln.
- [x] **Task 092:** Cross-Feature Integration: Smart Collections als Quelle für den AI Playlist Builder.

## Definition of Done
- Nutzer können komplexe Regeln grafisch definieren.
- Bibliothek reagiert in Echtzeit auf Filter und Sammlungen.
