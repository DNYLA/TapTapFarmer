using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TapTapFarmer.Constants
{
    class LocationConstants
    {
        //Global Locations || Locations that are used throughout the game
        public static Point GLOBAL_BOT_IDLE_CLICK = new Point(275,717);
        public static Point GLOBAL_BATTLE_SKIP = new Point(514, 830);
        public static Point GLOBAL_BATTLE_SKIP_CONFIRM = new Point(156, 579);
        public static Point GLOBAL_BATTLE_FINISHED = new Point(272, 834);
        public static Point GLOBAL_BATTLE_CHECK_WIN = new Point(269, 380);
        public static Point GLOBAL_LEVEL_BAR = new Point(130, 20);

        public static Point GLOBAL_ENEMYINFO_BATTLE_CONFIRM = new Point(265, 700);
        public static Point GLOBAL_TEAM_BATTLE_CONFIRM = new Point(269, 869);


        // Screen
        public static Point SCREEN_CITY_TOP = new Point(267, 56);
        public static Point SCREEN_CITY_BOTTOM = new Point(267, 870); 

        // Home Buttons
        public static Point HOME_MENU_BUTTON = new Point(61, 83);
        public static Point HOME_DAILYBONUS_BUTTON = new Point(493, 155);
        public static Point HOME_MONEYBONUS_BUTTON = new Point(515,24); 
        public static Point HOME_BAG_BUTTON = new Point(515, 895);
        public static Point HOME_CRATE_BUTTON = new Point(271, 787);
        public static Point HOME_PROFILE_BUTTON = new Point(42, 10);
        public static Point HOME_ACCOUNT_ALREADY_LOGGED = new Point(267, 590);
        public static Point HOME_BOSS_BATTLE_NEXT = new Point(290, 196);
        public static Point HOME_BOSS_IDLE_NEXT = new Point(288, 634);
        // Profile Buttons
        public static Point PROFILE_CLAIM_BUTTON = new Point(488, 590);

        // Crate Buttons
        public static Point CRATE_EXIT_BUTTON = new Point(505, 73);

        // Bag Buttons
        public static Point BAG_SHARDS_BUTTON = new Point(382, 57);
        public static Point BAG_SHARDS_REWARDS_BUTTON = new Point(114, 166);
        public static Point BAG_SHARDS_REWARDS_EXCHANGE_BUTTON = new Point(374, 689);
        public static Point BAG_SHARDS_REWARDS_CLAIM_BUTTON = new Point(272, 700);
        public static Point BAG_EXIT_BUTTON = new Point(503, 123);

        // Daily Bonus Buttons
        public static Point DAILYBONUS_CHECKIN_BUTTON = new Point(348, 860);
        public static Point DAILYBONUS_EXIT_BUTTON = new Point(502, 70);

        // 8hr Money Bonus Buttons (Meowth Coin)
        public static Point MONEYBONUS_FREE_BUTTON = new Point(419, 326);
        public static Point MONEYBONUS_EXIT_BUTTON = new Point(501, 168);

        //Home Bottom Buttons
        public static Point HOME_BOTTOM_BATTLE = new Point(273, 954);
        public static Point HOME_BOTTOM_BATTLE_ACTIVE = new Point(317, 235);
        public static Point HOME_BOTTOM_CITY = new Point(399, 944);

        //Menu Strip Buttons
        public static Point MENU_MAIL_BUTTON = new Point(54, 808);

        //City Buttons
        public static Point CITY_CURSOR_SCROLL = new Point(284, 119);
        public static Point CITY_ITEMCENTER_BUTTON = new Point(84, 552);
        public static Point CITY_SHOP_BUTTON = new Point(454, 532);
        public static Point CITY_PMGARDEN_BUTTON = new Point(231, 354);
        public static Point CITY_LINKTRADE_BUTTON = new Point(372, 149);
        public static Point CITY_GAMECORNER_BUTTON = new Point(66, 801);

        public static Point CITY_DISPATCH_BUTTON = new Point(380, 702);
        public static Point CITY_SAFARIZONE_BUTTON = new Point(195, 572);
        public static Point CITY_BATTLELEAGUE_BUTTON = new Point(518, 566);
        public static Point CITY_SKYPILAR_BUTTON = new Point(108, 465);
        public static Point CITY_BATTLESUBWAY_BUTTON = new Point(385, 360);
        public static Point CITY_GYM_BUTTON = new Point(254, 308);

        // Mail Buttons
        public static Point MAIL_CLAIMALL_BUTTON = new Point(94, 702);
        public static Point MAIL_DELETE_BUTTON = new Point(353, 707);
        public static Point MAIL_CLAIM_BUTTON = new Point(275, 701);

        //Sky Pillar
        public static Point SKYPILLAR_BATTLE_LOCATION = new Point(424, 676);
        //Gym
        public static Point GYM_BATTLE_LOCATION = new Point(448, 578);

        //Test
        public static Point GOLD_RED_CLAIM = new Point(525, 9);
    }
}
