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
