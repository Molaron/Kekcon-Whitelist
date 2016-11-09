using System;
using System.ComponentModel.Composition;
using BattleNET;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text;

namespace Whitelist
{
    [Export(typeof(Kekcon.IKekPlugin))]
    public class Whitelist : Kekcon.IKekPlugin
    {
        internal static BattlEyeClient beclient;
        private static string[] config;
        private static Match querycommand;

        private static string[] readConfig()
        {
            string[] configRet = new string[4];
            if (File.Exists("plugins/config/whitelist.cfg"))
            {
                string[] configRaw = File.ReadAllLines("plugins/config/whitelist.cfg");
                for (int i = 0; i < 4; i++)
                {
                    string[] typeArray = configRaw[i].Split('=');
                    switch (typeArray[0].ToLower().Trim())
                    {
                        case "activated":
                            configRet[0] = typeArray[1].Trim();
                            break;
                        case "kickreason":
                            configRet[1] = typeArray[1].Trim();
                            break;
                        case "whitelist":
                            configRet[2] = typeArray[1].Trim();
                            break;
                        case "admins":
                            configRet[3] = typeArray[1].Trim();
                            break;
                        default:
                            break;
                    }
                }
            }
            return configRet;
        }

        public bool Init(BattlEyeClient client)
        {
            beclient = client;
            if (!File.Exists("plugins/config/whitelist.cfg"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=> Plugin Fehler:\tWhitelist (Konfigfehler: whitelist.cfg nicht gefunden)");
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }

            config = readConfig();

            if(config.Length < 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=> Plugin Fehler:\tWhitelist (Konfigfehler: Nicht alle Parameter angegeben)");
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }

            if(!(config[0] == "true" || config[0] == "false"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=> Plugin Fehler:\tWhitelist (Konfigfehler: activated <true/false>)");
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }

            if(config[1] == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=> Plugin Fehler:\tWhitelist (Konfigfehler: kickreason <string>)");
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }

            if(config[2] == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=> Plugin Fehler:\tWhitelist (Konfigfehler: whitelist <guid,guid,guid,...>)");
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }

            if(config[3] == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=> Plugin Fehler:\tWhitelist (Konfigfehler: admins <guid,guid,guid,...>)");
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }

            client.BattlEyeMessageReceived += BattlEyeMessageReceived;

            if (config[0] != "true")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=> Plugin deaktiviert:\tWhitelist");
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }
           
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=> Plugin geladen:\tWhitelist");
            Console.ForegroundColor = ConsoleColor.Gray;

            return true;
        }

        private static void WaitingForPlayerList(BattlEyeMessageEventArgs args)
        {
            Regex playerregex = new Regex("Players on server:");
            Match playermatch = playerregex.Match(args.Message);

            if (playermatch.Success)
            {
                beclient.BattlEyeMessageReceived -= WaitingForPlayerList;

                string name = querycommand.Groups[1].Value;
                string cmd = querycommand.Groups[2].Value;
                char splitter = ' ';
                string[] argus = querycommand.Groups[3].Value.Split(splitter);

                string players = args.Message.Substring(114);

                Regex playerregexsub = new Regex("(\\d+)\\s+\\b\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}:\\d+\\b\\s+-?\\d+\\s+([0-9a-fA-F]+)\\(\\w+\\)\\s([\\S ]+)");
                Match playermatchsub = playerregexsub.Match(players);

                while (playermatchsub.Success)
                {
                    string p_beid = playermatchsub.Groups[1].Value;
                    string p_guid = playermatchsub.Groups[2].Value;
                    string p_name = playermatchsub.Groups[3].Value;

                    if (name == p_name)
                    {
                        if (config[3].Contains(p_guid))
                        {
                            if(cmd == "whitelist")
                            {
                                if (argus.Length < 1)
                                {
                                    beclient.SendCommand("say " + p_beid + "Kekcon: Falscher Syntax (whitelist <on/off/status/reload>).");
                                }
                                else
                                {
                                    string command;
                                    string[] arr;
                                    StreamWriter writer;


                                    switch (argus[0])
                                    {
                                        case "on":
                                            beclient.SendCommand("say " + p_beid + "Kekcon: Whitelist aktiviert.");
                                            config[0] = "true";
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("=> Plugin aktiviert:\tWhitelist");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            arr = File.ReadAllLines("plugins/config/whitelist.cfg");

                                            writer = new StreamWriter("plugins/config/whitelist.cfg");
                                            for (int i = 0; i < arr.Length; i++)
                                            {
                                                string line = arr[i];
                                                if (line.Trim().Substring(0, 9) == "activated")
                                                {
                                                    line = "activated = true";
                                                }
                                                writer.WriteLine(line);
                                            }
                                            writer.Close();
                                            break;
                                        case "off":
                                            beclient.SendCommand("say " + p_beid + "Kekcon: Whitelist deaktiviert.");
                                            config[0] = "false";
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("=> Plugin deaktiviert:\tWhitelist");
                                            Console.ForegroundColor = ConsoleColor.Gray;

                                            arr = File.ReadAllLines("plugins/config/whitelist.cfg");

                                            writer = new StreamWriter("plugins/config/whitelist.cfg");
                                            for (int i = 0; i < arr.Length; i++)
                                            {
                                                string line = arr[i];
                                                if (line.Trim().Substring(0, 9) == "activated")
                                                {
                                                    line = "activated = false";
                                                }
                                                writer.WriteLine(line);
                                            }
                                            writer.Close();
                                            break;
                                        case "status":
                                            if(config[0] == "true")
                                            {
                                                splitter = ',';
                                                command = string.Format("say " + p_beid + "Kekcon: Whitelist ist aktiviert ({0} GUID's gewhitelistet).", config[2].Split(splitter).Count());
                                                beclient.SendCommand(command);
                                            }
                                            else
                                            {
                                                beclient.SendCommand("say " + p_beid + "Kekcon: Whitelist ist deaktiviert.");
                                            }
                                            break;
                                        default:
                                            beclient.SendCommand("say " + p_beid + "Kekcon: Falscher Syntax (whitelist <on/off/status/reload>).");
                                            break;
                                        case "reload":
                                            config = readConfig();
                                            beclient.SendCommand("say " + p_beid + "Kekcon: Konfiguration neu geladen.");
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("=> Plugin Konfig-Reload:\tWhitelist");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                beclient.SendCommand("say " + p_beid + "Kekcon: Unbekannter Befehl.");
                            }                           
                        }
                        else
                        {
                            beclient.SendCommand("say "+ p_beid + "Kekcon: Keine Berechtigung.");
                        }
                    }

                    playermatchsub = playermatchsub.NextMatch();
                }
            }
        }

        private static void BattlEyeMessageReceived(BattlEyeMessageEventArgs args)
        {
            Regex regex = new Regex("Player #([0-9]{1,}) (.*) - GUID: (.*)");
            Match match = regex.Match(args.Message);
            Regex commandregex = new Regex("\\G\\(.[a-z]+\\) (.*): !([^\\s]+) ?(.*)");
            Match commandmatch = commandregex.Match(args.Message, 0);

            if(commandmatch.Success)
            {
                beclient.SendCommand("players");
                beclient.BattlEyeMessageReceived += WaitingForPlayerList;
                querycommand = commandmatch;
            }

            if (match.Success && config[0] == "true")
            {
                string beid = match.Groups[1].Value;
                string name = match.Groups[2].Value;
                string guid = match.Groups[3].Value;

                string[] whitelist = config[2].Split(',');

                if(!whitelist.Contains(guid))
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(config[1]);
                    beclient.SendCommand(string.Format("kick {0} {1}", beid, Encoding.Default.GetString(bytes)));
                }
            }
        }
    }
}