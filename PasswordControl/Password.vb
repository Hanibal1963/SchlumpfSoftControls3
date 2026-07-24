' --------------------------------------------------------------------------------------------------------
' Datei: Password.vb
' Author: Andreas Sauer
' Datum: 24.07.2026
' --------------------------------------------------------------------------------------------------------

Namespace PasswordControl

    ''' <summary>
    ''' Ein Control zur Eingabe und Vergleich von Passwörtern.
    ''' </summary>
    <SchlumpfSoft.ProvideToolboxControlAttribute("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Ein Control zur Eingabe und Vergleich von Passwörtern.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(Password), "PasswordControl.Password.bmp")>
    Public Class Password

#Region "Definition der Konstanten"

        Private Const SaltSize As Integer = 16
        Private Const HashSize As Integer = 32
        Private Const IterationCount As Integer = 100000

#End Region

#Region "Definition der Variablen"

        Private _passwortHash As String
        Private _showpasswort As Boolean

#End Region

#Region "Definition der Ereignisse"

        Public Event PasswortHashChanged(sender As Object, e As PasswordHashChangedEventArgs)

#End Region

#Region "Definition der Eigenschaften"

        Public ReadOnly Property PasswortHash As String
            Get
                Return Me._passwortHash
            End Get
        End Property

#End Region

#Region "Öffentliche Methoden"

        Public Sub New()
            Me.InitializeComponent()
            Me.MinimumSize = New System.Drawing.Size(100, 20)
            Me.Size = New System.Drawing.Size(100, 20)
            Me.PB.Image = My.Resources.pic_noshow
            Me.TB.UseSystemPasswordChar = True
        End Sub

#End Region

#Region "Interne Methoden"

        Private Sub CheckTBText()
            Dim tbtext As String = Me.TB.Text
            Dim Hash = If(String.IsNullOrWhiteSpace(tbtext), $"", Me.CreateHash(tbtext))
            RaiseEvent PasswortHashChanged(Me, New PasswordHashChangedEventArgs(Hash))
        End Sub

        Private Function CreateHash(inputtext As String) As String

            Dim result As String = $""

            If inputtext Is Nothing Then
                Return result
            End If

            Dim salt(SaltSize - 1) As Byte
            Using randomNumberGenerator As System.Security.Cryptography.RandomNumberGenerator = System.Security.Cryptography.RandomNumberGenerator.Create()
                randomNumberGenerator.GetBytes(salt)
            End Using

            Dim hash As Byte()
            Using deriveBytes As New System.Security.Cryptography.Rfc2898DeriveBytes(inputtext, salt, IterationCount, System.Security.Cryptography.HashAlgorithmName.SHA256)
                hash = deriveBytes.GetBytes(HashSize)
            End Using

            result = String.Format("{0}.{1}.{2}", IterationCount, System.Convert.ToBase64String(salt), System.Convert.ToBase64String(hash))

            Return result

        End Function

#End Region

#Region "Ereignisbehandlungen"

        Private Sub Password_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
            Me.Height = 20
        End Sub

        Private Sub PB_Click(sender As Object, e As System.EventArgs) Handles PB.Click
            Me._showpasswort = Not Me._showpasswort
            If Me._showpasswort Then
                Me.PB.Image = My.Resources.pic_show
                Me.TB.UseSystemPasswordChar = False
            Else
                Me.PB.Image = My.Resources.pic_noshow
                Me.TB.UseSystemPasswordChar = True
            End If
        End Sub

        Private Sub TB_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TB.KeyDown
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then Me.CheckTBText()
        End Sub

#End Region




        'Private Shared Function ByteArraysAreEqual(leftbytes As Byte(), rightbytes As Byte()) As Boolean

        '    ' Gibt sofort False zurück, wenn eines der Arrays Nothing ist
        '    ' oder wenn beide Arrays unterschiedlich lang sind.
        '    If leftbytes Is Nothing OrElse rightbytes Is Nothing OrElse leftbytes.Length <> rightbytes.Length Then
        '        Return False
        '    End If

        '    ' Speichert alle gefundenen Unterschiede zwischen den Bytes.
        '    Dim difference As Integer = 0

        '    ' Vergleicht jedes Byte-Paar an derselben Position.
        '    For index As Integer = 0 To leftbytes.Length - 1
        '        ' Xor liefert 0 bei gleichen Werten, sonst einen Wert ungleich 0.
        '        ' Mit Or werden alle Unterschiede gesammelt.
        '        difference = difference Or (leftbytes(index) Xor rightbytes(index))
        '    Next

        '    ' Nur wenn kein Unterschied gefunden wurde, sind die Arrays gleich.
        '    Return difference = 0

        'End Function

        'Public Shared Function VerifyPassword(passwort As String, gespeicherterHash As String) As Boolean
        '    If passwort Is Nothing Then
        '        Throw New ArgumentNullException(NameOf(passwort))
        '    End If

        '    If String.IsNullOrWhiteSpace(gespeicherterHash) Then
        '        Return False
        '    End If

        '    Dim teile As String() = gespeicherterHash.Split("."c)
        '    If teile.Length <> 3 Then
        '        Return False
        '    End If

        '    Dim iterationen As Integer
        '    If Not Integer.TryParse(teile(0), iterationen) Then
        '        Return False
        '    End If

        '    Dim salt As Byte()
        '    Dim erwarteterHash As Byte()

        '    Try
        '        salt = Convert.FromBase64String(teile(1))
        '        erwarteterHash = Convert.FromBase64String(teile(2))
        '    Catch ex As FormatException
        '        Return False
        '    End Try

        '    Dim aktuellerHash As Byte()
        '    Using deriveBytes As New Rfc2898DeriveBytes(passwort, salt, iterationen, HashAlgorithmName.SHA256)
        '        aktuellerHash = deriveBytes.GetBytes(erwarteterHash.Length)
        '    End Using

        '    Return ByteArraysAreEqual(aktuellerHash, erwarteterHash)
        'End Function


    End Class

End Namespace
