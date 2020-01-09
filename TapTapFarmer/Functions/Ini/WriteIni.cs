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
    class WriteIni
    {
        #region Write New Config
        public static void WriteConfig()
        {
            var IniFile = "BotConfig.Ini";

            var parser = new IniDataParser();

            if (!File.Exists(IniFile))
            {
                StreamWriter c = new StreamWriter(IniFile);
                c.Dispose();
            }
            else
            {
                return;
            }

            IniData parsedData;

            using (FileStream fs = File.Open(IniFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8))
                {
                    parsedData = parser.Parse(sr.ReadToEnd());
                }
            }

            // Modify the loaded ini file
            //General Header
            parsedData["General"]["version"] = "1.0";
            parsedData["General"]["Bot Last Ran"] = DateTime.Now.ToString();
            parsedData["General"]["Player Last"] = "0";

            parsedData["Battle"]["Boss Battle"] = "false";
            parsedData["Battle"]["Friend Battle"] = "false";
            parsedData["Battle"]["Guild Battle"] = "false";
            parsedData["Battle"]["Brave Battle"] = "false";
            parsedData["Battle"]["DoS Battle"] = "false";
            parsedData["Battle"]["Expedition Battle"] = "false";
            parsedData["Battle"]["Planet Trial Battle"] = "false";

            parsedData["Battle Extra"]["Planet Trial Retry"] = "3";
            parsedData["Battle Extra"]["Planet Trial Auto Retry"] = "false";

            parsedData["Battle Extra"]["Brave Max Tickets"] = "3";
            parsedData["Battle Extra"]["Brave Max CE"] = "3";
            parsedData["Battle Extra"]["Brave Auto Retry"] = "false";

            parsedData["Battle Extra"]["Guild Retry"] = "3";

            parsedData["Battle Extra"]["Friend Retry"] = "3";
            parsedData["Battle Extra"]["Friend Auto Retry"] = "false";

            parsedData["Daily"]["Alchemy"] = "false";
            parsedData["Daily"]["Send Hearts"] = "false";
            parsedData["Daily"]["Spin Wheel"] = "false";
            parsedData["Daily"]["Daily 2 Tavern"] = "false";
            parsedData["Daily"]["Combine Equipments"] = "false";
            parsedData["Daily"]["Common Summon"] = "false";
            parsedData["Daily"]["Grand Summon"] = "false";
            parsedData["Daily"]["Daily Brave League"] = "false";

            parsedData["Daily Extra"]["Send Friend Requests"] = "false";
            parsedData["Daily Extra"]["Accept Friend Requests"] = "false";
            parsedData["Daily Extra"]["Decline Friend Requests"] = "false";
            parsedData["Daily Extra"]["Claim Free Gift Bad"] = "false";
            parsedData["Daily Extra"]["Purchase DoS Tickets"] = "false";
            parsedData["Daily Extra"]["Delete Mail"] = "false";

            var fileparser = new FileIniDataParser();
            fileparser.WriteFile(IniFile, parsedData);
        }
        #endregion

        #region Update Config
        public static void UpdateConfig(AttackModel attackModel, DailyModel dailyModel)
        {
            var IniFile = "BotConfig.Ini";

            var parser = new IniDataParser();

            IniData parsedData;

            using (FileStream fs = File.Open(IniFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8))
                {
                    parsedData = parser.Parse(sr.ReadToEnd());
                }
            }

            // Modify the loaded ini file
            //General Header
            parsedData["General"]["version"] = "1.0";
            parsedData["General"]["Bot Last Ran"] = GlobalVariables.LAST_RAN.ToString();
            parsedData["General"]["Player Last"] = "0";
            //parsedData["General"]["Bot Last Ran"] = DateTime.Now.ToString();

            parsedData["Battle"]["Boss Battle"] = attackModel.Boss.ToString();
            parsedData["Battle"]["Friend Battle"] = attackModel.Friend.ToString();
            parsedData["Battle"]["Guild Battle"] = attackModel.Guild.ToString();
            parsedData["Battle"]["Brave Battle"] = attackModel.Brave.ToString();
            parsedData["Battle"]["DoS Battle"] = attackModel.DoS.ToString();
            parsedData["Battle"]["Expedition Battle"] = attackModel.Expedition.ToString();
            parsedData["Battle"]["Planet Trial Battle"] = attackModel.PlanetTrial.ToString();

            parsedData["Battle Extra"]["Planet Trial Retry"] = attackModel.PlanetTrialRetryAmount.ToString();
            parsedData["Battle Extra"]["Planet Trial Auto Retry"] = attackModel.PlanetTrialAutoRetry.ToString();

            parsedData["Battle Extra"]["Brave Max Tickets"] = attackModel.BraveMaxTickets.ToString();
            parsedData["Battle Extra"]["Brave Max CE"] = attackModel.BraveMaxCE.ToString();
            parsedData["Battle Extra"]["Brave Auto Retry"] = attackModel.BraveAutoRetry.ToString();

            parsedData["Battle Extra"]["Guild Retry"] = attackModel.GuildRetryAmount.ToString();

            parsedData["Battle Extra"]["Friend Retry"] = attackModel.FriendRetryAmount.ToString();
            parsedData["Battle Extra"]["Friend Auto Retry"] = attackModel.FriendMaxOnly.ToString();

            parsedData["Daily"]["Alchemy"] = dailyModel.Alchemy.ToString();
            parsedData["Daily"]["Send Hearts"] = dailyModel.SendHearts.ToString();
            parsedData["Daily"]["Spin Wheel"] = dailyModel.SpinWheel.ToString();
            parsedData["Daily"]["Daily 2 Tavern"] = dailyModel.DailyTavern.ToString();
            parsedData["Daily"]["Combine Equipments"] = dailyModel.CombineEquip.ToString();
            parsedData["Daily"]["Common Summon"] = dailyModel.CommonSummon.ToString();
            parsedData["Daily"]["Grand Summon"] = dailyModel.GrandSummon.ToString();
            parsedData["Daily"]["Daily Brave League"] = dailyModel.DailyBrave.ToString();

            parsedData["Daily Extra"]["Send Friend Requests"] = dailyModel.SendFriendReq.ToString();
            parsedData["Daily Extra"]["Accept Friend Requests"] = dailyModel.AcceptFreindReq.ToString();
            parsedData["Daily Extra"]["Decline Friend Requests"] = dailyModel.DeclineFriendReq.ToString();
            parsedData["Daily Extra"]["Claim Free Gift Bad"] = dailyModel.DailyBag.ToString();
            parsedData["Daily Extra"]["Purchase DoS Tickets"] = dailyModel.PurchaseDoSTicket.ToString();
            parsedData["Daily Extra"]["Delete Mail"] = dailyModel.DeleteMail.ToString();

            var fileparser = new FileIniDataParser();
            fileparser.WriteFile(IniFile, parsedData);
        }
        #endregion
    }
}
