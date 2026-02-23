using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using static sleeper.Sleeper;

namespace sleeper
{
    public class Json
    {
        public static string MainDevicesFile = $"{AppContext.BaseDirectory}/devices.json";
        public static List<Device> GetDevicesFromFile(string path)
        {

            List<Device> devices = new List<Device>();
            try
            {
                StreamReader fs = new(path);
                string file = fs.ReadToEnd();


                JsonDocument jd = JsonDocument.Parse(file);
                JsonElement root = jd.RootElement;

                JsonElement jedevices = root.GetProperty("Devices");


                foreach (JsonProperty device in jedevices.EnumerateObject())
                {
                    Device d = new Device();

                    d.Name = device.Value.GetProperty("Name").ToString();
                    d.Description = device.Value.GetProperty("Description").ToString();

                    d.ip = device.Value.GetProperty("ip").ToString();
                    d.user = device.Value.GetProperty("user").ToString();
                    d.password = device.Value.GetProperty("password").ToString();

                    d.Platform = Sleeper.GetPlatformFromString(device.Value.GetProperty("Platform").ToString());

                    d.port = device.Value.GetProperty("port").GetInt32();


                    devices.Add(d);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in getting devices from json file : " + ex);
            }
            
            return devices;
        }

        public static void AddDevice(Device device, string filepath)
        {
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw1 = new StreamWriter(filepath))
                {
                    sw1.Write("{\r\n  \"Devices\":{\r\n  }\r\n}");
                    sw1.Close();
                }
                
            }
            try
            {
                
                    

                string toaddplatform = "unknown";
                if(device.Platform == Platform.Unknown)
                    toaddplatform = "unknown";
                else if(device.Platform == Platform.Windows)
                    toaddplatform = "win";
                else if (device.Platform == Platform.Linux)
                    toaddplatform = "linux";


                var toadd = new JsonObject()
                {
                    ["Name"] = device.Name,
                    ["Description"] = device.Description,

                    ["Platform"] = toaddplatform,
                    ["ip"] = device.ip,
                    ["port"] = device.port,
                    ["user"] = device.user,
                    ["password"] = device.password
                };

                StreamReader sr = new(filepath);
                string file = sr.ReadToEnd();
                sr.Close();

                JsonDocument jd = JsonDocument.Parse(file);
                JsonNode rootNode = JsonNode.Parse(file)!;

                int devicecount = jd.RootElement.GetProperty("Devices").EnumerateObject().Count();

                bool isexists = false;
                /*
                foreach (JsonProperty jp in jd.RootElement.GetProperty("Devices").EnumerateObject())
                {
                    if(jp.Name == device.Name)
                        isexists = true;
                    if (jd.RootElement.GetProperty("Devices").GetProperty("Name").GetString() == device.Name)
                        isexists = true;
                }
                
                if (isexists == false)
                    rootNode["Devices"]![$"{device.Name}"] = toadd;
                else
                    Console.WriteLine($"Device {device.Name} is already exists");
                    rootNode["Devices"]![$"{device.Name}{Random.Shared.Next(1, 100)}"] = toadd;
                */
                rootNode["Devices"]![$"{device.Name}"] = toadd;
                string updatedJson = rootNode.ToJsonString(new JsonSerializerOptions { WriteIndented = true });



                StreamWriter sw = new(filepath);
                sw.Write(updatedJson);
                sw.Close();

                Console.WriteLine($"Device ({device.Name}) was successfuly added.");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Failed to adding device in json file : {e}");
                if (Sleeper.AskConfirmMsg("Do you want to recreate file?"))
                {
                    if (!File.Exists(filepath))
                    {
                        using (StreamWriter sw1 = new StreamWriter(filepath))
                        {
                            sw1.Write("{\r\n  \"Devices\":{\r\n  }\r\n}");
                            sw1.Close();
                        }

                    }
                    else
                    {
                        using (StreamWriter sw1 = new StreamWriter(filepath))
                        {
                            sw1.Write("{\r\n  \"Devices\":{\r\n  }\r\n}");
                            sw1.Close();
                        }
                    }

                    if (Sleeper.AskConfirmMsg("Do you want to try again?"))
                    {
                        AddDevice(device, filepath);
                    }
                }
            } 
        }

        public static void RemoveDevice(Device device, string filepath)
        {

        }
    }
}
