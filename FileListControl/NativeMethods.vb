' --------------------------------------------------------------------------------------------------------
' Datei: NativeMethods.vb
' Author: Andreas Sauer
' Datum: 30.04.2026
' --------------------------------------------------------------------------------------------------------

Namespace FileListControl

    ''' <summary>
    ''' Kapselt die benötigten nativen Win32-/Shell-Interop-Aufrufe
    ''' für das Ermitteln und Verwalten von Dateisymbolen (Icons).
    ''' </summary>
    Friend NotInheritable Class NativeMethods

        ' Utility-Klasse: keine Instanzierung vorgesehen.
        Private Sub New()
        End Sub

#Region "Konstantendefinitionen"

        ' SHGetFileInfo-Flag: Icon-Handle im Rückgabestrukturfeld hIcon bereitstellen.
        Public Const SHGFI_ICON As System.UInt32 = &H100

        ' SHGetFileInfo-Flag: Kleines Icon (typisch 16x16) anfordern.
        Public Const SHGFI_SMALLICON As System.UInt32 = &H1

        ' SHGetFileInfo-Flag: Dateiattribute verwenden, auch wenn die Datei nicht existiert.
        Public Const SHGFI_USEFILEATTRIBUTES As System.UInt32 = &H10

        ' Win32-Dateiattribut: Verzeichnis.
        Public Const FILE_ATTRIBUTE_DIRECTORY As System.UInt32 = &H10

        ' Win32-Dateiattribut: Normale Datei.
        Public Const FILE_ATTRIBUTE_NORMAL As System.UInt32 = &H80

#End Region

#Region "Strukturdefinitionen"

        ''' <summary>
        ''' Entspricht der nativen <c>SHFILEINFO</c>-Struktur aus der Windows-Shell-API.
        ''' Enthält u. a. Icon-Handle sowie Anzeige- und Typinformationen.
        ''' </summary>
        <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
        Public Structure SHFILEINFO

            ''' <summary>
            ''' Handle auf das gelieferte Icon. Muss nach Verwendung freigegeben werden.
            ''' </summary>
            Public hIcon As System.IntPtr

            ''' <summary>
            ''' Interner Icon-Index in der System-Image-Liste.
            ''' </summary>
            Public iIcon As System.Int32

            ''' <summary>
            ''' Dateiattribute entsprechend der Shell-Abfrage.
            ''' </summary>
            Public dwAttributes As System.UInt32

            ''' <summary>
            ''' Anzeigename (z. B. Dateiname) aus der Shell.
            ''' </summary>
            <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=260)>
            Public szDisplayName As String

            ''' <summary>
            ''' Typbezeichnung (z. B. "Textdokument") aus der Shell.
            ''' </summary>
            <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=80)>
            Public szTypeName As String

        End Structure

#End Region

#Region "Definition der API-Funktionen"

        ''' <summary>
        ''' Gibt ein Icon-Handle frei, das zuvor von der Windows-API bereitgestellt wurde.
        ''' </summary>
        ''' <param name="hIcon">Das freizugebende Icon-Handle.</param>
        ''' <returns><c>True</c>, wenn das Handle erfolgreich zerstört wurde; andernfalls <c>False</c>.</returns>
        <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function DestroyIcon(hIcon As System.IntPtr) As Boolean
        End Function

        ''' <summary>
        ''' Ruft Datei-/Ordnerinformationen (insbesondere Icon-Daten) über die Shell-API ab.
        ''' </summary>
        ''' <param name="pszPath">Pfad, Dateiname oder Platzhalter (z. B. <c>*.txt</c>) des Zielobjekts.</param>
        ''' <param name="dwFileAttributes">Dateiattribute (z. B. Verzeichnis oder normale Datei), relevant bei <c>SHGFI_USEFILEATTRIBUTES</c>.</param>
        ''' <param name="psfi">Ausgabestruktur mit den ermittelten Informationen, inklusive optionalem Icon-Handle.</param>
        ''' <param name="cbFileInfo">Größe der Struktur <paramref name="psfi"/> in Bytes.</param>
        ''' <param name="uFlags">Kombination aus SHGFI-Flags zur Steuerung der Abfrage.</param>
        ''' <returns>
        ''' Ein von der API zurückgegebener Ergebniswert. Bei Erfolg ungleich <see cref="System.IntPtr.Zero"/>,
        ''' bei Fehler gleich <see cref="System.IntPtr.Zero"/>.
        ''' </returns>
        <System.Runtime.InteropServices.DllImport("shell32.dll", CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
        Public Shared Function SHGetFileInfo(
                                        pszPath As String, dwFileAttributes As System.UInt32, ByRef psfi As SHFILEINFO,
                                        cbFileInfo As System.UInt32, uFlags As System.UInt32) As System.IntPtr
        End Function

#End Region

    End Class

End Namespace

