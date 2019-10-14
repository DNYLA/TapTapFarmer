using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTapFarmer.Constants;
using System.Drawing;

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
                for (int CurrentTry = 0; CurrentTry < OtherConstants.ATTACK_RETRY_AMOUNT; CurrentTry++)
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

        #region Battle League
        public static void BattleLeagueAttackHandler()
        {
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);

            Main.ResetToHome();

            OpenObjects.OpenBattleLeague();

            AttackBattleLeague();

        }

        public static void AttackBattleLeague()
        {
            //Similar to Main Menu Boss where it doesn't matter if you win or lose it just attacks the lowest number
            string PlayerAttackCE = ImageToText.GetOcrResponse(TextConstants.LEAGUE_PLAYER_CE_START, TextConstants.LEAGUE_PLAYER_CE_SIZE);
            int PlayerCE = Convert.ToInt32(PlayerAttackCE);

        }
        #endregion

        public static void HomeBossAttackHandler()
        {
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);

            Main.ResetToHome();

            HomeBossAttack();

        }

        public static void HomeBossAttack()
        {
            //Reset To Home
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);

            Main.Sleep(3);

            string BossStatus = ImageToText.HomeBoss();

            if (BossStatus == "next")
            {
                MouseHandler.MoveCursor(LocationConstants.HOME_BOSS_BATTLE_NEXT, true);
                Main.Sleep(2);
                MouseHandler.MoveCursor(LocationConstants.HOME_BOSS_IDLE_NEXT, true);
                Main.Sleep(1);
                HomeBossAttack(); //Starts Idling On Next Stage Then Re-Calls Function to Check for updates status
            }
            else if (BossStatus == "waves")
            {
                for (int i = 0; i < 100; i++)
                {
                    MouseHandler.MoveCursor(LocationConstants.GLOBAL_BOT_IDLE_CLICK, true);
                }
                HomeBossAttack(); //Clicks for 100 hundred times and then rechecks if waves have been defeated
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
                        Main.Sleep(1);
                        MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                        //HomeBossAttack();
                        break; //Not Sure if this is needed but ill just add it anyways
                    }
                    else
                    {
                        Main.Sleep(1);
                        MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
                    }

                    Console.WriteLine("The Outcome of the Battle Win: {0}", BattleWon);
                }
            }
            else //Sometimes there can be trouble if the Menu shows "waves" in this case we just try again
            {
                HomeBossAttackHandler();
            }
        }



    }
}
