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
                          "shutdown-all - shutdowns all saved devices\n");
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
}
