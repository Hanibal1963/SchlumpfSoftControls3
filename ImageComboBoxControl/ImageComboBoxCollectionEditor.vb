' --------------------------------------------------------------------------------------------------------
' Datei: ImageComboBoxCollectionEditor.vb
' Author: Andreas Sauer
' Datum: 05.07.2026
' --------------------------------------------------------------------------------------------------------

Namespace ImageComboBoxControl

    Public Class ImageComboBoxCollectionEditor

        Inherits System.ComponentModel.Design.CollectionEditor

        Public Sub New()
            MyBase.New(GetType(ImageComboBoxCollection(Of ImageComboBoxItem)))
        End Sub

        Protected Overrides Function CreateCollectionItemType() As System.Type
            Return GetType(ImageComboBoxItem)
        End Function

        Protected Overrides Function CreateNewItemTypes() As System.Type()
            Return New System.Type() {GetType(ImageComboBoxItem)}
        End Function

    End Class

End Namespace