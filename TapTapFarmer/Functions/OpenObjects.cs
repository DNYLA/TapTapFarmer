using TapTapFarmer.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapTapFarmer.Functions
{
    class OpenObjects
    {
        public static void OpenCity()
        {
            WindowCapture.CaptureApplication(GlobalVariables.GLOBAL_PROC_NAME);
            for (int i = 0; i < 5; i++)
            {
                MouseHandler.MoveCursor(LocationConstants.HOME_BOTTOM_CITY, true);
            }

            MouseHandler.MoveCursor(LocationConstants.CITY_CURSOR_SCROLL);
            MouseHandler.ResetCity();
            Main.Sleep(2);
        }

        public static void TopOfCity()
        {
            for (int i = 0; i < 5; i++)
            {
                MouseHandler.MouseWheelUp();
                Main.Sleep(1);
            }
        }

        public static void OpenItemCenter()
        {
            OpenCity();

            if (PixelChecker.CheckPixelValue(LocationConstants.CITY_ITEMCENTER_BUTTON, ColorConstants.CITY_ITEMCENTER_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.CITY_ITEMCENTER_BUTTON, true);
            }
            {
                //TODO: Max 3 retries before giving up || calling a different function
                //OpenItemCenter();
            }
        }

        public static void OpenShop()
        {
            OpenCity();

            if (PixelChecker.CheckPixelValue(LocationConstants.CITY_SHOP_BUTTON, ColorConstants.CITY_SHOP_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.CITY_SHOP_BUTTON, true);
            }
            {
                //TODO: Max 3 retries before giving up || calling a different function
                //OpenItemCenter();
            }
        }

        public static void OpenPM()
        {
            OpenCity();

            if (PixelChecker.CheckPixelValue(LocationConstants.CITY_PMGARDEN_BUTTON, ColorConstants.CITY_PMGARDEN_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.CITY_PMGARDEN_BUTTON, true);
            }
            {
                //TODO: Max 3 retries before giving up || calling a different function
                //OpenItemCenter();
            }
        }

        public static void OpenLinkTrade()
        {
            OpenCity();

            if (PixelChecker.CheckPixelValue(LocationConstants.CITY_LINKTRADE_BUTTON, ColorConstants.CITY_LINKTRADE_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.CITY_LINKTRADE_BUTTON, true);
            }
            {
                //TODO: Max 3 retries before giving up || calling a different function
                //OpenItemCenter();
            }
        }

        public static void OpenGameCorner()
        {
            OpenCity();

            TopOfCity();


            if (PixelChecker.CheckPixelValue(LocationConstants.CITY_GAMECORNER_BUTTON, ColorConstants.CITY_GAMECORNER_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.CITY_GAMECORNER_BUTTON, true);
            }
            {
                //TODO: Max 3 retries before giving up || calling a different function
                //OpenItemCenter();
            }
        }

        public static void OpenDispatch()
        {
            OpenCity();

            TopOfCity();

            if (PixelChecker.CheckPixelValue(LocationConstants.CITY_DISPATCH_BUTTON, ColorConstants.CITY_DISPATCH_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.CITY_DISPATCH_BUTTON, true);
            }
            {
                //TODO: Max 3 retries before giving up || calling a different function
                //OpenItemCenter();
            }
        }

        public static void OpenSafariZone()
        {
            OpenCity();
            TopOfCity();

            if (PixelChecker.CheckPixelValue(LocationConstants.CITY_SAFARIZONE_BUTTON, ColorConstants.CITY_SAFARIZONE_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.CITY_SAFARIZONE_BUTTON, true);
            }
            {
                //TODO: Max 3 retries before giving up || calling a different function
                //OpenItemCenter();
            }
        }

        public static void OpenBattleLeague()
        {
            OpenCity();
            TopOfCity();

            if (PixelChecker.CheckPixelValue(LocationConstants.CITY_BATTLELEAGUE_BUTTON, ColorConstants.CITY_BATTLELEAGUE_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.CITY_BATTLELEAGUE_BUTTON, true);
            }
            {
                //TODO: Max 3 retries before giving up || calling a different function
                //OpenItemCenter();
            }
        }

        public static void OpenSkyPilar()
        {
            OpenCity();
            TopOfCity();

            if (PixelChecker.CheckPixelValue(LocationConstants.CITY_SKYPILAR_BUTTON, ColorConstants.CITY_SKYPILAR_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.CITY_SKYPILAR_BUTTON, true);
            }
            {
                //TODO: Max 3 retries before giving up || calling a different function
                //OpenItemCenter();
            }
        }

        public static void OpenBattleSubway()
        {
            OpenCity();
            TopOfCity();

            if (PixelChecker.CheckPixelValue(LocationConstants.CITY_BATTLESUBWAY_BUTTON, ColorConstants.CITY_BATTLESUBWAY_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.CITY_BATTLESUBWAY_BUTTON, true);
            }
            {
                //TODO: Max 3 retries before giving up || calling a different function
                //OpenItemCenter();
            }
        }

        public static void OpenGym()
        {
            OpenCity();
            TopOfCity();

            if (PixelChecker.CheckPixelValue(LocationConstants.CITY_GYM_BUTTON, ColorConstants.CITY_GYM_COLOR))
            {
                MouseHandler.MoveCursor(LocationConstants.CITY_GYM_BUTTON, true);
            }
            {
                //TODO: Max 3 retries before giving up || calling a different function
                //OpenItemCenter();
            }
        }

    }
}
