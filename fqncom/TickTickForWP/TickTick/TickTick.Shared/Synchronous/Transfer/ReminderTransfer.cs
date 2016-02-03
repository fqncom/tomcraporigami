using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;

namespace TickTick.Synchronous.Transfer
{
    public class ReminderTransfer
    {
        public static TaskReminder ConvertServerToLocal(Reminder serverReminder)
        {
            try
            {
                TaskReminder reminder = new TaskReminder();
                reminder.Sid = serverReminder.Id;
                // TODO 由于比较复杂，暂时不实现
                reminder.SetDuration(serverReminder.Trigger);
                return reminder;
            }
            catch (Exception e)
            {
                //Log.debugMultiReminder("convertServerToLocal failed, " + e.getMessage());
            }
            return null;
        }

        public static List<Reminder> ConvertLocalToServer(List<TaskReminder> localTaskReminders)
        {
            List<Reminder> serverReminders = new List<Reminder>();
            foreach (TaskReminder localReminder in localTaskReminders)
            {
                Reminder serverReminder = new Reminder();
                serverReminder.Id = localReminder.Sid;
                serverReminder.Trigger = localReminder.GetDurationString();
                serverReminders.Add(serverReminder);
            }
            return serverReminders;
        }
    }
}
