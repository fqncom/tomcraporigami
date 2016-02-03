using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Utilities
{
    class TagUtils
    {
        public static HashSet<String> parseTags(String str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new HashSet<String>();
            }
            bool found = false;
            foreach (char c in str.ToCharArray())
            {
                if (c == '#' || c == '＃')
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                return new HashSet<String>();
            }

            HashSet<String> tags = new HashSet<String>();
            ////Matcher matcher = Regex.VALID_HASHTAG.matcher(str);

            ////while (matcher.find()) {
            ////    String after = str.substring(matcher.end());
            ////    if (!Regex.INVALID_HASHTAG_MATCH_END.matcher(after).find()) {
            ////        String tagStr = matcher.group(3);
            ////        if (!TextUtils.isEmpty(tagStr)) {
            ////            tags.add(tagStr.toLowerCase(Locale.getDefault()));
            ////        }
            ////        if (tags.size() >= 5) {
            ////            // 最多5个tag
            ////            return tags;
            ////        }
            ////    }
            //}
            return tags;
        }
    }
}
