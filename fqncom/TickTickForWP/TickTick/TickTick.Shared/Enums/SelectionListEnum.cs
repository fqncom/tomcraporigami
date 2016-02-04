using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Enum;
using TickTick.ViewModels;
using TickTick.Views;

namespace TickTick.Enums
{
    public static class SelectionListEnum
    {
        public static List<PrioritySelection> PrioritySelectionList = new List<PrioritySelection> 
        { 
            new PrioritySelection{ Name = "高优先级", PrioritiesEnum = PrioritiesEnum.HighPriorities},
            new PrioritySelection{ Name = "中优先级", PrioritiesEnum = PrioritiesEnum.MiddlePriorities},
            new PrioritySelection{ Name = "低优先级", PrioritiesEnum = PrioritiesEnum.LowPriorities},
            new PrioritySelection{ Name = "无优先级", PrioritiesEnum = PrioritiesEnum.NonePriorities},
        };

        public static List<SnoozeTimeSelection> SnoozeTimeSelectionList = new List<SnoozeTimeSelection> 
        { 
            new SnoozeTimeSelection { Name = "无提醒", SnoozeValue = MinuteIncrementEnum.No_Reminder } ,
            new SnoozeTimeSelection { Name = "在开始时间", SnoozeValue = MinuteIncrementEnum.At_StartTime } ,
            new SnoozeTimeSelection { Name = "提前5分钟", SnoozeValue = MinuteIncrementEnum.Five_Minute} ,
            //new RemindTimeSelection { Name = "10 minutes", RemindValue = MinuteIncrementEnum.Ten_Minute } ,
            //new RemindTimeSelection { Name = "15 minutes", RemindValue = MinuteIncrementEnum.Fifteen_Minute } ,
            new SnoozeTimeSelection { Name = "提前30分钟", SnoozeValue = MinuteIncrementEnum.Half_An_Hour } ,
            new SnoozeTimeSelection { Name = "提前1小时", SnoozeValue = MinuteIncrementEnum.One_Hour } ,
            //new RemindTimeSelection { Name = "4 hours", RemindValue = MinuteIncrementEnum.Four_Hours } ,
            //new RemindTimeSelection { Name = "18 hours", RemindValue = MinuteIncrementEnum.No_Reminder } ,
            //new SnoozeTimeSelection { Name = "提前1天", SnoozeValue = MinuteIncrementEnum.One_Day } 
        };
        public static List<SnoozeTimeSelection> SnoozeBackTimeSelectionList = new List<SnoozeTimeSelection> 
        { 
            new SnoozeTimeSelection { Name = "推迟5分钟", SnoozeBackValue = MinuteDecreaseEnum.Five_Minutes } ,
            new SnoozeTimeSelection { Name = "推迟10分钟", SnoozeBackValue = MinuteDecreaseEnum.Ten_Minutes } ,
            new SnoozeTimeSelection { Name = "推迟1小时", SnoozeBackValue = MinuteDecreaseEnum.One_Hour } ,
            new SnoozeTimeSelection { Name = "推迟4小时", SnoozeBackValue = MinuteDecreaseEnum.Four_Hours } ,
            new SnoozeTimeSelection { Name = "推迟1天", SnoozeBackValue = MinuteDecreaseEnum.One_Day } ,
        };
        public static List<RepeatTimeSelection> RepeatTimeSelection = new List<RepeatTimeSelection>
        {
            new RepeatTimeSelection{ Name ="永不", RepeatTimeEnum = TaskRepeatItemEnum.DOES_NOT_REPEAT },
            new RepeatTimeSelection{ Name ="每天", RepeatTimeEnum = TaskRepeatItemEnum.REPEATS_DAILY },
            new RepeatTimeSelection{ Name ="每周工作日（星期一至星期五）", RepeatTimeEnum = TaskRepeatItemEnum.REPEATS_EVERY_WEEKDAY },
            new RepeatTimeSelection{ Name ="每周", RepeatTimeEnum = TaskRepeatItemEnum.REPEATS_WEEKLY_ON_DAY },
            new RepeatTimeSelection{ Name ="每月", RepeatTimeEnum = TaskRepeatItemEnum.REPEATS_MONTHLY_ON_DAY },
            new RepeatTimeSelection{ Name ="每月", RepeatTimeEnum = TaskRepeatItemEnum.REPEATS_MONTHLY_ON_DAY_COUNT },
            new RepeatTimeSelection{ Name ="每年", RepeatTimeEnum = TaskRepeatItemEnum.REPEATS_YEARLY },
            //new RepeatTimeSelection{ Name ="每年", RepeatTimeEnum = TaskRepeatItemEnum.REPEATS_YEARLY_LUNAR },
            //new RepeatTimeSelection{ Name ="自定义", RepeatTimeEnum = TaskRepeatItemEnum.REPEATS_CUSTOM },
            //new RepeatTimeSelection{ Name ="结束重复", RepeatTimeEnum = TaskRepeatItemEnum.REPEATS_END },
        };
    }
}
