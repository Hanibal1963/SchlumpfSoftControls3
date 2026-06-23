' --------------------------------------------------------------------------------------------------------
' Datei: Form1.vb
' Author: Andreas Sauer
' Datum: 05.05.2026
' --------------------------------------------------------------------------------------------------------

Public Class FormMain

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        Me.InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        ' Titel der Form mit Anwendungsinformationen füllen
        Me.Text = $"{My.Application.Info.AssemblyName} V{My.Application.Info.Version} {My.Application.Info.Copyright}"

    End Sub

    Private Sub Button_SelectPath_Click(sender As Object, e As EventArgs)

        Dim selectedPath As String
        If Me.FolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            selectedPath = Me.FolderBrowserDialog.SelectedPath
        Else
            Exit Sub
        End If
        Me.Label_SelectedPath.Text = selectedPath
        Me.FileList.StartFolder = selectedPath

    End Sub

End Class
