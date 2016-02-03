using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using Newtonsoft.Json;
using System.IO;
using TickTick.Enums;
using TickTick.Models;

namespace TickTick.Synchronous.Transfer
{
    public class TaskTransfer
    {
        //private static readonly String TAG = TaskTransfer.class.getSimpleName();
        private static readonly int LIMIT_COUNT = 50;

        public static List<SyncTaskBean> DescribleSyncTaskBean(List<Tasks> created, List<Tasks> updated, List<Tasks> deleted)
        {
            List<SyncTaskBean> syncTaskBeans = new List<SyncTaskBean>();
            int count = 0;
            SyncTaskBean syncTaskBean = new SyncTaskBean();
            foreach (var task in created)
            {
                if (count++ >= LIMIT_COUNT)
                {
                    count = 0;
                    syncTaskBeans.Add(syncTaskBean);
                    syncTaskBean = new SyncTaskBean();
                }
                syncTaskBean.Add.Add(ConvertLocalToServer(task));
                AppendTaskAttachments(task, syncTaskBean);
            }
            foreach (Tasks task in updated)
            {
                if (count++ >= LIMIT_COUNT)
                {
                    count = 0;
                    syncTaskBeans.Add(syncTaskBean);
                    syncTaskBean = new SyncTaskBean();
                }
                syncTaskBean.Update.Add(ConvertLocalToServer(task));
                AppendTaskAttachments(task, syncTaskBean);
            }
            foreach (Tasks task in deleted)
            {
                if (count++ >= LIMIT_COUNT)
                {
                    count = 0;
                    syncTaskBeans.Add(syncTaskBean);
                    syncTaskBean = new SyncTaskBean();
                }
                syncTaskBean.Delete.Add(new TasksProjects(task.ProjectSid, task.SId));
            }
            syncTaskBeans.Add(syncTaskBean);
            return syncTaskBeans;
        }

        #region 本地转服务器端
        public static TasksServer ConvertLocalToServer(Tasks localTask)
        {
            TasksServer task = new TasksServer();
            task.Id = localTask.SId;
            task.CreatedTime = localTask.CreatedTime;
            task.ModifiedTime = localTask.ModifiedTime;
            task.Etag = localTask.Etag;
            task.ProjectId = localTask.ProjectSid;
            task.Title = localTask.Title;
            task.Content = localTask.Content;
            task.SortOrder = localTask.SortOrder;
            task.Priority = localTask.Priority;
            task.DueDate = localTask.DueDate;
            task.RepeatFirstDate = localTask.RepeatFirstDate;
            //task.Reminder = localTask.Reminder;
            task.RepeatFlag = localTask.RepeatFlag;
            task.RepeatTaskId = localTask.RepeatTaskId;
            // TODO task.setUserCount=UserCount);
            task.CompletedTime = localTask.CompletedTime;
            task.Status = localTask.TaskStatus;
            task.TimeZone = localTask.TimeZone;

            if (localTask.IsChecklistMode())
            {
                task.Items = ChecklistItemTransfer.ConvertCheckListItemLocalToServer(localTask.ChecklistItems);
            }
            else
            {
                task.Items = null;
            }
            if (localTask.HasLocation())
            {
                task.Location = LocationTransfer.ConvertLocationLocalToServer(localTask.Location);
            }
            //if (localTask.Attachments != null && localTask.Attachments.Count > 0)
            //{
            //    //task.Attachments =AttachmentTransfer.ConvertAttachmentLocalToServer(localTask.Attachments);
            //}

            if (localTask.HasReminder())
            {
                //DateTime? reminderTime = localTask.SnoozeRemindTime;
                //if (reminderTime == null)
                //{
                //    reminderTime = DateTimeUtils.CalculateReminderTime(localTask.Reminder, localTask.DueDate.Value);
                //}
                //task.RemindTime = reminderTime;
                task.Reminders = ReminderTransfer.ConvertLocalToServer(localTask.Reminders);
            }
            if (localTask.DueDate != null)
            {
                task.IsAllDay = localTask.IsAllDay;
            }
            task.RemindTime = localTask.SnoozeRemindTime;
            task.RepeatFrom = localTask.RepeatFrom;
            task.Assignee = localTask.Assignee;
            //task.Tags = localTask.Tags;
            return task;
        }

        #endregion

        #region 服务器端转本地

        public static Tasks ConvertTaskSyncedJsonToLocal(TaskSyncedJson json)//此处android中的ObjectMapper mapper可以使用newton.json代替进行json转对象的操作
        {
            if (json == null)
            {
                return null;
            }
            try
            {
                TasksServer task = JsonConvert.DeserializeObject<TasksServer>(json.JsonString);
                if (task != null)
                {
                    return ConvertServerTaskToLocalWithChecklistItem(task);
                }
            }
            //catch (JsonParseException e)
            //{
            //    Log.e(TAG, e.getMessage(), e);
            //}
            //catch (JsonMappingException e)
            //{
            //    Log.e(TAG, e.getMessage(), e);
            //}
            catch (IOException e)
            {
                //Log.e(TAG, e.getMessage(), e);
            }
            return null;
        }
        public static Tasks ConvertServerTaskToLocalWithChecklistItem(TasksServer serverTask)
        {
            Tasks task = new Tasks();
            ConvertServerTaskToLocalWithChecklistItem(task, serverTask);
            return task;
        }
        public static void ConvertServerTaskToLocalWithChecklistItem(Tasks localTask, TasksServer serverTask)
        {
            ConvertServerTaskToLocal(localTask, serverTask);
            ConvertServerChecklistItemToLocal(localTask, serverTask);
            ConvertServerRemindersToLocal(localTask, serverTask);
            localTask.SetTagsInner();// TODO 此处有坑，由于不知道转换逻辑是什么，所以先不实现
        }
        private static void ConvertServerRemindersToLocal(Tasks localTask, TasksServer serverTask)
        {
            if (serverTask.Reminder == null)
            {
                return;
            }
            List<TaskReminder> localReminders = new List<TaskReminder>();
            foreach (Reminder serverReminder in serverTask.Reminders)
            {
                if (serverReminder != null)
                {
                    TaskReminder taskReminder = ReminderTransfer.ConvertServerToLocal(serverReminder);
                    if (taskReminder != null)
                    {
                        localReminders.Add(taskReminder);
                    }
                }
            }
            localTask.Reminders = localReminders;
        }
        private static void ConvertServerChecklistItemToLocal(Tasks localTask, TasksServer serverTask)
        {
            if (serverTask.Items == null)
            {
                return;
            }
            List<ChecklistItem> localItems = new List<ChecklistItem>();
            foreach (ChecklistItem serverItem in serverTask.Items)
            {
                if (serverItem != null)
                {
                    localItems.Add(ChecklistItemTransfer.ConvertServerToLocal(serverItem));
                }
            }
            localTask.ChecklistItems = localItems;
            localTask.SetContentByItemsInner();
        }

