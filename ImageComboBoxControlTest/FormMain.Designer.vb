<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.ImageComboBox1 = New SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBox()
        Me.SuspendLayout()
        '
        'ImageComboBox1
        '
        Me.ImageComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.ImageComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ImageComboBox1.DropDownWidth = 163
        Me.ImageComboBox1.Elements.Add(CType(resources.GetObject("ImageComboBox1.Elements"), SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBoxItem))
        Me.ImageComboBox1.Elements.Add(CType(resources.GetObject("ImageComboBox1.Elements1"), SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBoxItem))
        Me.ImageComboBox1.Elements.Add(CType(resources.GetObject("ImageComboBox1.Elements2"), SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBoxItem))
        Me.ImageComboBox1.Elements.Add(CType(resources.GetObject("ImageComboBox1.Elements3"), SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBoxItem))
        Me.ImageComboBox1.Elements.Add(CType(resources.GetObject("ImageComboBox1.Elements4"), SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBoxItem))
        Me.ImageComboBox1.FormattingEnabled = True
        Me.ImageComboBox1.Location = New System.Drawing.Point(30, 24)
        Me.ImageComboBox1.Name = "ImageComboBox1"
        Me.ImageComboBox1.Size = New System.Drawing.Size(163, 21)
        Me.ImageComboBox1.TabIndex = 0
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(493, 268)
        Me.Controls.Add(Me.ImageComboBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormMain"
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents ImageComboBox1 As SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBox
End Class
