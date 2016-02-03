using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using TickTick.Enums;

namespace TickTick.Helper
{
    public static class TaskHelper
    {
        public static List<TaskReminder> CalculateTaskReminder(Tasks task)
        {
            List<TaskReminder> taskReminders = new List<TaskReminder>();
            if (task.DueDate == null)
            {
                task.Reminders = new List<TaskReminder>();
                return taskReminders;
            }
            if (!task.HasReminder())
            {
                return taskReminders;
            }
            long dueTime = GetTaskDueTime(task.DueDate.Value, task.IsAllDay);
            foreach (TaskReminder reminder in task.Reminders)
            {
                DateTime? remindTime = DateTimeUtils.CalculateRemindTime(reminder.Duration, dueTime);
                if (remindTime != null)
                {
                    TaskReminder taskReminder = new TaskReminder(reminder);
                    taskReminder.RemindTime = remindTime.Value;
                    taskReminders.Add(taskReminder);
                }
            }
            return taskReminders;
        }
        private static long GetTaskDueTime(DateTime dueDate, bool isAllDay)
        {
            if (dueDate == null)
            {
                return 0;
            }
            if (isAllDay)
            {
                DateTime timeDate = GetAllDayReminderTime(dueDate);
                return timeDate == null ? 0 : timeDate.Ticks / TimeSpan.TicksPerMillisecond;//.getTime();
            }
            return dueDate.Ticks / TimeSpan.TicksPerMillisecond;//.getTime();
        }
        public static DateTime GetAllDayReminderTime(DateTime dueDate)
        {
            return DateTimeUtils.SetHMToDate(GetDailyRemindTimePointUTC(), dueDate).Value;
        }
        private static String GetDailyRemindTimePointUTC()
        {
            String timePoint = TickTickApplicationBase.StaticApplication.GetDailyReminderTimeFlag();
            if (string.IsNullOrEmpty(timePoint) || string.Equals(timePoint, "-1"))
            {
                timePoint = DateTimeUtils.RemoveDailyReminderTimeZone("09:00");
            }
            return timePoint;
        }
        // TODO 这里有问题 返回taskremindstatus
        public static int IsRemindTask(Tasks task)
        {

            if (task.DueDate == null)
            {
                return TaskRemindStatus.NO_DUE_DATE;
            }

            if (task.Deleted != ModelStatusEnum.DELETED_NO)
            {
                return TaskRemindStatus.TASK_DELETED;
            }

            if (task.IsCompleted)
            {
                return TaskRemindStatus.TASK_COMPLETED;
            }

            if (!task.HasReminder())
            {
                return TaskRemindStatus.NO_REMINDER;
            }

            if (task.IsRepeatTask())
            {
                return TaskRemindStatus.VALID;
            }

            if (HasValidReminder(task.Reminders, task.DueDate.Value, task.IsAllDay))
            {
                return TaskRemindStatus.VALID;
            }

            if (IsValidSnoozeReminder(task.SnoozeRemindTime.Value))
            {
                return TaskRemindStatus.VALID;
            }

            return TaskRemindStatus.OVERDUE;
        }
        private static bool HasValidReminder(List<TaskReminder> reminders, DateTime dueDate, bool isAllDay)
        {
            if (reminders.Count <= 0)
            {
                return false;
            }
            long dueDateTime = GetTaskDueTime(dueDate, isAllDay);
            foreach (TaskReminder reminder in reminders)
            {
                //检查提醒时间是否过期
                DateTime? remindTime = DateTimeUtils.CalculateRemindTime(reminder.Duration, dueDateTime);
                if (remindTime != null && DateTimeUtils.IsAfterNow(remindTime.Value))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsValidSnoozeReminder(DateTime reminderTime)
        {
            //检查延迟提醒时间是否过期
            return DateTimeUtils.IsAfterNow(reminderTime);
        }
    }
}
