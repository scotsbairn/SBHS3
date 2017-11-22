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

        AddSecuritySceneController(253) ' Family Button 5
        AddSecuritySceneController(267) ' Hall Button 5

    End Sub

End Class
