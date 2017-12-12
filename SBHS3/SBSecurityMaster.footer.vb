

Sub Main(parm As Object)
    SkyviewSingleton.Init(hs)

    hs.WriteLog("me", "you")

#If SBHS3DEBUG Then
       hs.WriteLog("me", "look at house secure")
#End If

End Sub


Sub LockAllDoors(ByVal Parm As Object)
    SkyviewSingleton.Init(hs)
    Dim Security As SBSecurity = SBSingleton.GetSecurity()

    Security.SecureAll(True, False, True)
    Security.UpdateSecuritySceneControllers()
End Sub

Sub UpdateSecuritySceneControllers(ByVal Parm As Object)
    SkyviewSingleton.Init(hs)
    Dim Security As SBSecurity = SBSingleton.GetSecurity()

    Security.UpdateSecuritySceneControllers()
End Sub


Function GetSecurityStatus(ByVal Parm As Object) As String
    SkyviewSingleton.Init(hs)
    Dim Security As SBSecurity = SBSingleton.GetSecurity()

    Return Security.GetSecurityStatus()
End Function

Sub StatusChangeCB(ByVal Parm As Object())
    SkyviewSingleton.Init(hs)

    If Parm Is Nothing Then Exit Sub
    If Parm.Length < 5 Then Exit Sub

    Dim Ref As Integer = Parm(4)

#If SBHS3DEBUG > 10 Then
            hs.WriteLog("SBSecurityMaster", "Event Trigger, Ref:" & Ref)
#End If

    Dim Security As SBSecurity = SBSingleton.GetSecurity()

    If (Security.IsSecuritySensor(Ref)) Then
        Security.UpdateSecuritySceneControllers()
    End If

End Sub
