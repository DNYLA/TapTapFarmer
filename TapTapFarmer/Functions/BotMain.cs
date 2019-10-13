using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using TapTapFarmer.Constants;
using System.Runtime.InteropServices;
using Gma.System.MouseKeyHook;
using TapTapFarmer.Models;

namespace TapTapFarmer.Functions
{
    class BotMain
    {
        public static void BotStart()
        {
            if (GlobalVariables.BOT_STARTED == true)
            {
                Main.LogConsole("Bot Already Started");
                return;
            }
            //Add Functions to Check if a Supported Emulator Is Open
            WindowCapture.GetProccessName(); //Updates Process Name
            //if (GlobalVariables.GLOBAL_PROC_NAME == "InvalidProc")
            //{
            //    System.Windows.Application.Current.Shutdown();
            //}
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);
            //Add Functions to Check For Specific Options
            Console.WriteLine("Bot Started: " + GlobalVariables.BOT_STARTED.ToString());
            GlobalVariables.BOT_STARTED = true;

            //Adding this to Another Thread won't work.
            KeyHandler.StartKeyHandler();

            //Create a new Thread to Handle All Bot Related Things
            var MainThread = new Thread(() =>
            {
                DrawOverlay("Click F5 to stop the Bot");
                Thread.Sleep(2000);

                //KeyHandler.StartKeyHandler();

                //Everything must be contained into a while loop to make sure bot stops when it is told to stop
                while (GlobalVariables.BOT_STARTED == true)
                {
                    Main.LogConsole("Bot Started");

                    //Reset Bot To Main Menu
                    Main.ResetToHome();

                    //Check For Users Level, Gold, Purple ETC
                    GlobalVariables.CURRENCY_INFO = UpdatePlayerInfo.GetCurrecyDetails();
                    Main.LogConsole($"Gold: {GlobalVariables.CURRENCY_INFO[0].ToString()} - Gems: {GlobalVariables.CURRENCY_INFO[1].ToString()} -  Level: {GlobalVariables.CURRENCY_INFO[2].ToString()}");
                    Main.LogConsole($"Purple Souls: {GlobalVariables.CURRENCY_INFO[3].ToString()} - Golden Souls: {GlobalVariables.CURRENCY_INFO[4].ToString()}");

                    Main.CheckLimits();

                    ClaimDailies();



                    if (!GlobalVariables.EVENTS_COMPLETED)
                    {
                        Main.ResetToHome();
                        GlobalVariables.EVENTS_COMPLETED = UpdatePlayerInfo.ClaimEvents();
                    }

                    //Add BlackSmith


                    //Add Functions to Claim Daily Items, Open Crates, etc



                    //Make Bot Idle Click for 10 Minutes Then Move on
                    GlobalVariables.BOT_STATE = "Idling";
                    Console.WriteLine("Bot State: Idling");
                    Console.WriteLine("Bot Idling");

                    //Create a new Task To Handle Clicking so it doesnt take up Main Thread.
                    bool output = false;
                    //Task task = Task.Factory.StartNew(() =>
                    //{
                    //    output = Main.IdleClick();
                    //});

                    var task = new Thread(() =>
                    {
                        output = Main.IdleClick();
                    });

                    task.Start();

                    Thread.Sleep(15 * 60000); //Sleep For 15 Minutes Before Continuing
                    GlobalVariables.BOT_STATE = "Checking"; //Setting State

                    //Waiting for the Task To Return A Value & Then Double Checking If Task Returned true. TRUE = Idling Stopped, FALSE = Still Idling.
                    Console.WriteLine(output);

                    Console.WriteLine("Bot State: Checking");

                    Console.WriteLine("Checking If Account Alreay Logged In");
                    //Check If Bot Logged In
                    if (PixelChecker.CheckPixelValue(LocationConstants.HOME_ACCOUNT_ALREADY_LOGGED, ColorConstants.GLOBAL_OK_BUTTON))
                    {
                        Console.WriteLine("Account Logged In From Another Account Waiting 5 Minutes To Re-Log");
                        Thread.Sleep(5 * 60000);
                        MouseHandler.MoveCursor(LocationConstants.HOME_ACCOUNT_ALREADY_LOGGED, true);
                    }

                    Console.WriteLine("Bot Re-Idling");
                    Console.WriteLine("Bot Started: " + GlobalVariables.BOT_STARTED.ToString());
                }
            });
            MainThread.Start();

