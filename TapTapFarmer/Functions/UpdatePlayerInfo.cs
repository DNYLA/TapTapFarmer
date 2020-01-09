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

        public static bool ClaimTavernQuest()
        {
            Main.ResetToHome();
            OpenObjects.OpenTavern();
            Thread.Sleep(100);
            MouseHandler.MoveCursor(LocationConstants.CASTLE_SCROLL_LOCATION);
            for (int i = 0; i < 5; i++)
            {
                
                MouseHandler.MouseWheelDown();
            }
            
            int howmuch = 0;

            if (FinishQuest())
            {
                howmuch++;
                FinishQuest();
                if (FinishQuest())
                {
                    howmuch++;
                }
            }


            if (howmuch == 2)
            {
                return true;
            }

            if (CheckSpeedUp())
            {
                //Add Speed Up handling
                howmuch++;
            }

            if (CheckDispatch())
            {
                howmuch++;

                if(CheckDispatch())
                {
                    howmuch++;
                }
            }

            if (howmuch > 2)
            {
                Main.LogConsole("Completed Daily Tavern Quests");
                return true;
            }

            return false;
        }

        private static bool FinishQuest()
        {
            for (int i = 0; i < 15; i++)
            {
                if (PixelChecker.CheckPixelValue(LocationConstants.TAVERN_COMPLETE, ColorConstants.TAVER_FINISH))
                {
                    MouseHandler.MoveCursor(LocationConstants.TAVERN_COMPLETE, true);
                    return true;
                }

                Thread.Sleep(430);
            }

            return false;
        }

        private static bool CheckSpeedUp()
        {
            if (PixelChecker.CheckPixelValue(LocationConstants.TAVERN_COMPLETE, ColorConstants.TAVERN_SPEEDUP))
            {
                MouseHandler.MoveCursor(LocationConstants.TAVERN_COMPLETE, true);
                Thread.Sleep(1000);
                if (PixelChecker.CheckPixelValue(LocationConstants.TAVERN_SPEED, ColorConstants.TAVERN_SPEED))
                {
                    MouseHandler.MoveCursor(LocationConstants.TAVERN_SPEED, true);
                    MouseHandler.MoveCursor(LocationConstants.TAVERN_COMPLETE, true);
                    return true;
                }
                else
                {
                    MouseHandler.MoveCursor(LocationConstants.TAVERN_COMPLETE, true);
                    return true;
                }
            }
            return false;
        }
        
        private static bool CheckDispatch()
        {
            if (PixelChecker.CheckPixelValue(LocationConstants.TAVERN_COMPLETE, ColorConstants.TAVERN_DISPATCH))
            {
                MouseHandler.MoveCursor(LocationConstants.TAVERN_COMPLETE, true);
                Main.Sleep(1);
                MouseHandler.MoveCursor(LocationConstants.TAVERN_QUICK, true);
                Main.Sleep(2);
                if (PixelChecker.CheckPixelValue(LocationConstants.TAVERN_START, ColorConstants.TAVERN_START))
                {
                    MouseHandler.MoveCursor(LocationConstants.TAVERN_START, true);
                    Main.Sleep(1);
                    MouseHandler.MoveCursor(LocationConstants.TAVERN_COMPLETE, true);
                    Main.Sleep(1);
                    MouseHandler.MoveCursor(LocationConstants.TAVERN_COMPLETE, true);
                    return true;
                }
                else
                {
                    Main.LogConsole("Not Enough Heroes to complete event!...");
                    return false;
                }
            }
            return false;
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

            Main.ResetToHome();

            //if (!Main.ResetToHome())
            //{
            //    Console.WriteLine("Not Home");
            //    return false;
            //}

            Main.ResetToHome();

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
                MouseHandler.MoveCursor(LocationConstants.FRIENDS_REQUESTS, true);
                Thread.Sleep(2000);

                if (GlobalVariables.dailySettings.AcceptFreindReq)
                {
                    while(PixelChecker.CheckPixelValue(LocationConstants.FRIENDS_ACCEPT, ColorConstants.FRIENDS_ACCEPT))
                    {
                        MouseHandler.MoveCursor(LocationConstants.FRIENDS_ACCEPT, true);
                        Thread.Sleep(500);
                    }
                }

                if (GlobalVariables.dailySettings.DeclineFriendReq)
                {
                    MouseHandler.MoveCursor(LocationConstants.FRIENDS_DELETE, true);
                    Thread.Sleep(500);
                }
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
            Main.LogConsole("Trying to Claim Daily Events");
            Main.ResetToHome();

            Thread.Sleep(500);
            MouseHandler.MoveCursor(LocationConstants.HOME_MAINMENU_LOCATION, true);
            Main.Sleep(1);

            if (!PixelChecker.CheckPixelValue(LocationConstants.MENU_EVENTS, ColorConstants.MENU_EVENTS_GREEN) && !PixelChecker.CheckPixelValue(LocationConstants.MENU_EVENTS, ColorConstants.MENU_EVENTS_RED))
            {
                Main.LogConsole("Events Already Claimed!");
                return true;
            }

            MouseHandler.MoveCursor(LocationConstants.MENU_EVENTS, true);
            bool[] wonAttack = { false, false, false };
            bool[] attacked = { false, false, false };
            int totalClaimed = 0;
            //remove 96 Height

            #region Attack Event
            //Check if an attack is available
            if (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_ATTACK_1, ColorConstants.EVENTS_ATTACK_1))
            {
                attacked[0] = true;
                MouseHandler.MoveCursor(LocationConstants.EVENTS_ATTACK_1, true);
                OpenChallenge();
                if (!Attack.EventsAttack())
                {
                    Main.LogConsole("Failed Win Event 1 Challenge!");
                }
                else
                {
                    Main.LogConsole("Successfully Defeated Event 1 Challenge!");
                    totalClaimed++;
                    wonAttack[0] = true;
                }
            }

            if (attacked[0])
            {
                OpenObjects.OpenEvents();
            }

            if (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_ATTACK_2, ColorConstants.EVENTS_ATTACK_2))
            {
                attacked[1] = true;
                MouseHandler.MoveCursor(LocationConstants.EVENTS_ATTACK_2, true);
                OpenChallenge();
                if (!Attack.EventsAttack())
                {
                    Main.LogConsole("Failed Win Event 2 Challenge!");
                }
                else
                {
                    Main.LogConsole("Successfully Defeated Event 1 Challenge!");
                    totalClaimed++;
                    wonAttack[1] = true;
                }

            }

            if (attacked[1])
            {
                OpenObjects.OpenEvents();
            }

            if (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_ATTACK_3, ColorConstants.EVENTS_ATTACK_3))
            {
                attacked[2] = true;
                MouseHandler.MoveCursor(LocationConstants.EVENTS_ATTACK_3, true);
                OpenChallenge();
                if (!Attack.EventsAttack())
                {
                    Main.LogConsole("Failed Win Event 3 Challenge!");
                }
                else
                {
                    Main.LogConsole("Successfully Defeated Event 1 Challenge!");
                    totalClaimed++;
                    wonAttack[2] = true;
                }
            }
            #endregion

            if (attacked[2])
            {
                OpenObjects.OpenEvents();
            }
            
            #region Claim Events
            while (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_CLAIM_1, ColorConstants.EVENTS_CLAIM_1))
            {
                totalClaimed++;
                MouseHandler.MoveCursor(LocationConstants.EVENTS_CLAIM_1, true);
            }

            while (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_CLAIM_2, ColorConstants.EVENTS_CLAIM_2))
            {
                totalClaimed++;
                MouseHandler.MoveCursor(LocationConstants.EVENTS_CLAIM_2, true);
            }

            while (PixelChecker.CheckPixelValue(LocationConstants.EVENTS_CLAIM_3, ColorConstants.EVENTS_CLAIM_3))
            {
                totalClaimed++;
                MouseHandler.MoveCursor(LocationConstants.EVENTS_CLAIM_3, true);
            }

            #endregion

            Main.LogConsole($"Claimed {totalClaimed.ToString()} Daily Events");

            Main.ResetToHome();

            return true;
        }

        private static void OpenChallenge()
        {
            if (!PixelChecker.SearchPixel(ColorConstants.EVENTS_UNCLAIMABLE, out Point unClaimLoc))
            {
                for (int i = 0; i < 3; i++)
                {
                    MouseHandler.MoveCursor(LocationConstants.HOME_BOTTOM_CASTLE_LOCATION, true);
                }
                OpenChallenge();
            }
            unClaimLoc = new Point(unClaimLoc.X, unClaimLoc.Y - 96);
            MouseHandler.MoveCursor(unClaimLoc, true);
        }

        public static Boolean ClaimPrivellage()
        {
            Main.ResetToHome();

            Thread.Sleep(4000);

            MouseHandler.MoveCursor(LocationConstants.HOME_PIRVALLEGE_BUTTON, true);

            while (PixelChecker.CheckPixelValue(LocationConstants.PRIVALLEGE_CHECKIN_BUTTON, ColorConstants.PRIVALLEGE_CHECKIN_YELLOW))
            {
                MouseHandler.MoveCursor(LocationConstants.PRIVALLEGE_CHECKIN_BUTTON, true);
                Thread.Sleep(500);
            }

            return true;
        }

        public static Boolean CombineEquipment()
        {
            /*   0   1   2   3   4  
            * 0  D | D | D | D | D
            * 1  D | D | D | D | D
            * 2  D | X | X | X | N
            */

            /* START AT (115, 535)
             * WHEN MOVING TO THE RIGHT DO (115 + (POS * 95), 535)
             * WHEN MOVING DOWN DO (115, 535 + (POS * 85))
             * THIS SHOULD LEAVE U TO THE LAST Y =  705
             * SO IN TOTAL THE CALCULATION IS
             * (115 + (POS * 95), 535 + (POS * 85))
             */

            ColorConstants.SetColours();

            Main.ResetToHome();
            OpenObjects.OpenBlackSmith();

            

            Point startPos = LocationConstants.BLACKSMITH_DEFAULT;
            Point setPos = startPos;

            int combined = 0;

            Thread.Sleep(1000);
            
            for (int type = 0; type < 3; type++)
            {
                if (type == 1)
                {
                    MouseHandler.MoveCursor(LocationConstants.BLACKSMITH_ARMOR, true);
                }
                else if (type == 2)
                {
                    MouseHandler.MoveCursor(LocationConstants.BLACKSMITH_ACCESSORY, true);
                }
                else if (type == 3)
                {
                    MouseHandler.MoveCursor(LocationConstants.BLACKSMITH_HELMET, true);
                }


                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (combined >= 3)
                        {
                            return true;
                        }
                        setPos = new Point(115 + (x * 95), 535 + (y * 85));
                        try
                        {
                            if (PixelChecker.CheckPixelValue(setPos, ColorConstants.Equipments[y, x]))
                            {
                                if (setPos.X == 495)
                                {
                                    setPos = new Point(setPos.X - 5, setPos.Y);
                                }

                                Console.WriteLine($"Found Equipment that needs to be combined. Location - {x}:{y}");

                                MouseHandler.MoveCursor(setPos, true);



                                int CombineAmount = ImageToText.GetCombineAmount();

                                if (CombineAmount == -1)
                                {
                                    Console.WriteLine($"Unable To Read Combine Amount. Combining it Once");

                                    LowerCombineAmont(100);
                                    int price = ImageToText.GetBlacksmithPurchaseAmount();

                                    MouseHandler.MoveCursor(LocationConstants.BLACKSMITH_PURCHASE, true);

                                    Thread.Sleep(3000);

                                    if (PixelChecker.CheckPixelValue(LocationConstants.BLACKSMITH_CLAIM, ColorConstants.BLACKSMITH_CLAIM_COLOR))
                                    {
                                        MouseHandler.MoveCursor(LocationConstants.BLACKSMITH_CLAIM, true);
                                        combined++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Insufficient Gold!");
                                    }


                                }
                                else
                                {
                                    int loweramount = CombineAmount - 3;
                                    LowerCombineAmont(loweramount);
                                    int price = ImageToText.GetBlacksmithPurchaseAmount();

                                    MouseHandler.MoveCursor(LocationConstants.BLACKSMITH_PURCHASE, true);

                                    Thread.Sleep(3000);

                                    if (PixelChecker.CheckPixelValue(LocationConstants.BLACKSMITH_CLAIM, ColorConstants.BLACKSMITH_CLAIM_COLOR))
                                    {
                                        MouseHandler.MoveCursor(LocationConstants.BLACKSMITH_CLAIM, true);
                                        combined += 3;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Insufficient Gold!");
                                    }


                                }

                            }
                        }
                        catch
                        {
                            x++;
                        }
                    }
                }
               
            }
               
            
            Console.WriteLine("Finished");

            return true;
        }



        public static bool ClaimQuestReward()
        {
            Main.ResetToHome();
            Main.ResetToHome();
            MouseHandler.MoveCursor(LocationConstants.HOME_MAINMENU_LOCATION, true);
            Main.Sleep(1);
            MouseHandler.MoveCursor(LocationConstants.MENU_QUESTS_BUTTON_LOCATION, true);

            while (!PixelChecker.CheckPixelValue(LocationConstants.QUEST_CLAIM, ColorConstants.QUEST_UNCLAIMABLE_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.QUEST_CLAIMAIN_LOCATION, true);
            }

            bool isClaimable = false;

            //Checks is it is Unclaimable if since the claimable button is animated.
            if (!PixelChecker.CheckPixelValue(LocationConstants.QUEST_CLAIM_MASTER, ColorConstants.EVENTS_UNCLAIMABLE))
            {
                isClaimable = true;
            }
            
            if (isClaimable)
            {
                MouseHandler.MoveCursor(LocationConstants.QUEST_CLAIM_MASTER, true);
                return true;
            }

            return false;
        }

        private static void LowerCombineAmont(int LowerAmount)
        {
            for (int i = 0; i < LowerAmount; i++)
            {
                MouseHandler.MoveCursor(LocationConstants.BLACKSMITH_LOWER, true);
                //Thread.Sleep(30);
            }
        }
        public static Boolean ClaimAlchemy()
        {
            bool ClaimFree = false;
            bool Claim20 = false;
            bool Claim50 = false;
            MouseHandler.MoveCursor(LocationConstants.HOME_ALCHEMY_BUTTON, true);

            if (PixelChecker.CheckPixelValue(LocationConstants.ALCHEMY_FREE_BUTTON, ColorConstants.ALCHEMY_FREE))
            {
                ClaimFree = true;
            }

            if (PixelChecker.CheckPixelValue(LocationConstants.ALCHEMY_20GEMS_BUTTON, ColorConstants.ALCHEMY_20GEMS))
            {
                Claim20 = true;
                if (GlobalVariables.CURRENCY_INFO[2] < 30 && GlobalVariables.CURRENCY_INFO[2] != -1)
                {
                    Claim20 = false;
                    Main.LogConsole("Not Claiming 20 Gem Alchemy As Gem Amount is below 30");
                }
            }

            if (PixelChecker.CheckPixelValue(LocationConstants.ALCHEMY_50GEMS_BUTTON, ColorConstants.ALCHEMY_50GEMS))
            {
                Claim50 = true;
                if (ClaimFree && Claim20)
                {
                    Claim50 = false;
                    Main.LogConsole("Not Claiming 50 Gem Alchemy As Free & 20 Gem is Available");
                }
                else if (GlobalVariables.CURRENCY_INFO[2] < 400 && GlobalVariables.CURRENCY_INFO[2] != -1)
                {

                    Claim50 = false;
                    Main.LogConsole("Not Claiming 50 Gem Alchemy As Gem Amount is below 500");
                }
            }

            if (ClaimFree && Claim20)
            {
                MouseHandler.MoveCursor(LocationConstants.ALCHEMY_FREE_BUTTON, true);
                Thread.Sleep(400);

                MouseHandler.MoveCursor(LocationConstants.ALCHEMY_20GEMS_BUTTON, true);
                Thread.Sleep(400);
                Main.LogConsole("Claimed Daily Free & 20 Gem Alchemy");
            }
            else if (ClaimFree && Claim50)
            {
                MouseHandler.MoveCursor(LocationConstants.ALCHEMY_FREE_BUTTON, true);
                Thread.Sleep(400);

                MouseHandler.MoveCursor(LocationConstants.ALCHEMY_50GEMS_BUTTON, true);
                Thread.Sleep(400);
                Main.LogConsole("Claimed Daily Free & 50 Gem Alchemy");
            }
            else if (ClaimFree && Claim50)
            {
                MouseHandler.MoveCursor(LocationConstants.ALCHEMY_20GEMS_BUTTON, true);
                Thread.Sleep(400);

                MouseHandler.MoveCursor(LocationConstants.ALCHEMY_50GEMS_BUTTON, true);
                Thread.Sleep(400);
                Main.LogConsole("Claimed Daily 20 & 50 Gem Alchemy");
            }
            else
            {
                return false;
            }

            return true;
        }

        public static Boolean Defeat3Main()
        {
            Main.ResetToHome();
            bool IdleClick = true;


            var task = new Thread(() =>
            {
                while (IdleClick)
                {
                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_BOT_IDLE_CLICK, true);
                    Thread.Sleep(100);
                }
            });

            task.Start();

            Main.Sleep(25); //Sleep For 15 Minutes Before Continuing
            IdleClick = false;
            return true;
        }

        public static Boolean SpinWheel()
        {
            OpenObjects.OpenFortuneWheel();
            Thread.Sleep(500);
            int Tokens = ImageToText.GetWheelCoinAmount();

            if (Tokens < 2)
            {
                //Add Purchasing Token Below
                if (GlobalVariables.CURRENCY_INFO[1] < 300 && GlobalVariables.CURRENCY_INFO[1] != -1)
                {
                    Main.LogConsole("Not Enough Tokens & Gem Amount Below 300. Not Purchasing Any Tokens");
                    return false;
                }
                if (Tokens == 1)
                {
                    
                    //Add Token Purchasing
                    if (GlobalVariables.CURRENCY_INFO[1] < 100)
                    {
                        Main.LogConsole("Gems Below 100. Bot Purchasing Tokens");
                    }
                    else
                    {
                        Main.LogConsole("Gems Above 300. Purchasing 1 Token");
                        PurchaseTokens();
                    }
                    //MouseHandler.MoveCursor()
                    Thread.Sleep(1000);

                }
                else if (Tokens == 0 || Tokens == -1)
                {
                    Main.LogConsole("Gems Above 300. Purchasing 2 Tokens");
                    //Add Token Purchasing
                    PurchaseTokens();
                    Thread.Sleep(1000);
                    PurchaseTokens();
                    Thread.Sleep(1000);
                }
            }

            Main.LogConsole("Got Enough Tokens Spinning Wheel");
            MouseHandler.MoveCursor(LocationConstants.FORTUNE_SPIN1_BUTTON, true);
            Thread.Sleep(7000);
            MouseHandler.MoveCursor(LocationConstants.FORTUNE_SPINAGAIN1_BUTTON, true);
            Thread.Sleep(7000);
            Main.LogConsole("Spun Daily Wheel");
            


            Main.ResetToHome();

            return true;
        }

        private static void PurchaseTokens()
        {
            MouseHandler.MoveCursor(LocationConstants.FORTUNE_PURCHASE, true);
            MouseHandler.MoveCursor(LocationConstants.FORTUNE_BUY, true);
            //if (PixelChecker.CheckPixelValue(LocationConstants.FORTUNE_BUY, ColorConstants.FORTUNE_BUY))
            //{
            //    MouseHandler.MoveCursor(LocationConstants.FORTUNE_BUY, true);
            //}
            //else
            //{
            //    Main.ResetToHome();
            //    OpenObjects.OpenFortuneWheel();
            //    PurchaseTokens();
            //}

        }

        public static Boolean SummonGrandKey()
        {
            Main.ResetToHome();
            OpenObjects.OpenHeroChest();

            bool claimedGrand = false;

            //Goes to Bottom Of Hero Chest Screen
            for (int i = 0; i < 10; i++)
            {
                MouseHandler.MoveCursor(LocationConstants.HOME_BOTTOM_CASTLE_LOCATION, true);
            }

            Main.LogConsole("Checking For Grand Key");

            if (PixelChecker.CheckPixelValue(LocationConstants.HEROCHEST_GRAND_FREE_CIRCLE, ColorConstants.HEROCHEST_GRAND_FREE))
            {
                Main.LogConsole("Claiming Daily Free Grand Summon Key");
                MouseHandler.MoveCursor(LocationConstants.HEROCHEST_GRAND_FREE_CIRCLE, true);
                return true;
            }

            if (!claimedGrand)
            {
                int GrandKeyNo = ImageToText.GetGrandKeyAmount();
                if (GrandKeyNo > 0)
                {
                    Main.LogConsole("Using Extra Grand Summon Key In Backpack. Since No More Free Keys");
                    MouseHandler.MoveCursor(LocationConstants.HEROCHEST_GRAND_CLAIM, true);
                    return true;
                }
                
            }

            Main.LogConsole("No Keys Available to Summon Grand Chest");

            return false;
        }

        public static Boolean SummonCommonKey()
        {
            Main.ResetToHome();
            OpenObjects.OpenHeroChest();

            bool claimedCommon = false;

            //Goes to Bottom Of Hero Chest Screen
            for (int i = 0; i < 10; i++)
            {
                MouseHandler.MoveCursor(LocationConstants.CASTLE_SCROLL_LOCATION, true);
            }

            Main.LogConsole("Checking Daily Common Key");

            if (PixelChecker.CheckPixelValue(LocationConstants.HEROCHEST_COMMON_FREE_CIRCLE, ColorConstants.HEROCHEST_COMMON_FREE))
            {
                Main.LogConsole("Claiming Daily Free Common Summon Key");
                MouseHandler.MoveCursor(LocationConstants.HEROCHEST_COMMON_FREE_CIRCLE, true);
                return true;
            }

            if (!claimedCommon)
            {
                int CommonKeyNo = ImageToText.GetCommonKeyAmount();
                if (CommonKeyNo > 0)
                {
                    Main.LogConsole("Using Extra Common Summon Key In Backpack. Since No More Free Keys");
                    MouseHandler.MoveCursor(LocationConstants.HEROCHEST_COMMON_CLAIM, true);
                    return true;
                }
            }

            Main.LogConsole("No Keys Available to Summon Common Chest");

            return false; ;
        }

        public static Boolean ClaimMail()
        {
            WindowCapture.CaptureApplication("Nox");

            Main.ResetToHome();

            //if (!Main.ResetToHome())
            //{
            //    Console.WriteLine("Returning False");
            //    return false;
            //}

            Thread.Sleep(500);
            MouseHandler.MoveCursor(LocationConstants.HOME_MAINMENU_LOCATION, true);
            Main.Sleep(3);
            if (PixelChecker.CheckPixelValue(LocationConstants.MENU_MAILS, ColorConstants.MENU_MAIL_RED))
            {
                Console.WriteLine("Its Red");
                MouseHandler.MoveCursor(LocationConstants.MENU_MAILS, true);
                Main.Sleep(1);
                MouseHandler.MoveCursor(LocationConstants.MAIL_RECEIVEALL, true);

                if (GlobalVariables.dailySettings.DeleteMail == false)
                {
                    Main.ResetToHome();
                    return true;
                }

                while (PixelChecker.CheckPixelValue(LocationConstants.MAIL_RECEIVE, ColorConstants.MAIL_DELETE))
                {
                    MouseHandler.MoveCursor(LocationConstants.MAIL_RECEIVE, true);
                }
            }
            else
            {
                Console.WriteLine("Its Not Red");
                MouseHandler.MoveCursor(LocationConstants.GLOBAL_BOT_IDLE_CLICK, true);
            }
            Main.ResetToHome();

            return true;
        }

        public static Boolean MineGuildGold()
        {
            OpenObjects.OpenGuild();

            if (PixelChecker.CheckPixelValue(LocationConstants.GUILD_DAILY_COINS, ColorConstants.GUILD_DAILY_COINS))
            {
                MouseHandler.MoveCursor(LocationConstants.GUILD_DAILY_COINS, true);
            }

            if (PixelChecker.CheckPixelValue(LocationConstants.GUILD_MINE_BONUS, ColorConstants.GUILD_MINE_BONUS))
            {
                Main.LogConsole("Claiming Previous Bonus");
                MouseHandler.MoveCursor(LocationConstants.GUILD_CLAIM_BONUS, true);
                Main.Sleep(2);
                MouseHandler.MoveCursor(LocationConstants.GUILD_CLAIM, true);
                MineGuildGold();
            }

            if (PixelChecker.CheckPixelValue(LocationConstants.GUILD_START_DIGGING, ColorConstants.GUILD_START_DIGGING))
            {
                Main.LogConsole("Digging next bonus...");
                Main.Sleep(2);
                MouseHandler.MoveCursor(LocationConstants.GUILD_START_DIGGING, true);
            }

            return true;
        }

        public static Boolean SetGuildTeam()
        {
            OpenObjects.OpenGuild();

            if (PixelChecker.CheckPixelValue(LocationConstants.GUILD_WAR_TEAM, ColorConstants.GUILD_SET_WAR))
            {
                Main.Sleep(2);
                MouseHandler.MoveCursor(LocationConstants.GUILD_WAR_TEAM, true);
                Main.Sleep(2);
                MouseHandler.MoveCursor(LocationConstants.GUILD_SET_TEAM, true);
                Main.Sleep(2);
            }
            return true;
        }

    }
}
