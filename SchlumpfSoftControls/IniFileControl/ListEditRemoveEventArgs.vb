' --------------------------------------------------------------------------------------------------------
' Datei: ListEditRemoveEventArgs.vb
' Author: Andreas Sauer
' Datum: 29.05.2026
' --------------------------------------------------------------------------------------------------------

Namespace IniFileControl

    ''' <summary>
    ''' Stellt Ereignisdaten für das Entfernen eines vorhandenen Listeneintrags bereit.
    ''' </summary>
    Public Class ListEditRemoveEventArgs

        ' Durch die Vererbung von EventArgs kann die Klasse als typisierte Event-Nutzlast dienen.
        Inherits System.EventArgs

        ''' <summary>
        ''' Enthält den Eintrag, der aus der Liste entfernt werden soll oder bereits entfernt wurde.
        ''' </summary>
        Public Property ItemToRemove As System.String

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="ListEditRemoveEventArgs"/>-Klasse.
        ''' </summary>
        ''' <param name="ItemToRemove">Der eindeutige Name oder Text des zu entfernenden Eintrags.</param>
        Public Sub New(ItemToRemove As System.String)
            ' Der übergebene Wert wird unverändert übernommen, damit Event-Handler
            ' exakt den vom Aufrufer bestimmten Eintrag referenzieren können.
            Me.ItemToRemove = ItemToRemove
        End Sub

    End Class

End Namespace
