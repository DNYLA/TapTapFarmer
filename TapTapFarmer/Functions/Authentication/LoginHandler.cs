using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
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
            string resp = AuthenticateAccount(username, password);

            if (resp == "Subscription Active")
            {
                MessageBox.Show("Subscription Active. Enablind Bot...");
                return true;
            }
            else if (resp == "Subscription Ended")
            {
                Thread LFThread = new Thread(() =>
                {
                    MessageBox.Show("Subscription Ended. Shutting Down in 30 seconds...");
                });

                LFThread.Start();

                Main.Sleep(30);
                Application.Exit();
                return false;
            }
            else if (resp == "Login failed")
            {
                Thread LFThread = new Thread(() =>
                {
                    MessageBox.Show("Incorrect Username Or Password. Shutting Down in 30 seconds...");
                });

                LFThread.Start();

                Main.Sleep(30);
                Application.Exit();
                return false;
            }
            else
            {
                Thread LFThread = new Thread(() =>
                {
                    MessageBox.Show("UnExpected Error. Shutting Down in 30 seconds...");
                });

                LFThread.Start();

                Main.Sleep(30);
                Application.Exit();
                return false;
            }
        }

        public static string AuthenticateAccount(string username, string password)
        {
            string url = "https://sbond.ml/p/trainerbotsite/c-login/login.php";
            string pagesource;

            using (WebClient client = new WebClient())
            {
                NameValueCollection postData = new NameValueCollection()
                {
                    { "username", username },  //order: {"parameter name", "parameter value"}
	                { "password", password + 3 }
                };

                // client.UploadValues returns page's source as byte array (byte[])
                // so it must be transformed into a string
                pagesource = Encoding.UTF8.GetString(client.UploadValues(url, postData));
                Main.LogConsole(pagesource);
            }

            return pagesource;
        }
    }
}
