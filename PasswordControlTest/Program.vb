' --------------------------------------------------------------------------------------------------------
' Datei: Program.vb
' Author: Andreas Sauer
' Datum: 03.08.2026
' --------------------------------------------------------------------------------------------------------

Module Program

    <STAThread()>
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Dim startForm As Form
        If String.IsNullOrWhiteSpace(My.Settings.PasswordCode) Then
            startForm = New Form1()
        Else
            startForm = New Form2()
        End If
        Application.Run(startForm)
    End Sub

End Module
