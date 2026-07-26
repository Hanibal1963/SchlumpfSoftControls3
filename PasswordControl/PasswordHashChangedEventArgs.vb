' --------------------------------------------------------------------------------------------------------
' Datei: PasswordHashChangedEventArgs.vb
' Author: Andreas Sauer
' Datum: 24.07.2026
' --------------------------------------------------------------------------------------------------------

Namespace PasswordControl

    ''' <summary>
    ''' Stellt Daten für das Ereignis bei Änderung des Passwort-Hashs bereit.
    ''' </summary>
    Public Class PasswordHashChangedEventArgs

        Inherits System.EventArgs

        ''' <summary>
        ''' Gibt den erzeugten Hashwert des Passworts zurück.
        ''' </summary>
        Public ReadOnly Property Hash As String

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="PasswordHashChangedEventArgs" />-Klasse.
        ''' </summary>
        ''' <param name="Hash">Der erzeugte Hashwert des Passworts.</param>
        Public Sub New(Hash As String)
            ' Speichert den übergebenen Hashwert für den späteren Zugriff im Ereignishandler.
            Me.Hash = Hash
        End Sub

    End Class

End Namespace
