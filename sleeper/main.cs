using sleeper;
using System.Linq;
using System;

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
                else if (arg.Value == "new")
                {
                    // new device
                    Console.WriteLine("still in work! (nothing happen)");
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
