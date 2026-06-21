' --------------------------------------------------------------------------------------------------------
' Datei: TransparentLabel.vb
' Author: Andreas Sauer
' Datum: 05.05.2026
' --------------------------------------------------------------------------------------------------------

Namespace TransparentLabelControl

    ''' <summary>
    ''' Ein Steuerelement zum Anzeigen eines Textes mit durchscheinendem Hintergrund.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Ein Steuerelement zum Anzeigen eines Textes mit durchscheinendem Hintergrund.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(TransparentLabel), "TransparentLabelControl.TransparentLabel.bmp")>
    Public Class TransparentLabel

        Inherits System.Windows.Forms.Label

        ' Konstante für den erweiterten Fensterstil WS_EX_TRANSPARENT, um die Transparenz zu ermöglichen.
        Private Const WS_EX_TRANSPARENT As System.Int32 = &H20

#Region "Definition der öffentlichen Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="TransparentLabel"/> -Klasse.
        ''' </summary>
        Public Sub New()

            ' Eigene Stil-Einstellungen werden direkt in der Konstruktion gesetzt,
            ' damit Transparenz und Zeichenverhalten korrekt konfiguriert sind.
            Me.Name = NameOf(TransparentLabel)
            Me.InitializeStyles()
            Me.BackColor = System.Drawing.Color.Transparent

        End Sub

#End Region

#Region "Definition der Eigenschaften"

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        <System.ComponentModel.DefaultValue(GetType(System.Drawing.Color), "Transparent")>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Overrides Property BackColor As System.Drawing.Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As System.Drawing.Color)
                If value <> System.Drawing.Color.Transparent AndAlso value <> System.Drawing.Color.Empty Then
                    Throw New System.ArgumentException("Für dieses Steuerelement ist nur Color.Transparent zulässig.", NameOf(value))
                End If

                ' Für dieses Steuerelement wird BackColor immer transparent erzwungen.
                MyBase.BackColor = System.Drawing.Color.Transparent
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Overrides Property BackgroundImage As System.Drawing.Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As System.Drawing.Image)
                MyBase.BackgroundImage = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Overrides Property BackgroundImageLayout As System.Windows.Forms.ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(value As System.Windows.Forms.ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Shadows Property FlatStyle As System.Windows.Forms.FlatStyle
            Get
                Return MyBase.FlatStyle
            End Get
            Set(value As System.Windows.Forms.FlatStyle)
                MyBase.FlatStyle = value
            End Set
        End Property

#End Region

#Region "Definition der internen Methoden"

        ''' <summary>
        ''' Konfiguriert die relevanten <see cref="System.Windows.Forms.ControlStyles"/> für das Label.
        ''' </summary>
        ''' <remarks>
        ''' <para> Die Kombination der Styles sorgt dafür, dass das Steuerelement seinen Hintergrund nicht selbst opak
        ''' zeichnet und transparente Hintergründe unterstützt. </para> <para> Das Deaktivieren von <c>
        ''' OptimizedDoubleBuffer</c> hilft in diesem Szenario, unerwünschte Übermal-Effekte bei transparenter Darstellung
        ''' zu vermeiden. </para>
        ''' </remarks>
        Private Sub InitializeStyles()

            ' Relevante Transparenz-Styles in einem Schritt setzen.
            Me.SetStyle(System.Windows.Forms.ControlStyles.Opaque Or
                        System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)

            ' Doppelbuffering hier bewusst deaktivieren, damit die Transparenzdarstellung
            ' konsistent mit dem Eltern-Steuerelement funktioniert.
            Me.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, False)

            ' Style-Änderungen auf das Handle anwenden.
            Me.UpdateStyles()

        End Sub

        ''' <summary>
        ''' Liefert angepasste Erstellungsparameter für das Fensterhandle des Steuerelements.
        ''' </summary>
        ''' <remarks>
        ''' <para> Das Setzen von <c> WS_EX_TRANSPARENT</c> sorgt dafür, dass zuerst das Eltern-Steuerelement gezeichnet
        ''' wird und der Hintergrund dadurch durchscheinen kann. </para> <para> <b> Weitere Infos unter:</b><br/>
        ''' <see href="https://stackoverflow.com/questions/511320/transparent-control-backgrounds-on-a-vb-net-gradient-filled-form"/>
        ''' <br/> und<br/> <see href="https://learn.microsoft.com/de-de/windows/win32/winmsg/extended-window-styles"/>
        ''' </para>
        ''' </remarks>
        ''' <value>
        ''' Die angepassten <see cref="CreateParams"/> mit aktiviertem WS_EX_TRANSPARENT-Stil.
        ''' </value>
        Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
            Get
                ' Ausgangswerte vom Basistyp übernehmen.
                Dim cp As System.Windows.Forms.CreateParams = MyBase.CreateParams

                ' Erweiterten Fensterstil WS_EX_TRANSPARENT hinzufügen.
                ' Quelle: https://learn.microsoft.com/de-de/windows/win32/winmsg/extended-window-styles
                cp.ExStyle = cp.ExStyle Or WS_EX_TRANSPARENT

                ' Angepasste Erstellungsparameter an WinForms zurückgeben.
                Return cp
            End Get
        End Property

#End Region

    End Class

End Namespace
