using DDay.iCal;
using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Models;
using TickTick.Utilities;
using Windows.Globalization;

namespace System
{
    public static class DateTimeUtils
    {
        public static readonly long SECOND_IN_MILLIS = 1000;
        public static readonly long MINUTE_IN_MILLIS = SECOND_IN_MILLIS * 60;
        public static readonly long HOUR_IN_MILLIS = MINUTE_IN_MILLIS * 60;
        public static readonly long DAY_IN_MILLIS = HOUR_IN_MILLIS * 24;
        public static readonly long WEEK_IN_MILLIS = DAY_IN_MILLIS * 7;

        public static readonly long YEAR_IN_MILLIS = WEEK_IN_MILLIS * 52;



        private static readonly String TRIGGER_TAG = "TRIGGER:";

        /// <summary>
        /// 将时间转换为完全的milliseconds
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetAllMilliSeconds(this DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
        }
        public static DateTime GetDateTimeByMilliSeconds(this long milliSeconds)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(milliSeconds);
        }
        public static string ToStringDateMDY(this DateTime dateTime)
        {
            return dateTime.ToString("MM/dd/yyyy");
        }
        public static string ToStringDateTimeHms(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss");
        }
        public static string ToStringDateyMd(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
        public static string ToStringTimeSpanHms(this TimeSpan timeSpan)
        {
            return timeSpan.ToString(@"hh\:mm\:ss");
        }
        public static String RemoveDailyReminderTimeZone(String time)
        {
            if (string.IsNullOrEmpty(time) || string.Equals(time, "-1"))
            {
                return "-1";
            }
            DateTime date;
            try
            {
                // TODO 这又是什么鬼。。。
                //SimpleDateFormat time24Format = new SimpleDateFormat(DatePattern.HM_COLON_24,AppUtils.getAppLocale());
                //date = time24Format.parse(time);
                //time24Format.setTimeZone(TimeZone.getTimeZone("UTC"));
                //return time24Format.format(date);
                return null;
            }
            catch (Exception e)
            {
                //Log.e(tag, e.getMessage(), e);
                return "-1";
            }
        }
        public static DateTime? CalculateRemindTime(TickTickDuration duration, long dueTime)
        {
            if (dueTime <= 0 || duration == null)
            {
                return null;
            }
            Calendar calendar = new Calendar();
            //calendar.setTimeInMillis(dueTime); // TODO 有问题
            duration.AddDurationToDate(calendar);
            return DateTimeUtils.ClearSecondOfDay(calendar.GetDateTime().DateTime).Value;
        }
        public static DateTime? SetHMToDate(String timeHM, DateTime date)
        {
            if (date == null)
            {
                return null;
            }
            DateTime? timeDate = DateTimeUtils.ParseUTCTime(timeHM);
            if (timeDate == null)
            {
                return null;
            }
            Calendar calendar = new Calendar();
            calendar.SetDateTime(date);
            //int year = calendar.Year;//.get(Calendar.YEAR);
            //int month = calendar.Month;//.get(Calendar.MONTH);
            //int day = calendar.Day;//.get(Calendar.DAY_OF_MONTH);
            //calendar.SetDateTime(timeDate.Value);//.setTime(timeDate);
            //int hourOfDay = calendar.Hour;//.get(Calendar.HOUR_OF_DAY);
            //int minute = calendar.Minute;//.get(Calendar.MINUTE);
            ////calendar.clear();
            //calendar.set(year, month, day, hourOfDay, minute);
            return calendar.GetDateTime().DateTime;//.getTime();
        }
        public static DateTime? ParseUTCTime(String time)
        {
            if (string.IsNullOrEmpty(time) || string.Equals(time, "-1"))
            {
                return null;
            }
            DateTime date;
            try
            {
                // TODO 完全不知道怎么搞这个啊。。。
                //SimpleDateFormat time24Format = new SimpleDateFormat(DatePattern.HM_COLON_24,AppUtils.getAppLocale());
                //time24Format.setTimeZone(TimeZoneInfo.Utc);
                //date = time24Format.parse(time);
                //return date;
                return null;
            }
            catch (Exception e)
            {
                //Log.e(tag, e.getMessage(), e);
                return null;
            }

        }
        public static DateTime? CalculateReminderTime(String reminder, DateTime? dueDate)
        {
            if (dueDate == null)
            {
                return null;
            }

            DateTime newDate = new DateTime(dueDate.Value.Ticks);
            if (string.IsNullOrEmpty(reminder) || !reminder.Contains(TRIGGER_TAG))
            {
                return null;
            }
            try
            {
                String lexicalRepresentation = reminder.Substring(reminder.IndexOf(TRIGGER_TAG)
                        + TRIGGER_TAG.Length);
                return ReminderDurationParser.DurationAddToDate(lexicalRepresentation, newDate);
            }
            catch (Exception e)
            {
                //Log.e(tag, e.getMessage(), e);
                return null;
            }
        }
        public static DateTime? ClearSecondOfDay(DateTime date)
        {
            if (date == null)
            {
                return null;
            }
            Calendar c = new Calendar();
            c.SetDateTime(date);
            c.Second = 0;
            return c.GetDateTime().DateTime;

            //c.setTime(date);
            //c.set(Calendar.SECOND, 0);
            //c.set(Calendar.MILLISECOND, 0);
            //return c.getTime();
        }

        /// <summary>
        ///  TODO 是否是UTC时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsAfterNow(DateTime date)
        {
            return date != null && date > DateTime.UtcNow;
        }

        public static bool IsLastDayOfMonth(DateTime date)
        {
            return new Calendar().LastDayInThisMonth.Equals(date.Day);
        }

        public static bool IsRRuleWeekdays(IList<IWeekDay> weekdayNums)
        {
            if (weekdayNums == null || weekdayNums.Count != 5)
            {
                return false;
            }
            bool isWeekdays = false;
            int num = 20;
            foreach (WeekDay weekdayNum in weekdayNums)
            {
                if ((int)weekdayNum.DayOfWeek < 2 || (int)weekdayNum.DayOfWeek > 6)
                {
                    return false;
                }
                num -= (int)weekdayNum.DayOfWeek;
            }
            if (num == 0)
            {
                isWeekdays = true;
            }
            return isWeekdays;

        }

        public static bool IsRRuleWeekOnDay(TickRRule rule)
        {
            IList<IWeekDay> nums = rule.GetByDay();
            if (nums == null || nums.Count <= 0)
            {
                return false;
            }
            IList<int> byMonthDay = rule.GetByMonthDay();
            return byMonthDay == null || byMonthDay.Count == 0;
        }
    }
}
