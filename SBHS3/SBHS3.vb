Module Module1
#Const SBNOHSDEV = 1

    Sub Main()
        Dim hs As New IHSApplication

        Dim sv As New SkyView(hs)


        sv.DebugListSecuritySensors()

        Console.WriteLine("Goodbye!")

        Dim pause = Console.ReadLine()
    End Sub


End Module
