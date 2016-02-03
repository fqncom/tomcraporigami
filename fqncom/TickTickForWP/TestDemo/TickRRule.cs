using DDay.iCal;
using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Utilities
{
    public class TickRRule
    {
        public static readonly String LUNAR_RRULE_NAME = "LUNAR";
        public static readonly String RRULE_NAME = "RRULE";

        public static readonly String TT_COMPLETED_COUNT_KEY = "TK_COMPLETEDCOUNT";
        public static readonly String UNTIL_KEY = "UNTIL";

        private RecurrencePattern RRule;
        //public RecurringComponent RecurringComponent;

        private bool _isLunar;

        public bool IsLunar
        {
            get { return _isLunar; }
            set { _isLunar = value; }
        }

        private int CompletedRepeatCount = -1;

        public String ToTickTickIcal()
        {
            String icalString = ToIcal();
            if (CompletedRepeatCount >= 0)
            {
                icalString += ";" + TT_COMPLETED_COUNT_KEY + "=" + CompletedRepeatCount;
            }
            return icalString;
        }
        //RRule标准的规则
        public String ToIcal()
        {
            if (IsLunar)
            {
                // TODO name属性不存在，但是在recurringComponent里有
                //农历时，不保存具体时间，以task的dueDate转换为农历为循环时间
                //RRule.Name(LUNAR_RRULE_NAME);
            }
            //return RRule.ToCal();
            return RRule.ToString();
        }
        public TickRRule()
        {
            //RecurringComponent =  new DDay.iCal.RecurringComponent();
            //RRule = new RecurrencePattern("");
            //RecurringComponent.RecurrenceRules.Add(RRule);
            //RRule = new RecurrencePattern();//; RRule.Interval = 1;
        }
        public TickRRule(FrequencyType type)
        {
            RRule = new RecurrencePattern(type);
        }
        public TickRRule(string iCalString)
        {
            if (iCalString.Contains(TT_COMPLETED_COUNT_KEY))
            {
                //CompletedRepeatCount = RepeatUtils.GetIntFromRRule(TT_COMPLETED_COUNT_KEY, iCalString);
                //iCalString = RepeatUtils.RemoveKeyValueFromRRule(TT_COMPLETED_COUNT_KEY, iCalString);
            }
            String iCal;
            if (iCalString.Contains(LUNAR_RRULE_NAME))
            {
                iCal = iCalString.Replace(LUNAR_RRULE_NAME, RRULE_NAME);
                IsLunar = true;
            }
            else
            {
                iCal = iCalString;
                IsLunar = false;
            }
            //rRule = new RRule(iCal);
            //RecurringComponent = new DDay.iCal.RecurringComponent();
            RRule = new RecurrencePattern();
            //RRule = new RecurrencePattern(iCal);
            //RecurringComponent.RecurrenceRules.Add(RRule);
        }
        public bool IsLunarFrequency()
        {
            return IsLunar;
        }

        public void SetByDay(List<IWeekDay> byDay)
        {
            RRule.ByDay = byDay;
        }

        public void SetByMonthDay(int[] byMonthDay)
        {
            RRule.ByMonthDay = byMonthDay;
        }


        internal int GetCount()
        {
            return RRule.Count;
        }

        internal int GetCompletedRepeatCount()
        {
            return this.CompletedRepeatCount;
        }
        public DateTime GetUntil()
        {
            return RRule.Until;
        }

        public void SetInterval(int interval)
        {
            RRule.Interval = interval;
        }
        public void SetFreq(FrequencyType freq)
        {
            RRule.Frequency = freq;
        }



        public void SetByMonth(int[] byMonth)
        {
            RRule.ByMonth = byMonth;
        }

        public void SetLunarFrequency(FrequencyType lunarFrequency)
        {
            RRule.Frequency = lunarFrequency;
            // TODO 没有Name属性 RRule.Name = LUNAR_RRULE_NAME;
            IsLunar = true;
        }

        public FrequencyType GetFreq()
        {
            return RRule.Frequency;
        }

        public IList<IWeekDay> GetByDay()
        {
            return RRule.ByDay;
        }

        public IList<int> GetByMonthDay()
        {
            return RRule.ByMonthDay;
        }
    }
}
