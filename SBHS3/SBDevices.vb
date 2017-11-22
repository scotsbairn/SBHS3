﻿
Public Class SBDevices

    '
    ' Device Type, used to categorize devices
    '
    Enum SBDeviceType
        SecurityControl
        SecuritySensor
    End Enum

    '
    ' Device Base Class
    '
    Public Class SBDeviceBase
        ' handle to hs application
        Protected hs As IHSApplication

        ' device reference
        Protected Ref As Integer

        ' device type
        Protected DeviceType As SBDeviceType

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByVal _DeviceType As SBDeviceType)
            hs = _hs
            Ref = _Ref
            DeviceType = _DeviceType
#If DEBUG Then
            hs.WriteLog(Me.GetType.Name, "created: Ref:" & Ref.ToString & " Type:" & DeviceType.ToString)
#End If
        End Sub

        ''' <summary>
        ''' Get the Device Reference
        ''' </summary>
        ''' <returns>Device Reference</returns>
        Public Function GetRef() As Integer
            Return Ref
        End Function

        ''' <summary>
        ''' Get the Device Type
        ''' </summary>
        ''' <returns>Device Type</returns>
        Public Function GetDeviceType() As SBDeviceType
            Return DeviceType
        End Function

        Public Function GetAddress() As String
            Return "Me"
        End Function

        Public Function GetName() As String
            Return "Me"
        End Function

        Public Function GetDeviceValue() As Double
#If SBISHS3 Then
            Return hs.CAPIGetStatus(Ref).Value
#Else
            Return 0   
#End If
        End Function

        Public Function GetDeviceValueAsString() As String
#If SBISHS3 Then
            Return hs.CAPIGetStatus(Ref).Status
#Else
            Return "0"
#End If
        End Function

        Public Sub SetDeviceValue(ByRef Value)

        End Sub

    End Class

    '
    ' An abstract security device
    '
    ' Defines abstract methods:
    ' - isSecure()
    '
    Public MustInherit Class SBSecurityDeviceBase
        Inherits SBDeviceBase

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByVal _DeviceType As SBDeviceType)
            MyBase.New(_hs, _Ref, _DeviceType)
        End Sub

        ' is the device secure or not?
        Public MustOverride Function IsSecure() As Boolean

    End Class

    '
    ' specialization for a security sensor, 
    '
    ' Implements:
    ' - isSecure()
    '
    Public Class SBDeviceSecuritySensor
        Inherits SBSecurityDeviceBase

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer)
            MyBase.New(_hs, _Ref, SBDeviceType.SecuritySensor)
        End Sub

        Public Overrides Function IsSecure() As Boolean
            Return True
        End Function

    End Class

    '
    ' specialization for a device with security control
    '
    ' Defines abstract methods:
    ' - setSecure(boolean secure)
    '
    ' Implements:
    '   
    Public MustInherit Class SBDeviceSecurityControl
        Inherits SBSecurityDeviceBase

        Private CanSecureSensor As SBDeviceSecuritySensor

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByRef _CanSecureSensor As SBDeviceSecuritySensor)
            MyBase.New(_hs, _Ref, SBDeviceType.SecurityControl)
            CanSecureSensor = _CanSecureSensor
        End Sub

        '
        ' set the mode for a security device:
        ' - secure                      = true/falsels
        ' - force                       = for setting of secure mode even if associatd sensor shows as not secure
        ' - reportFailByNotification    = if we can't secure the device then notify of this
        '
        Public MustOverride Sub SetSecure(ByVal secure As Boolean, ByVal force As Boolean, ByVal reportFailByNotification As Boolean)

    End Class

    '
    ' specialization of a security control device to represent a lock
    '
    ' Implements:
    ' - isSecure()
    ' - setSecure(...)
    '
    Public Class SBDeviceSecurityLock
        Inherits SBDeviceSecurityControl

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByRef _CanSecureSensor As SBDeviceSecuritySensor)
            MyBase.New(_hs, _Ref, _CanSecureSensor)
        End Sub

        Public Overrides Function IsSecure() As Boolean
            Return True
        End Function

        Public Overrides Sub SetSecure(ByVal secure As Boolean, ByVal force As Boolean, ByVal reportFailByNotification As Boolean)
        End Sub

    End Class

    '
    ' specialization of a security control device to represent a barrier (e.g. Garage Door)
    '
    ' Implements:
    ' - isSecure()
    ' - setSecure(...)
    '
    Public Class SBDeviceSecurityBarrier
        Inherits SBDeviceSecurityControl

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByRef _CanSecureSensor As SBDeviceSecuritySensor)
            MyBase.New(_hs, _Ref, _CanSecureSensor)
        End Sub

        Public Overrides Function IsSecure() As Boolean
            Return True
        End Function

        Public Overrides Sub SetSecure(ByVal secure As Boolean, ByVal force As Boolean, ByVal reportFailByNotification As Boolean)

        End Sub

    End Class

End Class
