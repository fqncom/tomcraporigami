using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Enums
{
    public static class TaskRepeatItemEnum
    {
        public const int DOES_NOT_REPEAT = 0;
        public const int REPEATS_DAILY = 1;
        public const int REPEATS_EVERY_WEEKDAY = 2;
        public const int REPEATS_WEEKLY_ON_DAY = 3;
        public const int REPEATS_MONTHLY_ON_DAY = 4;
        public const int REPEATS_MONTHLY_ON_DAY_COUNT = 5;
        public const int REPEATS_YEARLY = 6;
        public const int REPEATS_YEARLY_LUNAR = 7;
        public const int REPEATS_CUSTOM = 8;
        public const int REPEATS_END = 9;
    }
}
