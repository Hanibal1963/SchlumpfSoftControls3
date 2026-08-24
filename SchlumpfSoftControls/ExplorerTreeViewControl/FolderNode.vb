' --------------------------------------------------------------------------------------------------------
' Datei: FolderNode.vb
' Author: Andreas Sauer
' Datum: 23.08.2026
' --------------------------------------------------------------------------------------------------------

Imports System
Imports System.IO
Imports System.Windows.Forms

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Repräsentiert einen Knoten für einen Ordner im ExplorerTreeViewControl.
    ''' </summary>
    ''' <remarks>
    ''' Dieser Knoten speichert den angezeigten Namen sowie den vollständigen Pfad des Ordners und kann Unterordner
    ''' dynamisch laden.
    ''' </remarks>
    Friend Class FolderNode : Inherits TreeNode

        ''' <summary>
        ''' Gibt den vollständigen Pfad des Ordners zurück, der im Tag-Property gespeichert ist.
        ''' </summary>
        ''' <returns></returns>
        Public Overloads ReadOnly Property FullPath As String
            Get
                Return Me.Tag.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz der FolderNode-Klasse mit dem angegebenen Text und vollständigen Pfad.
        ''' </summary>
        ''' <param name="Text"></param>
        ''' <param name="FullPath"></param>
        Public Sub New(Text As String, FullPath As String)
            ' Setzt den angezeigten Namen des Knotens
            Me.Text = Text
            ' Speichert den vollständigen Pfad des Ordners im Tag-Property
            Me.Tag = FullPath
            ' Holt den Bildschlüssel für das Ordner-Icon und weist ihn zu
            Dim key As String = GetImageKey(ICON_FOLDER_FOLDER)
            Me.ImageKey = key
            Me.SelectedImageKey = key
            ' Leert die Knoten, um Platz für Unterordner zu schaffen
            Me.Nodes.Clear()
            ' Füge einen Platzhalterknoten hinzu, der später durch die Unterordner ersetzt wird
            Dim unused = Me.Nodes.Add(New TreeNode("Ordner laden ..."))
        End Sub

        ''' <summary>
        ''' Lädt die Unterordner des aktuellen Ordners und fügt sie als FolderNode-Knoten hinzu.
        ''' </summary>
        Public Sub LoadSubfolders()
            Try
                ' Durchlaufe alle Unterverzeichnisse des aktuellen Ordners
                For Each dir As String In Directory.GetDirectories(Me.FullPath)
                    ' Füge für jedes Unterverzeichnis einen neuen FolderNode hinzu
                    Dim unused = Me.Nodes.Add(New FolderNode(Path.GetFileName(dir), dir))
                Next
            Catch ex As UnauthorizedAccessException
                ' Zugriff verweigert – Ordner wird übersprungen
                ' Hier könnte optional Logging oder eine Benutzerbenachrichtigung erfolgen
            End Try
        End Sub

    End Class

End Namespace
