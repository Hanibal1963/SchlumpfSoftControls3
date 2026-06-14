' --------------------------------------------------------------------------------------------------------
' Datei: SchlumpfSoftControlsPackage.vb
' Author: Andreas Sauer
' Datum: 14.06.2026
' --------------------------------------------------------------------------------------------------------

''' <summary>
''' Dies ist die Klasse, die das von dieser Assembly bereitgestellte Paket implementiert.
''' </summary>
''' <remarks>
''' <para>
''' Die Mindestanforderung, damit eine Klasse als gültiges Paket für Visual Studio gilt,
''' ist die Implementierung der IVsPackage-Schnittstelle sowie die Registrierung bei der Shell.
''' Dieses Paket verwendet dazu die Hilfsklassen, die im Managed Package Framework (MPF)
''' definiert sind: Es leitet von der Package-Klasse ab, die die Implementierung der
''' IVsPackage-Schnittstelle bereitstellt, und verwendet die im Framework definierten
''' Registrierungsattribute, um sich selbst und seine Komponenten bei der Shell zu
''' registrieren. Diese Attribute teilen dem pkgdef-Erstellungsprogramm mit,
''' welche Daten in die .pkgdef-Datei geschrieben werden sollen.
''' </para>
''' <para>
''' Damit das Paket in VS geladen wird, muss es in der Datei .vsixmanifest über
''' &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; referenziert werden.
''' </para>
''' </remarks>
<Microsoft.VisualStudio.Shell.PackageRegistration(UseManagedResourcesOnly:=True, AllowsBackgroundLoading:=True)>
<System.Runtime.InteropServices.Guid(SchlumpfSoftControlsPackage.PackageGuidString)>
Public NotInheritable Class SchlumpfSoftControlsPackage
    Inherits Microsoft.VisualStudio.Shell.AsyncPackage

    ''' <summary>
    ''' Paket-GUID
    ''' </summary>
    Public Const PackageGuidString As String = "44cc5e59-1bb1-4a68-91f8-9001571baf29"

#Region "Package Members"

    ''' <summary>
    ''' Initialisierung des Pakets; diese Methode wird direkt aufgerufen, nachdem das Paket eingebunden wurde.
    ''' Hier können Sie den gesamten Initialisierungscode platzieren, der auf von Visual Studio bereitgestellte Dienste zugreift.
    ''' </summary>
    ''' <param name="cancellationToken">Ein Abbruchtoken zur Überwachung eines Abbruchs der Initialisierung, der beim Herunterfahren von VS auftreten kann.</param>
    ''' <param name="progress">Ein Anbieter für Fortschrittsaktualisierungen.</param>
    ''' <returns>Eine Aufgabe, die die asynchrone Initialisierungsarbeit des Pakets repräsentiert, oder eine bereits abgeschlossene Aufgabe, falls keine Arbeit anfällt. Diese Methode darf nicht null zurückgeben.</returns>
    Protected Overrides Async Function InitializeAsync(cancellationToken As System.Threading.CancellationToken, progress As System.IProgress(Of Microsoft.VisualStudio.Shell.ServiceProgressData)) As System.Threading.Tasks.Task
        ' Bei asynchroner Initialisierung kann der aktuelle Thread an dieser Stelle ein Hintergrundthread sein.
        ' Führen Sie alle Initialisierungen, die den UI-Thread erfordern, erst nach dem Wechsel auf den UI-Thread aus.
        Await Me.JoinableTaskFactory.SwitchToMainThreadAsync()
    End Function

#End Region

End Class
