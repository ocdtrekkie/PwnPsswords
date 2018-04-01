Imports System

Module Program
    Sub Main(args As String())
        Dim strBreachCount As String = ""
        Dim txtPassword As String = ""
        Dim txtRange As String = ""
        Dim txtSearch As String = ""
        Dim txtRangeReply As String = ""
        Dim txtSha1 As String = ""
        Dim intFound As Integer
        Console.WriteLine("Enter your password here:")
        txtPassword = Console.ReadLine()

        Using sha = New System.Security.Cryptography.SHA1Managed()
            txtSha1 = BitConverter.ToString(sha.ComputeHash(Text.Encoding.UTF8.GetBytes(txtPassword)))
        End Using
        txtSha1 = txtSha1.Replace("-", "")
        Console.WriteLine("Hash is " & txtSha1)

        txtRange = txtSha1.Substring(0, 5)
        txtSearch = txtSha1.Substring(5, 35)
        Console.WriteLine("Range is " & txtRange)
        Console.WriteLine("Search is " & txtSearch)

        Using req = New System.Net.WebClient
            txtRangeReply = req.DownloadString("https://api.pwnedpasswords.com/range/" & txtRange)
        End Using

        intFound = txtRangeReply.IndexOf(txtSearch)

        If intFound = -1 Then
            Console.WriteLine("Your password was not found in the range")
        Else
            Console.WriteLine("Your password was found")
            strBreachCount = txtRangeReply.Substring(intFound + 36, txtRangeReply.IndexOf(Environment.NewLine, intFound) - intFound - 36)
            Console.WriteLine("Breached " & strBreachCount & " times")
        End If

        Console.WriteLine()
        Console.Write("Press any key to exit...")
        Console.ReadKey(True)
    End Sub
End Module
