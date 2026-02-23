using sleeper;
using System.Linq;
using System;
using System.Collections.Generic;

namespace sleeper_app;

// main class
public class MainClass
{
    public static void Main(string[] args)
    {
        Sleeper.InitSleeper(); // initing sleeper

        if (args.Length == 0)
        {
            Funcs.NullCommand();
        }
        else if(args.Length >= 0)
        {
            foreach (var arg in args.Select((value, i) => new { Value = value, Index = i }))
            {
                if (string.IsNullOrEmpty(arg.Value))
                {
                    Funcs.NullCommand();
                }
                else if (arg.Value == "help")
                {
                    Funcs.Help();
                }
                else if (arg.Value == "shutdown-all")
                {
                    if(Sleeper.AskConfirm())
                        Funcs.ShutdownAll();
                }
                else if (arg.Value == "shutdown-new")
                {
                    //Console.WriteLine($"{args[arg.Index+1]}, {args[arg.Index + 2]}, {args[arg.Index + 3]}"); // test showing
                    Funcs.ShutdownNew(args[arg.Index+1], args[arg.Index + 2], args[arg.Index + 3], Sleeper.Platform.Linux);
                }
                else if (arg.Value == "shutdown")
                {

                    Funcs.ShutdownByName(args[arg.Index + 1]);
                }
                else if (arg.Value == "new")
                {
                    // new device

                    Sleeper.Device devicetoadd = Sleeper.CreateNewDevice(args[arg.Index + 1], args[arg.Index + 2],
                        Sleeper.GetPlatformFromString(args[arg.Index + 3]),
                        args[arg.Index + 4], int.Parse(args[arg.Index + 5]), args[arg.Index + 6], args[arg.Index + 7]);
                    sleeper.Json.AddDevice(devicetoadd, $"{AppContext.BaseDirectory}/devices.json");
                }
                else if (arg.Value == "remove")
                {
                    // remove device
                    Console.WriteLine("still in work! (nothing happen)");
                }
                else if (arg.Value == "shutdown-this")
                {
                    if (Sleeper.AskConfirm())
                    {
                        Sleeper.ShutdownThisDevice();
                    }
                }
                else if (arg.Value == "test")
                {

                }
                /*
                else
                {
                    Funcs.InvalidCommand(arg.Value);
                }
                */
            }
        }
        
    }
}
