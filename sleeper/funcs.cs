using System;
using sleeper;

namespace sleeper_app;

public class Funcs
{
    // just an app title
    public static void NullCommand()
    {
        Console.WriteLine("sleeper by zero000zer ( zerozer000 )\nenter sleeper help for command list");
    }
    // invalid command
    public static void InvalidCommand(string command)
    {
        Console.WriteLine($"invalid command \"{command}\" enter \"sleeper help\" for command list");
    }
    // help / command list
    public static void Help()
    {
        Console.WriteLine("sleeper help - shows command list\n" +
                          "shutdown-all - shutdowns all saved devices\n" +
                          "shutdown-new [ip] [user] [password] - shutdowns device without saving it\n" +
                          "shutdown-this - shutdowns this device\n" +
                          "new [name] [ip] [user] [password] - adding new device\n" +
                          "remove [name] -\n");
    }
    // shutdown all devices
    public static void ShutdownAll()
    {
        Console.WriteLine("Shutdowning all dedvices...");
        foreach (var d in Sleeper.Devices)
        {
            Sleeper.Shutdown(d);
        }
    }

    public static void ShutdownNew(string ip, string user, string password, Sleeper.Platform platform)
    {
        Sleeper.Device d = Sleeper.CreateNewDevice("", "", platform, ip, 21, user, password);

        Sleeper.Shutdown(d);
    }
}
