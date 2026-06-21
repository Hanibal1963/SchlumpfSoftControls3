# Shape Control

Steuerelement zum Darstellen einer Linie, eines Rechtecks oder einer Ellipse.

`ShapeControl` ist ein leichtgewichtiges WinForms-Control zur Darstellung einfacher Vektorformen ohne Abhängigkeit zu GDI+ High-Level Wrappern oder externen Bibliotheken. Es eignet sich für UI-Trennlinien, einfache Markierungen, Status-Indikatoren oder visuelle Gruppierungen.

## Eigenschaften

- **ShapeModus** - Legt den Modus des Shape Controls fest. Mögliche Werte sind:
  - **HorizontalLine** - Eine horizontale Linie, die über die gesamte Breite des Controls verläuft.
  - **VerticalLine** - Eine vertikale Linie, die über die gesamte Höhe des Controls verläuft.
  - **DiagonalLine** - Eine diagonale Linie, die von eine Ecke zur gegenüberliegenden Ecke verläuft.
  - **Rectangle** - Ein Rechteck, das die gesamte Fläche des Controls ausfüllt.
  - **FilledRectangle** - Ein gefülltes Rechteck, das die gesamte Fläche des Controls ausfüllt.
  - **Ellipse** - Eine Ellipse, die die gesamte Fläche des Controls ausfüllt.
  - **FilledEllipse** - Eine gefüllte Ellipse, die die gesamte Fläche des Controls ausfüllt.
- **DiagonallineModus** - Legt den Modus für die Diagonallinien fest. Mögliche Werte sind:
  - **TopLeftToBottomRight** - Die Diagonallinie verläuft von der oberen linken Ecke zur unteren rechten Ecke.
  - **BottomLeftToTopRight** - Die Diagonallinie verläuft von der unteren linken Ecke zur rechten oberen Ecke.
- **LineWidth** - Legt die Breite der Linien fest.
- **LineColor** - Legt die Farbe der Linien fest.
- **FillColor** - Legt die Füllfarbe des Shapes fest.

## Methoden

## Ereignisse
