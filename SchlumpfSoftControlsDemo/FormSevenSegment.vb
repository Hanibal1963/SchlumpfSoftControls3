' --------------------------------------------------------------------------------------------------------
' Datei: FormSevenSegment.vb
' Author: Andreas Sauer
' Datum: 08.09.2026
' --------------------------------------------------------------------------------------------------------

Imports SchlumpfSoft.Controls

Public Class FormSevenSegment

    Public Sub New()
        Me.InitializeComponent()
    End Sub

    Private Sub TextBox_SingleDigit_TextChanged(sender As Object, e As EventArgs) Handles TextBox_SingleDigit.TextChanged
        Me.SingleDigit.DigitValue = CType(sender, TextBox).Text
    End Sub

    Private Sub TextBox_MultiDigit_TextChanged(sender As Object, e As EventArgs) Handles TextBox_MultiDigit.TextChanged
        Me.MultiDigit.Value = CType(sender, TextBox).Text
    End Sub

    Private Sub Button_InactiveColor_Click(sender As Object, e As EventArgs) Handles Button_InactiveColor.Click

    End Sub

    Private Sub Button_ForeColor_Click(sender As Object, e As EventArgs) Handles Button_ForeColor.Click

    End Sub

End Class