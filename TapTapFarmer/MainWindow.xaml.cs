using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TapTapFarmer.Functions;
using TapTapFarmer.Functions.Ini;
using TapTapFarmer.Models;

namespace TapTapFarmer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //GlobalVariables.GLOBAL_PROC_NAME = WindowCapture.GetProccessName();
        }

        private void TakeScreen_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.GLOBAL_PROC_NAME = WindowCapture.GetProccessName();
            Bitmap BMP = WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);
            BMP.Save("ScreenCap.bmp", ImageFormat.Bmp);
        }

        #region Button Handlers
        private void CheckSize_Click(object sender, RoutedEventArgs e)
        {
            //System.Drawing.Size ProcSize = WindowCapture.GetProcessSize(GlobalVariables.GLOBAL_PROC_NAME);

            /* if (ProcSize != new Size(544, 994))
            //{
            //    MessageBox.Show("Wrong Resoloution Selected, Bot Stopped. \nGo To NOX >> SETTINGS >> ADVANCED SETTINGS >> 540x960 >> Restore Window Settings \n Start Bot Once Window Size Restored");
            //}
            //else
            //{
            //    MessageBox.Show("Valid Size");
            //} */

            //MessageBox.Show(ProcSize.ToString());
        }

        private void MailCheck_Click(object sender, RoutedEventArgs e)
        {
            //UpdatePlayerInfo.CheckMenu();
            //UpdatePlayerInfo.UpdateMenu();
            UpdatePlayerInfo.ClaimFriends();
        }

        private void OpenObjectsTest_Click(object sender, RoutedEventArgs e)
        {
            Thread OpenObjectsThread = new Thread(() =>
            {
                OpenObjects.OpenBlackSmith();
                OpenObjects.OpenHeroChest();
                OpenObjects.OpenAltar();
                OpenObjects.OpenMarket();
                OpenObjects.OpenCreationBag();
                //OpenObjects.OpenFortuneWheel();
                //OpenObjects.OpenArena();
                //OpenObjects.OpenDoS();
                //OpenObjects.OpenMiracleEye();
                //OpenObjects.OpenTavern();
                //OpenObjects.OpenExpedition();
                //OpenObjects.OpenPlanetTrial();
            });


            OpenObjectsThread.Start();
        }

        private void ClaimDailyBonuses_Click(object sender, RoutedEventArgs e)
        {
            //UpdatePlayerInfo.ClaimDailyBonuses();
        }

        private void CheckShards_Click(object sender, RoutedEventArgs e)
        {
            //UpdatePlayerInfo.CheckShards();
        }

        private void CheckCrate_Click(object sender, RoutedEventArgs e)
        {
            //UpdatePlayerInfo.CheckCrate();
        }

        private void CheckAvievements_Click(object sender, RoutedEventArgs e)
        {
            //UpdatePlayerInfo.CheckAchievements();
        }

        private void AttackHandler_Click(object sender, RoutedEventArgs e)
        {

            Thread AttackThread = new Thread(Attack.DenOfSecretAttackHandler);
            AttackThread.Start();
        }

        private void ScrollUp_Click(object sender, RoutedEventArgs e)
        {
            //MouseHandler.MouseMoveUp();
            //ReadIni.ReadFile();
            //UpdatePlayerInfo.SpinWheel();
            //UpdatePlayerInfo.CombineEquipment();


            //Thread AttackThread = new Thread(Attack.BattleLeagueAttackHandler);
            //AttackThread.Start();
            
            
        }

        private void BtnActionMinimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnActionSystemInformation_OnClick(object sender, RoutedEventArgs e)
        {
            var systemInformationWindow = new SystemInformationWindow();
            systemInformationWindow.Show();
        }

        private void btnActionClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }
        private void ScrollDown_Click(object sender, RoutedEventArgs e)
        {
            Main.LogConsole("Hello");
            Main.LogConsole("Hello", "Success");
            Main.LogConsole("Hello", "Info");
            Main.LogConsole("Hello", "Error");

            //MouseHandler.MouseMoveDown();
        }

        private void StartBot_Click(object sender, RoutedEventArgs e)
        {
            bool yes = false;
            bool resp = false;
            Task OpenObjectsThread = new Task(() =>
            {
                yes = Functions.Authentication.LoginHandler.CheckHWID(Properties.Settings.Default.username);
                Main.LogConsole("Trying To Connect to server...");
                resp = Functions.Authentication.LoginHandler.CheckLogin(Properties.Settings.Default.username,
                        Properties.Settings.Default.password);
                Main.LogConsole("Sucessfully connected loging in...");
                
                return;
            });

            OpenObjectsThread.Start();
            OpenObjectsThread.Wait();

            if (resp && yes)
            {
                Main.LogConsole("Connected Successfully");
                BotMain.BotStart();

            }
            else
            {
                Main.LogConsole("Unable to Connect to Server...");
                Main.LogConsole("Shutting Down...");
                Thread.Sleep(5);
                Application.Current.Shutdown();
            }
            
            
        }

        private void TextTest_Click(object sender, RoutedEventArgs e)
        {

            ImageToText.HomeBoss();
        }

        private void GetPlayerDetails_Click(object sender, RoutedEventArgs e)
        {
            UpdatePlayerInfo.ClaimEvents();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        private void UpdateConfig()
        {
            ConsoleLogBox.Text = $"[{DateTime.Now.ToString("h:mm:ss tt")}] Trying to Connect to server...";
            Main.LogConsole("Successfully Connected to Server. Enjoy!");
            WriteIni.WriteConfig();
            ReadIni.ReadFile();

            BossBattle.IsChecked = GlobalVariables.attackSettings.Boss;
            FriendBattle.IsChecked = GlobalVariables.attackSettings.Friend;
            GuildBattle.IsChecked = GlobalVariables.attackSettings.Guild;
            BraveBattle.IsChecked = GlobalVariables.attackSettings.Brave;
            DoSBattle.IsChecked = GlobalVariables.attackSettings.DoS;
            ExpeditionBattle.IsChecked = GlobalVariables.attackSettings.Expedition;
            PlanetTrialBattle.IsChecked = GlobalVariables.attackSettings.PlanetTrial;

            PlanetTrialRetry.Text = GlobalVariables.attackSettings.PlanetTrialRetryAmount.ToString();
            PlanetTrialAutoRetry.IsChecked = GlobalVariables.attackSettings.PlanetTrialAutoRetry;
            BraveMaxTickets.Text = GlobalVariables.attackSettings.BraveMaxTickets.ToString();
            BraveMaxCE.Text = GlobalVariables.attackSettings.BraveMaxCE.ToString();
            BraveAttackMore.IsChecked = GlobalVariables.attackSettings.BraveAutoRetry;
            GuildMax.Text = GlobalVariables.attackSettings.GuildRetryAmount.ToString();
            FriendRetry.Text = GlobalVariables.attackSettings.FriendRetryAmount.ToString();
            FriendMax.IsChecked = GlobalVariables.attackSettings.FriendMaxOnly;

            Alchemy.IsChecked = GlobalVariables.dailySettings.Alchemy;
            SendHearts.IsChecked = GlobalVariables.dailySettings.SendHearts;
            SpinWheel.IsChecked = GlobalVariables.dailySettings.SpinWheel;
            CompleteTavern.IsChecked = GlobalVariables.dailySettings.DailyTavern;
            CombineEquip.IsChecked = GlobalVariables.dailySettings.CombineEquip;
            PerformCommon.IsChecked = GlobalVariables.dailySettings.CommonSummon;
            PerformGrand.IsChecked = GlobalVariables.dailySettings.GrandSummon;
            CompleteBrave.IsChecked = GlobalVariables.dailySettings.DailyBrave;

            SendFriend.IsChecked = GlobalVariables.dailySettings.DailyBrave;
            AcceptFriends.IsChecked = GlobalVariables.dailySettings.DailyBrave;
            DeclineFriends.IsChecked = GlobalVariables.dailySettings.DailyBrave;

            if (AcceptFriends.IsChecked == true && DeclineFriends.IsChecked == true)
            {
                AcceptFriends.IsChecked = false;
            }

            //DailyBag.IsChecked = GlobalVariables.dailySettings.DailyBrave;
            PurchaseDoS.IsChecked = GlobalVariables.dailySettings.DailyBrave;
            DeleteMail.IsChecked = GlobalVariables.dailySettings.DailyBrave;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //UpdateConfig();
        }

        #region Uneeded Event Handlers
        private void TextBlock_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.TapTapFarmer.com");
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void TabItem_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.No;
        }

        private void AcceptFriends_Click(object sender, RoutedEventArgs e)
        {
            CheckBox CB = (CheckBox)sender;
            Console.WriteLine(CB.Name);
            if (CB.Name == "AcceptFriends" && DeclineFriends.IsChecked == true)
            {
                DeclineFriends.IsChecked = false;
            }
            else if (CB.Name == "DeclineFriends" && AcceptFriends.IsChecked == true)
            {
                AcceptFriends.IsChecked = false;
            }

        }
        #endregion

        #region Old Console Log Method
        //public void LogConsole(string message, string type = "Default")
        //{
        //System.Windows.Media.Color TextColour = System.Windows.Media.Color.FromRgb(41, 43, 44);
        //if (type == "Default")
        //{
        //    TextColour = System.Windows.Media.Color.FromRgb(41,43,44);
        //}
        //else if (type == "Success")
        //{
        //    TextColour = System.Windows.Media.Color.FromRgb(92,184,92);
        //}
        //else if (type == "Info")
        //{
        //    TextColour = System.Windows.Media.Color.FromRgb(2, 117, 216);
        //}
        //else if (type == "Error")
        //{
        //    TextColour = System.Windows.Media.Color.FromRgb(237, 67, 55);
        //}

        //SolidColorBrush brush = new SolidColorBrush(TextColour);
            
        ////Temp Disabled as this method changes the whole textblock colour
        ////ConsoleLogBox.Foreground = brush; 

        //Thread LogConsoleThread = new Thread(() =>
        //{
        //    Application.Current.Dispatcher.Invoke(new Action(() =>
        //    {
        //        ConsoleLogBox.Text += $"{Environment.NewLine}[{DateTime.Now.ToString("h:mm:ss tt")}] {message}";
        //    }));

        //});
        //LogConsoleThread.Start();

        //ConsoleLogBox.ScrollToEnd();
        //}
        #endregion

        public void LogConsole(string message, string type = "Default")
        {
            ConsoleLogBox.Text += $"{Environment.NewLine}[{DateTime.Now.ToString("h:mm:ss tt")}] {message}";
            ConsoleLogBox.ScrollToEnd();
        }

        private void SaveConfig_Click(object sender, RoutedEventArgs e)
        {
            AttackModel attackModel = new AttackModel
            {
                Boss = (bool)BossBattle.IsChecked,
                Friend = (bool)FriendBattle.IsChecked,
                Guild = (bool)GuildBattle.IsChecked,
                Brave = (bool)BraveBattle.IsChecked,
                DoS = (bool)DoSBattle.IsChecked,
                Expedition = (bool)ExpeditionBattle.IsChecked,
                PlanetTrial = (bool)PlanetTrialBattle.IsChecked,

                PlanetTrialRetryAmount = Convert.ToInt32(PlanetTrialRetry.Text),
                PlanetTrialAutoRetry = (bool)PlanetTrialAutoRetry.IsChecked,

                BraveMaxTickets = Convert.ToInt32(BraveMaxTickets.Text),
                BraveMaxCE = Convert.ToInt32(BraveMaxCE.Text),
                BraveAutoRetry = (bool)BraveAttackMore.IsChecked,

                GuildRetryAmount = Convert.ToInt32(GuildMax.Text),

                FriendRetryAmount = Convert.ToInt32(FriendRetry.Text),
                FriendMaxOnly = (bool)FriendMax.IsChecked
            };

            GlobalVariables.attackSettings = attackModel;

            DailyModel dailyModel = new DailyModel
            {
                Alchemy = (bool)Alchemy.IsChecked,
                SendHearts = (bool)SendHearts.IsChecked,
                SpinWheel = (bool)SpinWheel.IsChecked,
                DailyTavern = (bool)CompleteTavern.IsChecked,
                CombineEquip = (bool)CombineEquip.IsChecked,
                CommonSummon = (bool)PerformCommon.IsChecked,
                GrandSummon = (bool)PerformGrand.IsChecked,
                DailyBrave = (bool)CompleteBrave.IsChecked,

                SendFriendReq = (bool)SendFriend.IsChecked,
                AcceptFreindReq = (bool)AcceptFriends.IsChecked,
                DeclineFriendReq = (bool)DeclineFriends.IsChecked
            };
            dailyModel.DailyBag = (bool)DailyBag.IsChecked;
            dailyModel.PurchaseDoSTicket = (bool)PurchaseDoS.IsChecked;
            dailyModel.DeleteMail = (bool)DeleteMail.IsChecked;
            WriteIni.UpdateConfig(attackModel, dailyModel);
        }

        private void ReloadCofig_Click(object sender, RoutedEventArgs e)
        {
            UpdateConfig();
        }

        private void ResetConfig_Click(object sender, RoutedEventArgs e)
        {
            var iniFile = "BotConfig.ini";
            File.Delete(iniFile);
            WriteIni.WriteConfig();
            ReadIni.ReadFile();
        }

        private void OpenConfig_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", "BotConfig.ini");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void FindConfigs_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://sbond.ml/p/trainerbotsite/forum/forum-8.html");
        }

        private void ShareConfig_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://sbond.ml/p/trainerbotsite/forum/newthread.php?fid=8");
        }

        private void ConsoleLogBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
