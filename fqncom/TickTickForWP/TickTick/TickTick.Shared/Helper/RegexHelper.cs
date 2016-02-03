using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TickTick.Helper
{
    public static class RegexHelper
    {
        /// <summary>
        /// 邮箱验证
        /// </summary>
        public static readonly string EmailPattern = @"\w+((-w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+";

        /// <summary>
        /// 判断是否满足email的格式
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsMatchEmail(string email)
        {
            return IsMatch(email,EmailPattern);
        }

        #region 验证输入字符串是否与模式字符串匹配
        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>        
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件</param>
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }
        #endregion
    }
}
