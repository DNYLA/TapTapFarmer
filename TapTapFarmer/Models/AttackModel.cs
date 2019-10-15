using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapTapFarmer.Models
{

    class AttackModel
    {
        public bool Boss { get; set; } = false;
        public bool Friend { get; set; } = false;
        public bool Guild { get; set; } = false;
        public bool Brave { get; set; } = false;
        public bool DoS { get; set; } = false;
        public bool Expedition { get; set; } = false;
        public bool PlanetTrial { get; set; } = false;
        public int PlanetTrialRetryAmount { get; set; } = 3;
        public bool PlanetTrialAutoRetry { get; set; } = false;
        public int BraveMaxTickets { get; set; } = 0;
        public int BraveMaxCE { get; set; } = 500000;
        public bool BraveAutoRetry { get; set; } = false;
        public int GuildRetryAmount { get; set; } = 3;
        public int FriendRetryAmount { get; set; } = 3;
        public bool FriendMaxOnly { get; set; } = false;


    }
}
