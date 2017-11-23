

Public MustInherit Class SBNotify

    Private Shared singleton As SBNotify

    Public Shared Sub SetSingleton(ByRef _singleton As SBNotify)
        singleton = _singleton
    End Sub

    Public Shared Sub CheckReady()
        If IsNothing(singleton) Then
            Throw New System.Exception("SBNotify needs to be initialized")
        End If
    End Sub


    Public Shared Sub SendErrorMsg(ByVal Msg As String)
        CheckReady()
        singleton._SendErrorMsg(Msg)
    End Sub

    Protected MustOverride Sub _SendErrorMsg(ByVal Msg As String)

End Class
