' --------------------------------------------------------------------------------------------------------
' Datei: Form1.vb
' Author: Andreas Sauer
' Datum: 30.04.2026
' --------------------------------------------------------------------------------------------------------

Public Class FormMain

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        Me.InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.Text = $"{My.Application.Info.AssemblyName} V{My.Application.Info.Version} {My.Application.Info.Copyright}"
        Me.ColorProgressBar.Value = CInt(Me.NumericUpDown_ProgressValue.Value)
        Me.ColorProgressBar.ProgressMaximumValue = CInt(Me.NumericUpDown_ProgressValue.Maximum)
        Me.CheckBox_ShowGliss.Checked = Me.ColorProgressBar.IsGlossy
        Me.CheckBox_ShowBorder.Checked = Me.ColorProgressBar.ShowBorder
    End Sub

    Private Sub CheckBox_ShowGliss_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ShowGliss.CheckedChanged

        Me.ColorProgressBar.IsGlossy = CType(sender, CheckBox).Checked

    End Sub

    Private Sub CheckBox_ShowBorder_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ShowBorder.CheckedChanged

        Me.ColorProgressBar.ShowBorder = CType(sender, CheckBox).Checked

    End Sub

    Private Sub NumericUpDown_ProgressValue_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_ProgressValue.ValueChanged

        Me.ColorProgressBar.Value = CInt(CType(sender, NumericUpDown).Value)

    End Sub

    Private Sub Button_BarColorChange_Click(sender As Object, e As EventArgs) Handles Button_BarColorChange.Click

        Me.ColorDialog.Color = Me.ColorProgressBar.BarColor
        If Me.ColorDialog.ShowDialog(Me) = DialogResult.OK Then
            Me.ColorProgressBar.BarColor = Me.ColorDialog.Color
        End If

    End Sub

    Private Sub Button_BorderColorChange_Click(sender As Object, e As EventArgs) Handles Button_BorderColorChange.Click

        Me.ColorDialog.Color = Me.ColorProgressBar.BorderColor
        If Me.ColorDialog.ShowDialog(Me) = DialogResult.OK Then
            Me.ColorProgressBar.BorderColor = Me.ColorDialog.Color
        End If

    End Sub

    Private Sub Button_EmptyColorChange_Click(sender As Object, e As EventArgs) Handles Button_EmptyColorChange.Click

        Me.ColorDialog.Color = Me.ColorProgressBar.EmptyColor
        If Me.ColorDialog.ShowDialog(Me) = DialogResult.OK Then
            Me.ColorProgressBar.EmptyColor = Me.ColorDialog.Color
        End If

    End Sub

    Private Sub ColorProgressBar_Click(sender As Object, e As EventArgs) Handles ColorProgressBar.Click

#If DEBUG Then
        Debug.Print($"Es wurde auf {CType(sender, SchlumpfSoft.Controls.ColorProgressBarControl.ColorProgressBar).Name} geklickt.")
#End If

    End Sub

End Class
