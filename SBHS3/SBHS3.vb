Module Module1

    Sub Main()
        Dim hs As New IHSApplication


        Dim d1 As New SBDevices.SBDeviceSecuritySensor(hs, 43)

        Console.WriteLine("Goodbye")

        Dim pause = Console.ReadLine()
    End Sub


End Module
