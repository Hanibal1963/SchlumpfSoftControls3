' --------------------------------------------------------------------------------------------------------
' Datei: FormMain.vb
' Author: Andreas Sauer
' Datum: 24.07.2026
' --------------------------------------------------------------------------------------------------------

Imports SchlumpfSoft.Controls.PasswordControl

Public Class FormMain

    Private pwspeicher As String = $""

    Public Sub New()
        Me.InitializeComponent()
        Me.Text = $"{My.Application.Info.AssemblyName} V{My.Application.Info.Version} {My.Application.Info.Copyright}"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        With Me.Panel1
            .Location = New Point(0 + Me.Width, 0)
            .Dock = DockStyle.None
        End With

        With Me.Panel2
            .Location = New Point(0, 0)
            .Dock = DockStyle.Fill
        End With

        Me.Button1.Enabled = False

    End Sub

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        With Me.Panel1
            .Location = New Point(0, 0)
            .Dock = DockStyle.Fill
        End With

        With Me.Panel2
            .Location = New Point(0 + Me.Width, 0)
            .Dock = DockStyle.None
        End With

        Me.Password2.Enabled = False
        Me.Label2.Enabled = False
        Me.Button1.Enabled = False

    End Sub

    Private Sub Password1_PasswortChanged(sender As Object, e As PasswordChangedEventArgs) Handles Password1.PasswortChanged

        Dim pwcode As String = e.PasswordCode
        Me.pwspeicher = pwcode
        Me.Password2.Enabled = True
        Me.Label2.Enabled = True

    End Sub

    Private Sub Password2_PasswortChanged(sender As Object, e As PasswordChangedEventArgs) Handles Password2.PasswortChanged

        If Me.Password2.VerifyPasswordCode(Me.pwspeicher) = True Then

            Me.Label3.Text = $"Das Passwort korrekt"
            Me.Label3.BackColor = Color.Green
            Me.Button1.Enabled = True

        Else

            Me.Label3.Text = $"Das Passwort ist inkorrekt"
            Me.Label3.BackColor = Color.Red
            Me.Button1.Enabled = False

        End If

    End Sub

End Class
