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
        //public static Boolean[] CheckMenu()
        //{
        //    WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);
        //    Reset To Main Menu
        //    Main.ResetToHome();

        //    Open Menu First
        //    MouseHandler.MoveCursor(LocationConstants.HOME_MENU_BUTTON, true);

        //    Setting Up Boolean Var
        //    Boolean[] NotifAvailable = new Boolean[7];

        //    Take New Screenshot
        //    WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);


        //    Training Center
        //    NotifAvailable[0] = false;

        //    Friends
        //   NotifAvailable[1] = false;

        //    Trainer
        //   NotifAvailable[2] = false;

        //    Mail
        //   NotifAvailable[3] = MailCheck();

        //    Claim Daily Bonus
        //   NotifAvailable[4] = ClaimDailyBonuses();

        //    Check For Shards
        //   NotifAvailable[5] = CheckShards();

        //    Check Contents of Crate
        //    NotifAvailable[6] = CheckCrate();

        //    Check Achievements
        //    NotifAvailable[7] = CheckAchievements();

        //    Boolean[] B = new Boolean[3];

        //    B[0] = true;

        //    return B;
        //}

        public static int[] GetCurrecyDetails()
        {
            Main.ResetToHome();
            Main.Sleep(1);
            OpenObjects.OpenAltar();
            Main.Sleep(2);
            int[] CurrencyArray = new int[5]; // 0 = Gold; 1 = Gem; 2 = Level; 3 = Purple Soul; 4 = Golden Soul;

            //Task/Thread This Later
            CurrencyArray[0] = ImageToText.GetMoneyAmount();
            CurrencyArray[1] = ImageToText.GetGemAmount();
            CurrencyArray[2] = ImageToText.GetLevel();
            CurrencyArray[3] = ImageToText.GetPurpleSoulAmount();
            CurrencyArray[4] = ImageToText.GetGoldenSoulAmount();

            if (CurrencyArray[2] == -1)
            {
                MouseHandler.MoveCursor(TextConstants.LEVEL_START, true);
                CurrencyArray[2] = ImageToText.GetLevelAdvanced();
            }

            Console.WriteLine("Gold " + CurrencyArray[0]);
            Console.WriteLine("Gem " + CurrencyArray[1]);
            Console.WriteLine("Level " + CurrencyArray[2]);
            Console.WriteLine("Purple " + CurrencyArray[3]);
            Console.WriteLine("Golden " + CurrencyArray[4]);

            return CurrencyArray;
        }

        public static Boolean EventsCompleted()
        {
            MouseHandler.MoveCursor(LocationConstants.HOME_MAINMENU_LOCATION, true);
            //MouseHandler.MoveCursor(LocationConstants., true);

            return PixelChecker.CheckPixelValue(LocationConstants.QUEST_CLAIMAIN_LOCATION, ColorConstants.QUEST_INACTIVE_COLOR);
        }

        public static Boolean MailEmpty()
        {
            WindowCapture.CaptureApplication("Nox");//

            if (!Main.ResetToHome())
            {
                return false;
            }

            Thread.Sleep(500);
            MouseHandler.MoveCursor(LocationConstants.HOME_MAINMENU_LOCATION, true);
            if (PixelChecker.CheckPixelValue(LocationConstants.MENU_MAIL_LOCATION, ColorConstants.MENU_REDINFO_MAIL_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.MENU_MAIL_LOCATION, true);
                Thread.Sleep(700);
                MouseHandler.MoveCursor(LocationConstants.MAIL_RECEIVEALL, true);

                while (PixelChecker.CheckPixelValue(LocationConstants.MAIL_RECEIVE, ColorConstants.MAIL_DELETE))
                {
                    MouseHandler.MoveCursor(LocationConstants.MAIL_RECEIVE, true);
                }
            }
            else
            {
                MouseHandler.MoveCursor(LocationConstants.GLOBAL_BOT_IDLE_CLICK, true);
            }
            Main.ResetToHome();

            return true;
        }

    }
}
