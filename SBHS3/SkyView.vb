Public Class SkyView
    Inherits SBHouse

    Public Sub New(ByRef _hs As IHSApplication)
        MyBase.New(_hs)
    End Sub

    Public Overrides Sub InitSecurityDevices(ByRef SecuritySensors As Hashtable, ByRef SecurityControls As Hashtable)
#If DEBUG Then
        hs.writeLog(Me.GetType.Name, "InitSecurityDevies(...)")
#End If
        ' add the sensors and control devices etc
        addSecuritySensor(2)
        addSecurityLock(10, 3)


    End Sub

End Class
