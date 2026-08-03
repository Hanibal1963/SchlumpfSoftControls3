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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Password1 = New SchlumpfSoft.Controls.PasswordControl.Password()
        Me.Password2 = New SchlumpfSoft.Controls.PasswordControl.Password()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Password1)
        Me.Panel1.Controls.Add(Me.Password2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(455, 295)
        Me.Panel1.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(23, 212)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(403, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 182)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(137, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Bitte Passwort wiederholen:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 148)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Bitte Passwort eingeben:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(327, 245)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(99, 29)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Weiter >>"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Password1
        '
        Me.Password1.Location = New System.Drawing.Point(163, 147)
        Me.Password1.Margin = New System.Windows.Forms.Padding(0)
        Me.Password1.MinimumSize = New System.Drawing.Size(100, 20)
        Me.Password1.Name = "Password1"
        Me.Password1.Size = New System.Drawing.Size(263, 20)
        Me.Password1.TabIndex = 0
        '
        'Password2
        '
        Me.Password2.Location = New System.Drawing.Point(163, 182)
        Me.Password2.Margin = New System.Windows.Forms.Padding(0)
        Me.Password2.MinimumSize = New System.Drawing.Size(100, 20)
        Me.Password2.Name = "Password2"
        Me.Password2.Size = New System.Drawing.Size(263, 20)
        Me.Password2.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(23, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(403, 84)
        Me.Label4.TabIndex = 6
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 295)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents Password1 As SchlumpfSoft.Controls.PasswordControl.Password
    Private WithEvents Password2 As SchlumpfSoft.Controls.PasswordControl.Password
    Private WithEvents Panel1 As Panel
    Private WithEvents Button1 As Button
    Private WithEvents Label1 As Label
    Private WithEvents Label2 As Label
    Private WithEvents Label3 As Label
    Private WithEvents Label4 As Label
End Class
