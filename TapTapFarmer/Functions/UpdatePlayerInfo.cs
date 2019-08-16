using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTapFarmer.Constants;
using System.Threading;
namespace TapTapFarmer.Functions
{
    class UpdatePlayerInfo
    {
        public static Boolean[] CheckMenu()
        {
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);
            // Reset To Main Menu
            Main.ResetToHome();

            // Open Menu First
            MouseHandler.MoveCursor(LocationConstants.HOME_MENU_BUTTON, true);

            // Setting Up Boolean Var
            Boolean[] NotifAvailable = new Boolean[7];

            // Take New Screenshot
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);


            // Training Center
            NotifAvailable[0] = false;

            // Friends
            NotifAvailable[1] = false;

            // Trainer
            NotifAvailable[2] = false;

            // Mail
            NotifAvailable[3] = MailCheck();

            // Claim Daily Bonus
            NotifAvailable[4] = ClaimDailyBonuses();

            // Check For Shards
            NotifAvailable[5] = CheckShards();

            // Check Contents of Crate
            NotifAvailable[6] = CheckCrate();

            // Check Achievements
            NotifAvailable[7] = CheckAchievements();

            Boolean[] B = new Boolean[3];

            B[0] = true;

            return B;
        }

        public static Boolean MailCheck()
        {
            Main.ResetToHome();

            MouseHandler.MoveCursor(LocationConstants.HOME_MENU_BUTTON, true);
            if (PixelChecker.CheckPixelValue(LocationConstants.MENU_MAIL_BUTTON, ColorConstants.MENU_MAIL_REDINFO_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.MENU_MAIL_BUTTON, true);
                Thread.Sleep(500);
                MouseHandler.MoveCursor(LocationConstants.MAIL_CLAIMALL_BUTTON, true);
                Thread.Sleep(500);
                MouseHandler.MoveCursor(LocationConstants.MAIL_CLAIM_BUTTON, true);

                while (PixelChecker.CheckPixelValue(LocationConstants.MAIL_DELETE_BUTTON, ColorConstants.MAIL_DELETE_COLOR))
                {
                    MouseHandler.MoveCursor(LocationConstants.MAIL_DELETE_BUTTON, true);
                }
            }
            else
            {
                Console.WriteLine("No Red Info");
            }

            Main.ResetToHome();
            
            return true;
        }

        public static Boolean ClaimDailyBonuses()
        {
            Main.ResetToHome();

            // Add: Check time before doing this
            Thread.Sleep(1000);
            MouseHandler.MoveCursor(LocationConstants.HOME_DAILYBONUS_BUTTON, true); 
            Thread.Sleep(500);
            MouseHandler.MoveCursor(LocationConstants.DAILYBONUS_CHECKIN_BUTTON, true);
            Thread.Sleep(500);
            //MouseHandler.MoveCursor(LocationConstants.DAILYBONUS_EXIT_BUTTON, true);
            Main.ResetToHomePage(1);

            // Claim Daily Money Bonus
            MouseHandler.MoveCursor(LocationConstants.HOME_MONEYBONUS_BUTTON, true);
            Thread.Sleep(500);
            MouseHandler.MoveCursor(LocationConstants.MONEYBONUS_FREE_BUTTON, true);
            Thread.Sleep(500);
            // MouseHandler.MoveCursor(LocationConstants.MONEYBONUS_EXIT_BUTTON, true);
            Main.ResetToHome();

            Main.ResetToHome();
            return true;
        }

        public static Boolean CheckShards()
        {
            Main.ResetToHome();
            if (PixelChecker.CheckPixelValue(LocationConstants.HOME_BAG_BUTTON, ColorConstants.HOME_BAG_REDINFO_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.HOME_BAG_BUTTON, true);
                Thread.Sleep(500);
                if (PixelChecker.CheckPixelValue(LocationConstants.BAG_SHARDS_BUTTON, ColorConstants.BAG_SHARDS_REDINFO_COLOR))
                {
                    MouseHandler.MoveCursor(LocationConstants.BAG_SHARDS_BUTTON, true);
                    Thread.Sleep(500);

                    while (PixelChecker.CheckPixelValue(LocationConstants.BAG_SHARDS_REWARDS_BUTTON, ColorConstants.BAG_SHARDS_REWARDS_REDINFO_COLOR))
                    {
                        MouseHandler.MoveCursor(LocationConstants.BAG_SHARDS_REWARDS_BUTTON, true);
                        Thread.Sleep(500); 
                        MouseHandler.MoveCursor(LocationConstants.BAG_SHARDS_REWARDS_EXCHANGE_BUTTON, true);
                        Thread.Sleep(1000);
                        MouseHandler.MoveCursor(LocationConstants.BAG_SHARDS_REWARDS_CLAIM_BUTTON, true);
                        Thread.Sleep(500);
                    }
                    MouseHandler.MoveCursor(LocationConstants.BAG_EXIT_BUTTON, true);
                }
                else
                {
                    MouseHandler.MoveCursor(LocationConstants.BAG_EXIT_BUTTON, true);
                }
            }
            else
            {
                // Console Log
            }
            Main.ResetToHome();
            return true;
        }

        public static Boolean CheckCrate()
        {
            Main.ResetToHome();
            MouseHandler.MoveCursor(LocationConstants.HOME_CRATE_BUTTON, true);
            Thread.Sleep(500);
            MouseHandler.MoveCursor(LocationConstants.CRATE_EXIT_BUTTON, true);
            Main.ResetToHome();
            return true;
        }

        public static Boolean CheckAchievements()
        {
            Main.ResetToHome();

            if (PixelChecker.CheckPixelValue(LocationConstants.HOME_PROFILE_BUTTON, ColorConstants.HOME_PROFILE_REDINFO_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.HOME_PROFILE_BUTTON, true);
                Thread.Sleep(500);

                while (PixelChecker.CheckPixelValue(LocationConstants.PROFILE_CLAIM_BUTTON, ColorConstants.PROFILE_CLAIM_REDINFO_COLOR))
                {
                    MouseHandler.MoveCursor(LocationConstants.PROFILE_CLAIM_BUTTON, true);
                    Thread.Sleep(500);
                }
            }
            else
            {
                // Console Log
            }

            Main.ResetToHome();
            return true;
        }
    }
}
