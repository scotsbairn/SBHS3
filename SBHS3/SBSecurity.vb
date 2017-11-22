'
' Security Operations on a House
'
Public Class SBSecurity

    Private House As SBHouse

    Public Sub New(ByRef _House As SBHouse)
        House = _House
    End Sub

    ''' <summary>
    ''' Is the house Secure, i.e. all security sensors report isSecure
    ''' </summary>
    ''' <returns>True if house is secure</returns>
    Public Function IsHouseSecure() As Boolean
        Dim Sensors As Hashtable = House.GetSecuritySensors

	Dim Item
        For Each Item In Sensors
            Dim dev As SBDevices.SBSecurityDeviceBase = Item.Value
            If Not dev.IsSecure Then
                Return False
            End If
        Next

        Return True
    End Function


End Class
