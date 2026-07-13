' --------------------------------------------------------------------------------------------------------
' Datei: ImageComboBoxItem.vb
' Author: Andreas Sauer
' Datum: 05.07.2026
' --------------------------------------------------------------------------------------------------------

Namespace ImageComboBoxControl

    ''' <summary>
    ''' Repräsentiert ein einzelnes Element für die <see cref="ImageComboBox"/>.
    ''' </summary>
    <System.Serializable>
    Public Class ImageComboBoxItem

        Private _value As String
        Private _image As System.Drawing.Image

        ''' <summary>
        ''' Ruft den anzuzeigenden Textwert des Elements ab oder legt ihn fest.
        ''' </summary>
        ''' <value>
        ''' Der Textwert des Elements.
        ''' </value>
        Public Property Value As String
            Get
                Return _value
            End Get
            Set(value As String)
                _value = value
            End Set
        End Property

        ''' <summary>
        ''' Ruft das dem Element zugeordnete Bild ab oder legt es fest.
        ''' </summary>
        ''' <value>
        ''' Das Bild des Elements.
        ''' </value>
        Public Property Image As System.Drawing.Image
            Get
                Return _image
            End Get
            Set(value As System.Drawing.Image)
                _image = value
            End Set
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz der Klasse <see cref="ImageComboBoxItem"/>.
        ''' </summary>
        Public Sub New()
            _value = String.Empty
            ' Ein minimales Platzhalterbild vermeidet Nullprüfungen in Zeichenroutinen.
            _image = New System.Drawing.Bitmap(1, 1)
        End Sub

        ''' <summary>
        ''' Initialisiert eine neue Instanz der Klasse <see cref="ImageComboBoxItem"/> mit einem Textwert.
        ''' </summary>
        ''' <param name="value">
        ''' Der Textwert des Elements.
        ''' </param>
        Public Sub New(value As String)
            _value = value
            ' Ein minimales Platzhalterbild vereinfacht die Verarbeitung in OwnerDraw-Szenarien.
            _image = New System.Drawing.Bitmap(1, 1)
        End Sub

        ''' <summary>
        ''' Initialisiert eine neue Instanz der Klasse <see cref="ImageComboBoxItem"/> mit Textwert und Bild.
        ''' </summary>
        ''' <param name="value">
        ''' Der Textwert des Elements.
        ''' </param>
        ''' <param name="image">
        ''' Das Bild des Elements.
        ''' </param>
        Public Sub New(value As String, image As System.Drawing.Image)
            _value = value
            _image = image
        End Sub

        ''' <summary>
        ''' Gibt den Textwert des Elements zurück.
        ''' </summary>
        ''' <returns>
        ''' Den Textwert des Elements oder eine leere Zeichenfolge, wenn kein Wert gesetzt ist.
        ''' </returns>
        Public Overrides Function ToString() As String
            Return If(_value, String.Empty)
        End Function

    End Class

End Namespace