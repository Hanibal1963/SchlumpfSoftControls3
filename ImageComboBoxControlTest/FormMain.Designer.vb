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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ImageComboBox1
        '
        Me.ImageComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.ImageComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ImageComboBox1.DropDownWidth = 121
        Me.ImageComboBox1.Elements.Add(CType(resources.GetObject("ImageComboBox1.Elements"), SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBoxItem))
        Me.ImageComboBox1.Elements.Add(CType(resources.GetObject("ImageComboBox1.Elements1"), SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBoxItem))
        Me.ImageComboBox1.Elements.Add(CType(resources.GetObject("ImageComboBox1.Elements2"), SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBoxItem))
        Me.ImageComboBox1.Elements.Add(CType(resources.GetObject("ImageComboBox1.Elements3"), SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBoxItem))
        Me.ImageComboBox1.Elements.Add(CType(resources.GetObject("ImageComboBox1.Elements4"), SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBoxItem))
        Me.ImageComboBox1.FormattingEnabled = True
        Me.ImageComboBox1.Location = New System.Drawing.Point(21, 12)
        Me.ImageComboBox1.Name = "ImageComboBox1"
        Me.ImageComboBox1.Size = New System.Drawing.Size(163, 21)
        Me.ImageComboBox1.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Location = New System.Drawing.Point(21, 39)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(460, 217)
        Me.TextBox1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(305, 275)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(176, 30)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Meldungen löschen"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(493, 317)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ImageComboBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormMain"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents ImageComboBox1 As SchlumpfSoft.Controls.ImageComboBoxControl.ImageComboBox
    Private WithEvents TextBox1 As TextBox
    Private WithEvents Button1 As Button
End Class
