using TapTapFarmer.Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TapTapFarmer.Functions
{
    class MouseHandler
    {


        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            WHEEL = 0x00000800,
            XDOWN = 0x00000080,
            XUP = 0x00000100
        }
        public static void MouseLeftClick()
        {
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep((new Random().Next(20, 30)));
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }

        public static void MouseWheelUp()
        {
            mouse_event((uint)MouseEventFlags.WHEEL, 0, 0, 120, 0);
        }

        public static void MouseWheelDown()
        {
            mouse_event((uint)MouseEventFlags.WHEEL, 0, 0, -120, 0);
            Thread.Sleep(1000);
        }

        public static void MouseMoveUp()
        {
            MouseHandler.MoveCursor(LocationConstants.SCREEN_CITY_TOP, true);
            Thread.Sleep(500);
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(500);
            MouseHandler.MoveCursor(LocationConstants.SCREEN_CITY_BOTTOM, true);
            Thread.Sleep(500);
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(500);
        }

        public static void MouseMoveDown()
        {
            MouseHandler.MoveCursor(LocationConstants.SCREEN_CITY_BOTTOM, true);
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            mouse_event((uint)MouseEventFlags.MOVE, 0, -400, 0, 0);
            Thread.Sleep(100);
            mouse_event((uint)MouseEventFlags.MOVE, 0, -400, 0, 0);
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(500);
        }


        public static void MoveCursorActualPos(Point Location, bool Click = false)
        {
            Cursor.Position = Location;
            if (Click)
            {
                MouseLeftClick();
            }
        }

        public static void MoveCursor(Point Location, bool Click = false)
        {
            Point ProcLocation = WindowCapture.GetProcessPosition(GlobalVariables.GLOBAL_PROC_NAME);
            Point TempPosition = new Point(Location.X + ProcLocation.X, Location.Y + ProcLocation.Y);
            
            switch (GlobalVariables.GLOBAL_PROC_NAME)
            {
                case "Nox":
                    TempPosition = new Point(TempPosition.X + 2, TempPosition.Y + 32);
                    break;
                case "MEmu":
                    TempPosition = new Point(TempPosition.X + 2, TempPosition.Y + 32);
                    break;
            }
            Cursor.Position = TempPosition;
            if (Click)
            {
                MouseLeftClick();
            }
        }

        /// <summary>
        /// Resets Castle Screen by scrolling to the bottom
        /// </summary>
        public static void ResetCastle()
        {
            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(300);
                MouseWheelDown();
            }
        }

        //public static void MoveCursor(Point Location, Color ColourToCheck, Point ProcLocation)
        //{
        //    Bitmap BMP = WindowCapture.CaptureApplication("Nox");
        //    if (BMP.GetPixel(Location.X, Location.Y) == ColourToCheck)
        //    {
        //        Cursor.Position = new Point(Location.X + ProcLocation.X, Location.Y + ProcLocation.Y);
        //        //MouseClick();
        //    }
        //    else
        //    {
        //        Cursor.Position = new Point(Location.X + ProcLocation.X, Location.Y + ProcLocation.Y);

        //        //MessageBox.Show("Invalid Position At " + Location.ToString());
        //    }
        //}
    }
}
