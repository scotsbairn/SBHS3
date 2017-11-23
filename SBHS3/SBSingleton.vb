Public MustInherit Class SBSingleton

    Protected Shared House As SBHouse

    Protected Shared Security As SBSecurity

    Protected Shared Notify As SBNotify

    Public Shared Function GetHouse() As SBHouse
        If IsNothing(House) Then
            Throw New System.Exception("SBSingleton not initialized")
        End If

        Return House
    End Function

    Public Shared Function GetSecurity() As SBSecurity
        If IsNothing(House) Then
            Throw New System.Exception("SBSingleton not initialized")
        End If

        Return Security
    End Function

    Public Shared Function GetNotify() As SBNotify
        If IsNothing(Notify) Then
            Throw New System.Exception("SBSingleton not initialized")
        End If

        Return Notify
    End Function


End Class
