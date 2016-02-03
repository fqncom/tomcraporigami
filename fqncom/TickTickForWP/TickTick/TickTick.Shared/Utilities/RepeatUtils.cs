using DDay.iCal;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using TickTick.Enums;
using Windows.Globalization;

namespace TickTick.Utilities
{
    public class RepeatUtils
    {
        private static readonly System.DayOfWeek[] WeekDays = new System.DayOfWeek[] 
        {
            System.DayOfWeek.Sunday, 
            System.DayOfWeek.Monday, 
            System.DayOfWeek.Tuesday, 
            System.DayOfWeek.Wednesday, 
            System.DayOfWeek.Thursday,
            System.DayOfWeek.Friday, 
            System.DayOfWeek.Saturday
        };
        private static DateTime InitRepeatStartDate(String repeatFrom, DateTime completedTime, DateTime dueDate, Calendar taskCal)
        {
            if (IsRepeatFromCompleteTime(repeatFrom, completedTime))
            {
                taskCal.Year = completedTime.Year;
                taskCal.Month = completedTime.Month;
                taskCal.Day = completedTime.Day;
                taskCal.Hour = dueDate.Hour;
                taskCal.Minute = dueDate.Minute;
                taskCal.Second = dueDate.Second;
                return taskCal.GetDateTime().DateTime;
                //taskCal.SetDateTime(completedTime);//.setTime(completedTime);
                //int year = taskCal.Year;//taskCal.get(Calendar.YEAR);
                //int month = taskCal.Month;//.get(Calendar.MONTH);
                //int day = taskCal.Day;//.get(Calendar.DAY_OF_MONTH);
                //taskCal.SetDateTime(dueDate)//.setTime(dueDate);
                //int hour = taskCal.Hour;//.get(Calendar.HOUR_OF_DAY);
                //int minute = taskCal.Minute;//.get(Calendar.MINUTE);
                //int second = taskCal.Second;//.get(Calendar.SECOND);
                //taskCal.clear();
                //taskCal.(year, month, day, hour, minute, second);
                //return taskCal.getTime();
            }
            return dueDate;
        }
        /**
     * This isn't a regular method, because there isn't exist utc date. It
     * convert date for example 2014/1/1 09:00 +0800 -> 2014/1/1 09:00 +0000
     *
     * @return utc date
     */
        private static DateTime ConvertDateToUTCDate(DateTime date, Calendar taskCal, Calendar utcCal)
        {
            taskCal.SetDateTime(date);
            utcCal.SetDateTime(taskCal.GetDateTime().DateTime.ToUniversalTime());
            return utcCal.GetDateTime().DateTime;
            //taskCal.setTime(date);
            //int year = taskCal.get(Calendar.YEAR);
            //int month = taskCal.get(Calendar.MONTH);
            //int day = taskCal.get(Calendar.DAY_OF_MONTH);
            //int hourOfDay = taskCal.get(Calendar.HOUR_OF_DAY);
            //int minute = taskCal.get(Calendar.MINUTE);
            //int second = taskCal.get(Calendar.SECOND);
            //utcCal.clear();
            //utcCal.set(year, month, day, hourOfDay, minute, second);
            //return utcCal.getTime();
        }
        private static long GetTimeFromDateValue(DateTime untilDate)
        {
            if (untilDate == null)
            {
                return -1;
            }
            Calendar dateValue = new Calendar();
            dateValue.Year = untilDate.Year;//.set(Calendar.YEAR, untilDate.year());
            dateValue.Month = untilDate.Month - 1;//TODO 为什么要减1         .set(Calendar.MONTH, untilDate.month() - 1);
            dateValue.Day = untilDate.Day;//.set(Calendar.DAY_OF_MONTH, untilDate.day());
            dateValue.Hour = untilDate.Hour;//.set(Calendar.HOUR_OF_DAY, 0);
            dateValue.Minute = untilDate.Minute;//.set(Calendar.MINUTE, 0);
            dateValue.Second = untilDate.Second;//.set(Calendar.SECOND, 0);
            dateValue.Nanosecond = untilDate.Millisecond * 1000;//.set(Calendar.MILLISECOND, 0);
            return dateValue.GetDateTime().Ticks / TimeSpan.TicksPerMillisecond;//.getTimeInMillis();
        }
        public static List<DateTime> GetLatestNextDueDates(Tasks task)
        {
            return GetNextDueDate(task.RepeatFlag, task.DueDate.Value, task.RepeatFrom, task.CompletedTime.Value, task.TimeZone, 3);
        }
        public static List<DateTime> GetNextDueDate(String repeatFlag, DateTime dueDate, String repeatFrom, DateTime completedTime, String timeZone, int limit)
        {
            List<DateTime> dueDates = new List<DateTime>();
            if (string.IsNullOrEmpty(repeatFlag) || dueDate == null)
            {
                return dueDates;
            }
            bool isLunar;
            try
            {
                TickRRule rRule = new TickRRule(repeatFlag);
                isLunar = rRule.IsLunarFrequency();
                if (IsRepeatFromCompleteTime(repeatFrom, completedTime))
                {
                    // update rRule, We must clear byDay and byMonthDay when the
                    // task repeat from completedTime.
                    rRule.SetByDay(new List<IWeekDay>());
                    rRule.SetByMonthDay(new int[0]);
                    repeatFlag = rRule.ToTickTickIcal();
                }
                if (rRule.GetCompletedRepeatCount() >= rRule.GetCount())
                {
                    return dueDates;
                }

                if (string.IsNullOrEmpty(timeZone))
                {
                    timeZone = NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault().Id;
                }
                DateTimeZone taskTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZone); //.getTimeZone(timeZone);

                // TODO 有问题 阳历
                //GregorianCalendar taskCal = new GregorianCalendar(taskTimeZone);
                //GregorianCalendar utcCal = new GregorianCalendar(TimeUtils.utcTimezone());
                Calendar taskCal = new Calendar();
                taskCal.ChangeTimeZone(taskTimeZone.Id);
                Calendar utcCal = new Calendar();
                utcCal.ChangeTimeZone(DateTimeZoneProviders.Tzdb.GetSystemDefault().Id);

                DateTime taskStart = InitRepeatStartDate(repeatFrom, completedTime, dueDate, taskCal);
                DateTime utcStart = ConvertDateToUTCDate(taskStart, taskCal, utcCal);
                long untilDateTime = GetTimeFromDateValue(rRule.GetUntil());
                if (isLunar)
                {
                    //DateTime now = DateTime.UtcNow; // TODO 为什么要这么写？   DateTimeUtils.GetCurrentDate();
                    ////当前时间已经超过截止重复时间，必定没有下个有效的重复时间
                    //if (isAfterUntilDate(now, untilDateTime))
                    //{
                    //    return dueDates;
                    //}
                    //GregorianCalendar start = new GregorianCalendar();
                    //start.setTime(dueDate);
                    //Date next = getNextLunarDueDate(start, rRule);
                    //int i = 0;
                    //while (next != null && dueDates.size() < limit && !isAfterUntilDate(next,
                    //        untilDateTime))
                    //{
                    //    start.setTime(next);
                    //    next = getNextLunarDueDate(start, rRule);
                    //    if (next != null && !now.after(next))
                    //    {
                    //        dueDates.add(next);
                    //    }
                    //    if (i++ > 1000)
                    //    {
                    //        break;
                    //    }
                    //}
                    //if (dueDates.isEmpty())
                    //{
                    //    Log.e(TAG, "Get next due_date error: repeatFlag = " + repeatFlag);
                    //}
                    //return dueDates;
                }
                String rRuleString = rRule.ToIcal();
                if (untilDateTime > 0)
                {
                    rRuleString = RemoveKeyValueFromRRule(TickRRule.UNTIL_KEY, rRuleString);
                }
                // TODO 这是干嘛的。。。
                //DateIterator di = DateIteratorFactory.createDateIterator(rRuleString, utcStart, TimeUtils.utcTimezone(), true);
                if (Constants.RepeatFromStatus.DEFAULT.Equals(repeatFrom)
                        || string.IsNullOrEmpty(repeatFrom))
                {
                    // Repeat from default, next dueDate 不会返回小于Now
                    DateTime utcNow = ConvertDateToUTCDate(DateTime.UtcNow, taskCal, utcCal);//(DateTimeUtils.getCurrentDate(), taskCal, utcCal);
                    // TODO advanceTo 是什么，比什么早？
                    //if (utcNow > utcStart)
                    //{
                    //    di.advanceTo(utcNow);
                    //}
                }
                DateTime next;
                int count = 0;
                //while (di.hasNext() && dueDates.Count < limit)
                //{
                //    next = ConvertUtcDateToDate(di.next(), taskCal, utcCal);
                //    if (next > taskStart)
                //    {
                //        //得到的下次重复时间已超过截止重复时间，无效
                //        if (IsAfterUntilDate(next, untilDateTime))
                //        {
                //            return dueDates;
                //        }
                //        else
                //        {
                //            dueDates.Add(next);
                //        }
                //    }
                //    if (count++ > 10000)
                //    {
                //        // 对极端的情况跳出来，不要循环了。
                //        break;
                //    }
                //}
            }
            catch (Exception e)
            {
                //Log.e(TAG, "Get next due_date error: repeatFlag = " + repeatFlag, e);
            }
            return dueDates;

        }
        private static bool IsAfterUntilDate(DateTime repeatDate, long untilDateTime)
        {
            //无效的截止时间，必定未被限制
            if (untilDateTime < 0)
            {
                return false;
            }
            //无效的重复时间，不会被限制
            if (repeatDate == null)
            {
                return false;
            }
            return repeatDate.Ticks / TimeSpan.TicksPerMillisecond > untilDateTime;
        }
        private static DateTime ConvertUtcDateToDate(DateTime utcDate, Calendar taskCal, Calendar utcCal)
        {
            utcCal.SetDateTime(utcDate);
            taskCal.SetDateTime(utcCal.GetDateTime().DateTime);
            return taskCal.GetDateTime().DateTime;
            //int year = utcCal.Year;//.get(Calendar.YEAR);
            //int month = utcCal.Month;//.get(Calendar.MONTH);
            //int day = utcCal.Day;//.get(Calendar.DAY_OF_MONTH);
            //int hourOfDay = utcCal.Hour;//.get(Calendar.HOUR_OF_DAY);
            //int minute = utcCal.Minute;//.get(Calendar.MINUTE);
            //int second = utcCal.Second;//.get(Calendar.SECOND);
            ////taskCal.clear();
            //taskCal.set(year, month, day, hourOfDay, minute, second);
            //return taskCal.getTime();
        }
        //public static String RemoveKeyValueFromRRule(String key, String calString)
        //{

