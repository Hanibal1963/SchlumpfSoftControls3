' --------------------------------------------------------------------------------------------------------
' Datei: ExtendedRTF.vb
' Author: Andreas Sauer
' Datum: 06.05.2026
' --------------------------------------------------------------------------------------------------------

Namespace ExtendedRTFControl

    ''' <summary>
    ''' Erweiterte <see cref="System.Windows.Forms.RichTextBox"/> mit bequemen
    ''' Formatierungs- und Abfrage-Hilfen für<br/>
    ''' Auswahl und Caret (u.a. Schriftgröße, Stil-Flags, Vorder-/Hintergrundfarbe,
    ''' Absatz-Einzüge, Ausrichtung)<br/>
    ''' sowie Batch-Updates über Redraw-Suppression.
    ''' </summary>
    ''' <remarks>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Redraw-Suppression zur Verringerung von Flackern und zur Leistungssteigerung via <c>WM_SETREDRAW</c> (siehe <see cref="BeginUpdate"/>/<see cref="EndUpdate"/>).</description>
    '''  </item>
    ''' </list>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Mischzustände in der Auswahl werden (soweit implementiert) als <c>Nothing</c> gemeldet (z.B. bei Stil-Flags, Schriftgröße, Einzug).<br/>
    ''' Farben melden derzeit keinen Mischzustand.</description>
    '''  </item>
    ''' </list>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Interne per-Zeichen-Operationen (z.B. Mischzustandserkennung,
    ''' Stiländerungen über die gesamte Auswahl) unterdrücken <see
    ''' cref="OnSelectionChanged(System.EventArgs)"/> bewusst, um UI-Event-Spam zu
    ''' vermeiden.</description>
    '''  </item>
    ''' </list>
    ''' <para><b>Hinweis:</b><br/>
    ''' Transformationen über die gesamte Auswahl erfolgen per Zeichen und können bei
    ''' sehr großen Texten zeitintensiv sein;<br/>
    ''' nutzen Sie nach Möglichkeit Batch-Blöcke.</para>
    ''' </remarks>
    <ProvideToolboxControlAttribute("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Erweiterte RichTextBox mit bequemen Formatierungs- und Abfrage-Hilfen für Auswahl und Caret (u.a. Schriftgröße, Stil-Flags, Vorder-/Hintergrundfarbe, Absatz-Einzüge, Ausrichtung) sowie Batch-Updates über Redraw-Suppression.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(ExtendedRTF), "ExtendedRTFControl.ExtendedRTF.bmp")>
    Public Class ExtendedRTF

#Region "Variablen"

        Private _updateNesting As System.Int32 = 0 ' Zähler für geschachtelte Update-Blöcke.
        Private _suppressSelectionEvents As Boolean = False ' Flag zur Unterdrückung von "OnSelectionChanged", wenn intern temporär per-Zeichen-Selektionen durchgeführt werden.

#End Region

#Region "Definiert die Eigenschaften"

        ''' <summary>
        ''' Liest oder setzt die Schriftgröße der aktuellen Auswahl bzw. am Caret.
        ''' </summary>
        ''' <remarks>
        ''' Bei uneinheitlicher Auswahl wird <see langword="Nothing"/> zurückgegeben
        ''' (Mischzustand).<br/>
        ''' Die Zuweisung validiert gegen <c>MIN_FONT_SIZE</c>.
        ''' </remarks>
        ''' <value>
        ''' <see cref="System.Nullable(Of Single)"/>: Konkreter Wert oder <see
        ''' langword="Nothing"/> bei Mischzustand.
        ''' </value>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionFontSize As System.Nullable(Of Single)
            Get
                If Me.SelectionLength = 0 Then
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Return f.Size
                End If
                Return Me.GetUniformFontValue(Function(f) f.Size)
            End Get
            Set(value As System.Nullable(Of Single))
                If Not value.HasValue Then Exit Property
                If value.Value < MIN_FONT_SIZE Then
                    Throw New System.ArgumentOutOfRangeException(NameOf(value),
                            $"Schriftgröße muss mindestens {MIN_FONT_SIZE} sein.")
                End If
                Me.SetSelectionFontSize(value.Value)
            End Set
        End Property

        ''' <summary>
        ''' Liest oder setzt den Fettdruck der aktuellen Auswahl bzw. am Caret.
        ''' </summary>
        ''' <remarks>
        ''' Bei uneinheitlicher Auswahl wird <see langword="Nothing"/> zurückgegeben
        ''' (Mischzustand).
        ''' </remarks>
        ''' <value>
        ''' <see cref="System.Nullable(Of Boolean)"/>: <see langword="True"/> oder <see
        ''' langword="False"/>, bzw. <see langword="Nothing"/> bei Mischzustand.
        ''' </value>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionBold As System.Nullable(Of Boolean)
            Get
                If Me.SelectionLength = 0 Then
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Return f.Bold
                End If
                Return Me.GetUniformFontFlag(Function(f) f.Bold)
            End Get
            Set(value As System.Nullable(Of Boolean))
                If Not value.HasValue Then Exit Property
                Me.ApplyStyleFlag(System.Drawing.FontStyle.Bold, value.Value)
            End Set
        End Property

        ''' <summary>
        ''' Liest oder setzt Kursiv (Italic) der aktuellen Auswahl bzw. am Caret.
        ''' </summary>
        ''' <remarks>
        ''' Bei uneinheitlicher Auswahl wird <see langword="Nothing"/> zurückgegeben
        ''' (Mischzustand).
        ''' </remarks>
        ''' <value>
        ''' <see cref="System.Nullable(Of Boolean)"/>: <see langword="True"/> oder <see
        ''' langword="False"/>, bzw. <see langword="Nothing"/> bei Mischzustand.
        ''' </value>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionItalic As System.Nullable(Of Boolean)
            Get
                If Me.SelectionLength = 0 Then
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Return f.Italic
                End If
                Return Me.GetUniformFontFlag(Function(f) f.Italic)
            End Get
            Set(value As System.Nullable(Of Boolean))
                If Not value.HasValue Then Exit Property
                Me.ApplyStyleFlag(System.Drawing.FontStyle.Italic, value.Value)
            End Set
        End Property

        ''' <summary>
        ''' Liest oder setzt Unterstreichung der aktuellen Auswahl bzw. am Caret.
        ''' </summary>
        ''' <remarks>
        ''' Bei uneinheitlicher Auswahl wird <see langword="Nothing"/> zurückgegeben
        ''' (Mischzustand).
        ''' </remarks>
        ''' <value>
        ''' <see cref="System.Nullable(Of Boolean)"/>: <see langword="True"/> oder <see
        ''' langword="False"/>, bzw. <see langword="Nothing"/> bei Mischzustand.
        ''' </value>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionUnderline As System.Nullable(Of Boolean)
            Get
                If Me.SelectionLength = 0 Then
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Return f.Underline
                End If
                Return Me.GetUniformFontFlag(Function(f) f.Underline)
            End Get
            Set(value As System.Nullable(Of Boolean))
                If Not value.HasValue Then Exit Property
                Me.ApplyStyleFlag(System.Drawing.FontStyle.Underline, value.Value)
            End Set
        End Property

        ''' <summary>
        ''' Liest oder setzt Durchstreichung der aktuellen Auswahl bzw. am Caret.
        ''' </summary>
        ''' <remarks>
        ''' Bei uneinheitlicher Auswahl wird <see langword="Nothing"/> zurückgegeben
        ''' (Mischzustand).
        ''' </remarks>
        ''' <value>
        ''' <see cref="System.Nullable(Of Boolean)"/>: <see langword="True"/> oder <see
        ''' langword="False"/>, bzw. <see langword="Nothing"/> bei Mischzustand.
        ''' </value>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionStrikeout As System.Nullable(Of Boolean)
            Get
                If Me.SelectionLength = 0 Then
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Return f.Strikeout
                End If
                Return Me.GetUniformFontFlag(Function(f) f.Strikeout)
            End Get
            Set(value As System.Nullable(Of Boolean))
                If Not value.HasValue Then Exit Property
                Me.ApplyStyleFlag(System.Drawing.FontStyle.Strikeout, value.Value)
            End Set
        End Property

        ''' <summary>
        ''' Liest oder setzt die aktuelle Vordergrundfarbe (Textfarbe) der Auswahl bzw. am
        ''' Caret.
        ''' </summary>
        ''' <remarks>
        ''' Meldet keinen Mischzustand (immer konkreter Wert).<br/>
        ''' Für echte Mischzustandserkennung wäre eine per-Zeichen-Prüfung analog zu den
        ''' Stil-Flags nötig.
        ''' </remarks>
        ''' <value>
        ''' <see cref="System.Drawing.Color"/> der Auswahl bzw. am Caret.
        ''' </value>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionForeColor As System.Drawing.Color
            Get
                Return MyBase.SelectionColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.SelectionColor = value
            End Set
        End Property

        ''' <summary>
        ''' Liest oder setzt die aktuelle Hintergrund-/Highlightfarbe der Auswahl bzw. am
        ''' Caret.
        ''' </summary>
        ''' <remarks>
        ''' Meldet keinen Mischzustand (immer konkreter Wert).<br/>
        ''' Für echte Mischzustandserkennung wäre eine per-Zeichen-Prüfung analog zu den
        ''' Stil-Flags nötig.
        ''' </remarks>
        ''' <value>
        ''' <see cref="System.Drawing.Color"/> der Markierung/Hinterlegung.
        ''' </value>
        <System.ComponentModel.Browsable(False)>
        Public Overloads Property SelectionBackColor As System.Drawing.Color
            Get
                Return MyBase.SelectionBackColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.SelectionBackColor = value
            End Set
        End Property

        ''' <summary>
        ''' Liest oder setzt den linken Absatz-Einzug (in Pixel) der aktuellen
        ''' Absatz-/Absatzauswahl bzw. am Caret.
        ''' </summary>
        ''' <remarks>
        ''' Bei uneinheitlicher Auswahl wird <see langword="Nothing"/> zurückgegeben
        ''' (Mischzustand).<br/>
        ''' Der Einzug wirkt absatzweise; die Auswahl wird intern absatzweise behandelt.
        ''' </remarks>
        ''' <value>
        ''' <see cref="System.Nullable(Of System.Int32)"/>: konkreter Einzug oder <see
        ''' langword="Nothing"/> bei Mischzustand.
        ''' </value>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionLeftIndent As System.Nullable(Of System.Int32)
            Get
                Return If(Me.SelectionLength = 0, MyBase.SelectionIndent, Me.GetUniformParagraphValue(Function() MyBase.SelectionIndent))
            End Get
            Set(value As System.Nullable(Of System.Int32))
                If Not value.HasValue Then Exit Property
                If value.Value < 0 Then Throw New System.ArgumentOutOfRangeException(NameOf(value), "Einzug darf nicht negativ sein.")
                MyBase.SelectionIndent = value.Value
            End Set
        End Property

