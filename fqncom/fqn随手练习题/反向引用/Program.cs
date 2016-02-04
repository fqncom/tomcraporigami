using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 反向引用
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 反向引用 其中“\1” 实现了正则表达式内部反向引用，而“$1”实现了正则表达式外部引用,只有内部引用才叫反向引用

            //string str = "杨杨杨杨杨杨杨中中中中中中中科科科科科科科科科科科";
            ////@"(.)\1+"   => 表示的是匹配到任意一个字符，然后引用第一组的内容。即找到和匹配到的任意字符之后紧接着的一致的字符，组成一个字符串。
            ////即如果像下面这么写，那么最后得到的是和第一个字符匹配的之后的相同的字符组成的字符串“杨杨杨杨杨杨杨”；
            ////Match result = Regex.Match(str, regex);
            //string regex = @"(.)\1+";
            ////以下的$1表示前面匹配到的字符串，即“杨杨杨杨杨杨杨”，替换成该字符串的第一个字符
            //string result = Regex.Replace(str, regex,"$1");
            //Console.WriteLine(result); 

            #endregion

            #region 反向引用 查找单词字典中有叠字的单词

            //string text = File.ReadAllText("英汉词典TXT格式.txt",Encoding.Default);
            //string regex = @"[a-zA-Z]*([a-zA-Z])\1+[a-zA-Z]*";
            //MatchCollection matches = Regex.Matches(text, regex);
            //foreach (Match match in matches)
            //{
            //    Console.WriteLine(match);
            //}
            #endregion

            Console.ReadKey();
        }
    }
}
