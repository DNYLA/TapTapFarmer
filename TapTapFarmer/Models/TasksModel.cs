using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapTapFarmer.Models
{
    class TasksModel
    {
        public string BotVersion = string.Empty;
        public DateTime LastRan = new DateTime();
        public int Playerlevel = -1;
        public bool FriendsClaimed = false;
        public bool DailyClaimed = false;
        public bool Defeat3Claimed = false;
        public bool AlchemyClaimed = false;
        public bool SentHears = false;
        public bool SpunWheel = false;
        public bool CompletedTavern = false;
        public bool CombinedEquip = false;
        public bool PerformedCommon = false;
        public bool PerformedGrand = false;
        public bool CompletedBrave = false;
        public bool CompletedEvents = false;
        public bool CompletedQuests = false;
    }
}
