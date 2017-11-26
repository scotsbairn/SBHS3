
Sub DimIfOn(parm As Object)
    Dim Parameters() As String = Split(parm.ToString, "|")

    Dim Ref As Integer = Parameters(0)
    Dim DimValue As Double = Parameters(1)

#If SBHS3DEBUG Then
    hs.WriteLog("SBHelper", "DimIfOn Ref:" & Ref & " Dim:" & DimValue)
#End If
    If SBDevices.SBDeviceBase.DeviceRefValid(hs, Ref) Then
        Dim dev As SBDevices.SBDeviceSwitchDimmable = New SBDevices.SBDeviceSwitchDimmable(hs, Ref)
        dev.DimIfOnTo(DimValue)
    Else
        Throw New System.Exception("Failed to find device by reference, Ref:" & Ref)
    End If

End Sub


