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
using TapTapFarmer.Ocr;

namespace TapTapFarmer.Functions
{
    class ImageToText
    {

        public static string ImageText(Point Location, Size SizeOfRec, bool x, bool y, bool z, bool f)
        {
            return "z";
        }
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

        public static String RemoveWhiteSpace(string Text)
        {
            //Going To Add More Checks Later
            Text = Text.Split()[0];
            return Text.Replace(" ", string.Empty);
        }

        public static String GymBattleCheck(out Point Section, int Multiplier = 0, bool secondCheck = false)
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
                if (secondCheck == true)
                {
                    Console.WriteLine("Returning");
                    return "Invalid";
                }

                Multiplier = 0;

                secondCheck = true;
            }

            int x = TextConstants.GYM_BATTLE_START.X;
            int y = TextConstants.GYM_BATTLE_START.Y;

            Size boxSize = TextConstants.GYM_BATTLE_SIZE;

            if (secondCheck)
            {
                x = TextConstants.GYM_BATTLE2_START.X;
                y = TextConstants.GYM_BATTLE2_START.Y;
                boxSize = TextConstants.GYM_BATTLE2_SIZE;
            }

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
                return GymBattleCheck(out Section, Multiplier + 1, secondCheck);
            }

            

            return boxText;
        }

       

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

            MessageBox.Show("The Finale: " + BossStatus);

            //Main.Sleep(5);

            
            BossStatus = BossStatus.ToLower();

            BossStatus = RemoveWhiteSpace(BossStatus);
            BossStatus = BossStatus.Split()[0];


            if (BossStatus.EndsWith("battle"))
            {
                BossStatus = "battle";
            }

            if ((BossStatus != "battle") && (BossStatus != "next"))
            {
                Console.WriteLine(BossStatus);
                Console.WriteLine("Problem Recalling Function");
                HomeBoss(); //Sometimes The Animation on the Boss Button makes the text unreadable // Incorrect
            }
            Console.WriteLine("Returning");
            return BossStatus;
        }

        /// <summary>
        /// Gets Level
        /// </summary>
        /// <returns></returns>
        public static String GetPlayerLevel()
        {
            //Requires Colour Space == Color
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);
            Main.Sleep(1);
            MouseHandler.MoveCursor(LocationConstants.GLOBAL_LEVEL_BAR, true);
            Main.Sleep(1);
            string playerLevel = GetOcrResponse(TextConstants.LEVEL_START, TextConstants.LEVEL_START_SIZE, "2");
            MessageBox.Show(playerLevel);

            return playerLevel;
        }

        public static int GetGemAmount()
        {
            //Requires ColorSpace == GrayScale
            string GemAmount = GetOcrResponse(TextConstants.GEM_START, TextConstants.GEM_START_SIZE, "2");
            MessageBox.Show(GemAmount);
            try
            {
                return Convert.ToInt32(GemAmount);
            }
            catch
            {
                return -1;
            }
        }

        public static int GetGoldAmount()
        {
            //Requires ColorSpace == GrayScale || Color (This Means a check needs to be added to see if the value is -1 retry with ColorSpace == Color.
            string MoneyText = GetOcrResponse(TextConstants.GOLD_START, TextConstants.GOLD_START_SIZE, "2");
            MoneyText = MoneyText.ToLower();
            MessageBox.Show(MoneyText);

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
            MessageBox.Show(MoneyValue.ToString());
            return MoneyValue;
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
    }
}
