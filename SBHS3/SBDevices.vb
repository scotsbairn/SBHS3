
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
        Private hs As IHSApplication

        ' device reference
        Public Ref As Integer

        ' device type
        Public Type As SBDeviceType


        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByVal _Type As SBDeviceType)
            hs = _hs
            Ref = _Ref
            Type = _Type
#If DEBUG Then
            hs.writeLog(Me.GetType.Name, "created: Ref:" & Ref.ToString & " Type:" & Type.ToString)
#End If
        End Sub

        Public Function getName() As String
            getName = "Me"
        End Function

    End Class

    '
    ' An abstract security device
    '
    ' Defines abstract methods:
    ' - isSecure()
    '
    Public MustInherit Class SBSecurityDeviceBase
        Inherits SBDeviceBase

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByVal _Type As SBDeviceType)
            MyBase.New(_hs, _Ref, _Type)
        End Sub

        ' is the device secure or not?
        Public MustOverride Function isSecure() As Boolean

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

        Public Overrides Function isSecure() As Boolean
            isSecure = True
        End Function

    End Class

    '
    ' specialization for a device with security control
    '
    ' Defines abstract methods:
    ' - setSecure(boolean secure)
    '
    ' Implements:
    ' - isSecure()
    '   
    Public MustInherit Class SBDeviceSecurityControl
        Inherits SBSecurityDeviceBase

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer)
            MyBase.New(_hs, _Ref, SBDeviceType.SecurityControl)
        End Sub

        ' is the device secure or not?
        Public MustOverride Sub setSecure(ByVal secure As Boolean, ByVal force As Boolean, ByVal reportFailByNotification As Boolean)
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

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer)
            MyBase.New(_hs, _Ref)
        End Sub

        Public Overrides Function isSecure() As Boolean
            isSecure = True
        End Function

        '
        ' set the mode for a security device:
        ' - secure                      = true/falsels
        ' - force                       = for setting of secure mode even if associatd sensor shows as not secure
        ' - reportFailByNotification    = if we can't secure the device then notify of this
        '
        Public Overrides Sub setSecure(ByVal secure As Boolean, ByVal force As Boolean, ByVal reportFailByNotification As Boolean)
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

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer)
            MyBase.New(_hs, _Ref)
        End Sub

        Public Overrides Function isSecure() As Boolean
            isSecure = True
        End Function

        Public Overrides Sub setSecure(ByVal secure As Boolean, ByVal force As Boolean, ByVal reportFailByNotification As Boolean)
        End Sub

    End Class

End Class
