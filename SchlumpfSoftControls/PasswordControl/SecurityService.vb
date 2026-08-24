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

        Private Const Iterations As System.Int32 = 100000
        Private Const SaltSize As System.Int32 = 16
        Private Const HashSize As System.Int32 = 32

        ''' <summary>
        ''' Erzeugt einen speicherbaren Passwort-Hash im Format PBKDF2$Iterationen$Salt$Hash.
        ''' </summary>
        ''' <param name="password">Klartext-Passwort.</param>
        ''' <returns>Serialisierter Passwort-Hash.</returns>
        ''' <exception cref="System.ArgumentException">Wenn das Passwort leer ist.</exception>
        Public Function CreatePasswordHash(password As String) As String

            If String.IsNullOrWhiteSpace(password) Then Throw New System.ArgumentException(
                "Passwort darf nicht leer sein.", NameOf(password))

            ' Erzeugt für jedes Passwort einen eigenen Zufalls-Salt, damit identische Eingaben unterschiedliche Hashwerte ergeben.
            Dim salt(SaltSize - 1) As Byte

            Using rng As System.Security.Cryptography.RandomNumberGenerator = System.Security.Cryptography.RandomNumberGenerator.Create()
                rng.GetBytes(salt)
            End Using

            Using pbkdf2 As New System.Security.Cryptography.Rfc2898DeriveBytes(
                password, salt, Iterations, System.Security.Cryptography.HashAlgorithmName.SHA256)

                ' Leitet aus Passwort, Salt und Iterationszahl einen festen Hashwert für die spätere Prüfung ab.
                Dim hash As Byte() = pbkdf2.GetBytes(HashSize)

                ' Serialisiert alle benötigten Bestandteile in ein speicherbares Textformat für die spätere Verifikation.
                Return $"PBKDF2${Iterations}${System.Convert.ToBase64String(salt)}${System.Convert.ToBase64String(hash)}"

            End Using

        End Function

        ''' <summary>
        ''' Validiert ein eingegebenes Passwort gegen einen gespeicherten PBKDF2-Hash.
        ''' </summary>
        ''' <param name="password">Eingegebenes Klartext-Passwort.</param>
        ''' <param name="storedHash">Gespeicherter Hash im Format PBKDF2$Iterationen$Salt$Hash.</param>
        ''' <returns>True, wenn gültig; sonst False.</returns>
        ''' <exception cref="System.ArgumentException">Wenn das Passwort oder der gespeicherte Hash leer ist.</exception>
        ''' <exception cref="System.FormatException">Wenn Salt oder Hash im gespeicherten Wert nicht gültig Base64-kodiert sind.</exception>
        Public Function VerifyPassword(password As String, storedHash As String) As Boolean

            If String.IsNullOrWhiteSpace(password) Then
                Throw New System.ArgumentException("Passwort darf nicht leer sein.", NameOf(password))
            End If

            If String.IsNullOrWhiteSpace(storedHash) Then
                Throw New System.ArgumentException("Hash darf nicht leer sein.", NameOf(storedHash))
            End If

            ' Zerlegt den gespeicherten Wert in Algorithmuskennung, Iterationszahl, Salt und Hash.
            Dim parts As String() = storedHash.Split("$"c)
            If parts.Length <> 4 OrElse Not String.Equals(parts(0), "PBKDF2", System.StringComparison.Ordinal) Then Return False

            Dim iterationCount As System.Int32

            ' Übernimmt die im gespeicherten Hash hinterlegte Iterationszahl, damit Altwerte weiterhin validiert werden können.
            If Not System.Int32.TryParse(parts(1), iterationCount) Then Return False

            ' Stellt Salt und Ziel-Hash aus der Base64-Darstellung wieder als Bytefolgen her.
            Dim salt As Byte() = System.Convert.FromBase64String(parts(2))
            Dim expectedHash As Byte() = System.Convert.FromBase64String(parts(3))

            Using pbkdf2 As New System.Security.Cryptography.Rfc2898DeriveBytes(
                password, salt, iterationCount, System.Security.Cryptography.HashAlgorithmName.SHA256)

                ' Berechnet aus dem eingegebenen Passwort erneut den Vergleichswert in derselben Länge wie der gespeicherte Hash.
                Dim actualHash As Byte() = pbkdf2.GetBytes(expectedHash.Length)

                ' Vergleicht beide Bytefolgen in konstanter Zeit, um Rückschlüsse über die Laufzeit zu vermeiden.
                Return Me.FixedTimeEquals(actualHash, expectedHash)

            End Using

        End Function

        ''' <summary>
        ''' Verschlüsselt Daten benutzergebunden mit DPAPI (reversibel).
        ''' </summary>
        ''' <param name="plainText">Zu schützender Klartext.</param>
        ''' <returns>Base64-kodierter Ciphertext.</returns>
        ''' <exception cref="system.ArgumentNullException">Wenn <paramref name="plainText" /> den Wert <c>Nothing</c> hat.</exception>
        ''' <exception cref="System.Security.Cryptography.CryptographicException">Wenn der Klartext nicht geschützt werden kann.</exception>
        Public Function ProtectSecret(plainText As String) As String

            If plainText Is Nothing Then Throw New System.ArgumentNullException(NameOf(plainText))

            ' Wandelt den Klartext in UTF-8-Bytes um, damit die DPAPI mit einem stabilen Binärformat arbeiten kann.
            Dim data As Byte() = System.Text.Encoding.UTF8.GetBytes(plainText)

            ' Schützt die Daten benutzergebunden, sodass nur derselbe Windows-Benutzer sie wieder entschlüsseln kann.
            Dim protectedBytes As Byte() = System.Security.Cryptography.ProtectedData.Protect(
                data, Nothing, System.Security.Cryptography.DataProtectionScope.CurrentUser)

            ' Kodiert die geschützten Bytes als Text, damit sie gespeichert oder übertragen werden können.
            Return System.Convert.ToBase64String(protectedBytes)

        End Function

        ''' <summary>
        ''' Entschlüsselt benutzergebundene DPAPI-Daten.
        ''' </summary>
        ''' <param name="protectedBase64">Base64-kodierter Ciphertext.</param>
        ''' <returns>Entschlüsselter Klartext.</returns>
        ''' <exception cref="System.ArgumentException">Wenn <paramref name="protectedBase64" /> leer ist.</exception>
        ''' <exception cref="System.FormatException">Wenn <paramref name="protectedBase64" /> kein gültiger Base64-Text ist.</exception>
        ''' <exception cref="System.Security.Cryptography.CryptographicException">Wenn die DPAPI-Daten nicht für den aktuellen Benutzer entschlüsselt werden können.</exception>
        Public Function UnprotectSecret(protectedBase64 As String) As String

            If String.IsNullOrWhiteSpace(protectedBase64) Then Throw New System.ArgumentException(
                "Wert darf nicht leer sein.", NameOf(protectedBase64))

            ' Wandelt den gespeicherten Base64-Text zurück in die ursprünglich geschützte Bytefolge.
            Dim protectedBytes As Byte() = System.Convert.FromBase64String(protectedBase64)

            ' Hebt den benutzergebundenen DPAPI-Schutz wieder auf und liefert die ursprünglichen Nutzdaten zurück.
            Dim data As Byte() = System.Security.Cryptography.ProtectedData.Unprotect(
                protectedBytes, Nothing, System.Security.Cryptography.DataProtectionScope.CurrentUser)

            ' Rekonstruiert den entschlüsselten Klartext aus den UTF-8-Bytes.
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

            ' Verknüpft alle Byte-Unterschiede bitweise, damit die Schleife unabhängig vom ersten Treffer vollständig durchläuft.
            Dim result As System.Int32 = 0
            For i As System.Int32 = 0 To a.Length - 1
                result = result Or (a(i) Xor b(i))
            Next

            Return result = 0

        End Function

    End Class

End Namespace


