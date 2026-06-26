' --------------------------------------------------------------------------------------------------------
' Datei: ListEditRenameEventArgs.vb
' Author: Andreas Sauer
' Datum: 29.05.2026
' --------------------------------------------------------------------------------------------------------

Namespace IniFileControl
    ''' <summary>
    ''' Stellt Ereignisdaten für das Umbenennen eines Listeneintrags bereit.
    ''' </summary>
    Public Class ListEditRenameEventArgs

        ' EventArgs-Vererbung ermöglicht die typsichere Übergabe der Umbenennungsdaten in Events.
        Inherits System.EventArgs

        ''' <summary>
        ''' Enthält den ursprünglichen Namen des Eintrags vor der Umbenennung.
        ''' </summary>
        Public Property OldName As System.String

        ''' <summary>
        ''' Enthält den neuen Namen des Eintrags nach der Umbenennung.
        ''' </summary>
        Public Property NewName As System.String

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="ListEditRenameEventArgs"/>-Klasse.
        ''' </summary>
        ''' <param name="OldName">Der bisherige Name des Eintrags.</param>
        ''' <param name="NewName">Der neue Name, der gesetzt werden soll.</param>
        Public Sub New(OldName As System.String, NewName As System.String)
            ' Der alte Name wird gespeichert, um in Event-Handlern Vergleiche,
            ' Protokollierung oder Rückabwicklungen zu ermöglichen.
            Me.OldName = OldName
            ' Der neue Name wird separat abgelegt, damit Handler direkt mit dem Zielwert
            ' weiterarbeiten können, ohne zusätzliche Konvertierung oder Nachschlagen.
            Me.NewName = NewName
        End Sub

    End Class

End Namespace
