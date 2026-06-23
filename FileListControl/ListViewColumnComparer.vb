' --------------------------------------------------------------------------------------------------------
' Datei: ListViewColumnComparer.vb
' Author: Andreas Sauer
' Datum: 30.04.2026
' --------------------------------------------------------------------------------------------------------

Namespace FileListControl

    ''' <summary>
    ''' Vergleicht zwei <see cref="ListViewItem"/>-Objekte anhand einer definierten Spalte,
    ''' damit eine sortierte Anzeige im <see cref="ListView"/> möglich ist.
    ''' </summary>
    Friend NotInheritable Class ListViewColumnComparer

        Implements System.Collections.IComparer

#Region "Definition der lokalen Variablen"

        ''' <summary>
        ''' Index der Spalte, deren Textwerte für den Vergleich verwendet werden.
        ''' </summary>
        Private ReadOnly _column As System.Int32

        ''' <summary>
        ''' Gibt an, ob auf- oder absteigend sortiert wird.
        ''' </summary>
        Private ReadOnly _order As System.Windows.Forms.SortOrder

#End Region

#Region "Definition der öffentlichen Methoden"

        ''' <summary>
        ''' Initialisiert einen neuen Comparer mit Zielspalte und Sortierreihenfolge.
        ''' </summary>
        ''' <param name="column">Der Spaltenindex, nach dem verglichen werden soll.</param>
        ''' <param name="order">Die gewünschte Sortierreihenfolge.</param>
        Public Sub New(column As System.Int32, order As System.Windows.Forms.SortOrder)

            Me._column = column
            Me._order = order

        End Sub

        ''' <summary>
        ''' Vergleicht zwei Listeneinträge für den Sortiervorgang der ListView.
        ''' </summary>
        ''' <param name="x">Erstes Vergleichsobjekt (erwartet: <see cref="ListViewItem"/>).</param>
        ''' <param name="y">Zweites Vergleichsobjekt (erwartet: <see cref="ListViewItem"/>).</param>
        ''' <returns>
        ''' Kleiner 0, wenn <paramref name="x"/> vor <paramref name="y"/> sortiert wird;
        ''' größer 0 im umgekehrten Fall; 0 bei Gleichheit.
        ''' Bei <see cref="System.Windows.Forms.SortOrder.Descending"/> wird das Ergebnis invertiert.
        ''' </returns>
        Public Function Compare(x As Object, y As Object) As System.Int32 Implements System.Collections.IComparer.Compare

            ' Die übergebene IComparer-Signatur liefert Object; hier wird auf ListViewItem konkretisiert.
            Dim leftItem As System.Windows.Forms.ListViewItem = DirectCast(x, System.Windows.Forms.ListViewItem)
            Dim rightItem As System.Windows.Forms.ListViewItem = DirectCast(y, System.Windows.Forms.ListViewItem)

            ' Die zu vergleichenden Texte stammen aus derselben Zielspalte.
            Dim leftText As String = GetSubItemText(leftItem, Me._column)
            Dim rightText As String = GetSubItemText(rightItem, Me._column)

            ' Kulturabhängiger, nicht case-sensitiver Textvergleich für benutzerfreundliche Sortierung.
            Dim result As System.Int32 = System.StringComparer.CurrentCultureIgnoreCase.Compare(leftText, rightText)

            ' Gleichheit (0) bleibt unabhängig von der Sortierrichtung unverändert.
            ' Für absteigende Sortierung wird das Vergleichsergebnis invertiert.
            If Me._order = System.Windows.Forms.SortOrder.Descending Then
                result = -result
            End If

            Return result

        End Function

#End Region

#Region "Definition der internen Methoden"

        ''' <summary>
        ''' Liefert den Text eines SubItems und schützt gegen ungültige Spaltenindizes.
        ''' </summary>
        ''' <param name="item">Der ListView-Eintrag, aus dem gelesen wird.</param>
        ''' <param name="index">Der gewünschte SubItem-Index.</param>
        ''' <returns>
        ''' Text des SubItems; bei ungültigem Index wird ein leerer String zurückgegeben.
        ''' </returns>
        Private Shared Function GetSubItemText(item As System.Windows.Forms.ListViewItem, index As System.Int32) As String
            ' Robuste Absicherung: Bei fehlender Spalte kein Fehler, sondern neutraler Vergleichswert.
            Return If(index < 0 OrElse index >= item.SubItems.Count, String.Empty, item.SubItems(index).Text)
        End Function

#End Region

    End Class

End Namespace

