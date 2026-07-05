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
    End Sub

End Class
