Module Module1

    Sub Main()
        Dim hs As New IHSApplication

        Dim sv As New SkyView(hs)

        Dim ss As Hashtable = sv.getSecuritySensors()

        sv.DebugListSecurityAllDevices()

        Console.WriteLine("Goodbye!")

        Dim pause = Console.ReadLine()
    End Sub


End Module