        private static void ConvertServerTaskToLocal(Tasks task, TasksServer serverTask)
        {
            DateTime? reminderTime = GetReminderTime(serverTask);
            task.SId = serverTask.Id;
            task.ProjectSid = serverTask.ProjectId;
            task.Title = serverTask.Title;
            task.Content = serverTask.Content;
            task.SortOrder = serverTask.SortOrder;
            task.Priority = serverTask.Priority;
            task.DueDate = serverTask.DueDate;
            task.RepeatFirstDate = serverTask.RepeatFirstDate;
            //task.Reminder = serverTask.Reminder;// TODO 此字段已经废弃
            task.RepeatFlag = serverTask.RepeatFlag;
            String repeatTaskId = serverTask.RepeatTaskId;
            task.RepeatTaskId = string.IsNullOrEmpty(repeatTaskId) ? task.SId : repeatTaskId;
            task.CompletedTime = serverTask.CompletedTime;
            task.TaskStatus = serverTask.Status;
            task.Etag = serverTask.Etag;
            task.Kind = GetTaskKind(serverTask);//task.setKind(getTaskKind(serverTask));
            String tz = string.IsNullOrEmpty(serverTask.TimeZone) ? NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault().Id : serverTask.TimeZone;// TimeZone.getDefault().getID() : serverTask.getTimeZone();
            task.TimeZone = tz;
            task.ModifiedTime = serverTask.ModifiedTime;
            task.SnoozeRemindTime = reminderTime;
            //task.RepeatReminderTime = task.IsRepeatTask() ? reminderTime : DateTime.MinValue;
            String repeatFromRemote = serverTask.RepeatFrom;
            task.RepeatFrom = string.IsNullOrEmpty(repeatFromRemote) ? Constants.RepeatFromStatus.DEFAULT : repeatFromRemote;
            task.CommentCount = serverTask.CommentCount;
            task.Assignee = serverTask.Assignee == null ? Constants.Removed.ASSIGNEE : serverTask.Assignee;
            task.Deleted = serverTask.Deleted;
            task.IsAllDay = serverTask.IsAllDay != null && serverTask.IsAllDay;
        }
        private static string GetTaskKind(TasksServer serverTask)
        {
            List<ChecklistItem> items = serverTask.Items;
            if (items == null || items.Count <= 0)
            {
                return Constants.Kind.TEXT;
            }
            else
            {
                return Constants.Kind.CHECKLIST;
            }
        }
        #endregion

        private static void AppendTaskAttachments(Tasks task, SyncTaskBean syncTaskBean)
        {
            List<Attachment> addedRemotes = new List<Attachment>();
            List<Attachment> deletedRemotes = new List<Attachment>();
            foreach (Attachment localAttach in task.Attachments)
            {
                if (localAttach.Status == ModelStatusEnum.SYNC_NEW
                        && localAttach.Deleted == ModelStatusEnum.DELETED_NO)
                {
                    addedRemotes.Add(AttachmentTransfer.ConvertLocalToRemote(localAttach));
                }
                else if (localAttach.Status != ModelStatusEnum.SYNC_NEW
                        && localAttach.Deleted == ModelStatusEnum.DELETED_TRASH)
                {
                    deletedRemotes.Add(AttachmentTransfer.ConvertLocalToRemote(localAttach));
                }
            }

            if (addedRemotes.Count > 0)
            {
                Tasks remote = new Tasks();
                remote.Id = Convert.ToInt32(task.SId);
                remote.ProjectId = task.ProjectSid;
                remote.Attachments = addedRemotes;
                syncTaskBean.AddAttachments.Add(remote);
            }
            if (deletedRemotes.Count > 0)
            {
                Tasks remote = new Tasks();
                remote.Id = Convert.ToInt32(task.SId);
                remote.ProjectId = task.ProjectSid;
                remote.Attachments = deletedRemotes;
                syncTaskBean.DeleteAttachments.Add(remote);
            }
        }
        private static string GetTaskKind(Tasks serverTask)
        {
            List<ChecklistItem> items = serverTask.ChecklistItems;
            if (items == null || items.Count <= 0)
            {
                return Constants.Kind.TEXT;
            }
            else
            {
                return Constants.Kind.CHECKLIST;
            }
        }
        private static DateTime? GetReminderTime(TasksServer serverTask)
        {
            DateTime? reminderTimeRemote = serverTask.RemindTime;
            DateTime? dueDateRemote = serverTask.DueDate;
            return dueDateRemote == null ? null : reminderTimeRemote;
            
        }
    }
}
