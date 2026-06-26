<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
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
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.DateiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemOeffnen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemSchliessen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemBeenden = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemSpeichern = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemSpeichernUnter = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntryValueEdit = New SchlumpfSoft.Controls.IniFileControl.EntryValueEdit()
        Me.CommentEditSections = New SchlumpfSoft.Controls.IniFileControl.CommentEdit()
        Me.CommentEditFileComment = New SchlumpfSoft.Controls.IniFileControl.CommentEdit()
        Me.ContentView = New SchlumpfSoft.Controls.IniFileControl.ContentView()
        Me.ListEditEntrys = New SchlumpfSoft.Controls.IniFileControl.ListEdit()
        Me.ListEditSections = New SchlumpfSoft.Controls.IniFileControl.ListEdit()
        Me.IniFile = New SchlumpfSoft.Controls.IniFileControl.IniFile()
        Me.MenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DateiToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(650, 24)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Text = "MenuStrip"
        '
        'DateiToolStripMenuItem
        '
        Me.DateiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemOeffnen, Me.ToolStripMenuItemSchliessen, Me.ToolStripMenuItemBeenden, Me.ToolStripMenuItemSpeichern, Me.ToolStripMenuItemSpeichernUnter})
        Me.DateiToolStripMenuItem.Name = "DateiToolStripMenuItem"
        Me.DateiToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.DateiToolStripMenuItem.Text = "Datei"
        '
        'ToolStripMenuItemOeffnen
        '
        Me.ToolStripMenuItemOeffnen.Name = "ToolStripMenuItemOeffnen"
        Me.ToolStripMenuItemOeffnen.Size = New System.Drawing.Size(168, 22)
        Me.ToolStripMenuItemOeffnen.Text = "öffnen ..."
        '
        'ToolStripMenuItemSchliessen
        '
        Me.ToolStripMenuItemSchliessen.Name = "ToolStripMenuItemSchliessen"
        Me.ToolStripMenuItemSchliessen.Size = New System.Drawing.Size(168, 22)
        Me.ToolStripMenuItemSchliessen.Text = "schliessen"
        '
        'ToolStripMenuItemBeenden
        '
        Me.ToolStripMenuItemBeenden.Name = "ToolStripMenuItemBeenden"
        Me.ToolStripMenuItemBeenden.Size = New System.Drawing.Size(168, 22)
        Me.ToolStripMenuItemBeenden.Text = "beenden"
        '
        'ToolStripMenuItemSpeichern
        '
        Me.ToolStripMenuItemSpeichern.Name = "ToolStripMenuItemSpeichern"
        Me.ToolStripMenuItemSpeichern.Size = New System.Drawing.Size(168, 22)
        Me.ToolStripMenuItemSpeichern.Text = "speichern"
        '
        'ToolStripMenuItemSpeichernUnter
        '
        Me.ToolStripMenuItemSpeichernUnter.Name = "ToolStripMenuItemSpeichernUnter"
        Me.ToolStripMenuItemSpeichernUnter.Size = New System.Drawing.Size(168, 22)
        Me.ToolStripMenuItemSpeichernUnter.Text = "speichern unter ..."
        '
        'EntryValueEdit
        '
        Me.EntryValueEdit.Location = New System.Drawing.Point(306, 454)
        Me.EntryValueEdit.Name = "EntryValueEdit"
        Me.EntryValueEdit.SelectedEntry = Nothing
        Me.EntryValueEdit.SelectedSection = ""
        Me.EntryValueEdit.Size = New System.Drawing.Size(332, 79)
        Me.EntryValueEdit.TabIndex = 7
        Me.EntryValueEdit.TitelText = "Eintragswert bearbeiten"
        Me.EntryValueEdit.Value = ""
        '
        'CommentEditSections
        '
        Me.CommentEditSections.Comment = New String() {""}
        Me.CommentEditSections.Location = New System.Drawing.Point(306, 194)
        Me.CommentEditSections.Name = "CommentEditSections"
        Me.CommentEditSections.SectionName = Nothing
        Me.CommentEditSections.Size = New System.Drawing.Size(332, 107)
        Me.CommentEditSections.TabIndex = 6
        Me.CommentEditSections.TitelText = "Abschnittskommentar bearbeiten"
        '
        'CommentEditFileComment
        '
        Me.CommentEditFileComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CommentEditFileComment.Comment = New String() {""}
        Me.CommentEditFileComment.Location = New System.Drawing.Point(12, 368)
        Me.CommentEditFileComment.Name = "CommentEditFileComment"
        Me.CommentEditFileComment.SectionName = Nothing
        Me.CommentEditFileComment.Size = New System.Drawing.Size(288, 165)
        Me.CommentEditFileComment.TabIndex = 1
        Me.CommentEditFileComment.TitelText = "Dateikommentar bearbeiten"
        '
        'ContentView
        '
        Me.ContentView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ContentView.Lines = Nothing
        Me.ContentView.Location = New System.Drawing.Point(12, 38)
        Me.ContentView.Name = "ContentView"
        Me.ContentView.Size = New System.Drawing.Size(288, 324)
        Me.ContentView.TabIndex = 0
        Me.ContentView.TitelText = "Dateiinhalt"
        '
        'ListEditEntrys
        '
        Me.ListEditEntrys.ListItems = New String() {""}
        Me.ListEditEntrys.Location = New System.Drawing.Point(306, 307)
        Me.ListEditEntrys.Name = "ListEditEntrys"
        Me.ListEditEntrys.Size = New System.Drawing.Size(332, 141)
        Me.ListEditEntrys.TabIndex = 3
        Me.ListEditEntrys.TitelText = "Einträge bearbeiten"
        '
        'ListEditSections
        '
        Me.ListEditSections.AutoSize = True
        Me.ListEditSections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListEditSections.ListItems = New String() {""}
        Me.ListEditSections.Location = New System.Drawing.Point(306, 38)
        Me.ListEditSections.Name = "ListEditSections"
        Me.ListEditSections.Size = New System.Drawing.Size(332, 150)
        Me.ListEditSections.TabIndex = 2
        Me.ListEditSections.TitelText = "Abschnitte bearbeiten"
        '
        'IniFile
        '
        Me.IniFile.AutoSave = False
        Me.IniFile.CommentPrefix = Global.Microsoft.VisualBasic.ChrW(59)
        Me.IniFile.FileName = "neue Datei.ini"
        Me.IniFile.FilePath = ""
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 542)
        Me.Controls.Add(Me.EntryValueEdit)
        Me.Controls.Add(Me.CommentEditSections)
        Me.Controls.Add(Me.CommentEditFileComment)
        Me.Controls.Add(Me.ContentView)
        Me.Controls.Add(Me.ListEditEntrys)
        Me.Controls.Add(Me.ListEditSections)
        Me.Controls.Add(Me.MenuStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MainMenuStrip = Me.MenuStrip
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents IniFile As SchlumpfSoft.Controls.IniFileControl.IniFile
    Private WithEvents ContentView As SchlumpfSoft.Controls.IniFileControl.ContentView
    Private WithEvents CommentEditFileComment As SchlumpfSoft.Controls.IniFileControl.CommentEdit
    Private WithEvents ListEditSections As SchlumpfSoft.Controls.IniFileControl.ListEdit
    Private WithEvents ListEditEntrys As SchlumpfSoft.Controls.IniFileControl.ListEdit
    Private WithEvents MenuStrip As MenuStrip
    Friend WithEvents DateiToolStripMenuItem As ToolStripMenuItem
    Private WithEvents ToolStripMenuItemOeffnen As ToolStripMenuItem
    Private WithEvents ToolStripMenuItemSchliessen As ToolStripMenuItem
    Private WithEvents ToolStripMenuItemBeenden As ToolStripMenuItem
    Private WithEvents ToolStripMenuItemSpeichern As ToolStripMenuItem
    Private WithEvents ToolStripMenuItemSpeichernUnter As ToolStripMenuItem
    Private WithEvents CommentEditSections As SchlumpfSoft.Controls.IniFileControl.CommentEdit
    Private WithEvents EntryValueEdit As SchlumpfSoft.Controls.IniFileControl.EntryValueEdit
End Class
