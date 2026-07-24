' --------------------------------------------------------------------------------------------------------
' Datei: PasswordHashChangedEventArgs.vb
' Author: Andreas Sauer
' Datum: 24.07.2026
' --------------------------------------------------------------------------------------------------------

Namespace PasswordControl

    Public Class PasswordHashChangedEventArgs

        Inherits System.EventArgs

        Public ReadOnly Property Hash As String

        Public Sub New(Hash As String)
            Me.Hash = Hash
        End Sub

    End Class

End Namespace
