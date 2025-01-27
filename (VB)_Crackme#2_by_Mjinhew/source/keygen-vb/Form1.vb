Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.Win32
Imports System.Security.Cryptography
Imports Org.BouncyCastle.Crypto.Digests
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim hash1 = 33736294, hash2 = 35191196
        Dim code = TextBox3.Text.Substring(0, 1)
        Dim text = TextBox3.Text.Substring(1, 1)
        Dim text3 = "3519119633736294-3.5191195985451E+15"
        Dim text2 As String = MD5(GetID().ToString())
        text2 = text2.Substring(5, 8)
        text2 = SHA512(text2)
        text2 = MD5(text2 + text3)
        text2 = text2.Substring(8, 16)
        text2 = Modify(text2, text, code)
        text2 = RIP(text2)
        TextBox1.Text = GetID().ToString().Replace("-", "")
        TextBox2.Text = text2
    End Sub
    Public Shared Function MD5(plaintext As String) As String
        Dim md As MD5 = Security.Cryptography.MD5.Create()
        Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(plaintext)
        Dim hashBytes As Byte() = md.ComputeHash(inputBytes)
        Return BitConverter.ToString(hashBytes).Replace("-", "")
    End Function

    Public Shared Function GetID() As String
        Dim registryKey As RegistryKey = If(Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\", False), Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\", False))
        Return registryKey.GetValue("ProductId", "").ToString()
    End Function
    Public Shared Function SHA512(plaintext As String) As String
        Dim sha As SHA512 = Security.Cryptography.SHA512.Create()
        Dim array As Byte() = Encoding.UTF8.GetBytes(plaintext)
        Dim hashBytes As Byte() = sha.ComputeHash(array)
        Return BitConverter.ToString(hashBytes)
    End Function


    Public Shared Function RIP(plaintext As String) As String
        Dim ripemd As New RipeMD160Digest()
        Dim array As Byte() = Encoding.UTF8.GetBytes(plaintext)
        Dim hash As Byte() = New Byte(ripemd.GetDigestSize() - 1) {}
        ripemd.BlockUpdate(array, 0, array.Length)
        ripemd.DoFinal(hash, 0)

        Return BitConverter.ToString(hash).Replace("-", "").Substring(4, 20)
    End Function

    Public Shared Function Modify(text As String, text4 As String, code As String) As String
        text = text.Insert(4, "-")
        text = text.Insert(9, "+")
        text = text.Insert(14, "*")
        Dim i As Integer = 0
        Dim text2 As String = ""
        While i < text.Length
            Dim text3 As String = text.Substring(i, 1)
            If Not IsNumeric(text3) Then
                text2 += Strings.Asc(text3).ToString().Substring(0, 1)
            Else
                text2 += text3
            End If
            i += 1
        End While
        text2 = text2.Insert(2, text4)
        Return text2 + code
    End Function
End Class
