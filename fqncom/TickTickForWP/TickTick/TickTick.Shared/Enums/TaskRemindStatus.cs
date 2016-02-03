using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Enums
{
    public class TaskRemindStatus
    {
        public static readonly int VALID = 0;
        public static readonly int TASK_COMPLETED = 1;
        public static readonly int TASK_DELETED = 2;
        public static readonly int NO_DUE_DATE = 3;
        public static readonly int NO_REMINDER, OVERDUE = 4;

        //public bool IsValid()
        //{
        //    return this == VALID;
        //}
    }
}
