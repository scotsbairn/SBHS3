
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

Sub CopyDimmerValue(parm As Object)
    Dim Parameters() As String = Split(parm.ToString, "|")

    Dim sRef As Integer = Parameters(0)
    Dim dRef As Integer = Parameters(1)

    If SBDevices.SBDeviceBase.DeviceRefValid(hs, sRef) And SBDevices.SBDeviceBase.DeviceRefValid(hs, dRef) Then
        Dim sDev As SBDevices.SBDeviceSwitchDimmable = New SBDevices.SBDeviceSwitchDimmable(hs, sRef)
        Dim dDev As SBDevices.SBDeviceSwitchDimmable = New SBDevices.SBDeviceSwitchDimmable(hs, dRef)

        Dim sValue As String = sDev.GetDeviceValueAsString()
        dDev.setDeviceValue(sValue)

#If SBHS3DEBUG Then
        hs.WriteLog("SBHelper", "CopyDimmerVAlue sRef:" & sRef & " dRef:" & dRef & " value: " &sValue)
#End If
    Else
        Throw New System.Exception("Failed to find devices by reference, sRef:" & sRef & " dRef:" & dRef)
    End If

End Sub
