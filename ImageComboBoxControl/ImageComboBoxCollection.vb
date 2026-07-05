' --------------------------------------------------------------------------------------------------------
' Datei: ImageComboBoxCollection.vb
' Author: Andreas Sauer
' Datum: 05.07.2026
' --------------------------------------------------------------------------------------------------------

Namespace ImageComboBoxControl

    Public Class ImageComboBoxCollection(Of TComboBoxItem)

        Inherits System.Collections.CollectionBase

        Public Event UpdateItems As System.EventHandler

        Private _itemsBase As System.Windows.Forms.ComboBox.ObjectCollection

        Public Property ItemsBase As System.Windows.Forms.ComboBox.ObjectCollection
            Get
                Return _itemsBase
            End Get
            Set(value As System.Windows.Forms.ComboBox.ObjectCollection)
                _itemsBase = value

                If _itemsBase IsNot Nothing Then
                    _itemsBase.Clear()

                    For Each item As ImageComboBoxItem In Me.List
                        Dim unused = _itemsBase.Add(item)
                    Next
                End If
            End Set
        End Property

        Default Public Property Item(index As System.Int32) As ImageComboBoxItem
            Get
                Return CType(Me.List(index), ImageComboBoxItem)
            End Get
            Set(value As ImageComboBoxItem)
                Me.List(index) = value

                If _itemsBase IsNot Nothing Then
                    _itemsBase(index) = value
                End If

                RaiseEvent UpdateItems(Me, System.EventArgs.Empty)
            End Set
        End Property

        Public Function Add(value As ImageComboBoxItem) As System.Int32
            Dim index As System.Int32 = Me.List.Add(value)

            _itemsBase?.Add(value)

            RaiseEvent UpdateItems(Me, System.EventArgs.Empty)
            Return index
        End Function

        Public Function IndexOf(value As ImageComboBoxItem) As System.Int32
            Return Me.List.IndexOf(value)
        End Function

        Public Sub Insert(index As System.Int32, value As ImageComboBoxItem)
            Me.List.Insert(index, value)

            _itemsBase?.Insert(index, value)

            RaiseEvent UpdateItems(Me, System.EventArgs.Empty)
        End Sub

        Public Sub Remove(value As ImageComboBoxItem)
            Dim index As System.Int32 = Me.List.IndexOf(value)
            If index < 0 Then
                Return
            End If

            Me.List.RemoveAt(index)

            _itemsBase?.RemoveAt(index)

            RaiseEvent UpdateItems(Me, System.EventArgs.Empty)
        End Sub

        Public Overloads Sub Clear()
            Me.List.Clear()

            _itemsBase?.Clear()

            RaiseEvent UpdateItems(Me, System.EventArgs.Empty)
        End Sub

        Public Function Contains(value As ImageComboBoxItem) As Boolean
            Return Me.List.Contains(value)
        End Function

    End Class

End Namespace