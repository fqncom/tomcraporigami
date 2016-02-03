using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Utilities
{
    public static class StringUtils
    {
        public static String[] ParseCompositeNotes(String compositeNotes)
        {
            String[] parseStrs = new String[] { "", "" };
            if (compositeNotes == null || compositeNotes.Trim().Length == 0)
                return parseStrs;
            int index = compositeNotes.IndexOf("\n");
            if (index > -1)
            {
                parseStrs[0] = compositeNotes.Substring(0, index);
                parseStrs[1] = compositeNotes.Substring(index + 1);
            }
            else
            {
                parseStrs[0] = compositeNotes;
            }
            return parseStrs;
        }
        /// <summary>
        /// 生成一个24位的短Guid字符串
        /// </summary>
        /// <returns></returns>
        public static string GenerateShortStringGuid()
        {
            long i = 1;
            var guid = Guid.NewGuid().ToByteArray();
            foreach (byte b in guid)
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}fromWP", i - DateTime.Now.Ticks);// TODO 产生的字符绝对唯一，这么做只是为了好玩。。。
        }
    }
}
