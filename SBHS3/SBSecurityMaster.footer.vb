
Sub Main(parm As Object)

   hs.WriteLog("me", "you")

   Dim sv As New SkyView(hs)
   sv.DebugListSecuritySensors()

#If SBHS3DEBUG
   hs.WriteLog("me", "look at house secure")
#End If

   Dim ss As New SBSecurity(hs,sv)

   ss.UpdateSecuritySceneControllers()

End Sub


Sub StatusChangeCB(ByVal Parm As Object())
   If Parm Is Nothing Then Exit Sub
   If Parm.Length < 5 Then Exit Sub

   Dim Ref As Integer = Parm(4)

   Dim sv As New SkyView(hs)
   
   hs.WriteLog("SBSecurityMaster", "hello")

   Dim sv As New SkyView(hs)
   sv.DebugListSecuritySensors()

#If SBHS3DEBUG
   hs.WriteLog("me", "and you")
#End If

End Sub