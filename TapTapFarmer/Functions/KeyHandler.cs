using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TapTapFarmer.Functions
{
    public class KeyHandler
    {
        private static IKeyboardMouseEvents m_Events;

        public static void StartKeyHandler()
        {
            Subscribe(Hook.GlobalEvents());
        }

        private static void Unsubscribe()
        {
            if (m_Events == null) return;
            m_Events.KeyDown -= OnKeyDown;
            m_Events.KeyUp -= OnKeyUp;
            m_Events.KeyPress -= HookManager_KeyPress;

            m_Events.Dispose();
            m_Events = null;
        }

        private static void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.KeyDown += OnKeyDown;
            m_Events.KeyUp += OnKeyUp;
            m_Events.KeyPress += HookManager_KeyPress;
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            //Console.WriteLine(string.Format("KeyDown  \t\t {0}\n", e.KeyCode));

        }

        private static void OnKeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(string.Format("KeyUp  \t\t {0}\n", e.KeyCode));
            if (e.KeyCode == Keys.F5 && GlobalVariables.BOT_STARTED == true)
            {
                BotMain.StopBot();
                //Unsubscribe();
            }
        }

        private static void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(string.Format("KeyPress \t\t {0}\n", e.KeyChar));
        }
    }
}
