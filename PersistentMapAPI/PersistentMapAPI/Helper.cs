﻿using BattleTech;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace PersistentMapAPI {
    public static class Helper {
        public static string currentMapFilePath = $"../Map/current.json";
        public static string settingsFilePath = $"../Settings/settings.json";
        public static string systemDataFilePath = $"../StarSystems/";

        
        public static StarMap initializeNewMap() {
            Console.WriteLine("Map Init Started");
            StarMap map = new StarMap();
            map.systems = new List<System>();
            foreach (string filePaths in Directory.GetFiles(systemDataFilePath)) {
                string originalJson = File.ReadAllText(filePaths);
                JObject originalJObject = JObject.Parse(originalJson);
                Faction owner = (Faction)Enum.Parse(typeof(Faction), (string)originalJObject["Owner"]);
                
                FactionControl ownerControl = new FactionControl();
                ownerControl.faction = owner;
                if (owner != Faction.NoFaction) {
                    ownerControl.percentage = 100;
                } else {
                    ownerControl.percentage = 0;
                }

                System system = new System();
                system.controlList = new List<FactionControl>();
                system.name = (string)originalJObject["Description"]["Name"];
                system.controlList.Add(ownerControl);
                
                map.systems.Add(system);
            }
            return map;
        }

        public static StarMap LoadCurrentMap() {
            if (Holder.currentMap == null) {
                if (File.Exists(currentMapFilePath)) {
                    using (StreamReader r = new StreamReader(currentMapFilePath)) {
                        string json = r.ReadToEnd();
                        Holder.currentMap = JsonConvert.DeserializeObject<StarMap>(json);
                    }
                }
                else {
                    Holder.currentMap = initializeNewMap();
                }
            }
            StarMap result = Holder.currentMap;
            Console.WriteLine("Map Loaded");
            return result;
        }

        public static void SaveCurrentMap(StarMap map) {
            (new FileInfo(currentMapFilePath)).Directory.Create();
            using (StreamWriter writer = new StreamWriter(currentMapFilePath, false)) {
                string json = JsonConvert.SerializeObject(map);
                writer.Write(json);
            }
            Console.WriteLine("Map Saved");
        }

        public static Settings LoadSettings() {
            Settings settings;
            if (File.Exists(settingsFilePath)) {
                using (StreamReader r = new StreamReader(settingsFilePath)) {
                    string json = r.ReadToEnd();
                    settings = JsonConvert.DeserializeObject<Settings>(json);
                }
            }
            else {
                settings = new Settings();
                (new FileInfo(settingsFilePath)).Directory.Create();
                using (StreamWriter writer = new StreamWriter(settingsFilePath, false)) {
                    string json = JsonConvert.SerializeObject(settings);
                    writer.Write(json);
                }
            }
            Console.WriteLine("Settings Loaded");
            return settings;
        }

        public static string GetIP() {
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint =
               prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endpoint.Address;

            return ip;

        }

        public static bool IsSpam(string ip) {
            if (Holder.connectionStore.ContainsKey(ip)) {
                if(Holder.connectionStore[ip].AddMinutes(LoadSettings().minMinutesBetweenPost) > DateTime.Now) {
                    return true;
                } else {
                    Holder.connectionStore[ip] = DateTime.Now;
                }
            } else {
                Holder.connectionStore.Add(ip, DateTime.Now);
            }
            return false;

        }
    }
}
