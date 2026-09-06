' --------------------------------------------------------------------------------------------------------
' Datei: FormMain.vb
' Author: Andreas Sauer
' Datum: 06.09.2026
' --------------------------------------------------------------------------------------------------------

''' <summary>
''' Hauptformular der Demoanwendung.
''' </summary>
Public Class FormMain

	''' <summary>
	''' Initialisiert eine neue Instanz von <see cref="FormMain"/>.
	''' </summary>
	Public Sub New()
		Me.InitializeComponent()
		Me.Text = $"{My.Application.Info.ProductName} - {My.Application.Info.Version}"
		Me.SetHandles()
	End Sub

	Private Sub SetHandles()
		' Handler korrekt in VB.NET registrieren
		Dim btn As Button
		For Each btn In Me.FlowLayoutPanel.Controls.OfType(Of Button)()
			AddHandler btn.Click, AddressOf Me.Button_Click
		Next
	End Sub

	Private Sub Button_Click(sender As Object, e As EventArgs)
		' Klickverarbeitung hier implementieren
		Dim btn As Button = CType(sender, Button)
#If DEBUG Then
		Dim unused = MessageBox.Show($"Button ""{btn.Name}"" wurde geklickt.")
#End If
		Select Case True
			Case btn Is Me.BtnNotifyForm : My.Forms.FormNotifyForm.ShowDialog(Me)
			Case btn Is Me.BtnSevenSegment : My.Forms.FormSevenSegment.ShowDialog(Me)
		End Select
	End Sub

End Class
