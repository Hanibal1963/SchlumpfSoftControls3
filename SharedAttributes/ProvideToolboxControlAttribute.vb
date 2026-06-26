' --------------------------------------------------------------------------------------------------------
' Datei: ProvideToolboxControlAttribute.vb
' Author: Andreas Sauer
' Datum: 26.06.2026
' --------------------------------------------------------------------------------------------------------

Option Strict On
Option Explicit On
Option Infer On
Option Compare Binary

''' <summary>
''' Dieses Attribut fügt einen ToolboxControlsInstaller-Schlüssel für die Assembly hinzu, um Toolbox-Steuerelemente aus
''' der Assembly zu installieren.
''' </summary>
''' <remarks>
''' Beispiel: [$(Rootkey)\ToolboxControlsInstaller\$FullAssemblyName$] "Codebase"="$path$" "WpfControls"="1"
''' </remarks>
<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False, Inherited:=True)>
<System.Runtime.InteropServices.ComVisibleAttribute(False)>
Public NotInheritable Class ProvideToolboxControlAttribute

    Inherits Microsoft.VisualStudio.Shell.RegistrationAttribute

    Private Const ToolboxControlsInstallerPath As String = "ToolboxControlsInstaller"

    Private _isWpfControls As Boolean
    Private _name As String

    ''' <summary>
    ''' Erstellt ein neues ProvideToolboxControl-Attribut zur Registrierung der Assembly für den
    ''' Toolbox-Controls-Installer.
    ''' </summary>
    ''' <param name="name">Name für die Steuerelemente</param>
    ''' <param name="isWpfControls">Gibt an, ob es sich um WPF-Steuerelemente handelt</param>
    ''' <exception cref="ArgumentNullException">Wird ausgelöst, wenn name Nothing ist</exception>
    Public Sub New(name As String, isWpfControls As Boolean)

        If name Is Nothing Then
            Throw New ArgumentException("name")
        End If

        Me.Name = name
        Me.IsWpfControls = isWpfControls

    End Sub

    ''' <summary>
    ''' Ruft ab oder legt fest, ob die Toolbox-Steuerelemente für WPF bestimmt sind.
    ''' </summary>
    Private Property IsWpfControls As Boolean
        Get
            Return Me._isWpfControls
        End Get
        Set(value As Boolean)
            Me._isWpfControls = value
        End Set
    End Property

    ''' <summary>
    ''' Ruft den Namen für die Steuerelemente ab oder legt diesen fest.
    ''' </summary>
    Private Property Name As String
        Get
            Return Me._name
        End Get
        Set(value As String)
            Me._name = value
        End Set
    End Property

    ''' <summary>
    ''' Wird aufgerufen, um dieses Attribut beim angegebenen Kontext zu registrieren.
    ''' </summary>
    ''' <param name="context">Kontext für die Registrierung</param>
    ''' <exception cref="ArgumentNullException">Wird ausgelöst, wenn context Nothing ist</exception>
    Public Overrides Sub Register(context As Microsoft.VisualStudio.Shell.RegistrationAttribute.RegistrationContext)

        If context Is Nothing Then
            Throw New ArgumentNullException("context")
        End If

        Using key As Key = context.CreateKey(String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}\{1}",
                                                         ToolboxControlsInstallerPath,
                                                         context.ComponentType.Assembly.FullName))

            key.SetValue(String.Empty, Me.Name)
            key.SetValue("Codebase", context.CodeBase)

            If Me.IsWpfControls Then
                key.SetValue("WPFControls", "1")
            End If

        End Using

    End Sub

    ''' <summary>
    ''' Wird aufgerufen, um die Registrierung dieses Attributs beim angegebenen Kontext aufzuheben.
    ''' </summary>
    ''' <param name="context">Kontext für die Deregistrierung</param>
    Public Overrides Sub Unregister(context As Microsoft.VisualStudio.Shell.RegistrationAttribute.RegistrationContext)

        If context IsNot Nothing Then

            context.RemoveKey(String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}\{1}",
                                                         ToolboxControlsInstallerPath,
                                                         context.ComponentType.Assembly.FullName))
        End If

    End Sub

End Class