using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TapTapFarmer.Functions.Authentication
{
    class LoginHandler
    {

        public static bool CheckLogin(string username, string password)
        {
            Console.WriteLine(username + " " + password);
            string resp = string.Empty;

            resp = AuthenticateAccount(username, password);

            Console.WriteLine("Checking Login");
            if (resp == "Subscription Active")
            {
                Console.WriteLine("Successfully Logedi n....");
                //MessageBox.Show("Subscription Active. Enablind Bot...");
                return true;
            }
            else if (resp == "Subscription Ended")
            {
                //Thread LFThread = new Thread(() =>
                //{
                //    MessageBox.Show("Subscription Ended. Shutting Down in 30 seconds...");
                //});

                //LFThread.Start();
                return false;
            }
            else if (resp == "Login failed")
            {
                //Thread LFThread = new Thread(() =>
                //{
                //    MessageBox.Show("Incorrect Username Or Password. Shutting Down...");
                //});

                //LFThread.Start();
                return false;
            }
            else
            {
                //Thread LFThread = new Thread(() =>
                //{
                //    MessageBox.Show("UnExpected Error! Shutting Down...");
                //});

                //LFThread.Start();
                return false;
            }
        }

        public static string AuthenticateAccount(string username, string password)
        {

            string url = "https://TapTapFarmer.com/c-login/login.php";
            string pagesource = string.Empty;

            using (WebClient client = new WebClient())
            {
                try
                {
                    NameValueCollection postData = new NameValueCollection()
                {
                    { "username", username },  //order: {"parameter name", "parameter value"}
	                { "password", password }
                };

                    // client.UploadValues returns page's source as byte array (byte[])
                    // so it must be transformed into a string
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(url, postData));
                    Main.LogConsole(pagesource);
                }
                catch(Exception)
                {
                    Main.LogConsole("Unable To Connect to Server...");
                }
            }

            return pagesource;
        }


        public static bool CheckHWID(string user)
        {
            string sHW = GetHWID(user);
            string cHW = FingerPrint.Value();
            if (string.IsNullOrEmpty(sHW))
            {
                SetHWID(user);
                CheckHWID(user);
            }
            else
            {
                
                if (sHW == cHW)
                {
                    Console.WriteLine(sHW);
                    Console.WriteLine(cHW);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private static string SetHWID(string username)
        {
            string url = "https://TapTapFarmer.com/hwid_check.php?method=1";
            string pagesource = string.Empty;

            Console.WriteLine("Settings HWID");

            string HWID = FingerPrint.Value();

            using (WebClient client = new WebClient())
            {
                try
                {
                    NameValueCollection postData = new NameValueCollection()
                {
                    { "username", username },  //order: {"parameter name", "parameter value"}
                    { "hwid", HWID }
                };

                    // client.UploadValues returns page's source as byte array (byte[])
                    // so it must be transformed into a string
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(url, postData));
                    //Main.LogConsole(pagesource);
                }
                catch (Exception)
                {
                    Main.LogConsole("Unable To Connect to Server...");
                }
            }

            return pagesource;
        }

        public static string GetHWID(string username)
        {
            string url = "https://TapTapFarmer.com/hwid_check.php?method=2";
            string pagesource = string.Empty;
            string HWID = string.Empty;

            using (WebClient client = new WebClient())
            {
                try
                {
                    NameValueCollection postData = new NameValueCollection()
                {
                    { "username", username },  //order: {"parameter name", "parameter value"}
                    { "hwid", HWID }
                };

                    // client.UploadValues returns page's source as byte array (byte[])
                    // so it must be transformed into a string
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(url, postData));
                    //Main.LogConsole(pagesource);
                }
                catch (Exception)
                {
                    Main.LogConsole("Unable To Connect to Server...");
                }
            }

            return pagesource;
        }

        public static string CheckUpdate()
        {
            string url = "https://TapTapFarmer.com/update_check.php";
            string pagesource = string.Empty;
            string cv = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            cv = "1.5";
            using (WebClient client = new WebClient())
            {
                try
                {
                    NameValueCollection postData = new NameValueCollection()
                {
                    { "cv", cv },  //order: {"parameter name", "parameter value"}
                };

                    // client.UploadValues returns page's source as byte array (byte[])
                    // so it must be transformed into a string
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(url, postData));
                    //Main.LogConsole(pagesource);
                }
                catch (Exception)
                {
                    Main.LogConsole("Unable To Connect to Server...");
                }
            }

            return pagesource;
        }
    }
}
