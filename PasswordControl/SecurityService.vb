' --------------------------------------------------------------------------------------------------------
' Datei: SecurityService.vb
' Author: Andreas Sauer
' Datum: 27.07.2026
' --------------------------------------------------------------------------------------------------------

Namespace PasswordControl

    ''' <summary>
    ''' Stellt Funktionen zum sicheren Hashing (Validierung) und optionalen Schützen/Entschützen (DPAPI) bereit.
    ''' </summary>
    Friend NotInheritable Class SecurityService

        Private Const Iterations As Integer = 100000
        Private Const SaltSize As Integer = 16
        Private Const HashSize As Integer = 32

        ''' <summary>
        ''' Erzeugt einen speicherbaren Passwort-Hash im Format PBKDF2$Iterationen$Salt$Hash.
        ''' </summary>
        ''' <param name="password">Klartext-Passwort.</param>
        ''' <returns>Serialisierter Passwort-Hash.</returns>
        ''' <exception cref="ArgumentException">Wenn das Passwort leer ist.</exception>
        Public Function CreatePasswordHash(password As String) As String

            If String.IsNullOrWhiteSpace(password) Then Throw New System.ArgumentException(
                "Passwort darf nicht leer sein.", NameOf(password))

            Dim salt(SaltSize - 1) As Byte

            Using rng As System.Security.Cryptography.RandomNumberGenerator = System.Security.Cryptography.RandomNumberGenerator.Create()
                rng.GetBytes(salt)
            End Using

            Using pbkdf2 As New System.Security.Cryptography.Rfc2898DeriveBytes(
                password, salt, Iterations, System.Security.Cryptography.HashAlgorithmName.SHA256)

                Dim hash As Byte() = pbkdf2.GetBytes(HashSize)
                Return $"PBKDF2${Iterations}${System.Convert.ToBase64String(salt)}${System.Convert.ToBase64String(hash)}"

            End Using

        End Function

        ''' <summary>
        ''' Validiert ein eingegebenes Passwort gegen einen gespeicherten PBKDF2-Hash.
        ''' </summary>
        ''' <param name="password">Eingegebenes Klartext-Passwort.</param>
        ''' <param name="storedHash">Gespeicherter Hash im Format PBKDF2$Iterationen$Salt$Hash.</param>
        ''' <returns>True, wenn gültig; sonst False.</returns>
        ''' <exception cref="ArgumentException">Wenn Eingaben ungültig sind.</exception>
        Public Function VerifyPassword(password As String, storedHash As String) As Boolean

            If String.IsNullOrWhiteSpace(password) Then Return False
            If String.IsNullOrWhiteSpace(storedHash) Then Return False

            Dim parts As String() = storedHash.Split("$"c)
            If parts.Length <> 4 OrElse Not String.Equals(parts(0), "PBKDF2", System.StringComparison.Ordinal) Then Return False

            Dim iterationCount As Integer
            If Not Integer.TryParse(parts(1), iterationCount) Then Return False

            Dim salt As Byte() = System.Convert.FromBase64String(parts(2))
            Dim expectedHash As Byte() = System.Convert.FromBase64String(parts(3))

            Using pbkdf2 As New System.Security.Cryptography.Rfc2898DeriveBytes(
                password, salt, iterationCount, System.Security.Cryptography.HashAlgorithmName.SHA256)

                Dim actualHash As Byte() = pbkdf2.GetBytes(expectedHash.Length)
                Return Me.FixedTimeEquals(actualHash, expectedHash)

            End Using

        End Function

        ''' <summary>
        ''' Verschlüsselt Daten benutzergebunden mit DPAPI (reversibel).
        ''' </summary>
        ''' <param name="plainText">Zu schützender Klartext.</param>
        ''' <returns>Base64-kodierter Ciphertext.</returns>
        Public Function ProtectSecret(plainText As String) As String

            If plainText Is Nothing Then Throw New System.ArgumentNullException(NameOf(plainText))

            Dim data As Byte() = System.Text.Encoding.UTF8.GetBytes(plainText)
            Dim protectedBytes As Byte() = System.Security.Cryptography.ProtectedData.Protect(
                data, Nothing, System.Security.Cryptography.DataProtectionScope.CurrentUser)

            Return System.Convert.ToBase64String(protectedBytes)

        End Function

        ''' <summary>
        ''' Entschlüsselt benutzergebundene DPAPI-Daten.
        ''' </summary>
        ''' <param name="protectedBase64">Base64-kodierter Ciphertext.</param>
        ''' <returns>Entschlüsselter Klartext.</returns>
        Public Function UnprotectSecret(protectedBase64 As String) As String

            If String.IsNullOrWhiteSpace(protectedBase64) Then Throw New System.ArgumentException(
                "Wert darf nicht leer sein.", NameOf(protectedBase64))

            Dim protectedBytes As Byte() = System.Convert.FromBase64String(protectedBase64)
            Dim data As Byte() = System.Security.Cryptography.ProtectedData.Unprotect(
                protectedBytes, Nothing, System.Security.Cryptography.DataProtectionScope.CurrentUser)

            Return System.Text.Encoding.UTF8.GetString(data)

        End Function

        ''' <summary>
        ''' Vergleicht zwei Byte-Arrays in konstanter Zeit, um Timing-Angriffe zu verhindern.
        ''' </summary>
        ''' <param name="a">Erstes Byte-Array.</param>
        ''' <param name="b">Zweites Byte-Array.</param>
        ''' <returns>True, wenn die Arrays identisch sind; sonst False.</returns>
        Private Function FixedTimeEquals(a As Byte(), b As Byte()) As Boolean

            If a Is Nothing OrElse b Is Nothing Then Return False
            If a.Length <> b.Length Then Return False

            Dim result As Integer = 0
            For i As Integer = 0 To a.Length - 1
                result = result Or (a(i) Xor b(i))
            Next

            Return result = 0

        End Function

    End Class

End Namespace


