using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TapTapFarmer
{
    class WindowCapture
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hWnd);

        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        /// <summary>
        /// Generates an Image from a specific window
        /// </summary>
        /// <param name="procName">Proccess/Window Name of Application you want to screenshot</param>
        /// <returns> A Bitmap image of a window is returned </returns>
        public static Bitmap CaptureApplication(string procName)
        {
            Process proc;
            // Cater for cases when the process can't be located.
            try
            {
                proc = Process.GetProcessesByName(procName)[0];
            }
            catch (IndexOutOfRangeException e)
            {
                return null;
            }

            // You need to focus on the application
            SetForegroundWindow(proc.MainWindowHandle);
            ShowWindow(proc.MainWindowHandle, SW_RESTORE);
            // You need some amount of delay, but 1 second may be overkill
            Thread.Sleep(1000);

            Rect rect = new Rect();
            IntPtr error = GetWindowRect(proc.MainWindowHandle, ref rect);

            // sometimes it gives error.
            while (error == (IntPtr)0)
            {
                error = GetWindowRect(proc.MainWindowHandle, ref rect);
            }

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Graphics.FromImage(bmp).CopyFromScreen(rect.left,
                                                   rect.top,
                                                   0,
                                                   0,
                                                   new Size(width, height),
                                                   CopyPixelOperation.SourceCopy);




            return ReSizeImage(bmp);
        }

        /// <summary>
        /// Gets the start co-ordinates of a process
        /// </summary>
        /// <param name="ProcName"> Name of process you are searching for </param>
        /// <returns> The 0,0 Position of the window is returned </returns>
        public static Point GetProcessPosition(string ProcName)
        {
            Process Proc;

            try
            {
                Proc = Process.GetProcessesByName(ProcName)[0];
            }
            catch (IndexOutOfRangeException e)
            {
                return Point.Empty; //Invalid Process Name // Process Not Open
            }

            Rect rect = new Rect();
            IntPtr error = GetWindowRect(Proc.MainWindowHandle, ref rect);

            // sometimes it gives error.
            while (error == (IntPtr)0)
            {
                error = GetWindowRect(Proc.MainWindowHandle, ref rect);
            }



            Point Location = new Point(rect.left, rect.top);

            return Location;
        }

        public static Size GetProcessSize(string ProcName)
        {
            Process Proc;

            try
            {
                Proc = Process.GetProcessesByName(ProcName)[0];
            }
            catch (IndexOutOfRangeException e)
            {
                return Size.Empty; //Invalid Process Name // Process Not Open
            }

            // You need to focus on the application
            SetForegroundWindow(Proc.MainWindowHandle);
            ShowWindow(Proc.MainWindowHandle, SW_RESTORE);
            // You need some amount of delay, but 1 second may be overkill
            Thread.Sleep(1000);

            Rect rect = new Rect();
            IntPtr error = GetWindowRect(Proc.MainWindowHandle, ref rect);

            // sometimes it gives error.
            while (error == (IntPtr)0)
            {
                error = GetWindowRect(Proc.MainWindowHandle, ref rect);
            }

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            return new Size(width, height);
        }

        //
        public static string GetProccessName()
        {
            if (Process.GetProcessesByName("Nox").Length == 1)
            {
                return "Nox";
            }
            else if (Process.GetProcessesByName("MEmu").Length == 1)
            {
                return "MEmu";
            }
            else
            {
                return "InvalidProc";
            }
        }

        public static Bitmap ReSizeImage(Bitmap ImageToReSize)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(540, 960);
            Rectangle section = new Rectangle(new Point(0, 0), new Size(540, 960));
            switch (GlobalVariables.GLOBAL_PROC_NAME)
            {
                case "Nox":
                    section = new Rectangle(new Point(2, 32), new Size(540, 960));
                    break;
                case "MEmu":
                    section = new Rectangle(new Point(2, 32), new Size(540, 960));
                    break;
            }

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(ImageToReSize, 0, 0, section, GraphicsUnit.Pixel);
            // MessageBox.Show($"Text Height: {bmp.Width}x{bmp.Height}");
            return bmp;
        }
    }
}
