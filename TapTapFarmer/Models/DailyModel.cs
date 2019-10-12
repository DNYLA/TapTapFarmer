using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapTapFarmer.Models
{
    class DailyModel
    {
        public bool Alchemy { get; set; } = false;
        public bool SendHearts { get; set; } = false;
        public bool SpinWheel { get; set; } = false;
        public bool DailyTavern { get; set; } = false;
        public bool CombineEquip { get; set; } = false;
        public bool CommonSummon { get; set; } = false;
        public bool GrandSummon { get; set; } = false;
        public bool DailyBrave { get; set; } = false;
        public bool SendFriendReq { get; set; } = false;
        public bool AcceptFreindReq { get; set; } = false;
        public bool DeclineFriendReq { get; set; } = false;
        public bool DailyBag { get; set; } = false;
        public bool PurchaseDoSTicket { get; set; } = false;
        public bool DeleteMail { get; set; } = false;

    }
}
