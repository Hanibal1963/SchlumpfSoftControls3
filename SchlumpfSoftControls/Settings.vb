' --------------------------------------------------------------------------------------------------------
' Datei: Settings.vb
' Author: Andreas Sauer
' Datum: 23.08.2026
' --------------------------------------------------------------------------------------------------------

Imports System.Configuration

Namespace My

    ''' <summary>
    ''' Ermöglicht die Behandlung bestimmter Ereignisse der Einstellungsklasse
    ''' </summary>
    Partial Friend NotInheritable Class MySettings

        ''' <summary>
        ''' Initialisiert eine neue Instanz der MySettings-Klasse.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Führt die erforderlichen Bereinigungen durch, bevor die MySettings-Klasse von der Garbage Collection
        ''' entfernt wird.
        ''' </summary>
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        ''' <summary>
        ''' Wird ausgelöst, nachdem der Wert einer Einstellung geändert wurde.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub MySettings_PropertyChanged(sender As System.Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged

        End Sub

        ''' <summary>
        ''' Wird ausgelöst, bevor der Wert einer Einstellung geändert wird.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub MySettings_SettingChanging(sender As System.Object, e As SettingChangingEventArgs) Handles Me.SettingChanging

        End Sub

        ''' <summary>
        ''' Wird ausgelöst, nachdem die Einstellungswerte geladen wurden.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub MySettings_SettingsLoaded(sender As System.Object, e As SettingsLoadedEventArgs) Handles Me.SettingsLoaded
        End Sub

        ''' <summary>
        ''' Wird ausgelöst, bevor die Einstellungswerte gespeichert werden.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub MySettings_SettingsSaving(sender As System.Object, e As CancelEventArgs) Handles Me.SettingsSaving
        End Sub

    End Class

End Namespace
