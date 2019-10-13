using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using TapTapFarmer.Constants;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using TapTapFarmer.OCR;

namespace TapTapFarmer.Functions
{
    class ImageToText
    {
        #region Main Method
        public static String GetOcrResponse(Point Location, Size SizeOfRec, string OCREngineMode = "1")
        {
            string APIResponse = string.Empty;

            Task task = Task.Factory.StartNew(() =>
            {
                APIResponse = DoOcr.DoAsync(Location, SizeOfRec, OCREngineMode).Result;
            });

            task.Wait();

            return APIResponse;
        }
        #endregion

        #region Player Info Methods
        public static int GetWheelCoinAmount()
        {
            string TokenAmount = GetOcrResponse(TextConstants.FORTUNEWHEEL_TOKEN_START, TextConstants.FORTUNEHWEEL_TOKEN_SIZE);

            return Convert.ToInt32(TokenAmount);
        }

        public static int GetGrandKeyAmount()
        {
            string KeyAmount = GetOcrResponse(TextConstants.HEROCHEST_GRAND_START, TextConstants.HEROCHEST_KEY_SIZE);

            return Convert.ToInt32(KeyAmount);
        }

        public static int GetCommonKeyAmount()
        {
            string KeyAmount = GetOcrResponse(TextConstants.HEROCHEST_COMMON_START, TextConstants.HEROCHEST_KEY_SIZE);

            return Convert.ToInt32(KeyAmount);
        }

        public static int GetCombineAmount()
        {
            string KeyAmount = GetOcrResponse(TextConstants.BLACKSMITH_COMBINEAMOUNT, TextConstants.BLACKSMITH_COMBINEAMOUNT_SIZE);
            Console.WriteLine(KeyAmount + "BEfore Changed back");
            KeyAmount = RemoveWhiteSpace(KeyAmount, true);
            Console.WriteLine(KeyAmount + "BEfore Sent back");
            try
            {
                return Convert.ToInt32(KeyAmount);
            }
            catch
            {
                return -1;
            }
        }

        public static int GetBlacksmithPurchaseAmount()
        {
            string MoneyText = GetOcrResponse(TextConstants.BLACKSMITH_PURCHASEAMOUNT, TextConstants.BLACKSMITH_PURCHASEAMOUNT_SIZE);
            MoneyText = MoneyText.ToLower();
            MoneyText = RemoveWhiteSpace(MoneyText, true);

            int MoneyLen = MoneyText.Length;
            int MoneyValue;


            if (MoneyText.EndsWith("k"))
            {
                MoneyValue = MultiplyValue(MoneyText, 1000);
            }
            else if (MoneyText.EndsWith("m"))
            {
                MoneyValue = MultiplyValue(MoneyText, 1000000);
            }
            else
            {
                MoneyValue = StringToInt(MoneyText);
            }

            return MoneyValue;
        }

        #endregion

        #region Battle Methods
        #region Enemy CE
        public static int GetEnemyCE()
        {

            int x = TextConstants.LEAGUE_ENEMY_CE_START.X;
            int y = TextConstants.LEAGUE_ENEMY_CE_START.Y;

            string[] CEArrayString = new string[3];
            int[] CEArray = new int[3];

            //for (int i = 0; i < 3; i++)
            //{
            //    y = TextConstants.LEAGUE_ENEMY_CE_START.Y + (i * 100);
            //    var newPoint = new Point(x, y);
            //    CEArrayString[i] = GetOcrResponse(newPoint, TextConstants.LEAGUE_PLAYER_CE_SIZE);
            //}

            //Console.WriteLine(CEArrayString[0]);
            //Console.WriteLine(CEArrayString[1]);
            //Console.WriteLine(CEArrayString[2]);

            ////Converts the Strings into Arrays
            //for (int i = 0; i < 3; i++)
            //{
            //    CEArray[i] = StringToInt(CEArrayString[i]);
            //}

            for (int i = 0; i < 3; i++)
            {
                Main.Sleep(1);
                x = TextConstants.LEAGUE_ENEMY_CE_START.X;
                y = TextConstants.LEAGUE_ENEMY_CE_START.Y + (i * 100);
                var PlayerLocation = new Point(x, y);
                MouseHandler.MoveCursor(PlayerLocation, true);
                Main.Sleep(2);
                CEArrayString[i] = GetOcrResponse(TextConstants.ENEMY_PROFILE_CE_START, TextConstants.ENEMY_PROFILE_CE_SIZE);
                CEArray[i] = StringToInt(CEArrayString[i]);
                Main.Sleep(1);
                MouseHandler.MoveCursor(LocationConstants.HOME_BOTTOM_BATTLE, true); //Closes Profile Menu
            }

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(CEArray[i]);
            }


