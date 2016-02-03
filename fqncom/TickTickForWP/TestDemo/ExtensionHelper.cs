using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo
{
    public static class ExtensionHelper
    {
        public static bool IsNumeric(this string s,float output)
        {
            //float output;
            return float.TryParse(s, out output);
        }


    }
}
