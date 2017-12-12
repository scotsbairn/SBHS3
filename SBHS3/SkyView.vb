

Public Class SkyView
    Inherits SBHouse

    Public Sub New(ByRef _hs As IHSApplication)
        MyBase.New(_hs)
    End Sub

    Public Overrides Sub InitSecurityDevices()
#If DEBUG Then
        hs.WriteLog(Me.GetType.Name, "InitSecurityDevies(...)")
#End If
        ' add the sensors and control devices etc


        AddSecurityLock(57, "Upper Lock", 95, "UpperDoor")         ' Upper Door lock and Sensor
        AddSecuritySensor(156, "Family Patio")          ' Family Room Door
        AddSecuritySensor(152, "Dining Patio")          ' Dining Room Door
        AddSecuritySensor(123, "TV Patio")          ' Main TV Room Door

        AddSecuritySensor(219, "Master Patio")          ' Master Door
        AddSecurityLock(24, "Laundry Lock", 242, "Laundry Door")        ' Garage House Door
        AddSecurityLock(34, "Pool Lock", 226, "Pool Door")        ' Garage Pool Door

        AddSecurityBarrier(69, "Garage")          ' Garage Door

        AddSecuritySensor(127, "Left Storage")
        AddSecuritySensor(131, "Right Storage")

        AddSecuritySceneController(253) ' Family Button 5
        AddSecuritySceneController(267) ' Hall Button 5

    End Sub

End Class


Public Class SkyViewNotify
    Inherits SBNotify

    Private hs As IHSApplication

    Public Sub New(ByRef _hs As IHSApplication)
        hs = _hs
        SetSingleton(Me)
    End Sub

    Protected Overrides Sub _SendInfoMsg(ByVal Subject As String, ByVal Msg As String)
        Dim notifyParameters As String = "" & Subject & "|" & Msg
        hs.RunScriptFunc("Notify.vb", "PushoverSendInfo", notifyParameters, True, False)
    End Sub

    Protected Overrides Sub _SendErrorMsg(ByVal Subject As String, ByVal Msg As String)
        Dim notifyParameters As String = "" & Subject & "|" & Msg
        hs.RunScriptFunc("Notify.vb", "PushoverSendError", notifyParameters, True, False)
    End Sub

End Class


Class SkyviewSingleton
    Inherits SBSingleton

    Public Shared Sub Init(ByRef hs As IHSApplication)
        If IsNothing(Notify) Then
            Notify = New SkyViewNotify(hs)
        End If
        If IsNothing(House) Then
            House = New SkyView(hs)
        End If
        If IsNothing(Security) Then
            Security = New SBSecurity(hs, House)
        End If
    End Sub

End Class


