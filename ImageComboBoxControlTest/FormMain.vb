' --------------------------------------------------------------------------------------------------------
' Datei: FormMain.vb
' Author: Andreas Sauer
' Datum: 05.07.2026
' --------------------------------------------------------------------------------------------------------

Public Class FormMain

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        Me.InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.Text = $"{My.Application.Info.AssemblyName} V{My.Application.Info.Version} {My.Application.Info.Copyright}"
        Me.ImageComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.TextBox1.Clear()
    End Sub

    Private Sub ImageComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ImageComboBox1.SelectedIndexChanged
        Dim index As Int32 = CType(sender, SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBox).SelectedIndex
        Dim message As String = $"SelectedIndexChanged meldet: der Index hat sich zu ""{index}"" geändert."
        Me.PrintMessage(message)
    End Sub

    Private Sub ImageComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ImageComboBox1.TextChanged
        Dim text As String = CType(sender, SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBox).Text
        Dim message As String = $"TextChanged meldet: Der Text hat sich zu ""{text}"" geändert."
        Me.PrintMessage(message)
    End Sub

    Private Sub PrintMessage(message As String)
        Me.TextBox1.AppendText($"{message}{Environment.NewLine}")
    End Sub

End Class
