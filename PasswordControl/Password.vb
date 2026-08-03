' --------------------------------------------------------------------------------------------------------
' Datei: Password.vb
' Author: Andreas Sauer
' Datum: 24.07.2026
' --------------------------------------------------------------------------------------------------------

Namespace PasswordControl

    ''' <summary>
    ''' Ein Control zur Eingabe und Vergleich von Passwörtern.
    ''' </summary>
    <SchlumpfSoft.ProvideToolboxControlAttribute("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Ein Control zur Eingabe und Vergleich von Passwörtern.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(Password), "PasswordControl.Password.bmp")>
    Public Class Password

#Region "Definition der Variablen"

        Private _showpasswort As Boolean
        Private ReadOnly _security As New SecurityService()

#End Region

#Region "Definition der Ereignisse"

        ''' <summary>
        ''' Tritt ein, wenn aus dem eingegebenen Passwort ein neuer Code erzeugt wurde.
        ''' </summary>
        ''' <param name="sender">Das Steuerelement, das das Ereignis auslöst.</param>
        ''' <param name="e">Enthält den neu erzeugten Passwort-Code.</param>
        <System.ComponentModel.Description("Tritt ein, wenn aus dem eingegebenen Passwort ein neuer Code erzeugt wurde.")>
        Public Event PasswortChanged(sender As Object, e As PasswordChangedEventArgs)

#End Region

#Region "Öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz des <see cref="Password" />-Steuerelements.
        ''' </summary>
        Public Sub New()
            Me.InitializeComponent()
            ' Legt eine Mindestgröße fest, damit Textfeld und Symbol stets sichtbar bleiben.
            Me.MinimumSize = New System.Drawing.Size(100, 20)
            ' Setzt die Standardgröße des Steuerelements beim Einfügen in den Designer.
            Me.Size = New System.Drawing.Size(100, 20)
            ' Zeigt beim Start das Symbol zum Ausblenden des Passworts an.
            Me.PB.Image = My.Resources.pic_noshow
            ' Aktiviert die maskierte Eingabe als Standardverhalten.
            Me.TB.UseSystemPasswordChar = True
        End Sub

        ''' <summary>
        ''' Prüft, ob das aktuell eingegebene Passwort zu einem zuvor erzeugten Passwort-Code passt.
        ''' </summary>
        ''' <param name="PasswordCode">Der geschützte Passwort-Code, der einen gespeicherten Passwort-Hash enthält.</param>
        ''' <returns>True, wenn das aktuell eingegebene Passwort mit dem übergebenen Passwort-Code übereinstimmt; andernfalls False.</returns>
        ''' <exception cref="ArgumentException">Wenn der übergebene Passwort-Code oder das aktuell eingegebene Passwort leer ist.</exception>
        ''' <exception cref="FormatException">Wenn der übergebene Passwort-Code kein gültiger Base64-Text ist.</exception>
        ''' <exception cref="System.Security.Cryptography.CryptographicException">Wenn der übergebene Passwort-Code nicht für den aktuellen Benutzer entschlüsselt werden kann.</exception>
        Public Function VerifyPasswordCode(PasswordCode As String) As Boolean
            ' Hebt zuerst den Schutz des übergebenen Passwort-Codes auf, um den darin enthaltenen Hash wiederherzustellen.
            Dim pwhash As String = Me._security.UnprotectSecret(PasswordCode)
            ' Vergleicht das aktuell eingegebene Passwort mit dem wiederhergestellten Hashwert.
            Return Me._security.VerifyPassword(Me.TB.Text, pwhash)
        End Function

#End Region

#Region "Interne Methoden"

        Private Sub CheckTBText()
            ' Liest den aktuell im Textfeld eingegebenen Passworttext aus.
            Dim tbtext As String = Me.TB.Text
            ' Erzeugt nur dann einen Hash, wenn tatsächlich ein Passwort eingegeben wurde.
            Dim hash = If(String.IsNullOrWhiteSpace(tbtext), $"", Me._security.CreatePasswordHash(tbtext))
            ' Schützt den erzeugten Hash zusätzlich, bevor er an andere Komponenten weitergegeben wird.
            Dim passwordcode As String = Me._security.ProtectSecret(hash)
            ' Benachrichtigt Abonnenten über den neu berechneten Hashwert.
            RaiseEvent PasswortChanged(Me, New PasswordChangedEventArgs(passwordcode))
        End Sub

        Private Sub Password_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
            ' Erzwingt eine konstante Höhe, damit das Layout des Passwortfeldes stabil bleibt.
            Me.Height = 20
        End Sub

        Private Sub Password_EnabledChanged(sender As Object, e As System.EventArgs) Handles Me.EnabledChanged
            If Me.Enabled Then
                ' Aktiviert die Eingabe und zeigt das Symbol an.
                Me.TB.Enabled = True
                Me.PB.Image = If(Me._showpasswort, My.Resources.pic_show, My.Resources.pic_noshow)
            Else
                ' Deaktiviert die Eingabe und blendet das Symbol aus.
                Me.TB.Enabled = False
                Me.PB.Image = If(Me._showpasswort, My.Resources.pic_show_gray, My.Resources.pic_noshow_gray)
            End If
        End Sub

        Private Sub PB_Click(sender As Object, e As System.EventArgs) Handles PB.Click
            ' Wechselt zwischen der Anzeige des Passworts und der Maskierung.
            Me._showpasswort = Not Me._showpasswort
            If Me._showpasswort Then
                ' Wechselt auf das Symbol für sichtbare Passwörter und zeigt den Text unmaskiert an.
                Me.PB.Image = My.Resources.pic_show
                Me.TB.UseSystemPasswordChar = False
            Else
                ' Stellt das Symbol für ausgeblendete Passwörter wieder her und maskiert die Eingabe.
                Me.PB.Image = My.Resources.pic_noshow
                Me.TB.UseSystemPasswordChar = True
            End If
        End Sub

        Private Sub TB_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TB.KeyDown
            ' Löst die Hashbildung direkt bei Bestätigung mit der Eingabetaste aus.
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then Me.CheckTBText()
        End Sub

#End Region

    End Class

End Namespace
