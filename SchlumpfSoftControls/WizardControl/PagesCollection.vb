' --------------------------------------------------------------------------------------------------------
' Datei: PagesCollection.vb
' Author: Andreas Sauer
' Datum: 25.05.2026
' --------------------------------------------------------------------------------------------------------

Imports System
Imports System.Collections
Imports System.Windows.Forms
Imports System.ComponentModel

Namespace WizardControl

    ''' <summary>
    ''' Definiert die Auflistung der Seiten des Assistenten
    ''' </summary>
    Public Class PagesCollection : Inherits CollectionBase

        Private ReadOnly _Owner As Wizard = Nothing

        Default Public Property Item(Index As Int32) As WizardPage
            Get
                Return CType(Me.List(Index), WizardPage)
            End Get
            Set(value As WizardPage)
                Me.List(Index) = value
            End Set
        End Property

        Friend Sub New(Owner As Wizard)
            Me._Owner = Owner
        End Sub

        Public Function Add(Page As WizardPage) As Int32
            Return Me.List.Add(Page)
        End Function

        Public Sub AddRange(Pages As WizardPage())
            For Each value As WizardPage In Pages
                Dim unused = Me.Add(value)
            Next
        End Sub

        Public Function IndexOf(Page As WizardPage) As Int32
            Return Me.List.IndexOf(Page)
        End Function

        Public Sub Insert(Index As Int32, Page As WizardPage)
            Me.List.Insert(Index, Page)
        End Sub

        Public Sub Remove(Page As WizardPage)
            Me.List.Remove(Page)
        End Sub

        Public Function Contains(Page As WizardPage) As Boolean
            Return Me.List.Contains(Page)
        End Function

        Protected Overrides Sub OnInsertComplete(Index As Int32, Value As Object)
            MyBase.OnInsertComplete(Index, Value)
            Me._Owner.SelectedIndex = Index
        End Sub

        Protected Overrides Sub OnRemoveComplete(Index As Int32, Value As Object)
            MyBase.OnRemoveComplete(Index, Value)
            If Me._Owner.SelectedIndex = Index Then
                Me._Owner.SelectedIndex = If(Index < Me.InnerList.Count, Index, Me.InnerList.Count - 1)
            End If
        End Sub

    End Class

End Namespace