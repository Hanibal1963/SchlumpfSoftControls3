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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.DriveWatcher = New SchlumpfSoft.Controls.DriveWatcherControl.DriveWatcher(Me.components)
        Me.Label_Info = New System.Windows.Forms.Label()
        Me.TextBox_DriveInfo = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label_Info
        '
        Me.Label_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Info.Image = Global.DriveWatcherControlTest.My.Resources.Resources.Information
        Me.Label_Info.Location = New System.Drawing.Point(12, 20)
        Me.Label_Info.Name = "Label_Info"
        Me.Label_Info.Size = New System.Drawing.Size(461, 131)
        Me.Label_Info.TabIndex = 0
        Me.Label_Info.Text = resources.GetString("Label_Info.Text")
        '
        'TextBox_DriveInfo
        '
        Me.TextBox_DriveInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox_DriveInfo.Location = New System.Drawing.Point(12, 166)
        Me.TextBox_DriveInfo.Multiline = True
        Me.TextBox_DriveInfo.Name = "TextBox_DriveInfo"
        Me.TextBox_DriveInfo.ReadOnly = True
        Me.TextBox_DriveInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_DriveInfo.Size = New System.Drawing.Size(461, 166)
        Me.TextBox_DriveInfo.TabIndex = 1
        Me.TextBox_DriveInfo.WordWrap = False
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 342)
        Me.Controls.Add(Me.TextBox_DriveInfo)
        Me.Controls.Add(Me.Label_Info)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents DriveWatcher As SchlumpfSoft.Controls.DriveWatcherControl.DriveWatcher
    Private WithEvents Label_Info As Label
    Private WithEvents TextBox_DriveInfo As TextBox
End Class
