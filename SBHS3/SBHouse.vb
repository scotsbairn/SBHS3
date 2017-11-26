

'Imports System.Collections

Public MustInherit Class SBHouse

    ' handle to hs application
    Protected hs As IHSApplication

    Private Shared SecuritySensors As Hashtable

    Private Shared SecurityControls As Hashtable

    Private Shared SecuritySceneControllers As Hashtable

    Public Sub New(ByRef _hs As IHSApplication)
        hs = _hs

    End Sub

    Protected Sub AddSecuritySceneController(ByVal Ref As Integer)
        Dim sDev = New SBDevices.SBDeviceSceneController(hs, Ref)
        SecuritySceneControllers.Add(Ref, sDev)
    End Sub

    Protected Sub AddSecuritySensor(ByVal Ref As Integer)
        Dim sDev = New SBDevices.SBDeviceSecuritySensor(hs, Ref)
        SecuritySensors.Add(Ref, sDev)
    End Sub

    Protected Sub AddSecurityLock(ByVal Ref As Integer)
        Dim cDev = New SBDevices.SBDeviceSecurityLock(hs, Ref, Nothing)
        SecuritySensors.Add(Ref, cDev)
        SecurityControls.Add(Ref, cDev)
    End Sub

    Protected Sub AddSecurityBarrier(ByVal Ref As Integer)
        Dim cDev = New SBDevices.SBDeviceSecurityBarrier(hs, Ref, Nothing)
        SecuritySensors.Add(Ref, cDev)
        SecurityControls.Add(Ref, cDev)
    End Sub

    Protected Sub AddSecurityLock(ByVal Ref As Integer, ByVal SensorRef As Integer)
        Dim sDev As SBDevices.SBDeviceSecuritySensor = New SBDevices.SBDeviceSecuritySensor(hs, SensorRef)
        SecuritySensors.Add(SensorRef, sDev)

        Dim cDev = New SBDevices.SBDeviceSecurityLock(hs, Ref, sDev)
        SecuritySensors.Add(Ref, cDev)
        SecurityControls.Add(Ref, cDev)
    End Sub

    Protected Sub AddSecurityBarrier(ByVal Ref As Integer, ByVal SensorRef As Integer)
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
            SecuritySceneControllers = New Hashtable

            InitSecurityDevices()
        End If
    End Sub

    Public Function getHS() As IHSApplication
        Return hs
    End Function


    '
    ' get the set of security sensor devices
    '
    Public Function GetSecuritySensors() As Hashtable
        CheckSecurityIsReady()

        Return SecuritySensors
    End Function

    '
    ' get the set of security control devices
    '
    Public Function GetSecurityControls() As Hashtable
        CheckSecurityIsReady()

        Return SecurityControls
    End Function

    '
    ' get the set of security scene control devices
    '
    Public Function GetSecuritySceneControls() As Hashtable
        CheckSecurityIsReady()

        Return SecuritySceneControllers

    End Function

    ''' <summary>
    ''' Lookup a device by its Ref ID to see if it is a security sensor
    ''' </summary>
    ''' <param name="Ref">Ref ID of device to lookup</param>
    ''' <returns>True if Ref refers to a security sensor</returns>
    Public Function IsSecuritySensor(ByRef Ref As Integer)
        CheckSecurityIsReady()

        Return SecuritySensors.ContainsKey(Ref)
    End Function

    Public MustOverride Sub InitSecurityDevices()

    Public Sub DebugListSecuritySensors()
        CheckSecurityIsReady()
        Dim Item

        For Each Item In SecuritySensors
            Dim dev As SBDevices.SBDeviceSecurityBase = Item.Value
            hs.WriteLog(Me.GetType.Name, "SecurityDevice: Ref:" & dev.GetRef() & " Name: " & dev.GetName())
        Next
    End Sub

End Class



