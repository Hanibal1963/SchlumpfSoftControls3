# FileList Control

FileList ist ein benutzerdefiniertes Control, das Dateien und Verzeichnisse in einem ListView darstellt.

Die folgenden Informationen werden angezeigt:

- Name der Datei oder des Ordners
- Typ der Datei (txt, pdf, ... usw.)
- Die Größe der Datei oder des Ordners (bei der Ordnergröße wir nur der Inhalt dieses Orners berücksichtigt, nicht die Größe der Unterordner)
- Das Erstelldatum der Datei oder des Ordners
- Das Datum des letzten Zugriffs auf die Datei oder den Ordner
- Das Datum der letzten Änderung der Datei oder des Ordners

Wenn in den Spaltenheader geklickt wird, so ändert sich die Sortierung der Liste nach der aktuellen Spalte.

Die Reihenfolge der Spalten kann durch Ziehen verändert werden und wird automatisch beim entladen des Controls gespeichert.

Die Breite der Spalten passt sich automatisch an den Inhalt an.

## Eigenschaften

- **StartFolder** - Legt den ordner fest dessen Inhalt angezeigt werden soll oder gibt diesen zurück.
- **AutoResizeColumnsEnabled** - Legt fest ob die Spaltenbreite automatisch an ihren Inhalt angepasst werden oder gibt den Zustand zurück.
- **ColumnOrderState** - Legt die Reihenfolge der Spalten fest oder gibt diese zurück.

## Methoden

## Ereignisse
