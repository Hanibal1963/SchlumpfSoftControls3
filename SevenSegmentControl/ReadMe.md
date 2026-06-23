# SevenSegment Control

Diese Bibliothek enthält zwei Controls:

- **SevenSegmentSingleDigit:** Ein Control, das eine einzelne Ziffer von 0 bis 9 anzeigt.
- **MultiDigitSevenSegment:** Ein Control, das mehrere Ziffern anzeigt und damit ganze Zahlen darstellen kann.

Ich habe für ein anderes Projekt versucht eine 7-Segmentanzeige zu programmieren.

Nach einigen Fehlversuchen und einer intensive Internetrecherche, bin ich auf GitHub fündig geworden.
([SevenSegment von Dimitry Brant](https://github.com/dbrant/SevenSegment))

Ich habe mich entschlossen den Code in VisualBasic neu zu erstellen da ich mit C# keinerlei Erfahrung habe.

## Eigenschaften

- **InactiveColor** - Die Farbe der inaktiven Segmente.
- **ForeColor** - Die Farbe der aktiven Segmente.
- **Segmentwidth** - Die Breite der Segmente.
- **ItalicFactor** - Ein Faktor, der die Neigung der Segmente steuert.
- **ShowDecimalPoint** - Ein Flag, das steuert, ob der Dezimalpunkt angezeigt wird.
- **DecimalPointActive** - Ein Flag, das steuert, ob der Dezimalpunkt aktiv ist (leuchtet).
- **ShowColon** - Ein Flag, das steuert, ob der Doppelpunkt angezeigt wird (nur für MultiDigit).
- **ColonActive** - Ein Flag, das steuert, ob der Doppelpunkt aktiv ist (leuchtet, nur für MultiDigit).
- **DigitValue** - Der Wert der angezeigten Ziffer (0-9 für SingleDigit, ganze Zahl für MultiDigit).
- **DigitalCount** - Die Anzahl der angezeigten Ziffern (nur für MultiDigit).
- **CustomBitPattern** - Ein benutzerdefiniertes Bitmuster, um die Segmente individuell zu steuern.

## Methoden

## Ereignisse
