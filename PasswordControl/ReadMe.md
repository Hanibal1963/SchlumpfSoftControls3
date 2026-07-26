# PasswordControl

Ein Control zum Eingeben und Validieren von Passwörtern.

## Eigenschaften

- `PasswortHash`  
  Gibt den zuletzt erzeugten Passwort-Hash zurück.

## Methoden

- `New()`  
  Initialisiert eine neue Instanz des `Password`-Steuerelements.

## Ereignisse

- `PasswortHashChanged`  
  Tritt ein, wenn aus dem eingegebenen Passwort ein neuer Hashwert erzeugt wurde.

  - `Hash` (`PasswordHashChangedEventArgs`)  
    Gibt den erzeugten Hashwert des Passworts zurück.
