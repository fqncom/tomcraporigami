using System;
using System.Collections.Generic;
using System.Text;
using Windows.Globalization;

namespace TickTick.Utilities
{
    public class ReminderDurationParser
    {
        private static String ParsePiece(String whole, int[] idx) //throws IllegalArgumentException 
        {
            int start = idx[0];
            var length = 0;
            while (idx[0] < whole.Length && IsDigitOrPeriod(whole[(idx[0])]))
            {
                idx[0]++;
                length++;
            }
            if (idx[0] == whole.Length)
            {
                //throw new IllegalArgumentException(whole); // ,idx[0]);
            }

            idx[0]++;
            length++;

            //return whole.Substring(start, idx[0]);
            return whole.Substring(start, length);
        }
        private static bool IsDigitOrPeriod(char ch)
        {
            return char.IsDigit(ch) || ch == '.';
        }
        private static int ParseInteger(String part)
        //throws IllegalArgumentException 
        {

            if (part == null)
            {
                return 0;
            }
            part = part.Substring(0, part.Length - 1);
            return Int32.Parse(part);
            // TODO 用以上内容替代
            //return Integer.valueOf(part);
        }
        public static DateTime DurationAddToDate(String lexicalRepresentation, DateTime date)
        {
            if (date == null)
            {
                //throw new NullPointerException();
                throw new NullReferenceException();
            }

            Calendar calendar = new Calendar();
            // TODO 此处有问题
            calendar.SetDateTime(date);
            //calendar.Second =date.mi
            if (lexicalRepresentation == null)
            {
                throw new NullReferenceException();
            }
            String s = lexicalRepresentation;
            bool positive;
            int[] idx = new int[1];
            int length = s.Length;
            bool timeRequired = false;

            idx[0] = 0;
            if (length != idx[0] && s[(idx[0])] == '-')
            {
                idx[0]++;
                positive = false;
            }
            else
            {
                positive = true;
            }

            if (length != idx[0] && s[(idx[0]++)] != 'P')
            {
                //throw new ArgumentException(s); // ,idx[0]-1);
            }

            // phase 1: chop the string into chunks
            // (where a chunk is '<number><a symbol>'
            // --------------------------------------
            int dateLen = 0;
            String[] dateParts = new String[3];
            int[] datePartsIndex = new int[3];
            while (length != idx[0] && char.IsDigit(s[idx[0]]) && dateLen < 3)
            {
                datePartsIndex[dateLen] = idx[0];
                dateParts[dateLen++] = ParsePiece(s, idx);
            }

            if (length != idx[0])
            {
                if (s[idx[0]++] == 'T')
                {
                    timeRequired = true;
                }
                else
                {
                    //throw new IllegalArgumentException(s); // ,idx[0]-1);
                }
            }

            int timeLen = 0;
            String[] timeParts = new String[3];
            int[] timePartsIndex = new int[3];
            while (length != idx[0] && IsDigitOrPeriod(s[idx[0]]) && timeLen < 3)
            {
                timePartsIndex[timeLen] = idx[0];
                timeParts[timeLen++] = ParsePiece(s, idx);
            }

            if (timeRequired && timeLen == 0)
            {
                //throw new IllegalArgumentException(s); // ,idx[0]);
            }

            if (length != idx[0])
            {
                //throw new IllegalArgumentException(s); // ,idx[0]);
            }
            if (dateLen == 0 && timeLen == 0)
            {
                //throw new IllegalArgumentException(s); // ,idx[0]);
            }

            OrganizeParts(s, dateParts, datePartsIndex, dateLen, "YMD");
            OrganizeParts(s, timeParts, timePartsIndex, timeLen, "HMS");

            int year = ParseInteger(dateParts[0]);
            int months = ParseInteger(dateParts[1]);
            int days = ParseInteger(dateParts[2]);
            int hours = ParseInteger(timeParts[0]);
            int minutes = ParseInteger(timeParts[1]);
            int seconds = ParseInteger(timeParts[2]);

            if (positive)
            {
                calendar.Year += year;
                calendar.Month += months;
                calendar.Day += days;
                calendar.Hour += hours;
                calendar.Minute += minutes;
                calendar.Second += seconds;
                //calendar.set(Calendar.YEAR, calendar.get(Calendar.YEAR) + year);
                //calendar.set(Calendar.MONTH, calendar.get(Calendar.MONTH) + months);
                //calendar.set(Calendar.DAY_OF_MONTH, calendar.get(Calendar.DAY_OF_MONTH) + days);
                //calendar.set(Calendar.HOUR_OF_DAY, calendar.get(Calendar.HOUR_OF_DAY) + hours);
                //calendar.set(Calendar.MINUTE, calendar.get(Calendar.MINUTE) + minutes);
                //calendar.set(Calendar.SECOND, calendar.get(Calendar.SECOND) + seconds);
            }
            else
            {
                calendar.Year -= year;
                calendar.Month -= months;
                calendar.Day -= days;
                calendar.Hour -= hours;
                calendar.Minute -= minutes;
                calendar.Second -= seconds;
                //calendar.set(Calendar.YEAR, calendar.get(Calendar.YEAR) - year);
                //calendar.set(Calendar.MONTH, calendar.get(Calendar.MONTH) - months);
                //calendar.set(Calendar.DAY_OF_MONTH, calendar.get(Calendar.DAY_OF_MONTH) - days);
                //calendar.set(Calendar.HOUR_OF_DAY, calendar.get(Calendar.HOUR_OF_DAY) - hours);
                //calendar.set(Calendar.MINUTE, calendar.get(Calendar.MINUTE) - minutes);
                //calendar.set(Calendar.SECOND, calendar.get(Calendar.SECOND) - seconds);
            }

            return calendar.GetDateTime().DateTime;
        }
        private static void OrganizeParts(String whole, String[] parts, int[] partsIndex, int len,String tokens) //throws IllegalArgumentException 
        {
            int idx = tokens.Length;
            for (int i = len - 1; i >= 0; i--)
            {
                if (parts[i] == null)
                {
                    //throw new IllegalArgumentException(whole);
                }
                int nidx = tokens.LastIndexOf(parts[i][parts[i].Length - 1], idx - 1);
                if (nidx == -1)
                {
                    throw new ArgumentException("数字不能是负数");
                }

                for (int j = nidx + 1; j < idx; j++)
                {
                    parts[j] = null;
                }
                idx = nidx;
                parts[idx] = parts[i];
                partsIndex[idx] = partsIndex[i];
            }
            for (idx--; idx >= 0; idx--)
            {
                parts[idx] = null;
            }
        }
    }
}
