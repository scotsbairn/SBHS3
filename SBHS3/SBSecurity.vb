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
    ''' Is the house Secure, i.e. all security sensors report isSecure
    ''' </summary>
    ''' <returns>True if house is secure</returns>
    Public Function IsHouseSecure() As Boolean
        Dim Sensors As Hashtable = House.GetSecuritySensors

        hs.WriteLog(Me.GetType.Name, "IsHouseSecure ?")

        Dim Item
        For Each Item In Sensors
            Dim dev As SBDevices.SBSecurityDeviceBase = Item.Value

            If Not dev.IsSecure Then
                Return False
            End If
        Next

        Return True
    End Function

    Public Sub UpdateSecuritySceneControllers()
        Dim IsSecure As Boolean = IsHouseSecure()
        Dim Controllers As Hashtable = House.GetSecuritySceneControls

        Dim Item
        For Each Item In Controllers
            Dim dev As SBDevices.SBSceneController = Item.Value
            dev.SetSceneActive(IsSecure)
        Next


    End Sub
End Class
