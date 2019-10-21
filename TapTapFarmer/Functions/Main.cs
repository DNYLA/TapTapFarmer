using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TapTapFarmer.Constants;

namespace TapTapFarmer.Functions
{
    class Main
    {
        public static Boolean IdleClick()
        {
            while (GlobalVariables.BOT_STATE == "Idling")
            {
                MouseHandler.MoveCursor(LocationConstants.GLOBAL_BOT_IDLE_CLICK, true);
                Thread.Sleep(100);
                //for (int i = 0; i < 50; i++)
                //{
                //    MouseHandler.MoveCursor(LocationConstants.GLOBAL_BOT_IDLE_CLICK, true);
                //}
            }

            return true;
        }

        public static void LogConsole(string message, string type = "Default")
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {

                if (MainWindow.Instance != null)
                {
                    MainWindow.Instance.LogConsole(message, type);
                }
                else
                {
                    //Do something if MainWindow isn't loaded yet
                }
            }));
        }


        public static Boolean CheckSize()
        {
            System.Drawing.Size ProcSize = WindowCapture.GetProcessSize(GlobalVariables.GLOBAL_PROC_NAME);

            if (ProcSize != new System.Drawing.Size(544, 994))
            {
                MessageBox.Show("Wrong Resoloution Selected, Bot Stopped. \nGo To NOX >> SETTINGS >> ADVANCED SETTINGS >> 540x960 >> Restore Window Settings \n Start Bot Once Window Size Restored");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Function that resets the game back to the main Menu
        /// </summary>
        /// <returns></returns>
        public static Boolean ResetToHome()
        {
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);

            //If Screen is stuck on a battle screen this will select it. (Doesn't Check if button is there just clicks it anyways); 
            // Now thinking about it this could cause problems so i added a check
            if (PixelChecker.CheckPixelValue(LocationConstants.GLOBAL_BATTLE_FINISHED, ColorConstants.GLOBAL_BATTLE_FINISHED))
            {
                MouseHandler.MoveCursor(LocationConstants.GLOBAL_BATTLE_FINISHED, true);
            }

            //Force Back To Main Menu && Collect any gold (Ran 50 Times because it will click nonstop)
            for (int i = 0; i < 50; i++)
            {
                MouseHandler.MoveCursor(LocationConstants.HOME_BOTTOM_BATTLE, true);
            }

            //Check if Menu is Open And Close it if it is

            //Check if Battle Menu is Active and Returns true
            if (isHome())
            {
                Console.WriteLine("Is Home");
                return true;
            }

            /* Going to Do this bit later since i need to find a place where the above will not return True
             * What i need to check ofr is 
             * If Not At Home check for X Button and Click
             * Multiple Close Button Locations
             */
            //Do Comment Above Here


            //ReCheck If Home
            if (isHome())
            {
                Console.WriteLine("Is Home");
                return true;
            }

            Console.WriteLine("Is Not Home");
            return false;
        }

        public static void CheckLimits()
        {
            if (GlobalVariables.CURRENCY_INFO[2] == -1)
            {
                LogConsole("Unable To Read Level. Assuming You Have Everything Unlocked. (If You don't have everything unlocked Edit BotConfig.Ini & Manually Change Level");
            }

            #region Massive If Statement
            if (GlobalVariables.CURRENCY_INFO[2] >= 72)
            {
                return;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 72)
            {
                GlobalVariables.PlayerLimits.PlanetTrial = false;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 70)
            {
                GlobalVariables.PlayerLimits.Familiar = false;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 55)
            {
                GlobalVariables.PlayerLimits.Legion = false;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 40)
            {
                GlobalVariables.PlayerLimits.Expedition = false;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 28)
            {
                GlobalVariables.PlayerLimits.Tavern = false;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 25)
            {
                GlobalVariables.PlayerLimits.MiracleEye = false;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 24)
            {
                GlobalVariables.PlayerLimits.DoS = false;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 21)
            {
                GlobalVariables.PlayerLimits.GuilD = false;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 20)
            {
                GlobalVariables.PlayerLimits.Events = false;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 18)
            {
                GlobalVariables.PlayerLimits.Brave = false;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 17)
            {
                GlobalVariables.PlayerLimits.Quests = false;
            }

            if (GlobalVariables.CURRENCY_INFO[2] < 14)
            {
                GlobalVariables.PlayerLimits.FortuneWheel = false;
            }
            #endregion
        }


        public static bool CheckSameDay(DateTime lastDate)
        {
            DateTime TodayDate = DateTime.Now;
            Console.WriteLine(TodayDate.Day.ToString());
            if (TodayDate.Day == lastDate.Day && TodayDate.Month == lastDate.Month)
            {
                return true;
            }
            Console.WriteLine(TodayDate.Day.ToString());
            return true;



        }

        public static bool ResetToHomePage(int ClicksUntilHome)
        {
            for (int i = 0; i < ClicksUntilHome; i++)
            {
                MouseHandler.MoveCursor(LocationConstants.HOME_BOTTOM_BATTLE, true);
            }

            return true;
        }


        public static void Sleep(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }

        public static Boolean isHome()
        {
            if (PixelChecker.CheckPixelValue(LocationConstants.HOME_BOTTOM_BATTLE_ACTIVE, ColorConstants.HOME_BOTTOM_BATTLE_ACTIVE_COLOR))
            {
                return true;
            }

            return false;
        }


    }
}
