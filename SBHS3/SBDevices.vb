
Public Class SBDevices

    '
    ' Device Type, used to categorize devices
    '
    Enum SBDeviceType
        SwitchBinary
        SwitchDimmable

        SecurityControl
        SecuritySensor
        SceneController
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

        ' alias used to describe the device
        Protected AliasName As String

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByVal _DeviceType As SBDeviceType)
            hs = _hs
            Ref = _Ref
            DeviceType = _DeviceType
#If DEBUG Then
            hs.WriteLog("SBDevice", "created: Ref:" & Ref.ToString & " Type:" & DeviceType.ToString)
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
            Dim dc = hs.GetDeviceByRef(Ref)
            Return dc.Address(hs)
        End Function

        Public Function GetName() As String
            Dim dc = hs.GetDeviceByRef(Ref)
            Return dc.Name(hs)
        End Function

        Public Function GetDeviceValue() As Double
#If SBISHS3 Then
	      Dim rv As String = hs.CAPIGetStatus(Ref).Value
	      #If SBHS3DEBUG
	          hs.WriteLog("SBDevice", "GetDeviceValue: Ref: " & Ref.toString & " Value: " & rv)
		  Return rv
	      #End If
#Else
            Return 0
#End If
        End Function

        Public Function GetDeviceValueAsString() As String
#If SBISHS3 Then
            Dim DeviceValue As String = hs.CAPIGetStatus(Ref).Status
#If SBHS3DEBUG Then
            hs.WriteLog("SBDevice", "GetDeviceValueAsString from Ref:" & Ref.ToString & " Value: " & DeviceValue)
#End If
            Return DeviceValue
#Else
            Return "0"
#End If
        End Function

        Public Sub SetAliasName(ByVal _AliasName As String)
            AliasName = _AliasName
        End Sub

        Public Function GetAliasName() As String
            If IsNothing(AliasName) Then
                Return GetName()
            Else
                Return AliasName
            End If
        End Function

        Public Sub SetDeviceValue(ByRef Value As String)
#If SBHS3DEBUG Then
            hs.WriteLog("SBDevice", "SetDeviceValueAsString for Ref:" & Ref.ToString & " Value: " & Value)
#End If
#If SBISHS3 Then
            hs.CAPIControlHandler(hs.CAPIGetSingleControl(Ref,True,Value,False,False))
#End If
        End Sub

        ''' <summary>
        ''' Lookup a device by its reference to see if it's a valid device
        ''' </summary>
        ''' <param name="hs">handle to HomeSeer</param>
        ''' <param name="dRef">device reference to lookup</param>
        ''' <returns></returns>
        Public Shared Function DeviceRefValid(ByRef hs As IHSApplication, ByRef dRef As Integer) As Boolean
            Return Not IsNothing(hs.GetDeviceByRef(dRef))
        End Function

    End Class

    Public MustInherit Class SBDeviceSwitchBase
        Inherits SBDeviceBase

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer, ByVal _DeviceType As SBDeviceType)
            MyBase.New(_hs, _Ref, _DeviceType)
        End Sub

        Public MustOverride Sub turnOn()

        Public MustOverride Sub turnOff()

        Public MustOverride Function IsOn() As Boolean

    End Class

    Public Class SBDeviceSwitchBinary
        Inherits SBDeviceSwitchBase

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer)
            MyBase.New(_hs, _Ref, SBDeviceType.SwitchBinary)
        End Sub

        Public Overrides Sub turnOn()
            SetDeviceValue("On")
        End Sub

        Public Overrides Sub turnOff()
            SetDeviceValue("Off")
        End Sub

        Public Overrides Function IsOn() As Boolean
            Return GetDeviceValueAsString().Contains("On")
        End Function
    End Class

    Public Class SBDeviceSwitchDimmable
        Inherits SBDeviceSwitchBase

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer)
            MyBase.New(_hs, _Ref, SBDeviceType.SwitchBinary)
        End Sub

        Public Overrides Sub TurnOn()
            SetDeviceValue("On")
        End Sub

        Public Overrides Sub TurnOff()
            SetDeviceValue("Off")
        End Sub

        Public Sub DimTo(ByRef DimValue As Double)

#If SBHS3DEBUG > 5 Then
    hs.WriteLog(Me.GetType.Name, "DimTo Ref:" & Ref & " Dim:" & DimValue)
#End If
            Dim objCAPIControl = hs.CAPIGetSingleControlByUse(Ref, ePairControlUse._Dim)

            If IsNothing(objCAPIControl) Then
