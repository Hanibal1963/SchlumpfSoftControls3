' --------------------------------------------------------------------------------------------------------
' Datei: Form2.vb
' Author: Andreas Sauer
' Datum: 03.08.2026
' --------------------------------------------------------------------------------------------------------

Imports SchlumpfSoft.Controls.PasswordControl

Public Class Form2

    Private pwspeicher As String = $""

    Public Sub New()
        Me.InitializeComponent()
        Me.Text = $"{My.Application.Info.AssemblyName} V{My.Application.Info.Version} {My.Application.Info.Copyright}"
        Me.Label1.Text = My.Resources.Text2
    End Sub

    Private Sub Password1_PasswortChanged(sender As Object, e As PasswordChangedEventArgs) Handles Password1.PasswortChanged
        If Me.Password1.VerifyPasswordCode(My.Settings.PasswordCode) = True Then
            Me.Label2.Text = $"Das passwort ist korrekt."
            Me.Label2.BackColor = Color.Green
        Else
            Me.Label2.Text = $"Das passwort ist nicht korrekt."
            Me.Label2.BackColor = Color.Red
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        My.Settings.PasswordCode = String.Empty
        My.Settings.Save()
        Me.Close()
    End Sub

End Class