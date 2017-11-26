
'
' dummy IHSApplication
'

Public Class IHSApplication
    Public Sub WriteLog(ByRef m1 As String, ByRef m2 As String)
        Console.WriteLine(m1 & " : " & m2)
    End Sub

    Friend Sub RunScriptFunc(v1 As String, v2 As String, msg As String, v3 As Boolean, v4 As Boolean)
        Throw New NotImplementedException()
    End Sub
End Class

