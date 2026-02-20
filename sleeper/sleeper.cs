using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
        public int Id;
        public string Name;
        public string Description;
        public Platform Platform;

        public bool IsActive;

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
            Process.Start("ssh", $"-p \"{device.password}\" {device.user}@{device.ip} \"shutdown now\"");
        }
        //shutdown command for windows
        if (device.Platform == Platform.Windows)
        {
            Process.Start("ssh", $"-p \"{device.password}\" {device.user}@{device.ip} \"shutdown /s /t 0\"");
        }
        Devices.Remove(device);

        if (AlsoShutdownThisDevice == true)
        {
            Process.Start("rundll32.exe", $"powrprof.dll,SetSuspendState 0,1,0");
        }
    }

    public static void CreateNewDevice(string name, string description, Platform platform, string ip, int port, string user, string password)
    {
        Device device = new Device();


        Devices.Add(device);
    }

    /// <summary>
    /// returns current os as Platform struct
    /// </summary>
    /// <returns></returns>
    public static Platform GetCurrentOS()
    {
        return CurrentOs;
    }

    /// <summary>
    /// return current os as string
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
    public class Config
    {
        public bool AlsoShutdownThisDevice {  get; set; }
    }
}