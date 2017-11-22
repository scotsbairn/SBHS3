
'Imports System.Collections

Public MustInherit Class SBHouse

    ' handle to hs application
    Protected hs As IHSApplication

    Public Shared SecuritySensors As Hashtable

    Private Shared SecurityControls As Hashtable

    Public Sub New(ByRef _hs As IHSApplication)
        hs = _hs

    End Sub

    Protected Sub addSecuritySensor(ByVal Ref As Integer)
        Dim sDev = New SBDevices.SBDeviceSecuritySensor(hs, Ref)
        SecuritySensors.Add(Ref, sDev)
    End Sub

    Protected Sub addSecurityLock(ByVal Ref As Integer)
        Dim cDev = New SBDevices.SBDeviceSecurityLock(hs, Ref, Nothing)
        SecuritySensors.Add(Ref, cDev)
        SecurityControls.Add(Ref, cDev)
    End Sub

    Protected Sub addSecurityBarrier(ByVal Ref As Integer)
        Dim cDev = New SBDevices.SBDeviceSecurityBarrier(hs, Ref, Nothing)
        SecuritySensors.Add(Ref, cDev)
        SecurityControls.Add(Ref, cDev)
    End Sub

    Protected Sub addSecurityLock(ByVal Ref As Integer, ByVal SensorRef As Integer)
        Dim sDev As SBDevices.SBDeviceSecuritySensor = New SBDevices.SBDeviceSecuritySensor(hs, SensorRef)
        SecuritySensors.Add(SensorRef, sDev)

        Dim cDev = New SBDevices.SBDeviceSecurityLock(hs, Ref, Nothing)
        SecuritySensors.Add(Ref, cDev)
        SecurityControls.Add(Ref, cDev)
    End Sub

    Protected Sub addSecurityBarrier(ByVal Ref As Integer, ByVal SensorRef As Integer)
        Dim sDev As SBDevices.SBDeviceSecuritySensor = New SBDevices.SBDeviceSecuritySensor(hs, SensorRef)
        SecuritySensors.Add(SensorRef, sDev)

        Dim cDev = New SBDevices.SBDeviceSecurityBarrier(hs, Ref, sDev)
        SecuritySensors.Add(Ref, cDev)
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

        Return SecuritySensors
    End Function

    '
    ' get the set of security control devices
    '
    Public Function getSecurityControls() As Hashtable
        CheckSecurityIsReady()

        Return SecurityControls
    End Function

    Public MustOverride Sub InitSecurityDevices(ByRef SecuritySensors As Hashtable, ByRef SecurityControls As Hashtable)

    Public Sub DebugListSecuritySensors()
        CheckSecurityIsReady()
        Dim Item
       
        For Each Item In SecuritySensors
            Dim dev As SBDevices.SBSecurityDeviceBase = Item.Value
            hs.writeLog(Me.GetType.Name, "SecurityDevice: Ref:" & dev.getRef() & " Name: " & dev.getName())
        Next
    End Sub

End Class