#If SBHS3DEBUG Then
                hs.WriteLog(Me.GetType.Name, "Failed to obtain CAPIControl object for device Ref:" & Ref)
#End If
                Throw New System.Exception("Failed to obtain CAPIControl object for device Ref:" & Ref)
            Else
                objCAPIControl.ControlValue = DimValue
                hs.CAPIControlHandler(objCAPIControl)

            End If
        End Sub

        Public Sub DimIfOnTo(ByRef DimValue As Double)
            If IsOn() Then
                DimTo(DimValue)
            End If
        End Sub


        Public Overrides Function IsOn() As Boolean
            Return Not GetDeviceValueAsString().Contains("Off")
        End Function
    End Class


    Public Class SBDeviceSceneController
        Inherits SBDeviceBase

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer)
            MyBase.New(_hs, _Ref, SBDeviceType.SceneController)
        End Sub

        Public Sub SetSceneActive(ByVal Active As Boolean)
            If Active Then
                SetDeviceValue("Scene On")
            Else
                SetDeviceValue("Scene Off")
            End If
        End Sub

    End Class


    '
    ' An abstract security device
    '
    ' Defines abstract methods:
    ' - isSecure()
    '
    Public MustInherit Class SBDeviceSecurityBase
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
        Inherits SBDeviceSecurityBase

        Public Sub New(ByRef _hs As IHSApplication, ByVal _Ref As Integer)
            MyBase.New(_hs, _Ref, SBDeviceType.SecuritySensor)
        End Sub

        Public Overrides Function IsSecure() As Boolean
            Dim Value As String = GetDeviceValueAsString()
            Dim Secure As Boolean = Value.Contains("Close")
#If SBHS3DEBUG > 5 Then
            hs.WriteLog("SBDevice", "IsSecure Ref: " & Ref & " Secure: " & Secure) 
#End If

            Return Secure
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
        Inherits SBDeviceSecurityBase

        Protected CanSecureSensor As SBDeviceSecuritySensor

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
            Dim Value As String = GetDeviceValueAsString()
            Dim Secure As Boolean = Value.Contains("Locked")

#If SBHS3DEBUG > 5 Then
            hs.WriteLog("SBDevice", "IsSecure Ref: " & Ref & " Secure: " & Secure) 
#End If
            Return Secure
        End Function

        Public Overrides Sub SetSecure(ByVal secure As Boolean, ByVal force As Boolean, ByVal reportFailByNotification As Boolean)
            If secure Then
                If Not IsSecure() Then
                    Dim doIt As Boolean = True

#If SBHS3DEBUG > 5 Then
                    hs.WriteLog("SBDevice", "Can we lock the door? Force: " & force & " sensor defined: " & Not IsNothing(CanSecureSensor)) 
#End If

                    If Not force And Not IsNothing(CanSecureSensor) Then
                        doIt = CanSecureSensor.IsSecure
                    End If
                    If doIt Then
                        SetDeviceValue("Lock")
                    Else
                        If reportFailByNotification Then
                            SBSingleton.GetNotify().SendInfoMsg("Cannot lock: " & GetName(), "Sensor associated sensor with " & GetName() & " not in secure state")
                        End If
                    End If
                End If
            Else
                If IsSecure() Then
                    SetDeviceValue("Unlock")
                End If
            End If
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
            Dim Value As String = GetDeviceValueAsString()
            Return Value.Contains("Closed")
        End Function

        Public Overrides Sub SetSecure(ByVal secure As Boolean, ByVal force As Boolean, ByVal reportFailByNotification As Boolean)
            If secure Then
                If Not IsSecure() Then
                    Dim doIt As Boolean = True

#If SBHS3DEBUG > 5 Then
                    hs.WriteLog("SBDevice", "Can we lock the door? Force: " & force & " sensor defined: " & Not IsNothing(CanSecureSensor)) 
#End If

                    If Not force And Not IsNothing(CanSecureSensor) Then
                        doIt = CanSecureSensor.IsSecure
                    End If
                    If doIt Then
                        SetDeviceValue("Close")
                    Else
                        If reportFailByNotification Then
                            SBSingleton.GetNotify().SendInfoMsg("Cannot lock: " & GetName(), "Sensor associated sensor with " & GetName() & " not in secure state")
                        End If
                    End If
                End If
            Else
                If IsSecure() Then
                    SetDeviceValue("Open")
                End If
            End If
        End Sub

    End Class

End Class
