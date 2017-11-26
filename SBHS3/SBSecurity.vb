'
' Security Operations on a House
'
Public Class SBSecurity

    ' handle to hs application
    Protected hs As IHSApplication

    Private House As SBHouse

    Public Sub New(ByRef _hs As IHSApplication, ByRef _House As SBHouse)
        hs = _hs
        House = _House
    End Sub


    ''' <summary>
    ''' Lookup a device by its Ref ID to see if it is a security sensor
    ''' </summary>
    ''' <param name="Ref">Ref ID of device to lookup</param>
    ''' <returns>True if Ref refers to a security sensor</returns>
    Public Function IsSecuritySensor(ByRef Ref As Integer)
        Return House.IsSecuritySensor(Ref)
    End Function

    ''' <summary>
    ''' Is the house Secure, i.e. all security sensors report isSecure
    ''' </summary>
    ''' <returns>True if house is secure</returns>
    Public Function IsHouseSecure() As Boolean
        Dim Sensors As Hashtable = House.GetSecuritySensors

        hs.WriteLog(Me.GetType.Name, "IsHouseSecure ?")

        Dim Item
        For Each Item In Sensors
            Dim dev As SBDevices.SBDeviceSecurityBase = Item.Value

            If Not dev.IsSecure Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' Update the scene controllers to reflect the current status of the house
    ''' </summary>
    Public Sub UpdateSecuritySceneControllers()
        Dim IsSecure As Boolean = IsHouseSecure()
        Dim Controllers As Hashtable = House.GetSecuritySceneControls

        Dim Item
        For Each Item In Controllers
            Dim dev As SBDevices.SBDeviceSceneController = Item.Value
            dev.SetSceneActive(IsSecure)
        Next
    End Sub


    Public Sub SetSecure(ByVal Ref As Integer, ByVal secure As Boolean, ByVal force As Boolean, ByVal reportFailByNotification As Boolean)
        If House.GetSecurityControls().ContainsKey(Ref) Then
            Dim lDev As SBDevices.SBDeviceSecurityControl = House.GetSecurityControls().Item(Ref)
            lDev.SetSecure(secure, force, reportFailByNotification)
        Else
            If reportFailByNotification Then
                SBNotify.SendErrorMsg("ERROR: SetSecure", "SetSecure request for device Ref:" & Ref.ToString & ", failed to find device")
            Else
                Throw New System.Exception("Failed to find device by Ref:" & Ref.ToString)
            End If
        End If
    End Sub

    Public Sub SecureAll(ByVal secure As Boolean, ByVal force As Boolean, ByVal reportFailByNotification As Boolean)
        Dim Controls As Hashtable = House.GetSecurityControls
        Dim Item

        For Each Item In Controls
            Dim dev As SBDevices.SBDeviceSecurityControl = Item.value
            dev.SetSecure(secure, force, reportFailByNotification)
        Next
    End Sub
End Class