        //    try
        //    {
        //        if (!key.Contains("="))
        //        {
        //            key += "=";
        //        }
        //        String[] headBody = calString.Split(':');
        //        String[] keyValues = headBody[1].Split(':');
        //        List<String> newKeyValues = new List<String>();
        //        foreach (String keyValue in keyValues)
        //        {
        //            if (!keyValue.StartsWith(key))
        //            {
        //                newKeyValues.Add(keyValue);
        //            }
        //        }
        //        if (newKeyValues.Count <= 0)
        //        {
        //            return calString;
        //        }
        //        String newCalString = headBody[0] + ":";
        //        for (int i = 0, size = newKeyValues.Count; i < size; i++)
        //        {
        //            newCalString += newKeyValues[i];
        //            if (i < size - 1 && size > 1)
        //            {
        //                newCalString += ";";
        //            }
        //        }
        //        return newCalString;
        //    }
        //    catch (Exception e)
        //    {
        //        //Log.w(TAG, e.getMessage(), e);
        //        return calString;
        //    }
        //}
        private static bool IsRepeatFromCompleteTime(String repeatFrom, DateTime completedTime)
        {
            return Constants.RepeatFromStatus.COMPLETETIME.Equals(repeatFrom) && completedTime != null;
        }
        public static int GetIntFromRRule(String key, String rRuleText)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(rRuleText))
            {
                return -1;
            }
            if (!rRuleText.Contains(key))
            {
                return -1;
            }
            try
            {
                return Convert.ToInt32(GetValueByKey(key, rRuleText));
            }
            catch (Exception e)
            {
                //Log.w(TAG, e.getMessage(), e);
                return -1;
            }
        }
        private static String GetValueByKey(String key, String rRuleText)
        {
            String keyValuesStr = rRuleText.Split(':')[1];
            String[] keyValues = keyValuesStr.Split(':');
            if (keyValues.Length == 0)
            {
                return null;
            }
            if (!key.Contains("="))
            {
                key += "=";
            }
            foreach (String keyValueStr in keyValues)
            {
                if (keyValueStr.StartsWith(key))
                {
                    String[] keyValue = keyValueStr.Split(':');
                    if (keyValue.Length == 2)
                    {
                        return keyValue[1];
                    }
                }
            }
            return null;
        }
        public static String RemoveKeyValueFromRRule(String key, String calString)
        {
            try
            {
                if (!key.Contains("="))
                {
                    key += "=";
                }
                String[] headBody = calString.Split(':');
                String[] keyValues = headBody[1].Split(':');
                List<String> newKeyValues = new List<String>();
                foreach (String keyValue in keyValues)
                {
                    if (!keyValue.StartsWith(key))
                    {
                        newKeyValues.Add(keyValue);
                    }
                }
                if (newKeyValues.Count <= 0)
                {
                    return calString;
                }
                String newCalString = headBody[0] + ":";
                for (int i = 0, size = newKeyValues.Count; i < size; i++)
                {
                    newCalString += newKeyValues[i];
                    if (i < size - 1 && size > 1)
                    {
                        newCalString += ";";
                    }
                }
                return newCalString;
            }
            catch (Exception e)
            {
                //Log.w(TAG, e.getMessage(), e);
                return calString;
            }
        }

        public static WeekDay CreateWeekdayNum(DateTime taskDate)
        {
            return new WeekDay(WeekDays[(int)taskDate.DayOfWeek - 1], 0);
        }


        public static WeekDay CreateMonthWeekdayNum(DateTime taskDate)
        {
            int num = 4;// TODO 一个月有多少个当前的星期几 taskDate.get(Calendar.DAY_OF_WEEK_IN_MONTH);
            if (num == 5)
            {
                num = -1;
            }
            return new WeekDay(WeekDays[(int)taskDate.DayOfWeek - 1], num);
        }
    }
}
