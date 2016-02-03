using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Models;

namespace TickTick.Bll
{
    public class ReminderTaskBll
    {

        TaskBll TaskBll = new TaskBll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="isChecked"></param>
        /// <returns></returns>
        public async Task UpdateTaskStatus(Tasks tasks)// TODO 临时使用tasks来进行适配。。。之后换成remindertask
        {
            //if (tasks.IsCompleted == isChecked)
            //{
            //    // do nothing
            //    return;
            //}
            Tasks task = await TaskBll.GetTasksByTasksId(tasks.Id);
            if (task == null)
            {
                // Task has been deleted
                return;
            }
            // TODO -临时使用这句代替，应该使用remindertask的属性来赋值
            task.TaskStatus = tasks.TaskStatus;
            await TaskBll.UpdateTaskCompleteStatus(task);
            if (task.IsRepeatTask() && task.IsCompleted)
            {
                //Toast.makeText(application, application.getString(R.string.repeat_task_complete_toast),
                //        Toast.LENGTH_SHORT).show();
            }
            if (task.HasLocation())
            {
                //application.sendLocationAlertChangedBroadcast(task.getLocation().getGeofenceId());
            }

            //application.getPreferencesHelper().setContentChanged(true);
            //application.tryToBackgroundSync();
            //application.sendTask2ReminderChangedBroadcast();
            //application.tryToSendBroadcast();
        }

        //   private static final String TAG = ReminderTaskService.class.getSimpleName();
        //private TickTickApplicationBase application;
        //private NotificationUtil notificationUtil;

        //public ReminderTaskService(TickTickApplicationBase application) {
        //    this.application = application;
        //    this.notificationUtil = NotificationUtil.getInstance(application);
        //}

        //public void updateTaskStatus(ReminderTask reminderTask, boolean isChecked) {
        //    if (reminderTask.isCompleted() == isChecked) {
        //        // do nothing
        //        return;
        //    }
        //    TaskService taskService = application.getTaskService();
        //    Task2 task = taskService.getTaskById(reminderTask.getId());
        //    if (task == null) {
        //        // Task has been deleted
        //        return;
        //    }
        //    taskService.updateTaskCompleteStatus(task, isChecked);
        //    if (task.isRepeatTask() && isChecked) {
        //        Toast.makeText(application, application.getString(R.string.repeat_task_complete_toast),
        //                Toast.LENGTH_SHORT).show();
        //    }
        //    if (task.hasLocation()) {
        //        application.sendLocationAlertChangedBroadcast(task.getLocation().getGeofenceId());
        //    }

        //    application.getPreferencesHelper().setContentChanged(true);
        //    application.tryToBackgroundSync();
        //    application.sendTask2ReminderChangedBroadcast();
        //    application.tryToSendBroadcast();
        //}

        //public void snoozeTaskNextDueDate(ReminderTask reminderTask) {
        //    TaskService taskService = application.getTaskService();
        //    Task2 task = taskService.getTaskById(reminderTask.getId());
        //    if (task == null) {
        //        Log.e(TAG, "Task missed when snooze reminder");
        //        return;
        //    }
        //    Date newDate = taskService.getNextDueDate(task);
        //    task.setDueDate(newDate);
        //    task.setReminderTime(
        //            DateUtils.calculateReminderTime(task.getReminder(), task.getDueDate()));
        //    taskService.updateTaskContent(task);

        //    application.getPreferencesHelper().setContentChanged(true);
        //    application.tryToBackgroundSync();
        //    application.sendTask2ReminderChangedBroadcast();
        //    application.tryToSendBroadcast();
        //}

        //public void snoozeTask(ReminderTask reminderTask, int snoozeMinutes) {
        //    long taskId = reminderTask.getId();
        //    Reminder.deleteReminderByTaskIDAndType(taskId, Reminder.REMINDER_TYPE_NORMAL,
        //            application.getDBHelper());
        //    Task2 task = application.getTaskService().getTaskById(taskId);
        //    if (task == null) {
        //        Log.e(TAG, "Task missed when snooze reminder");
        //        return;
        //    }
        //    application.getAnalyticsInstance().sendSnoozeSetEvent(snoozeMinutes);
        //    Date newDate = new Date(System.currentTimeMillis() + snoozeMinutes * 60 * 1000);
        //    newDate = DateUtils.clearSecondOfDay(newDate);
        //    if (isTasksNeedSnooze(task, newDate)) {
        //        Reminder reminder = new Reminder();
        //        reminder.setTaskId(taskId);
        //        reminder.setReminderTime(newDate);
        //        reminder.setType(Reminder.REMINDER_TYPE_NORMAL);
        //        Reminder.saveReminder(reminder, application.getDBHelper());
        //        application.getTaskService().saveSnoozeReminderTime(newDate, taskId);
        //        AlarmManager am = (AlarmManager) application.getSystemService(Context.ALARM_SERVICE);
        //        notificationUtil.sendTaskReminder(am, reminder);
        //    }
        //    application.tryToBackgroundSync();
        //}

        //private boolean isTasksNeedSnooze(Task2 task, Date newDate) {
        //    if (task.getRepeatReminderTime() != null && task.getRepeatReminderTime().equals(newDate)) {
        //        return false;
        //    }
        //    if (task.getReminderTime() != null && task.getReminderTime().equals(newDate)) {
        //        return false;
        //    }
        //    return true;
        //}

        //public Intent createReminderTaskViewIntent(ReminderTask task) {
        //    return IntentUtils.createReminderTaskViewIntent(task);
        //}
    }
}
