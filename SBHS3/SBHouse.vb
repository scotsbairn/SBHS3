Imports System.Collections

Public MustInherit Class SBHouse

    ' handle to hs application
    Protected hs As IHSApplication

    Public Shared SecurityDevices As Hashtable

    Public Shared SecuritySensors As Hashtable

    Private Shared SecurityControls As Hashtable

    Public Sub New(ByRef _hs As IHSApplication)
        hs = _hs

        SecurityDevices = New Hashtable
    End Sub

    Protected Sub addSecuritySensor(ByVal Ref As Integer)
        Dim sDev = New SBDevices.SBDeviceSecuritySensor(hs, Ref)
        SecurityDevices.Add(Ref, sDev)
        SecuritySensors.Add(Ref, sDev)
    End Sub

    Protected Sub addSecurityLock(ByVal Ref As Integer)
        Dim cDev = New SBDevices.SBDeviceSecurityLock(hs, Ref, Nothing)
        SecurityDevices.Add(Ref, cDev)
        SecurityControls.Add(Ref, cDev)
    End Sub

    Protected Sub addSecurityBarrier(ByVal Ref As Integer)
        Dim cDev = New SBDevices.SBDeviceSecurityBarrier(hs, Ref, Nothing)
        SecurityDevices.Add(Ref, cDev)
        SecurityControls.Add(Ref, cDev)
    End Sub

    Protected Sub addSecurityLock(ByVal Ref As Integer, ByVal SensorRef As Integer)
        Dim sDev As SBDevices.SBDeviceSecuritySensor = New SBDevices.SBDeviceSecuritySensor(hs, SensorRef)
        SecurityDevices.Add(SensorRef, sDev)
        SecuritySensors.Add(SensorRef, sDev)

        Dim cDev = New SBDevices.SBDeviceSecurityLock(hs, Ref, Nothing)
        SecurityDevices.Add(Ref, cDev)
        SecurityControls.Add(Ref, cDev)
    End Sub

    Protected Sub addSecurityBarrier(ByVal Ref As Integer, ByVal SensorRef As Integer)
        Dim sDev As SBDevices.SBDeviceSecuritySensor = New SBDevices.SBDeviceSecuritySensor(hs, SensorRef)
        SecurityDevices.Add(SensorRef, sDev)
        SecuritySensors.Add(SensorRef, sDev)

        Dim cDev = New SBDevices.SBDeviceSecurityBarrier(hs, Ref, sDev)
        SecurityDevices.Add(Ref, cDev)
        SecurityControls.Add(Ref, cDev)
    End Sub

    Private Sub CheckSecurityIsReady()
        If IsNothing(SecuritySensors) Then
            SecuritySensors = New Hashtable
            SecurityControls = New Hashtable

            InitSecurityDevices(SecuritySensors, SecurityControls)
        End If
    End Sub
    '
    ' get the set of security sensor devices
    '
    Public Function getSecuritySensors() As Hashtable
        CheckSecurityIsReady()

        getSecuritySensors = SecuritySensors
    End Function

    '
    ' get the set of security control devices
    '
    Public Function getSecurityControls() As Hashtable
        CheckSecurityIsReady()

        getSecurityControls = SecurityControls
    End Function

    Public MustOverride Sub InitSecurityDevices(ByRef SecuritySensors As Hashtable, ByRef SecurityControls As Hashtable)

    Public Sub DebugListSecurityAllDevices()
        CheckSecurityIsReady()
        For Each Item In SecurityDevices
            Dim dev As SBDevices.SBSecurityDeviceBase = Item.Value
            hs.writeLog(Me.GetType.Name, "SecurityDevice: Ref:" & dev.getRef() & " Name: " & dev.getName())
        Next
    End Sub

End Class
