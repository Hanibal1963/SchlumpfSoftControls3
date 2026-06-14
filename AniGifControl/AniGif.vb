' --------------------------------------------------------------------------------------------------------
' Datei: AniGif.vb
' Author: Andreas Sauer
' Datum: 25.04.2026
' --------------------------------------------------------------------------------------------------------
Namespace AniGifControl

    ''' <summary>
    ''' Control zum Anzeigen von animierten Grafiken.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Control zum Anzeigen von animierten Grafiken.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(AniGif), "AniGifControl.AniGif.bmp")>
    Public Class AniGif

        Inherits System.Windows.Forms.UserControl

        Implements System.IDisposable

#Region "Definition der Variablen"

        ' Gemeinsamer Handler für ImageAnimator zum Stoppen/Neu-Registrieren
        Private ReadOnly _AnimationHandler As System.EventHandler = AddressOf Me.OnNextFrame

        ' Benutzerdefinierte Wiedergabegeschwindigkeit (nur aktiv bei CustomDisplaySpeed = True)
        Private _FramesPerSecond As Decimal = 10D
        ' GIF-Frame-Dimension (bei GIFs i. d. R. Time)
        Private _Dimension As System.Drawing.Imaging.FrameDimension
        ' Aktueller Frame-Index für die Timer-basierte Wiedergabe
        Private _Frame As System.Int32
        ' Letzter gültiger Frame-Index des geladenen GIFs
        Private _MaxFrame As System.Int32
        ' Steuert, ob die Animation laufen soll
        Private _Autoplay As Boolean = False
        ' Zoomfaktor in Prozent für den Zoom-Modus
        Private _ZoomFactor As Decimal = 50D
        ' True = Timer/FPS verwenden, False = GIF-interne Verzögerung verwenden
        Private _CustomDisplaySpeed As Boolean = False
        ' Art der Darstellung (Normal, Stretch, Zoom, ...)
        Private _GifSizeMode As ImageSizeMode = ImageSizeMode.Normal
        ' Aktuell angezeigtes Bild (intern immer als Bitmap gehalten)
        Private _Gif As System.Drawing.Bitmap = My.Resources.Standard
        ' Kennzeichnet, ob _Gif vom Control geklont/erstellt wurde und damit freigegeben werden muss
        Private _OwnsGif As Boolean = False
        ' Interner Status, ob aktuell eine Animation aktiv läuft
        Private _IsAnimating As Boolean = False

#End Region

