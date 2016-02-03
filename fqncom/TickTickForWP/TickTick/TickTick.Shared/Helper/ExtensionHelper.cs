using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick
{
    public static class ExtensionHelper
    {
        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }


    }
}