            return 1;
        }
        #endregion 

        #region Gym Battle
        public static String GymBattleCheck(out Point Section, int Multiplier = 0)
        {
            /*
             * 1. Read Text From Bottom To Top (Only Reads Text From A Sepcific Box Size on the right hand side)
             * 2. If Text = Battle Save Position && Colour
             * 3. Return Colour && / || Battle
             * 4. Use Colour / Battle In Parent Function To Click The Button
             */

            //Requires ColourSpace == GrayScale

            Section = new Point(0, 0);

            if (Multiplier == 3) //Check to make sure Function doesn't keep recalling it self
            {
                Console.WriteLine("Returning");
                return "Invalid";
            }

            int x = TextConstants.SKYPILLAR_BATTLE_START.X;
            int y = TextConstants.SKYPILLAR_BATTLE_START.Y;

            Size boxSize = TextConstants.SKYPILLAR_BATTLE_SIZE;

            //x = x * Multiplier; No Need to Change X
            y = y - (254 * Multiplier);

            Point NewPoint = new Point(x, y);

            string boxText = GetOcrResponse(NewPoint, boxSize);

            boxText = boxText.ToLower();

            boxText = RemoveWhiteSpace(boxText);

            Console.WriteLine(boxText);

            Section = NewPoint;

            if (boxText == "battie")
            {
                boxText = "battle"; // 10/10 times battle is read as Battie
            }

            if (boxText != "battle")
            {
                return GymBattleCheck(out Section, Multiplier + 1);
            }

            

            return boxText;
        }
        #endregion

        #region Home Boss
        public static String HomeBoss()
        {
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);


            string BossStat = string.Empty;
            //Task task = Task.Factory.StartNew(() =>
            //{
            //    UpdateVar(TextConstants.HOME_BOSS_START, TextConstants.HOME_BOSS_SIZE);
            //});

            //Task task = Task.Factory.StartNew(() =>
            //{
            //    BossStat = DoOcr.DoAsync(TextConstants.HOME_BOSS_START, TextConstants.HOME_BOSS_SIZE).Result;
            //});

            //task.Wait();

            string BossStatus;

            BossStatus = GetOcrResponse(TextConstants.HOME_BOSS_START, TextConstants.HOME_BOSS_SIZE);

            Console.WriteLine("The Finale: " + BossStatus);

            //Main.Sleep(5);

            
            BossStatus = BossStatus.ToLower();

            BossStatus = RemoveWhiteSpace(BossStatus);
            //BossStatus = BossStatus.Split()[0]; //Using .Contains on a string now makes the use of removing exsess lines usless.

            if (BossStatus.Contains("boss"))
            {
                return "battle";
            }

            if (BossStatus.Contains("waves"))
            {
                return "waves";
            }

            if (BossStatus.Contains("next"))
            {
                return "next";
            }

