' --------------------------------------------------------------------------------------------------------
' Datei: FormMain.vb
' Author: Andreas Sauer
' Datum: 24.07.2026
' --------------------------------------------------------------------------------------------------------

Imports SchlumpfSoft.Controls.PasswordControl

Public Class Form1

    Private pwspeicher As String = $""

    Public Sub New()
        Me.InitializeComponent()
        Me.Text = $"{My.Application.Info.AssemblyName} V{My.Application.Info.Version} {My.Application.Info.Copyright}"
        Me.Label4.Text = My.Resources.Text1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Password2.Enabled = False
        Me.Label2.Enabled = False
        Me.Button1.Enabled = False
    End Sub

    Private Sub Password1_PasswortChanged(sender As Object, e As PasswordChangedEventArgs) Handles Password1.PasswortChanged
        Me.pwspeicher = e.PasswordCode
        Me.Password2.Enabled = True
        Me.Label2.Enabled = True
    End Sub

    Private Sub Password2_PasswortChanged(sender As Object, e As PasswordChangedEventArgs) Handles Password2.PasswortChanged
        If Me.Password2.VerifyPasswordCode(Me.pwspeicher) = True Then
            Me.Label3.Text = $"Das Passwort korrekt und wird gespeichert."
            Me.Label3.BackColor = Color.Green
            My.Settings.PasswordCode = Me.pwspeicher
            My.Settings.Save()
            Me.Button1.Enabled = True
        Else
            Me.Label3.Text = $"Das Passwort ist inkorrekt"
            Me.Label3.BackColor = Color.Red
            Me.Button1.Enabled = False
        End If
    End Sub

End Class
