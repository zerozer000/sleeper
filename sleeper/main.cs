using sleeper;

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
            foreach (string arg in args)
            {
                if (string.IsNullOrEmpty(arg))
                {
                    Funcs.NullCommand();
                }
                else if (arg == "help")
                {
                    Funcs.Help();
                }
                else if (arg == "shutdown-all")
                {
                    Funcs.ShutdownAll();
                }
                else
                {
                    Funcs.InvalidCommand(arg);
                }
            }
        }
        
    }
}
