using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTapFarmer.Models;

namespace TapTapFarmer
{
    class GlobalVariables
    {
        //Add Globals here
        public static bool BOT_STARTED = false;
        public static bool ISON = false;
        public static String BOT_STATE = "Stopped";
        public static String GLOBAL_PROC_NAME = "Nox"; //Nox is Default Application

        public static int[] CURRENCY_INFO = new int[5]; // 0 = Gold; 1 = Gem; 2 = Level; 3 = Purple Soul; 4 = Golden Soul;

        public static Boolean QUESTS_FINISHED = false;
        public static Boolean EVENTS_COMPLETED = false;
        public static Boolean HEARTS_SENT = false;
        public static Boolean UPGRADED_FAMILIAR = false;
        public static Boolean MAIL_EMPTY = false;

        public static DateTime LAST_RAN = new DateTime();

        public static AttackModel attackSettings = new AttackModel();
        public static DailyModel dailySettings = new DailyModel();
        public static TasksModel tasksSettings = new TasksModel();
        public static PlayerLimitsModel PlayerLimits = new PlayerLimitsModel();

    }
}
