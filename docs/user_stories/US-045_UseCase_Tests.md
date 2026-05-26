# User Story US-045: Use Case Tests

## Status: Offen
**Epic:** [E-026: Test Coverage](../epics/E-026_Test_Coverage.md)

## Beschreibung
**Als** Entwickler  
**möchte ich** Unit Tests für die zentralen Use Cases (`ImportRekordboxXmlUseCase`,
`UpdateTrackMetadataUseCase`),  
**um** sicherzustellen, dass die Geschäftslogik korrekt arbeitet und bei
zukünftigen Änderungen durch Tests abgesichert ist.

## Akzeptanzkriterien
- [ ] `ImportRekordboxXmlUseCase`: Happy-Path-Test (XML mit 3 Tracks → 3 gespeicherte Tracks).
- [ ] `ImportRekordboxXmlUseCase`: Fehler-Path-Test (Datei nicht gefunden → Exception propagiert).
- [ ] `UpdateTrackMetadataUseCase`: Happy-Path-Test (Metadaten werden aktualisiert).
- [ ] Alle Tests verwenden gemockte Repository-Interfaces (kein echtes SQLite).
- [ ] Tests laufen in unter 500ms durch.

## Linked Implementation
- **Neu:** `DJORGA.Tests/Application/UseCases/ImportRekordboxXmlUseCaseTests.cs`
- **Neu:** `DJORGA.Tests/Application/UseCases/UpdateTrackMetadataUseCaseTests.cs`