#End Region

#Region "Definiert die öffentlichen Methoden"

        Public Sub New()

            ' Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()
            ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        End Sub

        ''' <summary>
        ''' Entfernt Formatierungen (Schriftstil, Vorder-/Hintergrundfarbe,
        ''' Bullet-Aufzählung) vollständig aus aktueller Auswahl oder – ohne Auswahl – ab
        ''' der Caret-Position.
        ''' </summary>
        ''' <remarks>
        ''' Optimiert: Wendet die Normalisierung einmal auf die gesamte Auswahl an (statt
        ''' per Zeichen).<br/>
        ''' Bei keiner Auswahl wird das Format an der aktuellen Einfügemarke (Caret)
        ''' zurückgesetzt.
        ''' </remarks>
        Public Sub ClearFormatting()
            If Me.SelectionLength = 0 Then
                Dim baseFont = Me.SelectionFont ' Kein Bereich markiert -> Format am Caret zurücksetzen.
                If baseFont Is Nothing Then baseFont = Me.Font
                ' Neuer Font auf Regular (alle Stil-Flags weg)
                Using resetFont As New System.Drawing.Font(baseFont.FontFamily, baseFont.Size, System.Drawing.FontStyle.Regular, baseFont.Unit, baseFont.GdiCharSet, baseFont.GdiVerticalFont)
                    Me.SelectionFont = resetFont
                End Using
                Me.SelectionColor = Me.ForeColor
                Me.SelectionBackColor = Me.BackColor
                Me.SelectionBullet = False
                Return
            End If
            Me.BeginUpdate()
            Try
                Dim baseFont = Me.SelectionFont
                If baseFont Is Nothing Then baseFont = Me.Font
                Using resetFont As New System.Drawing.Font(baseFont.FontFamily, baseFont.Size, System.Drawing.FontStyle.Regular, baseFont.Unit, baseFont.GdiCharSet, baseFont.GdiVerticalFont)
                    Me.SelectionFont = resetFont
                End Using
                Me.SelectionColor = Me.ForeColor
                Me.SelectionBackColor = Me.BackColor
                Me.SelectionBullet = False
            Finally
                Me.EndUpdate()
            End Try
        End Sub

        ''' <summary>
        ''' Setzt die horizontale Ausrichtung der aktuellen Absatz-/Absatzauswahl.
        ''' </summary>
        ''' <remarks>
        ''' Wirkt absatzweise. Bei keiner Auswahl wird der aktuelle Absatz ausgerichtet.
        ''' </remarks>
        ''' <param name="alignment">Die gewünschte horizontale Ausrichtung.</param>
        Public Sub SetSelectionAlignment(alignment As System.Windows.Forms.HorizontalAlignment)
            Me.SelectionAlignment = alignment
        End Sub

        ''' <summary>
        ''' Schaltet Fettdruck für aktuelle Auswahl bzw. Caret um.
        ''' </summary>
        ''' <remarks>
        ''' Bei Auswahl mit Mischzustand wird für alle ausgewählten Zeichen auf den
        ''' invertierten Zustand umgestellt.
        ''' </remarks>
        Public Sub ToggleBold()
            Me.SelectionBold = Not Me.SelectionBold.GetValueOrDefault(False)
        End Sub

        ''' <summary>
        ''' Schaltet Kursiv für aktuelle Auswahl bzw. Caret um.
        ''' </summary>
        ''' <remarks>
        ''' Bei Auswahl mit Mischzustand wird für alle ausgewählten Zeichen auf den
        ''' invertierten Zustand umgestellt.
        ''' </remarks>
        Public Sub ToggleItalic()
            Me.SelectionItalic = Not Me.SelectionItalic.GetValueOrDefault(False)
        End Sub

        ''' <summary>
        ''' Schaltet Unterstreichung für aktuelle Auswahl bzw. Caret um.
        ''' </summary>
        ''' <remarks>
        ''' Bei Auswahl mit Mischzustand wird für alle ausgewählten Zeichen auf den
        ''' invertierten Zustand umgestellt.
        ''' </remarks>
        Public Sub ToggleUnderline()
            Me.SelectionUnderline = Not Me.SelectionUnderline.GetValueOrDefault(False)
        End Sub

        ''' <summary>
        ''' Schaltet Durchstreichung für aktuelle Auswahl bzw. Caret um.
        ''' </summary>
        ''' <remarks>
        ''' Bei Auswahl mit Mischzustand wird für alle ausgewählten Zeichen auf den
        ''' invertierten Zustand umgestellt.
        ''' </remarks>
        Public Sub ToggleStrikeout()
            Me.SelectionStrikeout = Not Me.SelectionStrikeout.GetValueOrDefault(False)
        End Sub

        ''' <summary>
        ''' Schaltet Bullet-Aufzählung für aktuelle Absatz-/Absatzauswahl um.
        ''' </summary>
        ''' <remarks>
        ''' Wirkt absatzweise. Funktioniert nur auf Absatzebene (SelectionLength = 0 -&gt;
        ''' aktueller Absatz).
        ''' </remarks>
        Public Sub ToggleBullet()
            Me.SelectionBullet = Not Me.SelectionBullet
        End Sub

#End Region

#Region "Definiert die Internen Methoden"

        ' Liefert ein einheitliches Bool-Stil-Flag (oder Nothing bei Mischzustand).
        Private Function GetUniformFontFlag(selector As System.Func(Of System.Drawing.Font, Boolean)) As System.Nullable(Of Boolean)
            Dim len = Me.SelectionLength
            If len <= 0 Then Return Nothing
            Dim start = Me.SelectionStart
            Dim result As System.Nullable(Of Boolean) = Nothing
            Me.BeginInternalSelectionScan()
            Try
                For i = 0 To len - 1
                    Me.[Select](start + i, 1)
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Dim v = selector(f)
                    If Not result.HasValue Then
                        result = v
                    ElseIf result.Value <> v Then
                        result = Nothing
                        Exit For
                    End If
                Next
            Finally
                Me.[Select](start, len)
                Me.EndInternalSelectionScan()
            End Try
            Return result
        End Function

        ' Liefert einen einheitlichen Single-Wert (Schriftgröße) oder Nothing (Mischzustand).
        Private Function GetUniformFontValue(selector As System.Func(Of System.Drawing.Font, Single)) As System.Nullable(Of Single)
            Dim len = Me.SelectionLength
            If len <= 0 Then Return Nothing
            Dim start = Me.SelectionStart
            Dim value As System.Nullable(Of Single) = Nothing
            Me.BeginInternalSelectionScan()
            Try
                For i = 0 To len - 1
                    Me.[Select](start + i, 1)
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Dim s = selector(f)
                    If Not value.HasValue Then
                        value = s
                    ElseIf System.Math.Abs(value.Value - s) > 0.01F Then
                        value = Nothing
                        Exit For
                    End If
                Next
            Finally
                Me.[Select](start, len)
                Me.EndInternalSelectionScan()
            End Try
            Return value
        End Function

        ' Liefert einen einheitlichen Absatzwert (Integer) oder Nothing bei Mischzustand.
        Private Function GetUniformParagraphValue(selector As System.Func(Of System.Int32)) As System.Nullable(Of System.Int32)
            Dim len = Me.SelectionLength
            If len <= 0 Then Return Nothing
            Dim start = Me.SelectionStart
            Dim v As System.Nullable(Of System.Int32) = Nothing
            Me.BeginInternalSelectionScan()
            Try
                For i = 0 To len - 1
                    Me.[Select](start + i, 1)
                    Dim cur = selector()
                    If Not v.HasValue Then
                        v = cur
                    ElseIf v.Value <> cur Then
                        v = Nothing
                        Exit For
                    End If
                Next
            Finally
                Me.[Select](start, len)
                Me.EndInternalSelectionScan()
            End Try
            Return v
        End Function

        ' Setzt die Schriftgröße (alle anderen Attribute bleiben erhalten).
        Private Sub SetSelectionFontSize(newSize As Single)
            If newSize <= 0 Then Throw New System.ArgumentOutOfRangeException(NameOf(newSize))
            Me.ApplyFontTransformation(
                    Function(f)
                        Return New System.Drawing.Font(f.FontFamily, newSize, f.Style, f.Unit, f.GdiCharSet, f.GdiVerticalFont)
                    End Function)
        End Sub

        ' Wendet / entfernt ein einzelnes FontStyle-Flag auf Auswahl/Caret an.
        Private Sub ApplyStyleFlag(flag As System.Drawing.FontStyle, enabled As Boolean)
            Me.ApplyFontTransformation(
                    Function(f)
                        Dim targetStyle = If(enabled, f.Style Or flag, f.Style And Not flag)
                        If targetStyle = f.Style Then
                            Return f ' Keine Änderung nötig -> selben Font zurückgeben (wird nicht ersetzt)
                        End If
                        Return New System.Drawing.Font(f, targetStyle)
                    End Function)
        End Sub

        ' Kernroutine zur Font-Transformation (Stil-/Größenänderungen) für Caret oder Auswahl.
        ' Funktion, die auf Basis des vorhandenen Fonts einen neuen zurückgibt.
        ' Gibt sie exakt denselben Font zurück, erfolgt keine Zuweisung.
        Private Sub ApplyFontTransformation(transform As System.Func(Of System.Drawing.Font, System.Drawing.Font))
            If Me.SelectionLength = 0 Then
                Dim f = Me.SelectionFont ' Nur Caret: Einfach einmal transformieren
                If f Is Nothing Then f = Me.Font
                Dim nf = transform(f)
                If nf Is Nothing Then Exit Sub
                If nf Is f Then Exit Sub ' keine Änderung
                Try
                    Me.SelectionFont = nf
                Finally
                    If nf IsNot f Then nf.Dispose()
                End Try
                Return
            End If
            Dim start = Me.SelectionStart ' Auswahl: pro Zeichen anwenden (RichTextBox hat kein native Multi-Teil-Transform API auf .NET-Level)
            Dim len = Me.SelectionLength
            Me.BeginUpdate()
            Try
                ' Cache zur Wiederverwendung identischer Fonts -> reduziert GDI Handles
                Dim cache As New System.Collections.Generic.Dictionary(Of String, System.Drawing.Font)(System.StringComparer.Ordinal)
                For i = 0 To len - 1
                    Me.[Select](start + i, 1)
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Dim nf = transform(f)
                    If nf Is Nothing OrElse nf Is f Then Continue For

                    Dim key = FontCacheKey(nf)
                    Dim apply As System.Drawing.Font
#Disable Warning BC42030 ' Die Variable wurde als Verweis übergeben, bevor ihr ein Wert zugewiesen wurde.
                    If cache.TryGetValue(key, apply) Then
#Enable Warning BC42030 ' Die Variable wurde als Verweis übergeben, bevor ihr ein Wert zugewiesen wurde.
                        nf.Dispose() ' Haben bereits ein identisches Font-Objekt -> erstellen entsorgtes verwerfen
                    Else
                        cache(key) = nf
                        apply = nf
                    End If
                    Me.SelectionFont = apply
                Next
                Me.[Select](start, len) ' Ursprüngliche Auswahl wiederherstellen
                For Each kv In cache ' Fonts aus Cache entsorgen (RichTextBox kopiert Formatdaten intern)
                    kv.Value.Dispose()
                Next
            Finally
                Me.EndUpdate()
            End Try
        End Sub

        ' Erzeugt einen konsistenten Cache-Schlüssel für einen Font (Familie+Größe+Stil+Einheit+Charset+Vertikal).
        Private Shared Function FontCacheKey(f As System.Drawing.Font) As String
            Return $"{f.FontFamily.Name}|{f.Size}|{CInt(f.Style)}|{CInt(f.Unit)}|{f.GdiCharSet}|{f.GdiVerticalFont}"
        End Function

        ' Start eines verschachtelbaren Batch-Blocks. Unterdrückt Neuzeichnen via WM_SETREDRAW.
        Private Sub BeginUpdate()
            If Not Me.IsHandleCreated Then Return
            If Me._updateNesting = 0 Then
                Dim unused = SendMessage(Me.Handle, WM_SETREDRAW, False, System.IntPtr.Zero)
            End If
            Me._updateNesting += 1
        End Sub

        ' Ende eines Batch-Blocks. Bei Erreichen von 0 wird Redraw wieder aktiviert und das Control neu gezeichnet.
        Private Sub EndUpdate()
            If Not Me.IsHandleCreated Then Return
            Me._updateNesting -= 1
            If Me._updateNesting <= 0 Then
                Me._updateNesting = 0
                Dim unused = SendMessage(Me.Handle, WM_SETREDRAW, True, System.IntPtr.Zero)
                Me.Invalidate() ' Neuzeichnen anfordern
                Me.Update()     ' Sofortige Ausführung (reduziert wahrnehmbares Flackern)
            End If
        End Sub

        ' Signalisiert Beginn eines internen Auswahl-Scans (Mischzustandserkennung): unterdrückt SelectionChanged.
        Private Sub BeginInternalSelectionScan()
            Me._suppressSelectionEvents = True
            Me.BeginUpdate()
        End Sub

        ' Beendet internen Auswahl-Scan und reaktiviert Events/Redraw (verschachtelt sicher).
        Private Sub EndInternalSelectionScan()
            Me._suppressSelectionEvents = False
            Me.EndUpdate()
        End Sub

        Protected Overrides Sub OnSelectionChanged(e As System.EventArgs)
            If Me._suppressSelectionEvents Then
                Return ' Intern ausgelöste per-Zeichen-Select-Operation -> nicht an UI weiterreichen.
            End If
            MyBase.OnSelectionChanged(e)
        End Sub

#End Region

    End Class

End Namespace