#Region "Definition der öffentlichen Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst wenn die Grafik nicht animiert werden kann.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Wird ausgelöst wenn die Grafik nicht animiert werden kann.")>
        Public Event NoAnimation(sender As Object, e As System.EventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn sich die Eigenschaft Autoplay geändert hat.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Wird ausgelöst wenn sich die Eigenschaft Autoplay geändert hat.")>
        Public Event AutoPlayChanged(sender As Object, e As System.EventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn die Animation gestartet wurde.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Wird ausgelöst wenn die Animation gestartet wurde.")>
        Public Event AnimationStarted(sender As Object, e As System.EventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn die Animation gestoppt wurde.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Wird ausgelöst wenn die Animation gestoppt wurde.")>
        Public Event AnimationStopped(sender As Object, e As System.EventArgs)

#End Region

#Region "Definition der internen Ereignisse"

        Private Event GifChanged()
        Private Event CustomDisplaySpeedChanged()
        Private Event FramesPerSecondChanged()

#End Region

#Region "Defnition der öffentlichen Eigenschaften"

        ''' <summary>
        ''' Steuert, ob die GIF‑Animation automatisch gestartet wird, sobald ein Bild vorhanden ist.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt fest ob die Animation sofort nach dem laden gestartet wird.")>
        Public Property AutoPlay() As Boolean
            Get
                Return _Autoplay
            End Get
            Set(value As Boolean)
                If _Autoplay = value Then Return
                _Autoplay = value
                RaiseEvent AutoPlayChanged(Me, System.EventArgs.Empty)
                Me.UpdateTimerState()
            End Set
        End Property

        ''' <summary>
        ''' Gibt die animierte GIF‑Grafik zurück oder legt diese fest.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt die animierte Gif-Grafik zurück oder legt diese fest.")>
        Public Property Gif() As System.Drawing.Bitmap
            Get
                Return _Gif
            End Get
            Set(value As System.Drawing.Bitmap)
                Me.SetGifImage(value)
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Anzeigemodus (Skalierung/Ausrichtung) der GIF‑Grafik zurück oder legt ihn fest.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt die Art wie die Grafik angezeigt wird zurück oder legt diese fest.")>
        Public Property GifSizeMode() As ImageSizeMode
            Get
                Return _GifSizeMode
            End Get
            Set(value As ImageSizeMode)
                Me.SetGifSizeMode(value)
            End Set
        End Property

        ''' <summary>
        ''' Legt fest, ob die benutzerdefinierte Anzeigegeschwindigkeit (Timer/FPS) oder die im GIF hinterlegte Bildfolge
        ''' (ImageAnimator) verwendet wird.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt fest ob die benutzerdefinierte Anzeigegeschwindigkeit oder die in der Datei festgelegte Geschwindigkeit benutzt wird.")>
        Public Property CustomDisplaySpeed As Boolean
            Get
                Return _CustomDisplaySpeed
            End Get
            Set(value As Boolean)
                Me.SetCustomDisplaySpeed(value)
            End Set
        End Property

        ''' <summary>
        ''' Legt die benutzerdefinierte Anzeigegeschwindigkeit in Bildern pro Sekunde (FPS) fest.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt die benutzerdefinierte Anzeigegeschwindigkeit in Bildern/Sekunde fest wenn CustomDisplaySpeed auf True festgelegt ist.")>
        Public Property FramesPerSecond As Decimal
            Get
                Return _FramesPerSecond
            End Get
            Set(value As Decimal)
                _FramesPerSecond = FunctionDefinitions.CheckFramesPerSecondValue(value)
                RaiseEvent FramesPerSecondChanged()
            End Set
        End Property

        ''' <summary>
        ''' Legt den Zoomfaktor in Prozent fest, mit dem das GIF skaliert wird.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt den Zoomfaktor fest wenn GifSizeMode auf Zoom festgelegt ist.")>
        Public Property ZoomFactor As Decimal
            Get
                Return _ZoomFactor
            End Get
            Set(value As Decimal)
                Me.SetZoomFactor(value)
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property MaximumSize As System.Drawing.Size
            Get
                Return MyBase.MaximumSize
            End Get
            Set(value As System.Drawing.Size)
                MyBase.MaximumSize = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property MinimumSize As System.Drawing.Size
            Get
                Return MyBase.MinimumSize
            End Get
            Set(value As System.Drawing.Size)
                MyBase.MinimumSize = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overloads Property Padding As System.Windows.Forms.Padding
            Get
                Return MyBase.Padding
            End Get
            Set(value As System.Windows.Forms.Padding)
                MyBase.Padding = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property RightToLeft() As System.Windows.Forms.RightToLeft
            Get
                Return MyBase.RightToLeft
            End Get
            Set(value As System.Windows.Forms.RightToLeft)
                MyBase.RightToLeft = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Text() As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property AllowDrop() As Boolean
            Get
                Return MyBase.AllowDrop
            End Get
            Set(value As Boolean)
                MyBase.AllowDrop = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property AutoScrollOffset As System.Drawing.Point
            Get
                Return MyBase.AutoScrollOffset
            End Get
            Set(value As System.Drawing.Point)
                MyBase.AutoScrollOffset = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property AutoSize As Boolean
            Get
                Return MyBase.AutoSize
            End Get
            Set(value As Boolean)
                MyBase.AutoSize = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImage() As System.Drawing.Image
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
        Public Overrides Property BackgroundImageLayout() As System.Windows.Forms.ImageLayout
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
        Public Overrides Property ContextMenuStrip() As System.Windows.Forms.ContextMenuStrip
            Get
                Return MyBase.ContextMenuStrip
            End Get
            Set(value As System.Windows.Forms.ContextMenuStrip)
                MyBase.ContextMenuStrip = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Dock() As System.Windows.Forms.DockStyle
            Get
                Return MyBase.Dock
            End Get
            Set(value As System.Windows.Forms.DockStyle)
                MyBase.Dock = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Font() As System.Drawing.Font
            Get
                Return MyBase.Font
            End Get
            Set(value As System.Drawing.Font)
                MyBase.Font = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property ForeColor() As System.Drawing.Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.ForeColor = value
            End Set
        End Property

#End Region

#Region "Definition der öffentlichen Methoden"

        Public Sub New()
            Me.InitializeComponent() 'Designer-Initialisierung
            Me.SetStyle(Global.System.Windows.Forms.ControlStyles.AllPaintingInWmPaint Or Global.System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer Or Global.System.Windows.Forms.ControlStyles.UserPaint, True)
            Me.UpdateStyles()
        End Sub

        ''' <summary>
        ''' Startet die Animation (falls noch nicht aktiv).
        ''' </summary>
        Public Sub StartAnimation()
            If Not Me.AutoPlay Then
                Me.AutoPlay = True
            End If
        End Sub

        ''' <summary>
        ''' Stoppt die Animation und beendet Timer sowie ImageAnimator.
        ''' </summary>
        Public Sub StopAnimation()
            If Me.AutoPlay Then
                Me.AutoPlay = False
            End If
        End Sub

#End Region

#Region "Definition der internen Methoden"

        Protected Overloads Overrides Sub InitLayout()
            MyBase.InitLayout()
            ' Nach Layout-Initialisierung den korrekten Animationsmodus setzen
            Me.UpdateTimerState()
        End Sub

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)

            MyBase.OnPaint(e)

            ' Null-Schutz
            If _Gif Is Nothing Then Return

            ' Variable für Zeichenfläche
            Dim g As System.Drawing.Graphics = e.Graphics

            ' Größe der Zeichenfläche berechnen
            Dim rectstartsize As System.Drawing.Size = FunctionDefinitions.GetRectStartSize(_GifSizeMode, Me, _Gif, _ZoomFactor / 100)

            ' Startpunkt der Zeichenfläche berechnen
            Dim rectstartpoint As System.Drawing.Point = FunctionDefinitions.GetRectStartPoint(_GifSizeMode, Me, _Gif, rectstartsize)

            ' Qualitätsverbesserung nur bei Skalierung
            If _GifSizeMode = ImageSizeMode.Zoom OrElse _GifSizeMode = ImageSizeMode.Fill Then
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            End If

            ' Zeichenfläche festlegen und Bild zeichnen
            g.DrawImage(_Gif, New System.Drawing.Rectangle(rectstartpoint, rectstartsize))

            ' Bild animieren wenn AutoPlay aktiv und Benutzerdefinierte Geschwindigkeit deaktiviert
            If Not Me.DesignMode AndAlso _Autoplay AndAlso Not _CustomDisplaySpeed Then
                ' im Bild gespeicherte Geschwindigkeit verwenden
                System.Drawing.ImageAnimator.UpdateFrames()
            End If

        End Sub

        Private Sub AniGif_GifChange() Handles Me.GifChanged

            If _Gif Is Nothing Then
                ' Kein Bild vorhanden -> Animationszähler zurücksetzen und sauber stoppen
                _MaxFrame = 0
                _Frame = 0
                Me.UpdateTimerState()
                Me.Invalidate()
                Exit Sub
            End If

            ' prüfen ob das Bild animiert werden kann
            If System.Drawing.ImageAnimator.CanAnimate(_Gif) = False AndAlso _Autoplay = True Then

                ' Anzahl der Frames auf 0 setzen (für nicht animiertes bild)
                _MaxFrame = 0
                _Frame = 0

            Else

                ' Werte für Benutzerdefinierte Geschwindigkeit speichern
                ' (FrameDimension + Anzahl Frames werden für Timer_Tick benötigt)
                _Dimension = New System.Drawing.Imaging.FrameDimension(_Gif.FrameDimensionsList(0))
                _MaxFrame = _Gif.GetFrameCount(_Dimension) - 1
                _Frame = 0

            End If

            ' neu zeichnen
            Me.Invalidate()

            ' Animation starten
            Me.UpdateTimerState()

        End Sub

        Private Sub AniGif_CustomDisplaySpeedChanged() Handles Me.CustomDisplaySpeedChanged

            ' Bei Moduswechsel zwischen Timer/ImageAnimator sofort neu konfigurieren
            Me.UpdateTimerState()

        End Sub

        Private Sub AniGif_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed

            ' Nur intern erzeugte Bitmaps freigeben (fremde Instanzen nicht ungefragt disposen)
            If _OwnsGif AndAlso _Gif IsNot Nothing Then
                _Gif.Dispose()
                _Gif = Nothing
                _OwnsGif = False
            End If

        End Sub

        Private Sub AniGif_FramesPerSecondChanged() Handles Me.FramesPerSecondChanged

            ' Sicherheitsprüfung
            If _FramesPerSecond < 1D Then _FramesPerSecond = 1D

            Me.UpdateTimerState()

        End Sub

        Private Sub OnNextFrame(o As Object, e As System.EventArgs)

            ' Callback von ImageAnimator: nur neu zeichnen, Frame-Umschaltung macht ImageAnimator selbst
            If Me.AutoPlay AndAlso Not Me.DesignMode Then
                Me.Invalidate() 'neu zeichnen
            End If

        End Sub

        Private Sub Timer_Tick(sender As Object, e As System.EventArgs) Handles Timer.Tick

            ' Bild animieren wenn AutoPlay und Benutzerdefinierte Geschwindigkeit aktiv
            If Not Me.DesignMode AndAlso Me.AutoPlay Then

                ' wenn Frames = 0 ist das Bild nicht animiert -> Ende
                If _MaxFrame = 0 Then Exit Sub

                ' Bildzähler zurücksetzen wenn maximale Anzahl überschritten
                If _Frame > _MaxFrame Then _Frame = 0

                ' nächstes Bild auswählen
                Dim unused = _Gif.SelectActiveFrame(_Dimension, _Frame)

                ' Bildzähler weiterschalten
                _Frame += 1

                ' neu zeichnen
                Me.Invalidate()

            End If

        End Sub

        Private Sub SetGifImage(value As System.Drawing.Bitmap)

            ' Vorherige Animation stoppen bevor neues Bild gesetzt wird
            If _Gif IsNot Nothing Then
                System.Drawing.ImageAnimator.StopAnimate(_Gif, Me._AnimationHandler)
            End If

            If _OwnsGif AndAlso _Gif IsNot Nothing Then
                ' Vorheriges, intern besessenes Bild freigeben
                _Gif.Dispose()
            End If

            ' Standardanimation verwenden wenn keine Auswahl erfolgte
            If value Is Nothing Then
                _Gif = My.Resources.Standard


                _OwnsGif = False
            Else
                ' Defensive Kopie: externe Änderungen/Dispose am Original beeinflussen das Control nicht
                _Gif = DirectCast(value.Clone(), System.Drawing.Bitmap)
                _OwnsGif = True
            End If

            RaiseEvent GifChanged()

        End Sub

        Private Sub SetGifSizeMode(value As ImageSizeMode)

            If _GifSizeMode = value Then Return
            _GifSizeMode = value
            Me.Invalidate()

        End Sub

        Private Sub SetCustomDisplaySpeed(value As Boolean)

            If _CustomDisplaySpeed = value Then Return
            _CustomDisplaySpeed = value
            RaiseEvent CustomDisplaySpeedChanged()

        End Sub

        Private Sub SetZoomFactor(value As Decimal)

            Dim validatedZoomFactor As Decimal = FunctionDefinitions.CheckZoomFactorValue(value)
            If _ZoomFactor = validatedZoomFactor Then Return

            _ZoomFactor = validatedZoomFactor
            ' neu zeichnen
            Me.Invalidate()

        End Sub

        Private Sub UpdateTimerState()

            ' Immer zuerst beide Mechanismen stoppen, danach den benötigten Modus aktivieren
            Dim wasAnimating As Boolean = _IsAnimating

            Me.Timer.Stop()
            If _Gif IsNot Nothing Then
                System.Drawing.ImageAnimator.StopAnimate(_Gif, Me._AnimationHandler)
            End If
            _IsAnimating = False

            If wasAnimating Then
                RaiseEvent AnimationStopped(Me, System.EventArgs.Empty)
            End If

            If Me.DesignMode OrElse Not _Autoplay OrElse _Gif Is Nothing Then Return

            If _CustomDisplaySpeed Then

                ' Timer-basierte Wiedergabe mit fixer FPS
                If _MaxFrame > 0 Then
                    Me.Timer.Interval = CInt(Global.System.Math.Max(1D, 1000D / Global.System.Math.Max(_FramesPerSecond, 1D)))
                    Me.Timer.Start()
                    _IsAnimating = True
                    RaiseEvent AnimationStarted(Me, System.EventArgs.Empty)
                Else
                    RaiseEvent NoAnimation(Me, System.EventArgs.Empty)

                End If

            Else

                ' GIF-interne Frame-Delays über ImageAnimator verwenden
                If System.Drawing.ImageAnimator.CanAnimate(_Gif) Then
                    System.Drawing.ImageAnimator.Animate(_Gif, Me._AnimationHandler)
                    _IsAnimating = True
                    RaiseEvent AnimationStarted(Me, System.EventArgs.Empty)
                Else
                    RaiseEvent NoAnimation(Me, System.EventArgs.Empty)
                End If

            End If

        End Sub

#End Region

    End Class

End Namespace
