
Imports System.Linq
Imports Microsoft.VisualBasic

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Steuerelement zur Anzeige und Navigation der lokalen Verzeichnisstruktur.
    ''' </summary>
    ''' <remarks>
    ''' <para>Das Control zeigt eine hierarchische Baumstruktur mit: </para>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>dem Wurzelknoten "Dieser Computer", allen verfügbaren Laufwerken,
    ''' sowie bekannten Spezialordnern (z. B. Desktop, Dokumente, Bilder). Funktionen:
    ''' </description>
    '''  </item>
    '''  <item>
    '''   <description>Lazy-Loading von Unterordnern beim Expandieren eines Knotens.
    ''' </description>
    '''  </item>
    '''  <item>
    '''   <description>Live-Aktualisierung durch <see
    ''' cref="System.IO.FileSystemWatcher"/> bei Änderungen im Dateisystem.
    ''' </description>
    '''  </item>
    '''  <item>
    '''   <description>Ereignis <see cref="SelectedPathChanged"/> bei Auswahl eines
    ''' Knotens mit gültigem Pfad. </description>
    '''  </item>
    '''  <item>
    '''   <description>Öffnen und Selektieren eines Pfads über <see
    ''' cref="ExpandPath(String)"/>. </description>
    '''  </item>
    ''' </list>
    ''' <para><b>Hinweise:</b> </para>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Der Wurzelknoten ("Dieser Computer") besitzt keinen Pfad.
    ''' </description>
    '''  </item>
    '''  <item>
    '''   <description>Für geöffnete Knoten wird automatisch ein FileSystemWatcher
    ''' angelegt; beim Zuklappen wird er entfernt.</description>
    '''  </item>
    ''' </list>
    ''' </remarks>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Stellt ein Steuerelement zur Anzeige und Navigation der Verzeichnisstruktur des Computers bereit.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(ExplorerTreeViewControl.ExplorerTreeView), "ExplorerTreeView.bmp")>
    Public Class ExplorerTreeView

        Inherits System.Windows.Forms.UserControl

        Implements System.IDisposable

#Region "Variablen"

        Private disposedValue As Boolean = False
        Private ReadOnly _FileSystemWatchers As New System.Collections.Generic.Dictionary(Of String, System.IO.FileSystemWatcher)
        Private _SelectedPath As String

#End Region

#Region "Ereignisse"

        ''' <summary>
        ''' Ereignis, das ausgelöst wird, wenn sich der ausgewählte Pfad geändert hat.
        ''' </summary>
        ''' <remarks>
        ''' <para>Dieses Ereignis wird verwendet, um andere Steuerelemente oder Logik zu
        ''' benachrichtigen, wenn der Benutzer einen anderen Knoten im TreeView auswählt.
        ''' </para>
        ''' <para>Es ermöglicht eine reaktive Programmierung, bei der andere Teile der
        ''' Anwendung auf Änderungen im ausgewählten Pfad reagieren können.</para>
        ''' </remarks>
        <System.ComponentModel.Description("Wird ausgelöst, wenn sich der ausgewählte Pfad geändert hat.")>
        <System.ComponentModel.Browsable(True)>
        Public Event SelectedPathChanged(sender As Object, e As SelectedPathChangedEventArgs)

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Gibt den aktuell ausgewählten Pfad zurück.
        ''' </summary>
        ''' <returns></returns>
        <System.ComponentModel.Browsable(False)>
        Public ReadOnly Property SelectedPath As System.String
            Get
                Return Me._SelectedPath
            End Get
        End Property

        ''' <summary>
        ''' Gibt die Farbe der Linien zwischen den Knoten zurück oder legt diese fest.
        ''' </summary>
        ''' <remarks>
        ''' Diese Eigenschaft beeinflusst die Farbe der Verbindungslinien im TreeView.<br/>
        ''' Eine Änderung wirkt sich sofort auf die Darstellung aus.
        ''' </remarks>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt die Farbe der Linien zwischen den Knoten zurück oder legt diese fest.")>
        <System.ComponentModel.Browsable(True)>
        Public Property LineColor As System.Drawing.Color
            Get
                Return Me.TV.LineColor
            End Get
            Set(value As System.Drawing.Color)
                Me.TV.LineColor = value
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob Linien zwischen den Knoten angezeigt werden.
        ''' </summary>
        ''' <remarks>
        ''' Wenn <see langword="True"/>, werden Verbindungslinien zwischen Eltern- und
        ''' Kindknoten dargestellt.
        ''' </remarks>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt an, ob Linien zwischen den Knoten angezeigt werden.")>
        <System.ComponentModel.Browsable(True)>
        Public Property ShowLines As Boolean
            Get
                Return Me.TV.ShowLines
            End Get
            Set(value As Boolean)
                Me.TV.ShowLines = value
            End Set
        End Property

        ''' <summary>
        ''' Legt fest, ob die Plus- und Minuszeichen zum Anzeigen von Unterknoten angezeigt
        ''' werden.
        ''' </summary>
        ''' <remarks>
        ''' Wenn <see langword="True"/>, werden Expand-/Collapse-Glyphen (Plus/Minus) neben
        ''' Knoten angezeigt.
        ''' </remarks>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt fest ob die Plus- und Minuszeichen zum Anzeigen von Unterknoten angezeigt werden.")>
        <System.ComponentModel.Browsable(True)>
        Public Property ShowPlusMinus As Boolean
            Get
                Return Me.TV.ShowPlusMinus
            End Get
            Set(value As Boolean)
                Me.TV.ShowPlusMinus = value
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob Linien zwischen den Stammknoten angezeigt werden.
        ''' </summary>
        ''' <remarks>
        ''' Betrifft ausschließlich die Linien zwischen den obersten Knotenebenen
        ''' (Root-Level).
        ''' </remarks>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt an, ob Linien zwischen den Stammknoten angezeigt werden.")>
        <System.ComponentModel.Browsable(True)>
        Public Property ShowRootLines As Boolean
            Get
                Return Me.TV.ShowRootLines
            End Get
            Set(value As Boolean)
                Me.TV.ShowRootLines = value
            End Set
        End Property

        ''' <summary>
        ''' Ruft den Abstand für das Einrücken der einzelnen Ebenen von untergeordneten
        ''' Strukturknoten ab oder legt diesen fest.
        ''' </summary>
        ''' <remarks>
        ''' Bestimmt die horizontale Einrückung pro Ebene in Pixel.
        ''' </remarks>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Ruft den Abstand für das Einrücken der einzelnen Ebenen von untergeordneten Strukturknoten ab oder legt diesen fest.")>
        <System.ComponentModel.Browsable(True)>
        Public Property Indent As System.Int32
            Get
                Return Me.TV.Indent
            End Get
            Set(value As System.Int32)
                Me.TV.Indent = value
            End Set
        End Property

        ''' <summary>
        ''' Ruft die Höhe des jeweiligen Strukturknotens im Strukturansicht-Steuerelement ab
        ''' oder legt diese fest.
        ''' </summary>
        ''' <remarks>
        ''' Die Knotenhöhe wird in Pixel angegeben und beeinflusst die vertikale Dichte der
        ''' Einträge.
        ''' </remarks>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Ruft die Höhe des jeweiligen Strukturknotens im Strukturansicht-Steuerelement ab oder legt diese fest.")>
        <System.ComponentModel.Browsable(True)>
        Public Property ItemHeight As System.Int32
            Get
                Return Me.TV.ItemHeight
            End Get
            Set(value As System.Int32)
                Me.TV.ItemHeight = value
            End Set
        End Property

        ''' <summary>
        ''' Legt die Hintergrundfarbe für das Steuerelement fest oder gibt diese zurück.
        ''' </summary>
        ''' <remarks>
        ''' Die Hintergrundfarbe des UserControls wird übernommen und auf das interne
        ''' TreeView angewendet, sodass beide konsistent erscheinen.
        ''' </remarks>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Hintergrundfarbe für das Steuerelement fest oder gibt diese zurück.")>
        <System.ComponentModel.Browsable(True)>
        Public Overrides Property BackColor As System.Drawing.Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.BackColor = value
                Me.TV.BackColor = value
            End Set
        End Property

        ''' <summary>
        ''' Legt die Vordergrundfarbe für das Anzeigen von Text fest oder gibt diese zurück.
        ''' </summary>
        ''' <remarks>
        ''' Die Textfarbe des UserControls wird übernommen und auf das interne TreeView
        ''' angewendet.
        ''' </remarks>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Vordergrundfarbe für das Anzeigen von Text fest oder gibt diese zurück.")>
        <System.ComponentModel.Browsable(True)>
        Public Overrides Property ForeColor As System.Drawing.Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.ForeColor = value
                Me.TV.ForeColor = value
            End Set
        End Property

        ''' <summary>
        ''' Legt die Schriftart für den Text im Steuerelement fest oder gibt diese zurück.
        ''' </summary>
        ''' <remarks>
        ''' Die Schriftart wird sowohl auf das UserControl als auch auf das interne TreeView
        ''' angewendet, um eine einheitliche Darstellung sicherzustellen.
        ''' </remarks>
        ''' <value>
        ''' Aktuell verwendete Schriftart.
        ''' </value>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Schriftart für den Text im Steuerelement fest oder gibt diese zurück.")>
        <System.ComponentModel.Browsable(True)>
        Public Overrides Property Font As System.Drawing.Font
            Get
                Return MyBase.Font
            End Get
            Set(value As System.Drawing.Font)
                MyBase.Font = value
                Me.TV.Font = value
            End Set
        End Property

        ''' <summary>
        ''' Ist für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property

        ''' <summary>
        ''' Ist für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImage As System.Drawing.Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As System.Drawing.Image)
                MyBase.BackgroundImage = value
            End Set
        End Property

        ''' <summary>
        ''' Ist für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImageLayout As System.Windows.Forms.ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(value As System.Windows.Forms.ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Konstruktor für das ExplorerTreeView-Steuerelement.
        ''' </summary>
        ''' <remarks>
        ''' <para>Dieser Konstruktor initialisiert das Steuerelement und lädt die
        ''' erforderlichen Bilder. </para>
        ''' <para>Außerdem wird der Wurzelknoten des TreeViews gesetzt, um die Struktur des
        ''' Steuerelements zu definieren.</para>
        ''' </remarks>
        Public Sub New()
            Me.InitializeComponent()
            Me.LoadImages()
            Me.SetRootNode()
        End Sub

        ''' <summary>
        ''' Öffnet und selektiert den Knoten zum angegebenen Verzeichnispfad.
        ''' </summary>
        ''' <remarks>
        ''' Funktioniert auch bei noch nicht geladenen Unterknoten.
        ''' </remarks>
        ''' <param name="Path">Vollständiger Pfad der göffnet werden soll.</param>
        ''' <returns>
        ''' <see langword="true"/>, wenn der Knoten gefunden wurde, sonst <see
        ''' langword="false"/>
        ''' </returns>
        Public Function ExpandPath(Path As String) As Boolean
            If String.IsNullOrWhiteSpace(Path) Then Return False
            Dim lastpath As String = String.Empty
            Dim lastnode As System.Windows.Forms.TreeNode = Me.TV.Nodes.Item(0)
            lastnode.Expand()
            Dim foundNode As System.Windows.Forms.TreeNode
            For Each pathsegment As String In Me.GetPathSegments(Path)
                lastpath = System.IO.Path.Combine(lastpath, pathsegment)
                foundNode = Me.FindNodeByPath(lastnode.Nodes, lastpath)
                If IsNothing(foundNode) Then Return False
                foundNode.Expand()
                lastnode = foundNode
            Next
            Me.TV.SelectedNode = lastnode
            Return True
        End Function


#End Region

#Region "Interne Methoden"

        Private Function GetPathSegments(Path As String) As System.Collections.Generic.List(Of String)
            Dim dirInfo As New System.IO.DirectoryInfo(Path)
            Dim result As New System.Collections.Generic.List(Of String)
            While dirInfo IsNot Nothing AndAlso Not String.IsNullOrEmpty(dirInfo.Name)
                result.Insert(0, dirInfo.Name)
                dirInfo = dirInfo.Parent
            End While
            Return result
        End Function

        Private Function FindNodeByPath(Nodes As System.Windows.Forms.TreeNodeCollection, SearchPath As String) As System.Windows.Forms.TreeNode
            For Each node As System.Windows.Forms.TreeNode In Nodes
                If String.Equals(Me.GetDirectoryPath(node), SearchPath, System.StringComparison.OrdinalIgnoreCase) Then
                    Return node
                End If
                Dim found As System.Windows.Forms.TreeNode = Me.FindNodeByPath(node.Nodes, SearchPath)
                If found IsNot Nothing Then
                    Return found
                End If
            Next
            Return Nothing
        End Function

        Private Function GetDirectoryPath(node As System.Windows.Forms.TreeNode) As String
            Select Case True
                Case TypeOf node Is ComputerNode : Return String.Empty
                Case TypeOf node Is DriveNode : Return CType(node, DriveNode).FullPath
                Case TypeOf node Is SpecialFolderNode : Return CType(node, SpecialFolderNode).FullPath
                Case TypeOf node Is FolderNode : Return CType(node, FolderNode).FullPath
                Case Else : Return String.Empty
            End Select
        End Function

        Private Sub SetRootNode()
            Me.TV.Nodes.Clear()
            Dim rootnode As New ComputerNode With {.ImageKey = $"Computer", .SelectedImageKey = $"Computer"}
            Dim unused = Me.TV.Nodes.Add(rootnode)
            Me.TV.Nodes.Item(0).Expand()
        End Sub

        Private Sub LoadImages()
            Me.TV.ImageList.Images.Clear()
            Me.TV.ImageList.Images.Add(ICON_COMPUTER, My.Resources.Computer)
            Me.TV.ImageList.Images.Add(ICON_DRIVE_SYSTEM, My.Resources.DriveSystem)
            Me.TV.ImageList.Images.Add(ICON_DRIVE_FIXED, My.Resources.DriveFixed)
            Me.TV.ImageList.Images.Add(ICON_DRIVE_CDROM, My.Resources.DriveCDRom)
            Me.TV.ImageList.Images.Add(ICON_DRIVE_REMOVABLE, My.Resources.DriveRemovable)
            Me.TV.ImageList.Images.Add(ICON_DRIVE_NETWORK, My.Resources.DriveNetwork)
            Me.TV.ImageList.Images.Add(ICON_DRIVE_RAM, My.Resources.DriveRamDisk)
            Me.TV.ImageList.Images.Add(ICON_DRIVE_FLOPPY, My.Resources.DriveDisk)
            Me.TV.ImageList.Images.Add(ICON_DRIVE_UNKNOWN, My.Resources.DriveUnknown)
            Me.TV.ImageList.Images.Add(ICON_FOLDER_FOLDER, My.Resources.Folder)
            Me.TV.ImageList.Images.Add(ICON_FOLDER_DESKTOP, My.Resources.FolderDesktop)
            Me.TV.ImageList.Images.Add(ICON_FOLDER_DOCUMENTS, My.Resources.FolderDocuments)
            Me.TV.ImageList.Images.Add(ICON_FOLDER_DOWNLOADS, My.Resources.FolderDownloads)
            Me.TV.ImageList.Images.Add(ICON_FOLDER_MUSIC, My.Resources.FolderMusic)
            Me.TV.ImageList.Images.Add(ICON_FOLDER_PICTURES, My.Resources.FolderPictures)
            Me.TV.ImageList.Images.Add(ICON_FOLDER_VIDEOS, My.Resources.FolderVideos)
        End Sub

        Private Sub LoadSubfolders(Node As System.Windows.Forms.TreeNode)
            Node.Nodes.Clear()
            Select Case True
                Case TypeOf Node Is ComputerNode
                    CType(Node, ComputerNode).LoadSpecialFolders()
                    CType(Node, ComputerNode).LoadDrives()
                Case TypeOf Node Is SpecialFolderNode
                    CType(Node, SpecialFolderNode).LoadSubfolders()
                Case TypeOf Node Is DriveNode
                    CType(Node, DriveNode).LoadSubfolders()
                Case TypeOf Node Is FolderNode
                    CType(Node, FolderNode).LoadSubfolders()
            End Select
        End Sub

        Private Sub CreateFileSystemWatcher(FolderPath As String)
            If String.IsNullOrEmpty(FolderPath) OrElse Not System.IO.Directory.Exists(FolderPath) Then Return
            If Me._FileSystemWatchers.ContainsKey(FolderPath) Then Return
            Try
                Dim FSW As New System.IO.FileSystemWatcher(FolderPath) With {
                        .NotifyFilter = System.IO.NotifyFilters.DirectoryName,
                        .IncludeSubdirectories = False}
                AddHandler FSW.Created, AddressOf Me.FSW_DirectoryChanged
                AddHandler FSW.Deleted, AddressOf Me.FSW_DirectoryChanged
                AddHandler FSW.Renamed, AddressOf Me.FSW_DirectoryChanged
                Me._FileSystemWatchers.Add(FolderPath, FSW)
                FSW.EnableRaisingEvents = True
            Catch ex As System.Exception
                System.Diagnostics.Debug.WriteLine($"Fehler beim Erstellen des FileSystemWatchers: {ex.Message}")
            End Try
        End Sub

        Private Sub RemoveFileSystemWatchers(FolderPath As String)
            Dim toRemove As New System.Collections.Generic.List(Of String)
            Me.FindWatchersToRemove(FolderPath, toRemove)
            Me.RemoveAndDisposeWatchers(toRemove)
        End Sub

        Private Sub FindWatchersToRemove(FolderPath As String, toRemove As System.Collections.Generic.List(Of String))
            For Each watcherPath In Me._FileSystemWatchers.Keys
                If watcherPath.Equals(FolderPath, System.StringComparison.OrdinalIgnoreCase) OrElse
                       watcherPath.StartsWith(FolderPath & System.IO.Path.DirectorySeparatorChar, System.StringComparison.OrdinalIgnoreCase) Then
                    toRemove.Add(watcherPath)
                End If
            Next
        End Sub

        Private Sub RemoveAndDisposeWatchers(toRemove As System.Collections.Generic.List(Of String))
            For Each watcherPath In toRemove
                Dim watcher = Me._FileSystemWatchers(watcherPath)
                watcher.EnableRaisingEvents = False
                Me.RemoveWatcherHandlers(watcher)
                watcher.Dispose()
                Dim unused = Me._FileSystemWatchers.Remove(watcherPath)
            Next
        End Sub

        Private Sub RemoveWatcherHandlers(watcher As System.IO.FileSystemWatcher)
            RemoveHandler watcher.Created, AddressOf Me.FSW_DirectoryChanged
            RemoveHandler watcher.Deleted, AddressOf Me.FSW_DirectoryChanged
            RemoveHandler watcher.Renamed, AddressOf Me.FSW_DirectoryChanged
        End Sub

        Private Sub FSW_DirectoryChanged(sender As Object, e As System.IO.FileSystemEventArgs)
            If Me.TV.InvokeRequired Then
                Dim unused = Me.TV.Invoke(New System.Windows.Forms.MethodInvoker(Sub() Me.FSW_DirectoryChanged(sender, e)))
                Return
            End If
            Dim node As System.Windows.Forms.TreeNode = Me.FindNodeByPath(Me.TV.Nodes, CType(sender, System.IO.FileSystemWatcher).Path)
            Select Case True
                Case TypeOf node Is DriveNode : Me.LoadSubfolders(node)
                Case TypeOf node Is SpecialFolderNode : Me.LoadSubfolders(node)
                Case TypeOf node Is FolderNode : Me.LoadSubfolders(node)
            End Select
        End Sub

        Private Sub TV_BeforeExpand(sender As Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TV.BeforeExpand
            Me.LoadSubfolders(e.Node) ' Lädt die untergeordneten Knoten des aktuellen Knotens.
        End Sub

        Private Sub TV_AfterExpand(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles TV.AfterExpand
            Me.CreateFileSystemWatcher(Me.GetDirectoryPath(e.Node)) ' Einen FileSystemWatcher für das geöffnete Verzeichnis erstellen
        End Sub

        Private Sub TV_AfterCollapse(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles TV.AfterCollapse
            Me.RemoveFileSystemWatchers(Me.GetDirectoryPath(e.Node))
        End Sub

        Private Sub TV_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles TV.AfterSelect
            Dim selectedpath As String = Me.GetDirectoryPath(e.Node)
            If String.IsNullOrEmpty(selectedpath) Then Exit Sub
            Me._SelectedPath = selectedpath
            RaiseEvent SelectedPathChanged(Me, New SelectedPathChangedEventArgs(selectedpath))
        End Sub

        Private Sub DW_DriveAdded(sender As Object, e As SchlumpfSoft.Controls.DriveWatcherControl.DriveAddedEventArgs) Handles DW.DriveAdded
            Dim newNode As New DriveNode(New System.IO.DriveInfo(e.DriveName)) With {.Tag = e.DriveName}
            Dim inserted As Boolean = False
            For i As System.Int32 = 0 To Me.TV.Nodes.Item(0).Nodes.Count - 1
                Dim currNode As System.Windows.Forms.TreeNode = Me.TV.Nodes.Item(0).Nodes(i)
                If TypeOf currNode Is DriveNode AndAlso
                        String.Compare(
                        currNode.Tag.ToString,
                        newNode.Tag.ToString,
                        System.StringComparison.OrdinalIgnoreCase) > 0 Then
                    Me.TV.Nodes.Item(0).Nodes.Insert(i, newNode)
                    inserted = True
                    Exit For
                End If
            Next
            If Not inserted Then
                Dim unused = Me.TV.Nodes.Item(0).Nodes.Add(newNode)
            End If
        End Sub

        Private Sub DW_DriveRemoved(sender As Object, e As SchlumpfSoft.Controls.DriveWatcherControl.DriveRemovedEventArgs) Handles DW.DriveRemoved
            For Each node As System.Windows.Forms.TreeNode In Me.TV.Nodes.Item(0).Nodes
                If TypeOf node Is DriveNode Then
                    If CType(node, DriveNode).Tag.ToString() = e.DriveName Then
                        CType(node, DriveNode).Remove()
                    End If
                End If
            Next
        End Sub

#End Region

    End Class

End Namespace

