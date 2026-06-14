' --------------------------------------------------------------------------------------------------------
' Datei: ApiDefinitions.vb
' Author: Andreas Sauer
' Datum: 06.05.2026
' --------------------------------------------------------------------------------------------------------

Namespace ExtendedRTFControl

    Module ApiDefinitions

        ' Win32-Konstante zum De-/Aktivieren des Redraws eines Fenster-Handles.
        Friend Const WM_SETREDRAW As System.Int32 = &HB

        ' Mindest-Schriftgröße.
        ' Kann bei Bedarf angepasst werden.
        Public Const MIN_FONT_SIZE As Single = 8.0F

        ' Sendet ein Windows-Message direkt an ein Fenster .
        ' Die Anwendung sollte 0 zurückgeben, wenn sie diese Nachricht verarbeitet.
        <System.Runtime.InteropServices.DllImport("user32.dll")>
        Friend Function SendMessage(hWnd As System.IntPtr, msg As System.Int32, wParam As Boolean, lParam As System.IntPtr) As System.IntPtr
        End Function

    End Module

End Namespace
