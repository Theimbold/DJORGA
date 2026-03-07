# Task 047: Rekordbox XML Writer Implementation

## Feature
XML Export Service (Roundtrip)

## Aufgabe
Konkrete Implementierung des XML-Generators, der die hierarchische Struktur von Rekordbox (DJMD, COLLECTION, PLAYLISTS) erzeugt.

## Schritte
1. Nutzung von `System.Xml.Linq` zur Erzeugung der XML-Struktur.
2. Mapping der `Track`-Entitäten zurück auf die Rekordbox-Attribute (Location, Name, Artist etc.).
3. Korrekte Verschachtelung der `NODE` Elemente für Playlists.

## Definition of Done
- Erzeugte XML-Datei lässt sich in Pioneer Rekordbox via "Import Playlist" einlesen.
- Alle Pfade und Metadaten bleiben konsistent.
