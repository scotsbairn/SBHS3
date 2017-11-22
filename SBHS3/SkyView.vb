Public Class SkyView
    Inherits SBHouse

    Public Sub New(ByRef _hs As IHSApplication)
        MyBase.New(_hs)
    End Sub

    Public Overrides Sub InitSecurityDevices(ByRef SecuritySensors As Hashtable, ByRef SecurityControls As Hashtable)
#If DEBUG Then
        hs.WriteLog(Me.GetType.Name, "InitSecurityDevies(...)")
#End If
        ' add the sensors and control devices etc
        AddSecuritySensor(2)
        AddSecurityLock(10, 3)


    End Sub

End Class
