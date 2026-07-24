<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Password1 = New SchlumpfSoft.Controls.PasswordControl.Password()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Password1
        '
        Me.Password1.Location = New System.Drawing.Point(25, 19)
        Me.Password1.Margin = New System.Windows.Forms.Padding(0)
        Me.Password1.MinimumSize = New System.Drawing.Size(100, 20)
        Me.Password1.Name = "Password1"
        Me.Password1.Size = New System.Drawing.Size(166, 20)
        Me.Password1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(223, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(298, 84)
        Me.Label1.TabIndex = 1
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 290)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Password1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Password1 As SchlumpfSoft.Controls.PasswordControl.Password
    Private WithEvents Label1 As Label
End Class
