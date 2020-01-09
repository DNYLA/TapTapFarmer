using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTapFarmer.Constants;
using System.Drawing;
using System.Threading;

namespace TapTapFarmer.Functions
{
    class Attack
    {
        /* Might Be Wondering why i have a seperate function which "Handles" the
         * Attack when i could just add the commands to the main function
         * but the reason is because i can call it to reset / reopen
         * the Attack Object when something goes wrong.
         */

        #region DenOfSecrets Attack
        public static void DenOfSecretAttackHandler()
        {
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);

            Main.ResetToHome();

            OpenObjects.OpenDoS();

            AttackDenOfSecrets();

        }

        public static void AttackDenOfSecrets()
        {
            bool AttackingDOS = true;
            int winAmount = 0;

            while (AttackingDOS)
            {
                Main.LogConsole($"Attacking Den Of Secrets. Total Levels Won: {winAmount.ToString()}");
                for (int CurrentTry = 0; CurrentTry < OtherConstants.ATTACK_RETRY_AMOUNT; CurrentTry++)
                {
                    Main.Sleep(2);
                    MouseHandler.MoveCursor(LocationConstants.DOS_BATTLE_LOCATION, true); //Maybe Add SearchPixel If Problems Occur
                    Main.Sleep(1);
                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_ENEMYINFO_BATTLE_CONFIRM, true);
                    Main.Sleep(1);
                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_TEAM_BATTLE_CONFIRM, true);
                    Main.Sleep(3);

                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_SKIP, true);
                    Main.Sleep(3);
                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_SKIP_CONFIRM, true);
                    bool BattleFinished = false;

                    while (!BattleFinished)
                    {
                        //Sleep for 2 seconds and then Check
                        Main.Sleep(2);

                        if (PixelChecker.CheckPixelValue(LocationConstants.GLOBAL_BATTLE_FINISHED, ColorConstants.GLOBAL_BATTLE_FINISHED))
                        {
                            
                            BattleFinished = true;
                        }
                        
                    }

                    bool BattleWon = CheckWin();

                    if (BattleWon)
                    {
                        Main.Sleep(1);
                        MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                        winAmount += 1;
                        break;
                    }
                    else
                    {
                        Main.Sleep(1);
                        MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                        if (CurrentTry == 2)
                        {
                            AttackingDOS = false;
                        }
                    }

                }
            }

            Main.LogConsole($"Finished Attacking Den Of Secrets After 3 Losses On Same Level. Total Levels Won: {winAmount.ToString()}");
        }
        #endregion

        #region Events Challenge
        public static Boolean EventsAttack()
        {
            Main.Sleep(1);
            MouseHandler.MoveCursor(LocationConstants.GLOBAL_ENEMYINFO_BATTLE_CONFIRM, true);
            Main.Sleep(1);
            MouseHandler.MoveCursor(LocationConstants.GLOBAL_TEAM_BATTLE_CONFIRM, true);
            Main.Sleep(3);

            MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_SKIP, true);
            Main.Sleep(1);
            MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_SKIP_CONFIRM, true);
            bool BattleFinished = false;

            while (!BattleFinished)
            {
                //Sleep for 2 seconds and then Check
                Main.Sleep(2);

                if (PixelChecker.CheckPixelValue(LocationConstants.GLOBAL_BATTLE_FINISHED, ColorConstants.GLOBAL_BATTLE_FINISHED))
                {
                    BattleFinished = true;
                }
            }

            bool BattleWon = CheckWin();

            if (BattleWon)
            {
                Main.Sleep(1);
                MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                return true;
            }
            else
            {
                Main.Sleep(1);
                MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                return false;
            }
      
        }
        #endregion

        #region Planet Trial
        public static void PlanetTrialAttackHandler()
        {
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);

            Main.ResetToHome();

            OpenObjects.OpenPlanetTrial();

            PlanetTrialAttack();
        }

        public static void PlanetTrialAttack()
        {
            bool AttackingPillar = true;

            while (AttackingPillar)
            {
                for (int CurrentTry = 0; CurrentTry < GlobalVariables.attackSettings.PlanetTrialRetryAmount; CurrentTry++)
                {
                    Main.Sleep(5);

                    var Location = new Point(0, 0);

                    if (!PixelChecker.SearchPixel(ColorConstants.SKYPILLAR_BATTLE_COLOR, out Location))
                    {
                        string BattleTest = ImageToText.GymBattleCheck(out Location);
                        if (BattleTest != "battle")
                        {
                            PlanetTrialAttackHandler();
                        }

                        Main.Sleep(1);

                        MouseHandler.MoveCursor(Location, true);
                    }
                    else
                    {
                        MouseHandler.MoveCursor(Location, true);
                    }

                    Main.Sleep(3);
                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_ENEMYINFO_BATTLE_CONFIRM, true);
                    Main.Sleep(3);
                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_TEAM_BATTLE_CONFIRM, true);
                    Main.Sleep(3);

                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_SKIP, true);
                    Main.Sleep(1);
                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_SKIP_CONFIRM, true);
                    bool BattleFinished = false;

                    while (!BattleFinished)
                    {
                        //Sleep for 2 seconds and then Check
                        Main.Sleep(2);

                        if (PixelChecker.CheckPixelValue(LocationConstants.GLOBAL_BATTLE_FINISHED, ColorConstants.GLOBAL_BATTLE_FINISHED))
                        {
                            BattleFinished = true;
                        }
                    }

                    bool BattleWon = CheckWin();

                    if (BattleWon)
                    {
                        Main.Sleep(1);
                        MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                        break;
                    }
                    else
                    {
                        Main.Sleep(1);
                        MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                        if (CurrentTry == 2)
                        {
                            AttackingPillar = false;
                        }
                    }

                }
            }
        }
        #endregion

        #region Check Win
        /// <summary>
        /// This function is only called once battle is finished
        /// </summary>
        /// <param name="checkAmount"></param>
        /// <returns></returns>
        public static Boolean CheckWin(int checkAmount = 0)
        {
            if (checkAmount == 5)
            {
                //Add Handler // Events To Stop This
                Main.ResetToHome();
                return false; //Returns False indicating battle was not won although no battle occured.
            }

            if (PixelChecker.CheckPixelValue(LocationConstants.GLOBAL_BATTLE_CHECK_WIN, ColorConstants.GLOBAL_BATTLE_WON))
            {
                return true;
            }
            else if (PixelChecker.CheckPixelValue(LocationConstants.GLOBAL_BATTLE_CHECK_LOSS, ColorConstants.GLOBAL_BATTLE_LOST))
            {
                return false;
            }
            else
            {
                return CheckWin(checkAmount + 1);
            }
        }
        #endregion

        #region Guild Boss
        public static bool GuildBossAttackHandler()
        {
            OpenObjects.OpenGuild();

            return GuildBossAttack();

        }

        public static bool GuildBossAttack(int BossesAttacked = 0)
        {
            Main.Sleep(2);
            MouseHandler.MoveCursor(LocationConstants.GUILD_BATTLE, true);
            Main.Sleep(2);
            MouseHandler.MoveCursor(LocationConstants.GUILD_ATTACK, true);

            Main.Sleep(3);
            MouseHandler.MoveCursor(LocationConstants.GLOBAL_ENEMYINFO_BATTLE_CONFIRM, true);
            Main.Sleep(3);
            MouseHandler.MoveCursor(LocationConstants.GLOBAL_TEAM_BATTLE_CONFIRM, true);
            Main.Sleep(3);

            MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_SKIP, true);
            Main.Sleep(1);
            MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_SKIP_CONFIRM, true);

            bool BattleFinished = false;

            while (!BattleFinished)
            {
                //Sleep for 2 seconds and then Check
                Main.Sleep(2);

                if (PixelChecker.CheckPixelValue(LocationConstants.GLOBAL_BATTLE_FINISHED, ColorConstants.GLOBAL_BATTLE_FINISHED))
                {
                    BattleFinished = true;
                }
            }

            bool BattleWon = CheckWin();

            if (BattleWon)
            {
                Main.Sleep(1);
                MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
            }
            else
            {
                Main.Sleep(1);
                MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
            }

            BossesAttacked++;

            if (BossesAttacked >= GlobalVariables.attackSettings.GuildRetryAmount)
            {
                return true;
            }
            else
            {
                GuildBossAttack(BossesAttacked);
            }

            return true;
        }


        #endregion

        #region Battle League
        public static bool BraveAttackHandler()
        {
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);

            Main.ResetToHome();

            OpenObjects.OpenArena();

            return AttackBraveLeague();

        }

        public static bool AttackBraveLeague(int RefreshAmount = 0)
        {
            Thread.Sleep(1000);
            int pos = GetLowestCE();

            if (RefreshAmount > 10)
            {
                Main.LogConsole("Tried Refreshing 10 Times to find CE which matches your settings but couldn't find an opponent to match the criteria.");
                return false;
            }

            switch (pos)
            {
                case -1:
                    Main.LogConsole("All EnemyCE larger than Max BraveCE");
                    MouseHandler.MoveCursor(LocationConstants.BRAVE_REFRESH, true);
                    AttackBraveLeague(RefreshAmount++);
                    break;
                case 0:
                    MouseHandler.MoveCursor(LocationConstants.BRAVE_BATTLE1, true);
                    break;
                case 1:
                    MouseHandler.MoveCursor(LocationConstants.BRAVE_BATTLE2, true);
                    break;
                case 2:
                    MouseHandler.MoveCursor(LocationConstants.BRAVE_BATTLE3, true);
                    break;
            }


            //Attack
            Main.Sleep(1);
            MouseHandler.MoveCursor(LocationConstants.GLOBAL_ENEMYINFO_BATTLE_CONFIRM, true);
            Main.Sleep(1);
            MouseHandler.MoveCursor(LocationConstants.GLOBAL_TEAM_BATTLE_CONFIRM, true);
            Main.Sleep(3);

            MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_SKIP, true);
            Main.Sleep(1);
            MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_SKIP_CONFIRM, true);
            bool BattleFinished = false;

            while (!BattleFinished)
            {
                //Sleep for 2 seconds and then Check
                Main.Sleep(2);

                if (PixelChecker.CheckPixelValue(LocationConstants.BRAVE_CHOSEREWARD, ColorConstants.BRAVE_REWARD_COLOR))
                {
                    MouseHandler.MoveCursor(LocationConstants.BRAVE_CHOSEREWARD, true);
                    MouseHandler.MoveCursor(LocationConstants.BRAVE_RANDOM, true);
                    MouseHandler.MoveCursor(LocationConstants.BRAVE_RANDOM, true);
                    MouseHandler.MoveCursor(LocationConstants.BRAVE_RANDOM, true);
                    Thread.Sleep(500);
                    if (PixelChecker.CheckPixelValue(LocationConstants.GLOBAL_BATTLE_FINISHED, ColorConstants.GLOBAL_BATTLE_FINISHED))
                    {
                        BattleFinished = true;
                    }
                }

                
            }

            bool BattleWon = CheckWin();

            if (BattleWon)
            {
                Main.Sleep(1);
                MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                return true;
            }
            else
            {
                Main.Sleep(1);
                MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                return false;
            }
        }

        private static int GetLowestCE()
        {
            string[] EnemyCEString = new string[3];
            int[] EnemyCE = new int[3];
            Console.WriteLine("Reading Values");

            //Similar to Main Menu Boss where it doesn't matter if you win or lose it just attacks the lowest number
            EnemyCEString[0] = ImageToText.GetOcrResponse(TextConstants.BRAVE_ENEMY_CE_1, TextConstants.BRAVE_ENEMY_CE_SIZE);
            Thread.Sleep(1000);
            EnemyCEString[1] = ImageToText.GetOcrResponse(TextConstants.BRAVE_ENEMY_CE_2, TextConstants.BRAVE_ENEMY_CE_SIZE);
            Thread.Sleep(1000);
            EnemyCEString[2] = ImageToText.GetOcrResponse(TextConstants.BRAVE_ENEMY_CE_3, TextConstants.BRAVE_ENEMY_CE_SIZE);

            Console.WriteLine("Yeea");

            try
            {
                EnemyCE[0] = Convert.ToInt32(ImageToText.RemoveWhiteSpace(EnemyCEString[0], true));
            }
            catch (Exception)
            {

                EnemyCE[0] = 999999999;
            }

            try
            {
                EnemyCE[1] = Convert.ToInt32(ImageToText.RemoveWhiteSpace(EnemyCEString[1], true));
            }
            catch (Exception)
            {

                EnemyCE[1] = 999999999;
            }

            try
            {
                EnemyCE[2] = Convert.ToInt32(ImageToText.RemoveWhiteSpace(EnemyCEString[2], true));
            }
            catch (Exception)
            {

                EnemyCE[2] = 999999999;
            }

            
            

            Console.WriteLine(EnemyCE[0]);

            int position = -1;

            int[] CEbuffer = new int[3];
            Array.Copy(EnemyCE, CEbuffer, 3);
            Array.Sort(CEbuffer);


            Console.WriteLine($"{EnemyCE[0]} - {EnemyCE[1]} - {EnemyCE[2]}");
            Console.WriteLine(CEbuffer[0]);

            Console.WriteLine(GlobalVariables.attackSettings.BraveMaxCE.ToString());

            if (GlobalVariables.attackSettings.BraveMaxCE > CEbuffer[0])
            {
                if (EnemyCE[0] > EnemyCE[1])
                {
                    if (EnemyCE[0] > EnemyCE[2])
                    {
                        position = 0;
                    }
                    else
                    {
                        position = 2;
                    }
                }
                else if (EnemyCE[1] > EnemyCE[2])
                {
                    if (EnemyCE[1] > EnemyCE[0])
                    {
                        position = 1;
                    }
                    else
                    {
                        position = 0;
                    }
                }
                else if (EnemyCE[2] > EnemyCE[0])
                {
                    if (EnemyCE[2] > EnemyCE[1])
                    {
                        position = 2;
                    }
                    else
                    {
                        position = 1;
                    }
                }
            }

            return position;
        }
        #endregion

        public static void HomeBossAttackHandler(int retryAmount = 0)
        {
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);

            Main.ResetToHome();

            HomeBossAttack(retryAmount);

        }

        public static void HomeBossAttack(int retryAmount)
        {
            //Reset To Home
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);
            bool lost = false;
            Main.Sleep(3);

            string BossStatus = ImageToText.HomeBoss();

            if (BossStatus == "next")
            {
                MouseHandler.MoveCursor(LocationConstants.HOME_BOSS_BATTLE_NEXT, true);
                Main.Sleep(2);
                Point loc = new Point();
                if (PixelChecker.SearchPixel(ColorConstants.HOME_BOSS_NEXT, out loc))
                {
                    MouseHandler.MoveCursor(loc, true);
                }
                Main.Sleep(1);
                HomeBossAttack(0); //Starts Idling On Next Stage Then Re-Calls Function to Check for updates status
            }
            else if (BossStatus == "waves")
            {
                Main.LogConsole("Idling main boss...");
                for (int i = 0; i < 250; i++)
                {
                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_BOT_IDLE_CLICK, true);
                    Thread.Sleep(200);
                }
                HomeBossAttack(0); //Clicks for 100 hundred times and then rechecks if waves have been defeated
            }
            else if (BossStatus == "battle")
            {
                for (int CurrentTry = 0; CurrentTry < OtherConstants.ATTACK_RETRY_AMOUNT; CurrentTry++)
                {
                    MouseHandler.MoveCursor(LocationConstants.HOME_BOSS_BATTLE_NEXT, true);
                    Main.Sleep(2);
                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_ENEMYINFO_BATTLE_CONFIRM, true);
                    Main.Sleep(2);
                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_TEAM_BATTLE_CONFIRM, true);
                    Main.Sleep(3);

                    Main.Sleep(15); //Sleeps for 15 seconds as player is unable to skip in Home Bosses

                    bool BattleWon = CheckWin();

                    if (BattleWon)
                    {
                        Main.LogConsole("Won Home Boss Attack!");
                        Main.Sleep(2);
                        MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                        HomeBossAttack(0);
                        break; //Not Sure if this is needed but ill just add it anyways
                    }
                    else
                    {
                        Main.LogConsole("Lost Home Boss Attack!");
                        Main.Sleep(1);
                        MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                    }

                    Main.LogConsole("Outcome of the Home Boss Battle Won: {0}", BattleWon.ToString());
                }
            }
            else //Sometimes there can be trouble if the Menu shows "waves" in this case we just try again
            {
                Main.LogConsole("Unable To Read...");
                if (PixelChecker.CheckPixelValue(LocationConstants.HOME_BOSS_MAP_CIRCLE, ColorConstants.HOME_BOSS_MAP_CIRCLE))
                {
                    Main.LogConsole("Yeet Skeet Neet Pee");
                    Main.Sleep(2);
                    MouseHandler.MoveCursor(LocationConstants.HOME_BOSS_MAP_CIRCLE, true);
                    if (PixelChecker.SearchPixel(ColorConstants.HOME_NEXT_MAP, out var loc))
                    {
                        MouseHandler.MoveCursor(loc, true);
                        if (PixelChecker.SearchPixel(ColorConstants.HOME_BOSS_NEXT, out var loc3))
                        {
                            MouseHandler.MoveCursor(loc3, true);
                            HomeBossAttackHandler();
                        }
                    }
                    else
                    {
                        bool found = PixelChecker.SearchPixel(ColorConstants.HOME_NEXT_MAP, out var locs);
                        if (found) { MouseHandler.MoveCursor(loc, true); }
                        MouseHandler.MoveCursor(loc, true);
                        if (PixelChecker.SearchPixel(ColorConstants.HOME_BOSS_NEXT, out var loc3))
                        {
                            MouseHandler.MoveCursor(loc3, true);
                            HomeBossAttackHandler();
                        }
                    }
                    
                }

                if (retryAmount == 15)
                {
                    Main.LogConsole("Unable to read Boss Status...");
                    return;
                }

                Main.LogConsole("Moving Onto Next Stage..");

                HomeBossAttackHandler(retryAmount++);
            }
        }



    }
}
