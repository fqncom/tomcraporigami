using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Helper
{
    class ResolutionHelper
    {
        public enum Resolutions { WVGA, WXGA, HD };

        //public static class ResolutionHelper
        //{
        //    private static bool IsWvga
        //    {
        //        get
        //        {
        //            return App.Current.Host.Content.ScaleFactor == 100;
        //        }
        //    }

        //    private static bool IsWxga
        //    {
        //        get
        //        {
        //            return App.Current.Host.Content.ScaleFactor == 160;
        //        }
        //    }

        //    private static bool IsHD
        //    {
        //        get
        //        {
        //            return App.Current.Host.Content.ScaleFactor == 150;
        //        }
        //    }

        //    public static Resolutions CurrentResolution
        //    {
        //        get
        //        {
        //            if (IsWvga) return Resolutions.WVGA;
        //            else if (IsWxga) return Resolutions.WXGA;
        //            else if (IsHD) return Resolutions.HD;
        //            else throw new InvalidOperationException("Unknown resolution");
        //        }
        //    }
        //}
    }
}
