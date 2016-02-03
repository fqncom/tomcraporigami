using System;
using System.Collections.Generic;
using System.Text;
using Windows.Globalization;

namespace TickTick.Models
{
    public class TickTickDuration
    {
        public static readonly String ON_TIME = "TRIGGER:PT0S";

        private bool isPositive = false;
        private int years = 0;
        private int months = 0;
        private int weeks = 0;
        private int days = 0;
        private int hours = 0;
        private int minutes = 0;
        private int seconds = 0;

        private int GetValue(int number)
        {
            if (number == null)
            {
                return 0;
            }
            return number;
        }
        public void AddDurationToDate(Calendar calendar)
        {
            if (isPositive)
            {
                calendar.Year += GetValue(years);//.set(Calendar.YEAR, calendar.get(Calendar.YEAR) + GetValue(years));
                calendar.Month += GetValue(months);//.set(Calendar.MONTH, calendar.get(Calendar.MONTH) + getValue(months));
                calendar.Day += GetValue(days);//.set(Calendar.WEEK_OF_YEAR,calendar.get(Calendar.WEEK_OF_YEAR) + getValue(weeks));
                //calendar.set(Calendar.DAY_OF_MONTH,
                //        calendar.get(Calendar.DAY_OF_MONTH) + getValue(days));
                calendar.Hour += GetValue(hours);//.set(Calendar.HOUR_OF_DAY,calendar.get(Calendar.HOUR_OF_DAY) + getValue(hours));
                calendar.Minute += GetValue(minutes);//.set(Calendar.MINUTE, calendar.get(Calendar.MINUTE) + getValue(minutes));
                calendar.Second += GetValue(seconds);//.set(Calendar.SECOND, calendar.get(Calendar.SECOND) + getValue(seconds));
            }
            else
            {
                calendar.Year -= GetValue(years);//.set(Calendar.YEAR, calendar.get(Calendar.YEAR) + GetValue(years));
                calendar.Month -= GetValue(months);//.set(Calendar.MONTH, calendar.get(Calendar.MONTH) + getValue(months));
                calendar.Day -= GetValue(days);//.set(Calendar.WEEK_OF_YEAR,calendar.get(Calendar.WEEK_OF_YEAR) + getValue(weeks));
                //calendar.set(Calendar.DAY_OF_MONTH,
                //        calendar.get(Calendar.DAY_OF_MONTH) + getValue(days));
                calendar.Hour -= GetValue(hours);//.set(Calendar.HOUR_OF_DAY,calendar.get(Calendar.HOUR_OF_DAY) + getValue(hours));
                calendar.Minute -= GetValue(minutes);//.set(Calendar.MINUTE, calendar.get(Calendar.MINUTE) + getValue(minutes));
                calendar.Second -= GetValue(seconds);//.set(Calendar.SECOND, calendar.get(Calendar.SECOND) + getValue(seconds));
            }
        }
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
        public TickTickDuration(String lexicalRepresentation)
        {

            String s = lexicalRepresentation.Replace("TRIGGER:", "");
            bool positive;
            int[] idx = new int[1];
            int length = s.Length;
            bool timeRequired = false;

            idx[0] = 0;

            if (length != idx[0] && s[idx[0]] == '-')
            {
                idx[0]++;
                positive = false;
            }
            else
            {
                positive = true;
            }

            if (length != idx[0] && s[idx[0]++] != 'P')
            {
                //throw new IllegalArgumentException(s);
            }

            int dateLen = 0;
            String[] dateParts = new String[4];
            int[] datePartsIndex = new int[4];
            while (length != idx[0] && char.IsDigit(s[idx[0]]) && dateLen < 4)
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
                    //throw new IllegalArgumentException(s);
                }
            }

            int timeLen = 0;
            String[] timeParts = new String[3];
            int[] timePartsIndex = new int[3];
            while (length != idx[0] && IsDigitOrPeriod(s[idx[0]])
                    && timeLen < 3)
            {
                timePartsIndex[timeLen] = idx[0];
                timeParts[timeLen++] = ParsePiece(s, idx);
            }

            if (timeRequired && timeLen == 0)
            {
                //throw new IllegalArgumentException(s);
            }

            if (length != idx[0])
            {
                //throw new IllegalArgumentException(s);
            }
            if (dateLen == 0 && timeLen == 0)
            {
                //throw new IllegalArgumentException(s);
            }

            OrganizeParts(s, dateParts, datePartsIndex, dateLen, "YMWD");
            OrganizeParts(s, timeParts, timePartsIndex, timeLen, "HMS");

            // parse into numbers
            years = ParseInteger(dateParts[0]);
            months = ParseInteger(dateParts[1]);
            weeks = ParseInteger(dateParts[2]);
            days = ParseInteger(dateParts[3]);
            hours = ParseInteger(timeParts[0]);
            minutes = ParseInteger(timeParts[1]);
            seconds = ParseInteger(timeParts[2]);
            isPositive = positive;
        }
        private static void OrganizeParts(String whole, String[] parts, int[] partsIndex, int len, String tokens)
        {

            int idx = tokens.Length;
            for (int i = len - 1; i >= 0; i--)
            {
                int nidx =
                        tokens.LastIndexOf(
                                parts[i][parts[i].Length - 1], idx - 1);
                if (nidx == -1)
                {
                    //throw new IllegalArgumentException(whole);
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
        public long ToMillis()
        {
            long toMillis = 0;
            if (HasValue(years))
            {
                toMillis += years * DateTimeUtils.YEAR_IN_MILLIS;
            }
            if (HasValue(months))
            {
                toMillis += months * DateTimeUtils.DAY_IN_MILLIS * 30;
            }
            if (HasValue(weeks))
            {
                toMillis += weeks * DateTimeUtils.WEEK_IN_MILLIS;
            }
            if (HasValue(days))
            {
                toMillis += days * DateTimeUtils.DAY_IN_MILLIS;
            }
            if (HasValue(hours))
            {
                toMillis += hours * DateTimeUtils.HOUR_IN_MILLIS;
            }
            if (HasValue(minutes))
            {
                toMillis += minutes * DateTimeUtils.MINUTE_IN_MILLIS;
            }
            if (HasValue(seconds))
            {
                toMillis += seconds * DateTimeUtils.SECOND_IN_MILLIS;
            }
            return toMillis;
        }
        private bool HasValue(int value)
        {
            return value != 0;
        }
        public bool EqualValue(TickTickDuration duration)
        {
            return duration != null && ToMillis() == duration.ToMillis();
        }
    }
}
