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
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(IniFile);

            try
            {
                GlobalVariables.LAST_RAN = DateTime.Parse(data["General"]["Bot Last Ran"]);
                GlobalVariables.CURRENCY_INFO[2] = Convert.ToInt32(data["General"]["Player Level"]);
                AttackModel attackModel = new AttackModel();

                attackModel.Boss = Boolean.Parse(data["Battle"]["Boss Battle"]);
                attackModel.Friend = Boolean.Parse(data["Battle"]["Friend Battle"]);
                attackModel.Guild = Boolean.Parse(data["Battle"]["Guild Battle"]);
                attackModel.Brave = Boolean.Parse(data["Battle"]["Brave Battle"]);
                attackModel.DoS = Boolean.Parse(data["Battle"]["DoS Battle"]);
                attackModel.Expedition = Boolean.Parse(data["Battle"]["Expedition Battle"]);
                attackModel.PlanetTrial = Boolean.Parse(data["Battle"]["Planet Trial Battle"]);

                attackModel.PlanetTrialRetryAmount = Convert.ToInt32(data["Battle Extra"]["Planet Trial Retry"]);
                attackModel.PlanetTrialAutoRetry = Boolean.Parse(data["Battle Extra"]["Planet Trial Auto Retry"]);

                attackModel.BraveMaxTickets = Convert.ToInt32(data["Battle Extra"]["Brave Max Tickets"]);
                attackModel.BraveMaxCE = Convert.ToInt32(data["Battle Extra"]["Brave Max CE"]);
                attackModel.BraveAutoRetry = Boolean.Parse(data["Battle Extra"]["Brave Auto Retry"]);

                attackModel.GuildRetryAmount = Convert.ToInt32(data["Battle Extra"]["Guild Retry"]);

                attackModel.FriendRetryAmount = Convert.ToInt32(data["Battle Extra"]["Friend Retry"]);
                attackModel.FriendMaxOnly = Boolean.Parse(data["Battle Extra"]["Friend Auto Retry"]);

                GlobalVariables.attackSettings = attackModel;

                DailyModel dailyModel = new DailyModel();

                dailyModel.Alchemy = Boolean.Parse(data["Daily"]["Alchemy"]);
                dailyModel.SendHearts = Boolean.Parse(data["Daily"]["Send Hearts"]);
                dailyModel.SpinWheel = Boolean.Parse(data["Daily"]["Spin Wheel"]);
                dailyModel.DailyTavern = Boolean.Parse(data["Daily"]["Daily 2 Tavern"]);
                dailyModel.CombineEquip = Boolean.Parse(data["Daily"]["Combine Equipments"]);
                dailyModel.CommonSummon = Boolean.Parse(data["Daily"]["Common Summon"]);
                dailyModel.GrandSummon = Boolean.Parse(data["Daily"]["Grand Summon"]);
                dailyModel.DailyBrave = Boolean.Parse(data["Daily"]["Daily Brave League"]);

                dailyModel.SendFriendReq = Boolean.Parse(data["Daily Extra"]["Send Friend Requests"]);
                dailyModel.AcceptFreindReq = Boolean.Parse(data["Daily Extra"]["Accept Friend Requests"]);
                dailyModel.DeclineFriendReq = Boolean.Parse(data["Daily Extra"]["Decline Friend Requests"]);
                dailyModel.DailyBag = Boolean.Parse(data["Daily Extra"]["Claim Free Gift Bad"]);
                dailyModel.PurchaseDoSTicket = Boolean.Parse(data["Daily Extra"]["Purchase DoS Tickets"]);
                dailyModel.DeleteMail = Boolean.Parse(data["Daily Extra"]["Delete Mail"]);

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
