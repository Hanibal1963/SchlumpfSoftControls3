' --------------------------------------------------------------------------------------------------------
' Datei: CommentEdit.vb
' Author: Andreas Sauer
' Datum: 29.05.2026
' --------------------------------------------------------------------------------------------------------

Imports System.Linq

Namespace IniFileControl

    ''' <summary>
    ''' Stellt ein Steuerelement zum Anzeigen und Bearbeiten von Datei- oder
    ''' Abschnittskommentaren einer INI-Datei bereit.
    ''' </summary>
    ''' <remarks>
    ''' Die Bearbeitung erfolgt im mehrzeiligen Textfeld. Änderungen werden erst nach
    ''' Klick auf den Übernehmen-Button per <see cref="CommentChanged"/> nach außen
    ''' gemeldet.
    ''' </remarks>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Steuerelement zum Anzeigen und Bearbeiten des Datei- oder Abschnitts- Kommentars einer INI - Datei.")>
    <ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(CommentEdit), "IniFileControl.CommentEdit.bmp")>
    Public Class CommentEdit

        Inherits System.Windows.Forms.UserControl

#Region "Variablen"

        Private _Lines As String() = {""}
        Private _TitelText As String

#End Region

#Region "Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst, wenn der aktuell bearbeitete Kommentar per Übernehmen-Button
        ''' bestätigt wurde.
        ''' </summary>
        ''' <param name="sender">Die auslösende Instanz von <see cref="CommentEdit"/>.</param>
        ''' <param name="e">Enthält Abschnittsname und die übernommenen Kommentarzeilen.</param>
        <Description("Wird ausgelöst wenn sich der Kommentartext geändert hat.")>
        Public Event CommentChanged(sender As Object, e As CommentEditEventArgs)

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Gibt den Titeltext der umschließenden GroupBox zurück oder legt ihn fest.
        ''' </summary>
        ''' <value>Der in der Benutzeroberfläche angezeigte Titeltext.</value>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
        Public Property TitelText As String
            Set(value As String)
                If Me._TitelText <> value Then
                    Me._TitelText = value
                    ' Synchronisiert den Titel sofort mit der UI.
                    Me.GroupBox.Text = Me._TitelText
                End If
            End Set
            Get
                Return Me._TitelText
            End Get
        End Property

        ''' <summary>
        ''' Gibt den Kommentar als Zeilenarray zurück oder legt ihn fest.
        ''' </summary>
        ''' <remarks>
        ''' Beim Setzen werden die Zeilen nur dann übernommen, wenn sich der Inhalt
        ''' gegenüber dem aktuellen Array tatsächlich geändert hat.
        ''' </remarks>
        ''' <value>Kommentarzeilen; jedes Arrayelement entspricht genau einer Textzeile.</value>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Kommentartext zurück oder legt diesen fest.")>
        Public Property Comment As String()
            Get
                Return Me._Lines
            End Get
            Set
                If Not Me._Lines.SequenceEqual(Value) Then
                    ' Übernimmt den neuen Kommentarzustand in das interne Modell.
                    Me._Lines = Value
                    ' Spiegelt das interne Modell in der TextBox wider.
                    Me.TextBox.Lines = Me._Lines
                    ' Nach externer Zuweisung gilt der Zustand als bestätigt.
                    Me.Button.Enabled = False
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Namen des aktuell bearbeiteten INI-Abschnitts zurück oder legt ihn fest.
        ''' </summary>
        ''' <value>
        ''' Abschnittsname, der im Ereignis <see cref="CommentChanged"/> zusammen mit dem
        ''' Kommentar übertragen wird.
        ''' </value>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Namen des Abschnitts zurück oder legt diesen fest, für den der Kommentar angezeigt werden soll.")>
        Public Property SectionName As String

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz der Klasse <see cref="CommentEdit"/>.
        ''' </summary>
        ''' <remarks>
        ''' Nach dem Erstellen der Designer-Elemente wird der initiale GroupBox-Titel
        ''' übernommen und der Übernehmen-Button deaktiviert.
        ''' </remarks>
        Public Sub New()

            Me.InitializeComponent()
            ' Übernimmt den im Designer definierten Standardtitel.
            Me._TitelText = Me.GroupBox.Text
            ' Ohne Benutzeränderung darf kein Commit ausgelöst werden.
            Me.Button.Enabled = False

        End Sub

#End Region

#Region "interne Methoden"

        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles Button.Click

            ' Liest die aktuell sichtbaren Zeilen als neuen bestätigten Zustand ein.
            Me._Lines = Me.TextBox.Lines
            ' Deaktiviert den Button, bis erneut Änderungen erfolgen.
            Me.Button.Enabled = False
            ' Meldet die bestätigte Änderung an den aufrufenden Code.
            RaiseEvent CommentChanged(Me, New CommentEditEventArgs(Me.SectionName, Me._Lines))

        End Sub

        Private Sub TextBox_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox.TextChanged

            ' Jede Benutzereingabe markiert den Inhalt als "nicht übernommen".
            Me.Button.Enabled = True

        End Sub

#End Region

    End Class

End Namespace