            if (GlobalVariables.BOT_STARTED == false)
            {
                MainThread.Abort();
            }

        }

        private static void ClaimDailies()
        {
            if (!Main.CheckSameDay(GlobalVariables.LAST_RAN))
            {
                Main.LogConsole($"Bot Hasn't Been Ran Since {GlobalVariables.LAST_RAN} Completing Daily Tasks");
                GlobalVariables.tasksSettings = new TasksModel(); //Resets Everything back to default
            }
            else
            {
                Main.LogConsole($"Bot Already Ran Today. No Need To Re-Do Everything");
            }

            TasksModel tasks = GlobalVariables.tasksSettings;

            if (!tasks.FriendsClaimed)
            {
                
                if (UpdatePlayerInfo.ClaimFriends())
                {
                    tasks.FriendsClaimed = true;
                    tasks.SentHears = true;
                }
                
            }

            if(!tasks.DailyClaimed)
            {
                tasks.DailyClaimed = UpdatePlayerInfo.ClaimPrivellage(); ;
            }

            if (!tasks.Defeat3Claimed)
            {
                tasks.Defeat3Claimed = UpdatePlayerInfo.Defeat3Main();
            }


            if (!tasks.AlchemyClaimed)
            {
                tasks.AlchemyClaimed = UpdatePlayerInfo.ClaimAlchemy();
            }


            if (!tasks.SpunWheel)
            {
                tasks.SpunWheel = UpdatePlayerInfo.SpinWheel();
            }

            if (!tasks.CompletedTavern)
            {
                //Add Tavern Handling below
            }

            if (!tasks.CombinedEquip)
            {
                //Add CombinedEquip Handling below
            }

            if (!tasks.PerformedCommon)
            {
                tasks.PerformedCommon = UpdatePlayerInfo.SummonCommonKey();
            }

            if (!tasks.PerformedGrand)
            {
                tasks.PerformedGrand = UpdatePlayerInfo.SummonGrandKey();
            }

            if (!tasks.CompletedBrave)
            {
                //Add CompletedBrave Handling below
            }

            if (!tasks.CompletedEvents)
            {
                tasks.CompletedEvents = UpdatePlayerInfo.ClaimEvents();
            }

            

            //Claim Mail
            UpdatePlayerInfo.ClaimMail();

            //Update Global Variables Settings
            GlobalVariables.tasksSettings = tasks;

        }

        public static void StopBot()
        {
            Console.WriteLine("Stopping Bot...");
            GlobalVariables.BOT_STARTED = false;
            GlobalVariables.BOT_STATE = "Stopped";
            Console.WriteLine("Bot Started: " + GlobalVariables.BOT_STARTED.ToString());
        }


        [DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);
        public static bool DrawOverlay(string overlayMessage)
        {
            bool OverlayOn = true;
            Thread Thr = new Thread(() =>
            {
                while (OverlayOn)
                {
                    IntPtr desktop = GetDC(IntPtr.Zero);
                    using (Graphics g = Graphics.FromHdc(desktop))
                    {


                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;
                        Font font = new Font("Microsoft YaHei UI", 25, System.Drawing.FontStyle.Bold);

                        Rectangle Rect = new Rectangle(750, 100, 500, 100);
                        //g.FillRectangle(Brushes.Black, 800, 100, 500, 100);
                        g.DrawString(overlayMessage, font, Brushes.HotPink, Rect, sf);
                    }
                    //Graphics.FromHwnd(IntPtr.Zero);
                    ReleaseDC(IntPtr.Zero, desktop);
                }
            });
            Thr.Start();

            bool output = false;
            Task task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                output = false;
            });

            task.Wait();

            OverlayOn = output;
            return true;
        }
    }
}
