# DriveWatcher Control

Ein Control zum Überwachen der Anzahl der Laufwerke.

Grundlage und Anregung für dieses Control stammen aus dem Internet.

[ActiveVB - VB.NET Tipp 0055:Hinzufügen und Entfernen von USB-Wechselmedien erkennen](http://www.activevb.de/tipps/vbnettipps/tipp0055.html)

Wenn ein neues Laufwerk angeschlossen oder erstellt wird (z.B. eine virtuelle Festplatte), wird ein Ereignis ausgelöst und es werden verschiedene Eigenschaften übergeben.

Wenn ein Laufwerk getrennt wird so wird ebenfalls ein Ereignis ausgelöst und der Laufwerksname wird übergeben.

DriveWatcher Control wurde mit folgenden Geräten wurde getestet:

- USB-Stick
- CD oder DVD Laufwerk fest verbaut oder über USB-Adapter
- Festplatten über USB-Adapter
- USB-Floppylaufwerk
- Virtuelle Festplatte


## Eigenschaften

## Methoden

## Ereignisse

## Versionsinformationen

### V1.2026.0517 (17.05.2026)

Version aktualisiert, ReleaseNotes entfernt, Refactorings

Die Versionsnummern wurden in mehreren Dateien aktualisiert:

- `DriveWatcherControl.vbproj`, `DriveWatcherControlTest.vbproj` und `AssemblyInfo.vb`.

Die Datei `ReleaseNotes.rtf` wurde aus dem Projekt entfernt:

- Verweise in `DriveWatcherControl.vbproj` und `source.extension.vsixmanifest` entfernt.

Ein neuer Link zur Projektseite wurde hinzugefügt:

- `<MoreInfo>` in `source.extension.vsixmanifest` verweist auf GitHub.

Refactorings und neue Funktionen:

- Verbesserte Dispose-Logik für Ressourcenfreigabe.
- Neue Methoden `OnDriveAdded` und `OnDriveRemoved` für bessere Erweiterbarkeit.
- Unterstützung für Medienwechsel- und Netzlaufwerkereignisse hinzugefügt.
- Ereignisse wie `DriveAdded` und `DriveRemoved` entprellt.

Dokumentation und Testanwendung:

- `README.md` überarbeitet, Versionsinformationen strukturiert.
- Testanwendung erweitert und dokumentiert.

---

### V1.2026.0505  (05.05.2026)

- Im Dispose-Override wird nun zusätzlich geprüft, ob NatForm existiert. Falls ja, wird NatForm explizit freigegeben und auf Nothing gesetzt. Die Aufräumlogik für components wurde in einen eigenen Block verschoben, um sicherzustellen, dass sowohl NatForm als auch components korrekt entsorgt werden.
- Eigenschaften von DriveAddedEventArgs werden nun immer mit Standardwerten initialisiert. Beim Zugriff auf Laufwerksinformationen wird ein Try-Block verwendet, um Ausnahmen wie IOException und UnauthorizedAccessException abzufangen. Die Else-Verzweigung entfällt, da Standardwerte bereits vorab gesetzt werden. Dies erhöht die Stabilität beim Umgang mit nicht verfügbaren oder gesperrten Laufwerken.
- Die Events DriveAdded und DriveRemoved werden nun über die neuen, als Protected Overridable deklarierten Methoden OnDriveAdded und OnDriveRemoved ausgelöst. Dies ermöglicht es abgeleiteten Klassen, das Auslösen der Events zu überschreiben und zu erweitern. Die Methoden kapseln das RaiseEvent und verbessern so die Erweiterbarkeit der Klasse.
- DriveAdded- und DriveRemoved-Ereignisse werden nun entprellt, um doppelte oder zu schnell aufeinanderfolgende Ereignisse zu unterdrücken. Dazu wurden neue Felder und ein Enum eingeführt sowie die Methode IsDebouncedEvent implementiert, die Ereignisse innerhalb von 200 ms blockiert.
- Die Klasse NativeForm unterstützt nun die Events MediaInserted und MediaRemoved, die bei Einlegen bzw. Entfernen eines Mediums in Wechsel-/CD-/DVD-Laufwerken ausgelöst werden. Die Events werden anhand von WM_DEVICECHANGE und dem DBTF_MEDIA-Flag erkannt. Die Dokumentation wurde entsprechend erweitert.
- Zwei leere Methoden NatForm_MediaInserted und NatForm_MediaRemoved zur Behandlung von Medienwechsel-Ereignissen in bestehenden Laufwerken ergänzt. Beide Methoden sind mit XML-Kommentaren dokumentiert und dienen als Platzhalter für zukünftige Implementierungen.
- Neue Events NetworkDriveAdded und NetworkDriveRemoved implementiert in der Klasse NativeForm, die bei Hinzufügen oder Entfernen von Netzlaufwerken ausgelöst werden. Ereignisbehandlung und XML-Dokumentation entsprechend erweitert.
- DriveWatcher kann nun Netzlaufwerke erkennen. Neue Events NetworkDriveAdded und NetworkDriveRemoved wurden hinzugefügt. Die interne Ereignisbehandlung und die XML-Dokumentation wurden entsprechend erweitert. Der Event-Typ-Enum berücksichtigt jetzt auch Netzlaufwerk-Ereignisse.
- DriveWatcher kann nun das Einlegen und Entfernen von Medien (z. B. CDs, DVDs, USB-Sticks) in vorhandenen Laufwerken erkennen. Dafür wurden die neuen Events MediaInserted und MediaRemoved sowie die zugehörigen Handler und Methoden implementiert. Die Aufzählung DriveEventType und die XML-Kommentare wurden entsprechend erweitert. Debouncing wird für die neuen Ereignisse ebenfalls berücksichtigt.

---

### V1.2026.0429 (29.04.2026)

- Testanwendung hinzugefügt
- Kommentare überarbeitet

---

### V1.2026.0428 (28.04.2026)

- Erstveröffentlichung in dieser Form.