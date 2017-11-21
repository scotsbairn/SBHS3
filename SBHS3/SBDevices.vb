
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
        Public Type As SBDeviceType


        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByVal _Type As SBDeviceType)
            hs = _hs
            Ref = _Ref
            Type = _Type
#If DEBUG Then
            hs.writeLog(Me.GetType.Name, "created: Ref:" & Ref.ToString & " Type:" & Type.ToString)
#End If
        End Sub

        Public Function getRef() As Integer
            getRef = Ref
        End Function

        Public Function getAddress() As String
            getAddress = "Me"
        End Function

        Public Function getName() As String
            getName = "Me"
        End Function

        Public Function getValue() As Double
            getValue = 0
        End Function

        Public Function getValueAsString() As String
            getValueAsString = ""
        End Function

        Public Sub setValue(ByRef Value)

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

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByRef _CanSecureSensor As SBDeviceSecuritySensor)
            MyBase.New(_hs, _Ref, _CanSecureSensor)
        End Sub

        Public Overrides Function isSecure() As Boolean
            isSecure = True
        End Function

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

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByRef _CanSecureSensor As SBDeviceSecuritySensor)
            MyBase.New(_hs, _Ref, _CanSecureSensor)
        End Sub

        Public Overrides Function isSecure() As Boolean
            isSecure = True
        End Function

        Public Overrides Sub setSecure(ByVal secure As Boolean, ByVal force As Boolean, ByVal reportFailByNotification As Boolean)

        End Sub

    End Class

End Class
