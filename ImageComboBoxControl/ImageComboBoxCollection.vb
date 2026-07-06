' --------------------------------------------------------------------------------------------------------
' Datei: ImageComboBoxCollection.vb
' Author: Andreas Sauer
' Datum: 05.07.2026
' --------------------------------------------------------------------------------------------------------

Namespace ImageComboBoxControl

    ''' <summary>
    ''' Stellt eine Kollektion von <see cref="ImageComboBoxItem"/>-Elementen bereit.
    ''' </summary>
    ''' <typeparam name="TComboBoxItem">
    ''' Der Typ der verwalteten ComboBox-Elemente.
    ''' </typeparam>
    Public Class ImageComboBoxCollection(Of TComboBoxItem)

        Inherits System.Collections.CollectionBase

        ''' <summary>
        ''' Tritt auf, wenn sich die Elemente der Kollektion geändert haben.
        ''' </summary>
        Public Event UpdateItems As System.EventHandler

        Private _itemsBase As System.Windows.Forms.ComboBox.ObjectCollection

        ''' <summary>
        ''' Ruft die zugrunde liegende <see cref="System.Windows.Forms.ComboBox.ObjectCollection"/> ab oder legt sie fest.
        ''' </summary>
        ''' <value>
        ''' Die zugrunde liegende Objektkollektion der ComboBox.
        ''' </value>
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

        ''' <summary>
        ''' Ruft das Element am angegebenen Index ab oder legt es fest.
        ''' </summary>
        ''' <param name="index">
        ''' Der nullbasierte Index des Elements.
        ''' </param>
        ''' <value>
        ''' Das <see cref="ImageComboBoxItem"/> am angegebenen Index.
        ''' </value>
        ''' <exception cref="System.ArgumentOutOfRangeException">
        ''' Der angegebene Index liegt außerhalb des gültigen Bereichs.
        ''' </exception>
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

        ''' <summary>
        ''' Fügt der Kollektion ein Element hinzu.
        ''' </summary>
        ''' <param name="value">
        ''' Das hinzuzufügende Element.
        ''' </param>
        ''' <returns>
        ''' Der nullbasierte Index des hinzugefügten Elements.
        ''' </returns>
        Public Function Add(value As ImageComboBoxItem) As System.Int32
            Dim index As System.Int32 = Me.List.Add(value)

            _itemsBase?.Add(value)

            RaiseEvent UpdateItems(Me, System.EventArgs.Empty)
            Return index
        End Function

        ''' <summary>
        ''' Ermittelt den Index eines bestimmten Elements in der Kollektion.
        ''' </summary>
        ''' <param name="value">
        ''' Das zu suchende Element.
        ''' </param>
        ''' <returns>
        ''' Der nullbasierte Index des Elements oder -1, wenn das Element nicht gefunden wurde.
        ''' </returns>
        Public Function IndexOf(value As ImageComboBoxItem) As System.Int32
            Return Me.List.IndexOf(value)
        End Function

        ''' <summary>
        ''' Fügt ein Element an der angegebenen Position in die Kollektion ein.
        ''' </summary>
        ''' <param name="index">
        ''' Der nullbasierte Index, an dem das Element eingefügt werden soll.
        ''' </param>
        ''' <param name="value">
        ''' Das einzufügende Element.
        ''' </param>
        ''' <exception cref="System.ArgumentOutOfRangeException">
        ''' Der angegebene Index liegt außerhalb des gültigen Bereichs.
        ''' </exception>
        Public Sub Insert(index As System.Int32, value As ImageComboBoxItem)
            Me.List.Insert(index, value)

            _itemsBase?.Insert(index, value)

            RaiseEvent UpdateItems(Me, System.EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' Entfernt das angegebene Element aus der Kollektion.
        ''' </summary>
        ''' <param name="value">
        ''' Das zu entfernende Element.
        ''' </param>
        Public Sub Remove(value As ImageComboBoxItem)
            Dim index As System.Int32 = Me.List.IndexOf(value)
            If index < 0 Then
                Return
            End If

            Me.List.RemoveAt(index)

            _itemsBase?.RemoveAt(index)

            RaiseEvent UpdateItems(Me, System.EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' Entfernt alle Elemente aus der Kollektion.
        ''' </summary>
        Public Overloads Sub Clear()
            Me.List.Clear()

            _itemsBase?.Clear()

            RaiseEvent UpdateItems(Me, System.EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' Prüft, ob ein bestimmtes Element in der Kollektion enthalten ist.
        ''' </summary>
        ''' <param name="value">
        ''' Das zu suchende Element.
        ''' </param>
        ''' <returns>
        ''' <c>True</c>, wenn das Element enthalten ist, andernfalls <c>False</c>.
        ''' </returns>
        Public Function Contains(value As ImageComboBoxItem) As Boolean
            Return Me.List.Contains(value)
        End Function

    End Class

End Namespace