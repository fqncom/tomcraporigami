using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;

namespace TickTick.Dal
{
    public class TaskReminderDal : BaseDal<TaskReminder>
    {
        public async Task<List<TaskReminder>> GetRemindersByTaskId(int taskId)
        {
            var conn = await CreateTableAsync();
            return await conn.Table<TaskReminder>().Where(t => t.TaskId == taskId).ToListAsync();

            //    StringBuilder selection = new StringBuilder();
            //    selection.append(TaskReminderField.task_id.name()).append(" = ?");
            //    String[] selectionArgs = {
            //        taskId + ""
            //};
            //    return getAllTaskReminders(selection.toString(), selectionArgs);
        }

        public async Task<Dictionary<int, List<TaskReminder>>> GetTaskRemindersMap(HashSet<long> taskIds)
        {
            Dictionary<int, List<TaskReminder>> map = new Dictionary<int, List<TaskReminder>>();
            List<TaskReminder> allReminders = await GetDataTableByExpression(t => taskIds.Contains(t.TaskId), null);
            foreach (var item in allReminders)
            {
                if (map.ContainsKey(item.TaskId))
                {
                    map[item.TaskId].Add(item);
                }
                else
                {
                    List<TaskReminder> reminderList = new List<TaskReminder>();
                    reminderList.Add(item);
                    map.Add(item.TaskId, reminderList);
                }
            }
            return map;
            //StringBuilder selection = new StringBuilder();
            //DBUtils.appendInLongIds(selection, TaskReminderField.task_id.nameWithTable(), taskIds);
            //List<TaskReminder> reminders = getAllTaskReminders(selection.toString(), null);
            //Map<Long, List<TaskReminder>> map = new HashMap<Long, List<TaskReminder>>();
            //for (TaskReminder reminder : reminders) {
            //    if (map.containsKey(reminder.getTaskId())) {
            //        map.get(reminder.getTaskId()).add(reminder);
            //    } else {
            //        List<TaskReminder> reminderList = new ArrayList<TaskReminder>();
            //        reminderList.add(reminder);
            //        map.put(reminder.getTaskId(), reminderList);
            //    }
            //}
            //return map;
        }

        public async Task DeleteRemindersPhysicalByTaskId(long taskId)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<TaskReminder>().Where(r => r.TaskId == taskId).ToListAsync();
            foreach (var item in queryResult)
            {
                await conn.DeleteAsync(item);
            }

            //TABLE.delete(TaskReminderField.task_id, taskId, dbHelper);
        }

        public async Task<TaskReminder> InsertTaskReminder(TaskReminder taskReminder)
        {

            var conn = await CreateTableAsync();
            await conn.InsertAsync(taskReminder);
            return taskReminder;
            //ContentValues values = getContentValue(taskReminder);
            //long id = TABLE.create(values, dbHelper);
            //if (id > 0)
            //{
            //    taskReminder.setId(id);
            //}
            //return taskReminder;
        }
    }
}
