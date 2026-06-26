# IniFileControl

Ein Set von Controls zum Verwalten und bearbeiten von INI - Dateien.

Das Set beinhaltet die folgenden Controls:

- **IniFile** - Eine Komponente zum laden und bearbeiten von INI - Dateien.
- **ContentView** - Ein Control zum Anzeigen des Inhaltes einer INI - Datei.
- **CommentEdit** - Ein Control zum bearbeiten des Datei- oder Abschnittskommentars.
- **ListEdit** - Ein Control zum auswählen, hinzufügen,  umbenennen oder löschen von Abschnitten oder Einträgen.
- **EntryValueEdit** - Ein Control zum bearbeiten des Wertes eines Eintrages.

---

## Beschreibung der Controls

### IniFile Eigenschaften

- **FileName** - Der Name der INI - Datei.
- **FilePath** - Der Pfad der INI - Datei ohne den Dateinamen.
- **AutoSave** - Speichert die INI - Datei automatisch nach jeder Änderung.
- **CommentPrefix** - Das Zeichen, das für Kommentare in der INI - Datei verwendet wird.
- **FileSaved** - zeigt and ob die Datei gespeichert ist oder nicht.

### IniFile Methoden

- **CreateNewFile** - Erstellt eine neue INI - Datei. (mit Standardname oder unter Angabe des Namens)
- **LoadFile** - Lädt eine INI - Datei. (mit dem in der Eigenschaft Filename angegeben Datei oder unter Angabe einer Datei)
- **SaveFile** - Speichert die INI - Datei. (mit dem in der Eigenschaft Filename angegeben Datei oder unter Angabe einer Datei)
- **GetFileContent** - Gibt den Inhalt der INI - Datei zurück.
- **GetFileComment** - Gibt den Kommentar der INI - Datei zurück.
- **SetFileComment** - Setzt den Kommentar der INI - Datei.
- **GetSectionNames** - Gibt eine Liste der Abschnittsnamen zurück.
- **GetEntryNames** - Gibt eine Liste der Eintragsnamen eines Abschnitts zurück.
- **AddSection** - Fügt einen neuen Abschnitt hinzu.
- **AddEntry** - Fügt einen neuen Eintrag zu einem Abschnitt hinzu.
- **RenameSection** - Benennt einen Abschnitt um.
- **RenameEntry** - Benennt einen Eintrag um.
- **DeleteSection** - Löscht einen Abschnitt.
- **DeleteEntry** - Löscht einen Eintrag.
- **GetSectionComment** - Gibt den Kommentar eines Abschnitts zurück.
- **GetEntryValue** - Gibt den Wert eines Eintrags zurück.
- **SetSectionComment** - Setzt den Kommentar eines Abschnitts.
- **SetEntryValue** - Setzt den Wert eines Eintrags.

### IniFile Events

- **FileContentChanged** - Wird ausgelöst, wenn sich der Inhalt der INI - Datei geändert hat.
- **SectionNameExist** - Wird ausgelöst, wenn versucht wird einen Abschnitt hinzuzufügen oder umzubenennen, dessen Name bereits existiert.
- **EntryNameExist** - Wird ausgelöst, wenn versucht wird einen Eintrag hinzuzufügen oder umzubenennen, dessen Name bereits existiert.

---

### ContentView Eigenschaften

- **TitelText** - Text der in der Headerzeile des Controls angezeigt wird.
- **Lines** - Eine Liste der Zeilen, die im Control angezeigt werden.

---

### CommentEdit  Eigenschaften

- **TitelText** - Text der in der Headerzeile des Controls angezeigt wird.
- **Comment** - Der Kommentar, der im Control angezeigt und bearbeitet werden kann.
- **SectionName** - Der Name des Abschnitts, dessen Kommentar bearbeitet wird.

### CommentEdit Events

- **CommentChanged** - Wird ausgelöst, wenn sich der Kommentar geändert hat.

---

### ListEdit Eigenschaften

- **TitelText** - Text der in der Headerzeile des Controls angezeigt wird.
- **ListItems** - Eine Liste der Einträge, die im Control angezeigt werden.
- **SelectedItem** - Der aktuell ausgewählte Eintrag im Control.

### ListEdit Events

- **ItemAdded** - Wird ausgelöst, wenn ein neuer Eintrag hinzugefügt wurde.
- **ItemRenamed** - Wird ausgelöst, wenn ein Eintrag umbenannt wurde.
- **ItemRemoved** - Wird ausgelöst wenn ein Eintrag gelöscht wurde.
- **SelectedItemChanged** - Wird ausgelöst, wenn sich der ausgewählte Eintrag geändert hat.

---

### EntryValueEdit Eigenschaften

- **SelectedSection** - Der Name des Abschnitts, dessen Eintrag bearbeitet wird.
- **SelectedEntry** - Der Name des Eintrags, dessen Wert bearbeitet wird.
- **NewValue** - Der neue Wert, der im Control eingegeben wurde.

