
Sub Main(parm As Object)
    SkyviewSingleton.Init(hs)

    hs.WriteLog("me", "you")

#If SBHS3DEBUG Then
       hs.WriteLog("me", "look at house secure")
#End If

End Sub


Sub StatusChangeCB(ByVal Parm As Object())
    SkyviewSingleton.Init(hs)

    If Parm Is Nothing Then Exit Sub
    If Parm.Length < 5 Then Exit Sub

    Dim Ref As Integer = Parm(4)

#If SBHS3DEBUG > 5 Then
            hs.WriteLog("SBSecurityMaster", "Event Trigger, Ref:" & Ref)
#End If

    Dim Security As SBSecurity = SBSingleton.GetSecurity()

    If (Security.IsSecuritySensor(Ref)) Then
#If SBHS3DEBUG Then
                hs.WriteLog("SBSecurityMaster", "Event is for a security sensor, Ref:" & Ref)
#End If
        Security.UpdateSecuritySceneControllers()
    End If

End Sub