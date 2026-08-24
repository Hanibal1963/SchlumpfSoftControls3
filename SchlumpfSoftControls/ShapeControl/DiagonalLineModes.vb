' --------------------------------------------------------------------------------------------------------
' Datei: DiagonalLineModes.vb
' Author: Andreas Sauer
' Datum: 05.05.2026
' --------------------------------------------------------------------------------------------------------

Namespace ShapeControl

    ''' <summary>
    ''' Legt fest, in welcher Richtung eine diagonale Linie gezeichnet wird.
    ''' </summary>
    Public Enum DiagonalLineModes

        ''' <summary>
        ''' Zeichnet die diagonale Linie von links oben nach rechts unten.
        ''' </summary>
        TopLeftToBottomRight = 0 ' Startpunkt: obere linke Ecke, Endpunkt: untere rechte Ecke

        ''' <summary>
        ''' Zeichnet die diagonale Linie von links unten nach rechts oben.
        ''' </summary>
        BottomLeftToTopRight = 1 ' Startpunkt: untere linke Ecke, Endpunkt: obere rechte Ecke

    End Enum

End Namespace
