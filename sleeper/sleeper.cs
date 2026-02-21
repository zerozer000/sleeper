using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Renci.SshNet;

namespace sleeper;

// main sleeper class
public class Sleeper
{
    //current os
    public static Platform CurrentOs = Platform.Unknown;
    //version
    public const string VERSION = "0.1";
    

    //config
    public static bool AlsoShutdownThisDevice = false; //will shutdown this device on ShutdownAll()

    //platform special for command: for windows - shutdown /s /t 0, for linux - shutdown now
    public enum Platform
    {
        Linux,
        Windows,
        Unknown
    }

    //devices list
    public static List<Device> Devices = new();
    public struct Device
    {
        public string Name;
        public string Description;
        public Platform Platform;

        //for ssh
        public string ip;
        public int port;
        public string user;
        public string password;
    }

    //init sleeper
    public static void InitSleeper()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            CurrentOs = Platform.Windows;
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            CurrentOs = Platform.Linux;
        }
    }
    //shutdown device
    public static void Shutdown(Device device)
    {
        //shutdown command for linux
        if (device.Platform == Platform.Linux)
        {
            var c = new SshClient(device.ip, device.user, device.password);
            try
            {
                using (c)
                {
                    c.Connect();
                    ShellStream shells = c.CreateShellStream("sleeper", 80, 24, 800, 600, 1024);
                    //var r = c.CreateCommand("shutdown now");
                    using SshCommand cmd = c.RunCommand($"echo \"{device.password}\"| sudo -S shutdown now");

                    c.Disconnect();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : {e.Message}");
            }

            Devices.Remove(device);
        }
        //shutdown command for windows
        if (device.Platform == Platform.Windows)
        {
            /*
            try
            {

            }
            catch( Exception e )
            {

            }
            */
            Console.WriteLine("sorry, no windows support yet :(");
            //Devices.Remove(device);
        }
        
        

        if (AlsoShutdownThisDevice == true)
        {
            Process.Start("rundll32.exe", $"powrprof.dll,SetSuspendState 0,1,0");
        }
    }

    public static Device CreateNewDevice(string name, string description, Platform platform, string ip, int port, string user, string password)
    {
        Device d = new Device();

        d.Name = name;
        d.Description = description;

        d.ip = ip;
        d.user = user;
        d.password = password;
        d.Platform = platform;
        d.port = port;

        Devices.Add(d);

        return d;
    }

    /// <summary>
    /// shuting down current device
    /// </summary>
    public static void ShutdownThisDevice()
    {
        if (CurrentOs == Platform.Windows)
        {
            Process.Start("rundll32.exe", $"powrprof.dll,SetSuspendState 0,1,0");
        }
        if (CurrentOs == Platform.Linux)
        {
            Console.WriteLine("sorry, no linux support yet :(");
        }
    }
    /// <summary>
    /// returns current os
    /// </summary>
    /// <returns></returns>
    public static Platform GetCurrentOS()
    {
        return CurrentOs;
    }

    /// <summary>
    /// returns current os as string
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentOSString()
    {
        string toreturn = "";

        if (CurrentOs == Platform.Linux)
            toreturn = "Linux";
        if (CurrentOs == Platform.Windows)
            toreturn = "Windows";
        if (CurrentOs == Platform.Unknown)
            toreturn = "Unknown";

        return toreturn;
    }

    public static bool AskConfirm()
    {
        Console.Write("Are you sure? [y / n] : ");
        bool r = false;

        string line = Console.ReadLine().ToLower();
        if (line == "y")
        {
            r = true;
        }
        else
        {
            r = false;
        }

        return r;
    }
    //config
    public class Config
    {
        public bool AlsoShutdownThisDevice {  get; set; }
    }
}