            Console.WriteLine(BossStatus);
            Console.WriteLine("Problem With Conversion.");
            return "Invalid";
        }
        #endregion
        #endregion

        #region Player Info Methods
        #region Level Method (Normal)
        /// <summary>
        /// Gets Level
        /// </summary>
        /// <returns></returns>
        public static int GetLevel()
        {
            string PlayerLevel = GetOcrResponse(TextConstants.LEVEL_START, TextConstants.HOME_LEVEL_SIZE);
            Console.WriteLine(PlayerLevel);
            PlayerLevel = RemoveWhiteSpace(PlayerLevel, false);
            PlayerLevel = PlayerLevel.Split()[0];
            Console.WriteLine(PlayerLevel);

            if (PlayerLevel.Contains("Level "))
            {
                PlayerLevel = PlayerLevel.TrimStart("Level ".ToCharArray());
            }

            try
            {
                return Convert.ToInt32(PlayerLevel.Substring(0, PlayerLevel.Length));
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region Level Method (Advanced)
        public static int GetLevelAdvanced()
        {
            string PlayerLevel = GetOcrResponse(TextConstants.LEVEL_ADVANCED_START, TextConstants.HOME_LEVEL_ADVANCED_SIZE);
            PlayerLevel = RemoveWhiteSpace(PlayerLevel, true);
            Console.WriteLine(PlayerLevel);

            try
            {
                return Convert.ToInt32(PlayerLevel.Substring(0, PlayerLevel.Length));
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region Gem Method
        public static int GetGemAmount()
        {
            string GemAmount = GetOcrResponse(TextConstants.GEM_START, TextConstants.GLOBAL_CURRENCY_SIZE);
            GemAmount = RemoveWhiteSpace(GemAmount, true);

            try
            {
                return Convert.ToInt32(GemAmount.Substring(0, GemAmount.Length));
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region Money Method
        public static int GetMoneyAmount()
        {
            string MoneyText = GetOcrResponse(TextConstants.GOLD_START, TextConstants.GLOBAL_CURRENCY_SIZE);
            MoneyText = MoneyText.ToLower();
            MoneyText = RemoveWhiteSpace(MoneyText, true);

            int MoneyLen = MoneyText.Length;
            int MoneyValue;


            if (MoneyText.EndsWith("k"))
            {
                MoneyValue = MultiplyValue(MoneyText, 1000);
            }
            else if (MoneyText.EndsWith("m"))
            {
                MoneyValue = MultiplyValue(MoneyText, 1000000);
            }
            else
            {
                MoneyValue = StringToInt(MoneyText);
            }

            return MoneyValue;
        }
        #endregion

        #region Purple Soul Method
        public static int GetPurpleSoulAmount()
        {
            string PurpleSoulText = GetOcrResponse(TextConstants.ALTAR_PURPLE_SOUL_START, TextConstants.ALTAR_SOUL_SIZE);
            PurpleSoulText = PurpleSoulText.ToLower();
            PurpleSoulText = RemoveWhiteSpace(PurpleSoulText, true);

            int PurpleSoulValue;

            if (PurpleSoulText.EndsWith("k"))
            {
                PurpleSoulValue = MultiplyValue(PurpleSoulText, 1000);
            }
            else if (PurpleSoulText.EndsWith("m"))
            {
                PurpleSoulValue = MultiplyValue(PurpleSoulText, 1000000);
            }
            else
            {

                PurpleSoulValue = StringToInt(PurpleSoulText);
            }

            return PurpleSoulValue;
        }
        #endregion

        #region Golden Soul Method
        public static int GetGoldenSoulAmount()
        {
            string GoldenSoulText = GetOcrResponse(TextConstants.ALTAR_GOLDEN_SOUL_START, TextConstants.ALTAR_SOUL_SIZE);
            GoldenSoulText = RemoveWhiteSpace(GoldenSoulText, true);

            try
            {
                return Convert.ToInt32(GoldenSoulText.Substring(0, GoldenSoulText.Length));
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #endregion

        #region Helper Methods
        public static String RemoveWhiteSpace(string Text, bool RemoveExtraLines = false)
        {
            //Going To Add More Checks Later
            if (RemoveExtraLines)
            {
                Text = Text.Split()[0];
            }
            return Text.Replace(" ", string.Empty);
        }

        public static int StringToInt(string value)
        {
            try
            {
                return Convert.ToInt32(value.Substring(0, value.Length));
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int MultiplyValue(string ValToMultiply, int Amount)
        {
            try
            {
                return Convert.ToInt32(ValToMultiply.Substring(0, ValToMultiply.Length - 1)) * Amount;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion
    }
}
