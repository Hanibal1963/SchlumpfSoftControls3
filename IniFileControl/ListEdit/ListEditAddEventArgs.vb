' --------------------------------------------------------------------------------------------------------
' Datei: ListEditAddEventArgs.vb
' Author: Andreas Sauer
' Datum: 29.05.2026
' --------------------------------------------------------------------------------------------------------

Namespace IniFileControl

    ''' <summary>
    ''' Stellt Ereignisdaten für das Hinzufügen eines neuen Listeneintrags bereit.
    ''' </summary>
    Public Class ListEditAddEventArgs

        ' Die Klasse erbt von EventArgs, damit sie direkt in .NET-Events verwendet werden kann.
        Inherits System.EventArgs

        ''' <summary>
        ''' Enthält den neu hinzugefügten Eintrag.
        ''' </summary>
        Public Property ItemToAdd As System.String

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="ListEditAddEventArgs"/>-Klasse.
        ''' </summary>
        ''' <param name="ItemToAdd">Der Textwert des neu angelegten Eintrags.</param>
        Public Sub New(ItemToAdd As System.String)
            ' Der vom auslösenden Steuerelement übergebene Wert wird in der Property gespeichert,
            ' damit Event-Handler den neuen Eintrag direkt weiterverarbeiten können.
            Me.ItemToAdd = ItemToAdd
        End Sub

    End Class

End Namespace
