using IniParser;
using IniParser.Model;
using IniParser.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTapFarmer.Models;

namespace TapTapFarmer.Functions.Ini
{
    class ReadIni
    {
        public static void ReadFile()
        {
            var IniFile = "BotConfig.Ini";
            

            try
            {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(IniFile);
                GlobalVariables.LAST_RAN = DateTime.Parse(data["General"]["Bot Last Ran"]);
                GlobalVariables.CURRENCY_INFO[2] = Convert.ToInt32(data["General"]["Player Level"]);
                AttackModel attackModel = new AttackModel
                {
                    Boss = Boolean.Parse(data["Battle"]["Boss Battle"]),
                    Friend = Boolean.Parse(data["Battle"]["Friend Battle"]),
                    Guild = Boolean.Parse(data["Battle"]["Guild Battle"]),
                    Brave = Boolean.Parse(data["Battle"]["Brave Battle"]),
                    DoS = Boolean.Parse(data["Battle"]["DoS Battle"]),
                    Expedition = Boolean.Parse(data["Battle"]["Expedition Battle"]),
                    PlanetTrial = Boolean.Parse(data["Battle"]["Planet Trial Battle"]),

                    PlanetTrialRetryAmount = Convert.ToInt32(data["Battle Extra"]["Planet Trial Retry"]),
                    PlanetTrialAutoRetry = Boolean.Parse(data["Battle Extra"]["Planet Trial Auto Retry"]),

                    BraveMaxTickets = Convert.ToInt32(data["Battle Extra"]["Brave Max Tickets"]),
                    BraveMaxCE = Convert.ToInt32(data["Battle Extra"]["Brave Max CE"]),
                    BraveAutoRetry = Boolean.Parse(data["Battle Extra"]["Brave Auto Retry"]),

                    GuildRetryAmount = Convert.ToInt32(data["Battle Extra"]["Guild Retry"]),

                    FriendRetryAmount = Convert.ToInt32(data["Battle Extra"]["Friend Retry"]),
                    FriendMaxOnly = Boolean.Parse(data["Battle Extra"]["Friend Auto Retry"])
                };

                GlobalVariables.attackSettings = attackModel;

                DailyModel dailyModel = new DailyModel
                {
                    Alchemy = Boolean.Parse(data["Daily"]["Alchemy"]),
                    SendHearts = Boolean.Parse(data["Daily"]["Send Hearts"]),
                    SpinWheel = Boolean.Parse(data["Daily"]["Spin Wheel"]),
                    DailyTavern = Boolean.Parse(data["Daily"]["Daily 2 Tavern"]),
                    CombineEquip = Boolean.Parse(data["Daily"]["Combine Equipments"]),
                    CommonSummon = Boolean.Parse(data["Daily"]["Common Summon"]),
                    GrandSummon = Boolean.Parse(data["Daily"]["Grand Summon"]),
                    DailyBrave = Boolean.Parse(data["Daily"]["Daily Brave League"]),

                    SendFriendReq = Boolean.Parse(data["Daily Extra"]["Send Friend Requests"]),
                    AcceptFreindReq = Boolean.Parse(data["Daily Extra"]["Accept Friend Requests"]),
                    DeclineFriendReq = Boolean.Parse(data["Daily Extra"]["Decline Friend Requests"]),
                    DailyBag = Boolean.Parse(data["Daily Extra"]["Claim Free Gift Bad"]),
                    PurchaseDoSTicket = Boolean.Parse(data["Daily Extra"]["Purchase DoS Tickets"]),
                    DeleteMail = Boolean.Parse(data["Daily Extra"]["Delete Mail"])
                };

                GlobalVariables.dailySettings = dailyModel;
            }
            catch
            {
                Main.LogConsole("Error Reading Config! Reseting Config....");
                File.Delete("BotConfig.ini");
                WriteIni.WriteConfig();
            }
            
        }
    }
}
