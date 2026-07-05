' --------------------------------------------------------------------------------------------------------
' Datei: ImageComboBoxItem.vb
' Author: Andreas Sauer
' Datum: 05.07.2026
' --------------------------------------------------------------------------------------------------------

Namespace ImageComboBoxControl

    <System.Serializable>
    Public Class ImageComboBoxItem

        Private _value As String
        Private _image As System.Drawing.Image

        Public Property Value As String
            Get
                Return _value
            End Get
            Set(value As String)
                _value = value
            End Set
        End Property

        Public Property Image As System.Drawing.Image
            Get
                Return _image
            End Get
            Set(value As System.Drawing.Image)
                _image = value
            End Set
        End Property

        Public Sub New()
            _value = String.Empty
            _image = New System.Drawing.Bitmap(1, 1)
        End Sub

        Public Sub New(value As String)
            _value = value
            _image = New System.Drawing.Bitmap(1, 1)
        End Sub

        Public Sub New(value As String, image As System.Drawing.Image)
            _value = value
            _image = image
        End Sub

        Public Overrides Function ToString() As String
            Return If(_value, String.Empty)
        End Function

    End Class

End Namespace