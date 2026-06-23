' --------------------------------------------------------------------------------------------------------
' Datei: Form1.vb
' Author: Andreas Sauer
' Datum: 06.05.2026
' --------------------------------------------------------------------------------------------------------

Public Class FormMain

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        Me.InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        ' Titel der Form mit Anwendungsinformationen füllen
        Me.Text = $"{My.Application.Info.AssemblyName} V{My.Application.Info.Version} {My.Application.Info.Copyright}"

    End Sub

    Private Sub TextBox_SingleDigit_TextChanged(sender As Object, e As EventArgs)
        Me.SingleDigit.DigitValue = CType(sender, TextBox).Text
    End Sub

    Private Sub TextBox_MultiDigit_TextChanged(sender As Object, e As EventArgs)
        Me.MultiDigit.Value = CType(sender, TextBox).Text
    End Sub

    Private Sub Button_InactiveColor_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button_ForeColor_Click(sender As Object, e As EventArgs)

    End Sub

End Class
