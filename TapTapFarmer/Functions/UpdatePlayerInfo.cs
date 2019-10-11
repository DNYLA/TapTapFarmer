using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTapFarmer.Constants;
using System.Threading;
using System.Drawing;

namespace TapTapFarmer.Functions
{
    class UpdatePlayerInfo
    {
        public static void CheckMenu()
        {
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);
            //Reset To Main Menu
            Main.ResetToHome();

            //Open Menu First
            MouseHandler.MoveCursor(LocationConstants.HOME_MENU_BUTTON, true);

            //Setting Up Boolean Var
            Boolean[] NotifAvailable = new Boolean[7];

            //Take New Screenshot
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);
            
            //Check If Anything on the menu needs to be completed (Keep In Mind Quests Needs a deeper Check)
            GlobalVariables.QUESTS_FINISHED = !PixelChecker.CheckPixelValue(LocationConstants.MENU_QUEST, ColorConstants.MENU_QUEST_RED); //Requires more checks

            //Events Require Two Checks
            if (PixelChecker.CheckPixelValue(LocationConstants.MENU_EVENTS, ColorConstants.MENU_EVENTS_GREEN) || PixelChecker.CheckPixelValue(LocationConstants.MENU_EVENTS, ColorConstants.MENU_EVENTS_RED))
            {
                GlobalVariables.EVENTS_COMPLETED = false;
            }


            GlobalVariables.HEARTS_SENT = !PixelChecker.CheckPixelValue(LocationConstants.MENU_FRIENDS, ColorConstants.MENU_FRIENDS_RED);
            GlobalVariables.UPGRADED_FAMILIAR = !PixelChecker.CheckPixelValue(LocationConstants.MENU_FAMILIAR, ColorConstants.MENU_FAMILIAR_GREEN);
            GlobalVariables.MAIL_EMPTY = !PixelChecker.CheckPixelValue(LocationConstants.MENU_MAILS, ColorConstants.MENU_MAIL_RED);

