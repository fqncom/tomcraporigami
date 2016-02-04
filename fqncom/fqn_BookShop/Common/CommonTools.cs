using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MyBookShop.Common
{
    public class CommonTools
    {
        /// <summary>
        /// 根据输入的值计算md5值，并返回MD5值
        /// </summary>
        /// <param name="pwd">输入的要加密的字符串</param>
        /// <returns>返回Md5加密之后的数据</returns>
        public static string GetMd5String(string pwd)
        {
            string result = "";
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(pwd);
            byte[] bts = md5.ComputeHash(buffer);
            foreach (byte bt in bts)
            {
                result += bt.ToString("x2");
            }
            md5.Clear();//清一下md5
            return result;
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static String GetStreamMD5(Stream stream)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;
            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            arrbytHashValue = oMD5Hasher.ComputeHash(stream); //计算指定Stream 对象的哈希值
            //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
            strHashData = System.BitConverter.ToString(arrbytHashValue);
            //替换-
            strHashData = strHashData.Replace("-", "");
            strResult = strHashData;
            return strResult;
        }

        /// <summary>
        /// 用户未登入，则跳转到登入页面
        /// </summary>
        public static void PageRedirectToLogin()
        {
            string currentUrl = HttpContext.Current.Request.Url.ToString();
            if (currentUrl.ToLower().Contains("login.aspx"))
            {
                return;
            }
            else
            {
                HttpContext.Current.Response.Redirect("/Login.aspx?ReturnUrl=" + currentUrl);
            }
        }

        /// <summary>
        /// 获取pagebar字符串
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static string GetPageBarString(int pageIndex, int pageCount)
        {
            StringBuilder sb = new StringBuilder();
            if (pageIndex > 1)
            {
                sb.Append(string.Format("<a href='Index_{0}.aspx'>上一页</a>", pageIndex - 1));
            }

            int start = pageIndex - 5 > 0 ? pageIndex - 5 : 1;
            int end = start + 9 < pageCount ? start + 9 : pageCount;

            for (int i = start; i < end; i++)
            {
                if (i == pageIndex)
                {
                    sb.Append(string.Format("&nbsp;{0}", i));
                }
                else
                {
                    sb.Append(string.Format("&nbsp;<a href='Index_{0}.aspx'>{0}</a>", i));
                }
            }

            if (pageIndex < pageCount)
            {
                sb.Append(string.Format("<a href='Index_{0}.aspx'>下一页</a>", pageIndex + 1));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 根据文件路径读取文件所有信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadFileGetAllText(string fileName)
        {
            return File.ReadAllText(HttpContext.Current.Request.MapPath(fileName));
        }

        /// <summary>
        /// 根据日期获取文件路径
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetDirectoryPath(object time)
        {
            DateTime dateTime = Convert.ToDateTime(time);
            return "/" + dateTime.Year + "/" + dateTime.Month + "/";
        }

        /// <summary>
        /// 将时间差转换成字符串形式
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static string ChangeTimeSpanToString(TimeSpan ts)
        {
            if (ts.TotalDays > 365)
            {
                return Math.Floor(ts.TotalDays / 365) + "年前";
            }
            if (ts.TotalDays > 30)
            {
                return Math.Floor(ts.TotalDays / 30) + "月前";
            }
            if (ts.TotalHours > 24)
            {
                return Math.Floor(ts.TotalHours / 24) + "天前";
            }
            if (ts.TotalMinutes > 60)
            {
                return Math.Floor(ts.TotalMinutes / 60) + "小时前";
            }
            if (ts.TotalSeconds > 60)
            {
                return Math.Floor(ts.TotalSeconds / 60) + "分钟前";
            }
            else
            {
                return "刚刚";
            }
        }

        //将UBB标签语言转换成HTML代码语言
        public static string UBBDecode(string argString)
        {
            string tString = argString;
            if (tString != "")
            {
                Regex tRegex;
                bool tState = true;
                tString = tString.Replace("&", "&amp;");
                tString = tString.Replace(">", "&gt;");
                tString = tString.Replace("<", "&lt;");
                tString = tString.Replace("\"", "&quot;");
                tString = Regex.Replace(tString, @"\[br\]", "<br />", RegexOptions.IgnoreCase);
                string[,] tRegexAry = {
          {@"\[p\]([^\[]*?)\[\/p\]", "$1<br />"},
          {@"\[b\]([^\[]*?)\[\/b\]", "<b>$1</b>"},
          {@"\[i\]([^\[]*?)\[\/i\]", "<i>$1</i>"},
          {@"\[u\]([^\[]*?)\[\/u\]", "<u>$1</u>"},
          {@"\[ol\]([^\[]*?)\[\/ol\]", "<ol>$1</ol>"},
          {@"\[ul\]([^\[]*?)\[\/ul\]", "<ul>$1</ul>"},
          {@"\[li\]([^\[]*?)\[\/li\]", "<li>$1</li>"},
          {@"\[code\]([^\[]*?)\[\/code\]", "<div class=\"ubb_code\">$1</div>"},
          {@"\[quote\]([^\[]*?)\[\/quote\]", "<div class=\"ubb_quote\">$1</div>"},
          {@"\[color=([^\]]*)\]([^\[]*?)\[\/color\]", "<font style=\"color: $1\">$2</font>"},
          {@"\[hilitecolor=([^\]]*)\]([^\[]*?)\[\/hilitecolor\]", "<font style=\"background-color: $1\">$2</font>"},
          {@"\[align=([^\]]*)\]([^\[]*?)\[\/align\]", "<div style=\"text-align: $1\">$2</div>"},
          {@"\[url=([^\]]*)\]([^\[]*?)\[\/url\]", "<a href=\"$1\">$2</a>"},
          {@"\[img\]([^\[]*?)\[\/img\]", "<img src=\"$1\" />"}
        };
                while (tState)
                {
                    tState = false;
                    for (int ti = 0; ti < tRegexAry.GetLength(0); ti++)
                    {
                        tRegex = new Regex(tRegexAry[ti, 0], RegexOptions.IgnoreCase);
                        if (tRegex.Match(tString).Success)
                        {
                            tState = true;
                            tString = Regex.Replace(tString, tRegexAry[ti, 0], tRegexAry[ti, 1], RegexOptions.IgnoreCase);
                        }
                    }
                }
            }
            return tString;
        }

        /// <summary>
        /// 检查用户是否登入
        /// </summary>
        public static Model.Users CheckIsLoginOrNot()
        {
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                return HttpContext.Current.Session["userInfo"] as Model.Users;
            }
            return null;
        }


    }
}
