using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TapTapFarmer.Functions;

namespace TapTapFarmer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            //UpdatePlayerInfo.MailCheck();
        }

        private void ResetToHome_Click(object sender, RoutedEventArgs e)
        {
            //Main.ResetToHome();
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

            Thread AttackThread = new Thread(Attack.PlanetTrialAttackHandler);
            AttackThread.Start();
        }

        private void ScrollUp_Click(object sender, RoutedEventArgs e)
        {
            //MouseHandler.MouseMoveUp();
        }

        private void ScrollDown_Click(object sender, RoutedEventArgs e)
        {
            //MouseHandler.MouseMoveDown();
        }

        private void StartBot_Click(object sender, RoutedEventArgs e)
        {
            //GlobalVariables.BOT_STARTED = true;
            //BotMain.BotStart();
        }

        private void TextTest_Click(object sender, RoutedEventArgs e)
        {

            //ImageToText.GymBattleCheck();
        }
    }
}
