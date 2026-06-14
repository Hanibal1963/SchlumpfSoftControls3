# AniGif Control

Ein Control zum Anzeigen von animierten GIF - Grafiken.

Grundlage und Anregung für dieses Steuerelement stammen aus dem Buch **"Visual Basic 2015 - Grundlagen und Profiwissen"** von Walter Dobrenz und Thomas Gewinnus.

Der ursprüngliche Quelltext wurde von mir verändert und um weitere Funktionen erweitert.

Dieser Code sollte für mich als Übung dienen und ich denke das er auch für andere Anfänger interessant sein dürfte.

Weitere Infos unter:

[HANSER Fachbuch](https://www.hanser-fachbuch.de/fachbuch/artikel/9783446446052)

[Buchleser freigegeben auf onedrive](https://onedrive.live.com/?id=root&cid=D73E81A6F971DBA7&qt=people&personId=de18bb46da92110)

## Eigenschaften

- **AutoPlay** - Steuert, ob die GIF‑Animation automatisch gestartet wird, sobald ein Bild vorhanden ist.
- **Gif** - Gibt die animierte GIF‑Grafik zurück oder legt diese fest.
- **GifSizeMode** - Gibt den Anzeigemodus (Skalierung/Ausrichtung) der GIF‑Grafik zurück oder legt ihn fest und kann einen der folgenden Werte annehmen:
  - **Normal** - Zeigt die GIF‑Grafik in ihrer Originalgröße an.
  - **CenterImage** - Zentriert die GIF‑Grafik im Control, ohne sie zu skalieren.
  - **StretchImage** - Skaliert die GIF‑Grafik, um den gesamten Bereich des Controls auszufüllen, wobei das Seitenverhältnis möglicherweise verzerrt wird.   - **Zoom** - Skaliert die GIF‑Grafik, um den gesamten Bereich des Controls auszufüllen, während das Seitenverhältnis beibehalten wird. Es kann zu schwarzen Balken kommen, wenn die Proportionen nicht übereinstimmen.
- **CustomDisplaySpeed** - Legt fest, ob die benutzerdefinierte Anzeigegeschwindigkeit (Timer/FPS) oder die im GIF hinterlegte Bildfolge (ImageAnimator) verwendet wird.
- **FramesPerSecound** - Legt die benutzerdefinierte Anzeigegeschwindigkeit in Bildern pro Sekunde (FPS) fest.
- **ZoomFactor** - Legt den Zoomfaktor in Prozent fest, mit dem das GIF skaliert wird.

## Methoden

- **StartAnimation** - Startet die Animation (falls noch nicht aktiv).
- **StopAnimation** - Stoppt die Animation und beendet Timer sowie ImageAnimator.

## Ereignisse

- **NoAnimation** - Wird ausgelöst wenn die Grafik nicht animiert werden kann.
- **AutoPlayChanged** - Wird ausgelöst wenn sich die AutoPlay‑Eigenschaft geändert hat.
- **AnimationStarted** - Wird ausgelöst wenn die Animation gestartet wurde.
- **AnimationStopped** - Wird ausgelöst wenn die Animation gestoppt wurde.

## Versionsinformationen

### V1.2026.0517 (17.05.2026)

Versionen aktualisiert und Referenzen bereinigt

Die Assembly-Versionen in mehreren Dateien (`AniGifControl.vbproj`, `AssemblyInfo.vb`, `source.extension.vsixmanifest`, `AniGifControlTest.vbproj`) wurden auf `1.2026.0517.x` aktualisiert.

In `AniGifControlTest.vbproj` wurden nicht mehr benötigte Referenzen entfernt, darunter `Microsoft.VisualStudio.Shell.15.0`, `Microsoft.VisualStudio.Shell.Framework` und `Microsoft.VisualStudio.Threading`.

Zusätzlich wurde in der Testprojektdatei `AssemblyInfo.vb` die Sichtbarkeit (`ComVisible`) auf `False` geändert und die GUID entfernt.

---

### V1.2026.0503 (03.05.2026)

- neue Events hinzugefügt: AutoPlayChanged, AnimationStarted, AnimationStopped.

---

### V1.2026.0426 (26.04.2026)

- Als eigenes Projekt erstellt.
- Interne Funktionen in eigene Klasse ausgelagert.
- Interne Variablendefinitionen in eigenes Modul ausgelagert.
- Member voll qualifiziert.

---

### V1.2026.0425 (25.04.2026)

- Erstveröffentlichung in dieser Form.