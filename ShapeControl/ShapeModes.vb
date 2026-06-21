' --------------------------------------------------------------------------------------------------------
' Datei: ShapeModes.vb
' Author: Andreas Sauer
' Datum: 05.05.2026
' --------------------------------------------------------------------------------------------------------

Namespace ShapeControl

    ''' <summary>
    ''' Legt fest, welche Form vom <see cref="Shape"/>-Steuerelement dargestellt wird.
    ''' </summary>
    Public Enum ShapeModes

        ''' <summary>
        ''' Zeichnet eine horizontale Linie in der vertikalen Mitte des Steuerelements.
        ''' </summary>
        HorizontalLine = 0 ' Linie verläuft von links nach rechts durch die Mitte

        ''' <summary>
        ''' Zeichnet eine vertikale Linie in der horizontalen Mitte des Steuerelements.
        ''' </summary>
        VerticalLine = 1 ' Linie verläuft von oben nach unten durch die Mitte

        ''' <summary>
        ''' Zeichnet eine diagonale Linie entsprechend dem Wert von <see cref="DiagonalLineModes"/>.
        ''' </summary>
        DiagonalLine = 2 ' Richtung wird über die Eigenschaft DiagonalLineModus festgelegt

        ''' <summary>
        ''' Zeichnet ein nicht gefülltes Rechteck.
        ''' </summary>
        Rectangle = 3 ' Nur der Rahmen wird gezeichnet

        ''' <summary>
        ''' Zeichnet ein Rechteck und füllt dessen Innenbereich.
        ''' </summary>
        FilledRectangle = 4 ' Rahmen und Füllfläche werden gezeichnet

        ''' <summary>
        ''' Zeichnet eine nicht gefüllte Ellipse (bei gleichen Seitenlängen ein Kreis).
        ''' </summary>
        Ellipse = 5 ' Nur die Ellipsenkontur wird gezeichnet

        ''' <summary>
        ''' Zeichnet eine Ellipse und füllt deren Innenbereich (bei gleichen Seitenlängen ein Kreis).
        ''' </summary>
        FilledEllipse = 6 ' Ellipsenkontur und Füllfläche werden gezeichnet

    End Enum

End Namespace
