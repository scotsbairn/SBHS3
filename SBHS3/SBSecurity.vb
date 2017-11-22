'
' Security Operations on a House
'
Public Class SBSecurity

    Private House As SBHouse

    Public Sub New(ByRef _House As SBHouse)
        House = _House
    End Sub

    Public Function isHouseSecure() As Boolean
        Dim Sensors As Hashtable = House.getSecuritySensors

        For Each Item In Sensors
            Dim dev As SBDevices.SBSecurityDeviceBase = Item.Value
            If Not dev.isSecure Then
                Return False
            End If
        Next

        Return True
    End Function


End Class
