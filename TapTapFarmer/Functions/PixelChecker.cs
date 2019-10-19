using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TapTapFarmer.Functions
{
    class PixelChecker
    {

        /// <summary>
        /// Checks if a Pixel value in the process is the same as paramter passed through
        /// </summary>
        /// <param name="LocationToCheck"> Location of the pixel to check against </param>
        /// <param name="ColourToCheck"> Colour of the pixel to check against </param>
        /// <param name="ClickPixel"> If true pixel will be left clicked </param>
        /// <returns> True/False Depending on weather the pixel value matched.</returns>
        public static Boolean CheckPixelValue(Point LocationToCheck, Color ColourToCheck, Boolean ClickPixel = false)
        {
            Bitmap bmp = WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);
            Point ProcessLocation = WindowCapture.GetProcessPosition(GlobalVariables.GLOBAL_PROC_NAME);
            Point PixelScreenLocation = new Point(LocationToCheck.X + ProcessLocation.X, LocationToCheck.Y + ProcessLocation.Y);

            if (bmp.GetPixel(LocationToCheck.X, LocationToCheck.Y) == ColourToCheck)
            {
                if (ClickPixel)
                {
                    MouseHandler.MoveCursor(PixelScreenLocation);
                    MouseHandler.MouseLeftClick();
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ColorToSearch"></param>
        /// <param name="Location"></param>
        /// <returns></returns>
        public static Boolean SearchPixel(Color ColorToSearch, out Point Location)
        {
            //Takes Screencap of NOX
            Bitmap ScreenCap = WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);

            //ScreenCap.Save("ScreenTest.bmp");

            Point ProcessLocation = WindowCapture.GetProcessPosition(GlobalVariables.GLOBAL_PROC_NAME);

            Location = new Point(0, 0);

            //Loops Through Each Pixel
            for (int x = 0; x < ScreenCap.Width; x++)
            {
                for (int y = 0; y < ScreenCap.Height; y++)
                {
                    Color currentPixelColor = ScreenCap.GetPixel(x, y);

                    if (ColorToSearch == currentPixelColor)
                    {
                        Location = new Point(x, y);
                        return true;
                    }
                }
            }

            return false;
        }

        public static Bitmap ReSizeImage(Point Location, Size SizeOfRec)
        {
            Rectangle section = new Rectangle(Location, SizeOfRec);

            Bitmap ScreenCap = WindowCapture.CaptureApplication("Nox");
            Bitmap ExtractedPart = new Bitmap(section.Width, section.Height);

            Graphics G = Graphics.FromImage(ExtractedPart);

            G.DrawImage(ScreenCap, 0, 0, section, GraphicsUnit.Pixel);
            ExtractedPart.Save("Resized.png");

            return ExtractedPart;
        }

        /// <summary>
        /// Takes a screencap of process and gets the pixel value of location
        /// </summary>
        /// <param name="PixelLocation"> Pixel X & Y that you want the value of </param>
        /// <returns> returns colour of pixel </returns>
        public static Color GetPixelColor(Point PixelLocation)
        {
            Bitmap bmp = WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);
            Color PixelValue = bmp.GetPixel(PixelLocation.X, PixelLocation.Y);

            return PixelValue;
        }

    }
}
