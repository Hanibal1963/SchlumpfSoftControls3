' --------------------------------------------------------------------------------------------------------
' Datei: Form1.vb
' Author: Andreas Sauer
' Datum: 24.07.2026
' --------------------------------------------------------------------------------------------------------

Imports SchlumpfSoft.Controls

Public Class Form1

    Private Sub Password1_PasswortHashChanged(sender As Object, e As PasswordControl.PasswordHashChangedEventArgs) Handles Password1.PasswortHashChanged
        Me.Label1.Text = $"Erzeugter Hashwert = {e.Hash}"
    End Sub

End Class
