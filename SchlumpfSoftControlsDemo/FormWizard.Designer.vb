<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormWizard
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
        Me.Wizard = New SchlumpfSoft.Controls.WizardControl.Wizard()
        Me.PageCustom = New SchlumpfSoft.Controls.WizardControl.PageCustom()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PageFinish = New SchlumpfSoft.Controls.WizardControl.PageFinish()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PageStandard2 = New SchlumpfSoft.Controls.WizardControl.PageStandard()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PageStandard1 = New SchlumpfSoft.Controls.WizardControl.PageStandard()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PageWelcome = New SchlumpfSoft.Controls.WizardControl.PageWelcome()
        Me.Label0 = New System.Windows.Forms.Label()
        Me.Wizard.SuspendLayout()
        Me.PageCustom.SuspendLayout()
        Me.PageFinish.SuspendLayout()
        Me.PageStandard2.SuspendLayout()
        Me.PageStandard1.SuspendLayout()
        Me.PageWelcome.SuspendLayout()
        Me.SuspendLayout()
        '
        'Wizard
        '
        Me.Wizard.Controls.Add(Me.PageWelcome)
        Me.Wizard.Controls.Add(Me.PageStandard1)
        Me.Wizard.Controls.Add(Me.PageFinish)
        Me.Wizard.Controls.Add(Me.PageCustom)
        Me.Wizard.Controls.Add(Me.PageStandard2)
        Me.Wizard.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wizard.HeaderTitleFont = New System.Drawing.Font("Microsoft Sans Serif", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Wizard.ImageHeader = Global.SchlumpfSoft.My.Resources.Resources.PapaSchlumpf1
        Me.Wizard.ImageWelcome = Global.SchlumpfSoft.My.Resources.Resources.PapaSchlumpf2
        Me.Wizard.Location = New System.Drawing.Point(0, 0)
        Me.Wizard.Name = "Wizard"
        Me.Wizard.Pages.AddRange(New SchlumpfSoft.Controls.WizardControl.WizardPage() {Me.PageWelcome, Me.PageStandard1, Me.PageStandard2, Me.PageCustom, Me.PageFinish})
        Me.Wizard.Size = New System.Drawing.Size(474, 313)
        Me.Wizard.TabIndex = 0
        Me.Wizard.WelcomeFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wizard.WelcomeTitleFont = New System.Drawing.Font("Microsoft Sans Serif", 18.25!, System.Drawing.FontStyle.Bold)
        '
        'PageCustom
        '
        Me.PageCustom.Controls.Add(Me.Label3)
        Me.PageCustom.Description = "Beschreibung der Univesalseite"
        Me.PageCustom.Location = New System.Drawing.Point(0, 0)
        Me.PageCustom.Name = "PageCustom"
        Me.PageCustom.Size = New System.Drawing.Size(374, 172)
        Me.PageCustom.TabIndex = 12
        Me.PageCustom.Title = "Universalseite"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(134, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(231, 52)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Hier können weitere Controls eingefügt werden." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Diese Seite ist individuell g" &
    "estaltbar."
        '
        'PageFinish
        '
        Me.PageFinish.Controls.Add(Me.Label4)
        Me.PageFinish.Description = "Beschreibung der Zielseite"
        Me.PageFinish.Location = New System.Drawing.Point(0, 0)
        Me.PageFinish.Name = "PageFinish"
        Me.PageFinish.Size = New System.Drawing.Size(520, 288)
        Me.PageFinish.TabIndex = 13
        Me.PageFinish.Title = "Zielseite"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(222, 239)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(231, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Hier können weitere Controls eingefügt werden."
        '
        'PageStandard2
        '
        Me.PageStandard2.Controls.Add(Me.Label2)
        Me.PageStandard2.Description = "Beschreibung der Standardseite 2"
        Me.PageStandard2.Location = New System.Drawing.Point(0, 0)
        Me.PageStandard2.Name = "PageStandard2"
        Me.PageStandard2.Size = New System.Drawing.Size(374, 172)
        Me.PageStandard2.TabIndex = 14
        Me.PageStandard2.Title = "Standardseite 2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(277, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(231, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Hier können weitere Controls eingefügt werden."
        '
        'PageStandard1
        '
        Me.PageStandard1.Controls.Add(Me.Label1)
        Me.PageStandard1.Description = "Beschreibung der Standardseite 1"
        Me.PageStandard1.Location = New System.Drawing.Point(0, 0)
        Me.PageStandard1.Name = "PageStandard1"
        Me.PageStandard1.Size = New System.Drawing.Size(475, 261)
        Me.PageStandard1.TabIndex = 11
        Me.PageStandard1.Title = "Standardseite 1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(106, 154)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(231, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Hier können weitere Controls eingefügt werden."
        '
        'PageWelcome
        '
        Me.PageWelcome.Controls.Add(Me.Label0)
        Me.PageWelcome.Description = "Beschreibung der Willkommensseite"
        Me.PageWelcome.Location = New System.Drawing.Point(0, 0)
        Me.PageWelcome.Name = "PageWelcome"
        Me.PageWelcome.Size = New System.Drawing.Size(474, 265)
        Me.PageWelcome.TabIndex = 10
        Me.PageWelcome.Title = "Willkommen"
        '
        'Label0
        '
        Me.Label0.AutoSize = True
        Me.Label0.Location = New System.Drawing.Point(215, 159)
        Me.Label0.Name = "Label0"
        Me.Label0.Size = New System.Drawing.Size(231, 13)
        Me.Label0.TabIndex = 0
        Me.Label0.Text = "Hier können weitere Controls eingefügt werden."
        '
        'FormWizard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 313)
        Me.Controls.Add(Me.Wizard)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormWizard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Wizard Demo"
        Me.Wizard.ResumeLayout(False)
        Me.PageCustom.ResumeLayout(False)
        Me.PageCustom.PerformLayout()
        Me.PageFinish.ResumeLayout(False)
        Me.PageFinish.PerformLayout()
        Me.PageStandard2.ResumeLayout(False)
        Me.PageStandard2.PerformLayout()
        Me.PageStandard1.ResumeLayout(False)
        Me.PageStandard1.PerformLayout()
        Me.PageWelcome.ResumeLayout(False)
        Me.PageWelcome.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Wizard As SchlumpfSoft.Controls.WizardControl.Wizard
    Private WithEvents PageFinish As SchlumpfSoft.Controls.WizardControl.PageFinish
    Private WithEvents PageCustom As SchlumpfSoft.Controls.WizardControl.PageCustom
    Private WithEvents PageStandard2 As SchlumpfSoft.Controls.WizardControl.PageStandard
    Private WithEvents PageStandard1 As SchlumpfSoft.Controls.WizardControl.PageStandard
    Private WithEvents PageWelcome As SchlumpfSoft.Controls.WizardControl.PageWelcome
    Private WithEvents Label0 As Label
    Private WithEvents Label1 As Label
    Private WithEvents Label2 As Label
    Private WithEvents Label3 As Label
    Private WithEvents Label4 As Label
End Class
