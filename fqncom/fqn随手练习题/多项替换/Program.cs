using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 多项替换
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 多项替换,将多个“-”替换成单个“-”

            //string str = "123------45----56---78-9";
            //string result = Regex.Replace(str, "-+", "-");
            //Console.WriteLine(result);

            #endregion

            #region 多项日期替换，将日期格式由“10/06/1992”转换成“1992-10-06”格式

            //string str = "我的生日是：10/06/1992，哈哈，今天是我生日。我的生日是：10/06/1992，哈哈，今天是我生日。我的生日是：10/06/1992，哈哈，今天是我生日。我的生日是：10/06/1992，哈哈，今天是我生日。";

            //string result = Regex.Replace(str, @"(\d{2})/(\d{2})/(\d{4})", "$3-$1-$2");
            //Console.WriteLine(result);

            #endregion


            Console.ReadKey();
        }
    }
}
