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


        AddSecurityLock(57, 95)         ' Upper Door lock and Sensor
        AddSecuritySensor(156)          ' Family Room Door
        AddSecuritySensor(152)          ' Dining Room Door
        AddSecuritySensor(123)          ' Main TV Room Door

        AddSecuritySensor(219)          ' Master Door
        AddSecurityLock(24, 242)        ' Garage House Door
        AddSecurityLock(34, 226)        ' Garage Pool Door

        AddSecurityBarrier(69)          ' Garage Door

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

    Protected Overrides Sub _SendErrorMsg(ByVal Msg As String)
        hs.RunScriptFunc("Notify.vb", "PushoverSendError", Msg, True, False)
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


