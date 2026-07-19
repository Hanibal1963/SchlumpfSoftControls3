<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ImageComboBox
    Inherits System.Windows.Forms.UserControl

    ' UserControl überschreibt Dispose, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    ' Erforderlich für den Windows-Form-Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: Die folgende Prozedur ist für den Windows-Form-Designer erforderlich.
    ' Sie kann mit dem Windows-Form-Designer geändert werden.  
    ' Nicht mit dem Code-Editor ändern.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'ImageComboBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "ImageComboBox"
        Me.ResumeLayout(False)

    End Sub

End Class
