' --------------------------------------------------------------------------------------------------------
' Datei: AfterSwitchPagesEventArgs.vb
' Author: Andreas Sauer
' Datum: 25.05.2026
' --------------------------------------------------------------------------------------------------------

Namespace WizardControl

    ''' <summary>
    ''' Enthält die Indexwerte der Seiten nachdem die Seiten gewechselt wurden.
    ''' </summary>
    Public Class AfterSwitchPagesEventArgs

        Inherits System.EventArgs

        Protected _NewIndex As System.Int32

        ''' <summary>
        ''' Index der alten Seite
        ''' </summary>
        Public ReadOnly Property OldIndex As System.Int32

        ''' <summary>
        ''' Index der neuen Seite
        ''' </summary>
        Public ReadOnly Property NewIndex As System.Int32
            Get
                Return Me._NewIndex
            End Get
        End Property

        Friend Sub New(OldIndex As System.Int32, NewIndex As System.Int32)
            Me.OldIndex = OldIndex
            Me._NewIndex = NewIndex
        End Sub

    End Class

End Namespace
