' --------------------------------------------------------------------------------------------------------
' Datei: ContentView.vb
' Author: Andreas Sauer
' Datum: 29.05.2026
' --------------------------------------------------------------------------------------------------------

Namespace IniFileControl

    ''' <summary>
    ''' Stellt ein Steuerelement zur schreibgeschützten Anzeige von Textinhalten bereit.
    ''' </summary>
    ''' <remarks>
    ''' Die Anzeige erfolgt zeilenbasiert über <see cref="Lines"/>. Änderungen an
    ''' <see cref="TitelText"/> und <see cref="Lines"/> werden intern auf die UI
    ''' synchronisiert.
    ''' </remarks>
    <SchlumpfSoft.ProvideToolboxControlAttribute("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Steuerelement zum Anzeigen des Dateiinhaltes.")> ' Beschreibt das Control im Designer (Eigenschaftenfenster/Toolbox).
    <System.ComponentModel.ToolboxItem(True)> ' Markiert die Klasse als Toolbox-Element.
    <System.Drawing.ToolboxBitmap(GetType(ContentView), "IniFileControl.ContentView.bmp")> ' Legt das Symbol in der Toolbox fest.
    Public Class ContentView

        Inherits System.Windows.Forms.UserControl

#Region "Variablen"

        Private _Lines As String()
        Private _TitelText As String

#End Region

#Region "Ereignisse"

        Private Event PropertyLinesChanged()
        Private Event PropertyTitelTextChanged()

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Gibt den Titeltext der umschließenden GroupBox zurück oder legt ihn fest.
        ''' </summary>
        ''' <value>Text, der in der Benutzeroberfläche als Überschrift angezeigt wird.</value>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
        Public Property TitelText As String
            Set(value As String)
                If Me._TitelText <> value Then
                    Me._TitelText = value
                    ' Löst die Aktualisierung der GroupBox-Beschriftung aus.
                    RaiseEvent PropertyTitelTextChanged()
                End If
            End Set
            Get
                Return Me._TitelText
            End Get
        End Property

        ''' <summary>
        ''' Gibt den angezeigten Inhalt als Zeilenarray zurück oder legt ihn fest.
        ''' </summary>
        ''' <remarks>
        ''' Die Änderungsprüfung erfolgt über Referenzvergleich. Bei inhaltlichen
        ''' Änderungen sollte daher ein neues Array zugewiesen werden.
        ''' </remarks>
        ''' <value>Zeilenarray für die schreibgeschützte Textanzeige.</value>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den Dateiinhalt zurück oder legt diesen fest.")>
        Public Property Lines As String()
            Get
                Return Me._Lines
            End Get
            Set
                If Me._Lines IsNot Value Then
                    ' Übernimmt den neuen Modellzustand und triggert die UI-Synchronisierung.
                    Me._Lines = Value
                    RaiseEvent PropertyLinesChanged()
                End If
            End Set
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz der Klasse <see cref="ContentView"/>.
        ''' </summary>
        ''' <remarks>
        ''' Erstellt die Designer-Komponenten und übernimmt den initialen Titel der
        ''' GroupBox in das interne Titelfeld.
        ''' </remarks>
        Public Sub New()

            Me.InitializeComponent()
            ' Übernimmt den im Designer hinterlegten Starttitel.
            Me._TitelText = Me.GroupBox.Text

        End Sub

#End Region

#Region "interne Methoden"

        Private Sub ContentView_LinesChanged() Handles Me.PropertyLinesChanged

            ' Spiegelt den internen Zeileninhalt im Anzeigefeld wider.
            Me.TextBox.Lines = Me._Lines

        End Sub

        Private Sub ContentView_TitelTextChanged() Handles Me.PropertyTitelTextChanged

            ' Aktualisiert die sichtbare Gruppenüberschrift.
            Me.GroupBox.Text = Me._TitelText

        End Sub

#End Region

    End Class

End Namespace