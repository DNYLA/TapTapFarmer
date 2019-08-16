using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                //for (int i = 0; i < 50; i++)
                //{
                //    MouseHandler.MoveCursor(LocationConstants.GLOBAL_BOT_IDLE_CLICK, true);
                //}
            }

            return true;
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

            //Force Back To Main Menu && Collect any gold (Ran 30 Times because it will click nonstop)
            for (int i = 0; i < 30; i++)
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
