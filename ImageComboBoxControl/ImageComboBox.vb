' --------------------------------------------------------------------------------------------------------
' Datei: ImageComboBox.vb
' Author: Andreas Sauer
' Datum: 05.07.2026
' --------------------------------------------------------------------------------------------------------

Namespace ImageComboBoxControl

    ''' <summary>
    ''' Eine erweiterte ComboBox mit Symbolen.
    ''' </summary>
    <SchlumpfSoft.ProvideToolboxControl("SchlumpfSoft.Controls.ImageComboBox", False)>
    <System.ComponentModel.Description("Eine erweiterte ComboBox mit Symbolen.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(ImageComboBox), "ImageComboBoxControl.ImageComboBox.bmp")>
    Public Class ImageComboBox

        Inherits System.Windows.Forms.ComboBox

        Private _items As ImageComboBoxCollection(Of ImageComboBoxItem)

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Shadows ReadOnly Property Items As ImageComboBoxCollection(Of ImageComboBoxItem)
            Get
                Return Me.Elements
            End Get
        End Property

        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)>
        <System.ComponentModel.Editor(GetType(ImageComboBoxCollectionEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <System.ComponentModel.MergableProperty(False)>
        Public ReadOnly Property Elements As ImageComboBoxCollection(Of ImageComboBoxItem)
            Get
                If Me._items Is Nothing Then
                    Me.InitializeItemsCollection()
                End If

                Return Me._items
            End Get
        End Property

        Public Sub New()

            Me.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
            Me.InitializeItemsCollection()
            AddHandler DrawItem, AddressOf Me.ComboBoxDrawItemEvent
            AddHandler MeasureItem, AddressOf Me.ComboBoxMeasureItem

        End Sub

        Protected Overrides Function CreateControlsInstance() As System.Windows.Forms.Control.ControlCollection
            Dim result As System.Windows.Forms.Control.ControlCollection = MyBase.CreateControlsInstance()
            Me.InitializeItemsCollection()
            Return result
        End Function

        Private Sub InitializeItemsCollection()
            If Me._items Is Nothing Then
                Me._items = New ImageComboBoxCollection(Of ImageComboBoxItem)()
                AddHandler Me._items.UpdateItems, AddressOf Me.UpdateItems
            End If
            Me._items.ItemsBase = MyBase.Items
        End Sub

        Private Sub UpdateItems(sender As Object, e As System.EventArgs)
            Me.Invalidate()
        End Sub

        Private Sub ComboBoxMeasureItem(sender As Object, e As System.Windows.Forms.MeasureItemEventArgs)
            Using g As System.Drawing.Graphics = Me.CreateGraphics()
                Dim maxWidth As System.Int32 = 0

                For Each comboItem As ImageComboBoxItem In Me.Elements
                    Dim itemText As String = String.Empty
                    Dim imageWidth As System.Int32 = 0

                    If comboItem IsNot Nothing Then
                        itemText = If(comboItem.Value, String.Empty)

                        If comboItem.Image IsNot Nothing Then
                            imageWidth = Me.ItemHeight
                        End If
                    End If

                    Dim textWidth As System.Int32 = CInt(g.MeasureString(itemText, Me.Font).Width)
                    Dim totalWidth As System.Int32 = imageWidth + textWidth + 8

                    If totalWidth > maxWidth Then
                        maxWidth = totalWidth
                    End If
                Next

                Me.DropDownWidth = System.Math.Max(maxWidth, Me.Width)
                e.ItemHeight = Me.ItemHeight
            End Using
        End Sub

        Private Sub ComboBoxDrawItemEvent(sender As Object, e As System.Windows.Forms.DrawItemEventArgs)
            e.DrawBackground()

            If e.Index >= 0 AndAlso e.Index < Me.Elements.Count Then
                Dim comboboxItem As ImageComboBoxItem = Me.Elements(e.Index)
                If comboboxItem IsNot Nothing Then
                    Dim imageWidth As System.Int32 = Me.ItemHeight
                    Dim textLeft As System.Int32 = e.Bounds.X

                    If comboboxItem.Image IsNot Nothing Then
                        e.Graphics.DrawImage(comboboxItem.Image, e.Bounds.X, e.Bounds.Y, imageWidth, Me.ItemHeight)
                        textLeft += imageWidth
                    End If

                    Dim text As String = If(comboboxItem.Value, String.Empty)

                    e.Graphics.DrawString(
                        text,
                        Me.Font,
                        System.Drawing.Brushes.Black,
                        New System.Drawing.RectangleF(textLeft, e.Bounds.Y, Me.DropDownWidth, Me.ItemHeight))
                End If
            End If

            e.DrawFocusRectangle()

        End Sub

    End Class

End Namespace