            Console.WriteLine(GlobalVariables.QUESTS_FINISHED);
            Console.WriteLine(GlobalVariables.EVENTS_COMPLETED);
            Console.WriteLine(GlobalVariables.HEARTS_SENT);
            Console.WriteLine(GlobalVariables.UPGRADED_FAMILIAR);
            Console.WriteLine(GlobalVariables.MAIL_EMPTY);
        }

        public static void UpdateMenu()
        {
            //Easiest Think on List is To Claim Mail so ill do it first
            if (GlobalVariables.MAIL_EMPTY == false)
            {
                ClaimMail();
            }
        }

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

        public static Boolean ClaimFriends()
        {
            WindowCapture.CaptureApplication("Nox");

            if (!Main.ResetToHome())
            {
                Console.WriteLine("Not Home");
                return false;
            }

            Thread.Sleep(500);
            MouseHandler.MoveCursor(LocationConstants.HOME_MAINMENU_LOCATION, true);
            Main.Sleep(1);
            
            if (!PixelChecker.CheckPixelValue(LocationConstants.MENU_FRIENDS, ColorConstants.MENU_FRIENDS_RED))
            {
                Console.WriteLine("Its Not Red");
                return true;
            }

            Main.Sleep(1);

            MouseHandler.MoveCursor(LocationConstants.MENU_FRIENDS, true);
            Main.Sleep(1);

            if (PixelChecker.CheckPixelValue(LocationConstants.FRIENDS_LIST, ColorConstants.FRIENDS_LIST_RED))
            {
                MouseHandler.MoveCursor(LocationConstants.FRIENDS_LIST, true);
                Main.Sleep(1);
                MouseHandler.MoveCursor(LocationConstants.FRIENDS_CLAIM_SEND, true);
                Main.Sleep(1);
            }

            if (PixelChecker.CheckPixelValue(LocationConstants.FRIENDS_REQUESTS, ColorConstants.FRIENDS_REQUESTS_GREEN))
            {
                //Do Nothing for now

            }

            if (PixelChecker.CheckPixelValue(LocationConstants.FRIENDS_COOP, ColorConstants.FRIENDS_COOP_RED))
            {
                MouseHandler.MoveCursor(LocationConstants.FRIENDS_COOP, true);
                Main.Sleep(1);
                MouseHandler.MoveCursor(LocationConstants.FRIENDS_SCOUT, true);
                Main.Sleep(1);
            }

            Main.ResetToHome();

            return true;
        }

        public static Boolean ClaimEvents()
        {
            WindowCapture.CaptureApplication("Nox");

            if (!Main.ResetToHome())
            {
                Console.WriteLine("Not Home");
                return false;
            }

            Thread.Sleep(500);
            MouseHandler.MoveCursor(LocationConstants.HOME_MAINMENU_LOCATION, true);
            Main.Sleep(1);

            if (!PixelChecker.CheckPixelValue(LocationConstants.MENU_EVENTS, ColorConstants.MENU_EVENTS_GREEN) && !PixelChecker.CheckPixelValue(LocationConstants.MENU_EVENTS, ColorConstants.MENU_EVENTS_RED))
            {
                Console.WriteLine("Its Not Green/Red");
                return true;
            }

            MouseHandler.MoveCursor(LocationConstants.MENU_EVENTS, true);

            #region Attack Event
            //Check if an attack is available
            if (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_ATTACK_1, ColorConstants.EVENTS_ATTACK_1))
            {

            }

            if (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_ATTACK_2, ColorConstants.EVENTS_ATTACK_2))
            {
                MouseHandler.MoveCursor(LocationConstants.EVENTS_ATTACK_2, true);

                int TempX = LocationConstants.EVENTS_CHALLENGE_BOTTOM.X;
                int TempY = LocationConstants.EVENTS_CHALLENGE_BOTTOM.Y;
                var TempLoc = new Point(TempX, TempY);
                int CheckAmount = 0;
                bool LocFound = false;
                //90 Difference
                while (!LocFound) //PixelChecker.CheckPixelValue(TempLoc, ColorConstants.EVENTS_CHALLENGE))
                {
                    Console.WriteLine(TempLoc.ToString());
                    if (PixelChecker.CheckPixelValue(TempLoc, ColorConstants.EVENTS_CHALLENGE))
                    {
                        LocFound = true;
                    }
                    TempY = TempY - 93;
                    TempLoc = new Point(TempX, TempY);
                    CheckAmount++;
                    if (CheckAmount == 7)
                    {
                        break;
                    }
                }

                if (PixelChecker.CheckPixelValue(TempLoc, ColorConstants.EVENTS_CHALLENGE))
                {
                    Console.WriteLine("You Are Throug");
                    MouseHandler.MoveCursor(TempLoc, true);
                    bool BattleWon = Attack.EventsAttack();
                    Console.WriteLine("Result of Battle: " + BattleWon.ToString());
                }
            }

            if (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_ATTACK_3, ColorConstants.EVENTS_ATTACK_3))
            {

            }
            #endregion

            #region Claim Events
            while (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_CLAIM_1, ColorConstants.EVENTS_CLAIM_1))
            {
                MouseHandler.MoveCursor(LocationConstants.EVENTS_CLAIM_1, true);
            }

            while (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_CLAIM_2, ColorConstants.EVENTS_CLAIM_2))
            {
                MouseHandler.MoveCursor(LocationConstants.EVENTS_CLAIM_2, true);
            }

            while (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_CLAIM_3, ColorConstants.EVENTS_CLAIM_3))
            {
                MouseHandler.MoveCursor(LocationConstants.EVENTS_CLAIM_3, true);
            }

            #endregion

            Main.ResetToHome();

            return true;
        }

        public static Boolean ClaimMail()
        {
            WindowCapture.CaptureApplication("Nox");

            if (!Main.ResetToHome())
            {
                return false;
            }

            Thread.Sleep(500);
            MouseHandler.MoveCursor(LocationConstants.HOME_MAINMENU_LOCATION, true);
            Main.Sleep(3);
            if (PixelChecker.CheckPixelValue(LocationConstants.MENU_MAILS, ColorConstants.MENU_MAIL_RED))
            {
                Console.WriteLine("Its Red");
                MouseHandler.MoveCursor(LocationConstants.MENU_MAILS, true);
                Main.Sleep(1);
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
