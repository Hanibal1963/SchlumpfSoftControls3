# ColorProgressBar Control

Ein benutzerdefiniertes Windows Forms-Steuerelement zur Anzeige eines farbigen Fortschrittsbalkens mit optionalem Rahmen und Glanzeffekt.

Die Idee hinter dem `ColorProgressBarControl` ist es, einen Fortschrittsbalken zu erstellen, der in optisch anpassbar ist.

Der Standard-Fortschrittsbalken in Windows ist ein einfacher Balken, der den Fortschritt in Form einer Füllung anzeigt.

Der `ColorProgressBarControl` hingegen kann in verschiedenen Farben und Stilen angezeigt werden.

Als Anregung diente der Artikel [A Better ProgressBar - Using Panels!](https://www.codeproject.com/Articles/31903/A-Better-ProgressBar-Using-Panels) von Saul Johnson.

Da die Donwnloads auf der Seite nicht mehr zu funktionieren scheinen und die Beschreibung nur Ausschnitte aus dem Original C# Code enthält und ich wenig Ahnung von C# habe, habe ich das Control in VB NET umgesetzt.

## Eigenschaften

- **Value** - Der aktuelle Fortschrittswert.
- **ProgressMaximumValue** - Der maximale Fortschrittswert.
- **Barcolor** - Die Farbe des Fortschrittsbalkens.
- **EmptyColor** - Die Farbe des leeren Bereichs des Fortschrittsbalkens.
- **BorderColor** - Die Farbe des Rahmens um den Fortschrittsbalken.
- **ShowBorder** - Gibt an, ob der Rahmen um den Fortschrittsbalken angezeigt werden soll.
- **IsGlossy** - Gibt an, ob der Fortschrittsbalken einen Glanzeffekt haben soll.

## Methoden

## Ereignisse

- **Click** - Wird ausgelöst, wenn auf den Fortschrittsbalken geklickt wird.

## Versionsinformationen

### V1.2026.0517 (17.05.2026)

Versionen aktualisiert und Metadaten ergänzt

Die Assembly-Versionen und Assembly-Dateiversionen wurden in den Projekten `ColorProgressBarControl` und `ColorProgressBarControlTest` aktualisiert.

Im Projekt `ColorProgressBarControl`:

- Assembly-Version von `1.2026.0505.16` auf `1.2026.0517.1` geändert.
- Paket-Versionsnummer in `source.extension.vsixmanifest` auf `1.2026.0517` aktualisiert.
- Ein neuer `<MoreInfo>`-Eintrag mit Verweis auf die GitHub-Seite hinzugefügt.

Im Projekt `ColorProgressBarControlTest`:

- Assembly-Version von `1.2026.0505.2` auf `1.2026.0517.0` geändert.
- `<UseWinFormsOutOfProcDesigner>` für Debug- und Release-Konfigurationen hinzugefügt.
- Assembly-Beschreibung ergänzt, `<Assembly: Runtime.InteropServices.ComVisible>` auf `False` gesetzt und GUID entfernt.

---

### V1.2026.0505 (05.05.2026)

- Die Begrenzung von MaxValue in ColorProgressBar ist nun unabhängig von der Steuerelement-Breite und basiert nur noch auf dem übergebenen Wert (mindestens 1).
- Öffentliches Click-Ereignis entfernt, stattdessen OnClick der Basisklasse in Panel-Handlern verwendet.
- Padding-Logik optimiert, um unnötige Änderungen zu vermeiden.
- XML-Kommentare bereinigt.
- Die Breite des gefüllten Fortschrittsbalkens wird jetzt proportional zum Wert berechnet, was die Genauigkeit verbessert. Zudem wurden die vier separaten Click-Event-Handler zu einem gemeinsamen Handler zusammengeführt, um doppelten Code zu vermeiden und die Wartung zu erleichtern.
- Die Zuweisung des Paddings erfolgt nun nur noch, wenn sich der Wert tatsächlich ändert. Dadurch werden unnötige Zuweisungen vermieden und die Performance verbessert.
- UpdateProgress und UpdateGloss zu Subs ohne Rückgabewert geändert, Fehlerbehandlung entfernt.
- Im Konstruktor der ColorProgressBar-Klasse wurde DoubleBuffering sowie zusätzliche ControlStyles gesetzt, um das Flackern beim Zeichnen zu reduzieren und die Performance zu verbessern. UpdateStyles() wird aufgerufen, um die Änderungen zu übernehmen.

---

### V1.2026.0429 (29.04.2026)

- Erstveröffentlichung in dieser Form.