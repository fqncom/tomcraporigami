using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Helper;
using TickTick.Models;
using TickTick.Utilities;
using WindowsUniversalLogger.Interfaces;

namespace TickTick.Bll
{
    public class TaskBll : BaseBll<Tasks>
    {
        /// <summary>
        /// Tasks的dal层对象
        /// </summary>
        private TaskDal TaskDal = new TaskDal();
        private LocationDal LocationDal = new LocationDal();
        private ChecklistItemDal ChecklistItemDal = new ChecklistItemDal();
        private ChecklistItemBll ChecklistItemBll = new ChecklistItemBll();
        private AttachmentBll AttachmentBll = new AttachmentBll();
        private AttachmentDal AttachmentDal = new AttachmentDal();
        private CommentBll CommentBll = new CommentBll();
        private SyncStatusBll SyncStatusBll = new SyncStatusBll();
        private LocationBll LocationBll = new LocationBll();
        public TaskReminderDal TaskReminderDal = new TaskReminderDal();
        public TaskSyncedJsonBll TaskSyncedJsonBll = new TaskSyncedJsonBll();

        #region 自定义方法
        private void FillReviseChecklistItems(Tasks reviseTask)
        {
            long sortOrder = 0L;
            foreach (var item in reviseTask.ChecklistItems)
            {
                item.SortOrder = sortOrder++;
                if (item.Id == null && item.Id < 0)
                {
                    item.SId = StringUtils.GenerateShortStringGuid();//(Utils.randomUUID32());
                    item.TaskId = reviseTask.Id;
                    item.TaskSid = reviseTask.SId;
                    item.UserId = reviseTask.UserId;
                }
            }
        }
        private bool IsCheckListItemEqual(ChecklistItem item1, ChecklistItem item2)
        {
            if (item1.Checked != item2.Checked)
            {
                return false;
            }
            if (!string.Equals(item1.Title, item2.Title))
            {
                return false;
            }
            if (item1.SortOrder != item2.SortOrder)
            {
                return false;
            }
            return true;
        }
        private bool IsChecklistChanged(Tasks originalTask, Tasks reviseTask)
        {
            if (originalTask.ChecklistItems.Count != reviseTask.ChecklistItems.Count)
            {
                return true;
            }
            Dictionary<long, ChecklistItem> originalItems = new Dictionary<long, ChecklistItem>();
            if (originalTask.IsChecklistMode())
            {
                foreach (var checklistItem in originalTask.ChecklistItems)
                {
                    originalItems.Add(checklistItem.Id, checklistItem);
                }
            }
            bool ret = false;
            foreach (var item in reviseTask.ChecklistItems)
            {
                if (item.Id == null)
                {//|| item.Id < 0) {
                    ret = true;
                }
                else if (originalItems.ContainsKey(item.Id))
                {
                    ChecklistItem originalItem = originalItems[item.Id];
                    if (!IsCheckListItemEqual(item, originalItem))
                    {
                        ret = true;
                    }
                    originalItems.Remove(item.Id);
                }
            }
            if (originalItems.Values.Count > 0)
            {
                ret = true;
            }
            return ret;
        }
        private bool IsTaskChanged(Tasks originalTask, Tasks reviseTask)
        {
            String reviseTaskTitle = reviseTask.Title ?? reviseTask.Title;
            String reviseTaskContent = reviseTask.Content ?? reviseTask.Content;
            String originalTaskTitle = originalTask.Title ?? originalTask.Title;
            String originalTaskContent = originalTask.Content ?? originalTask.Content;
            if (originalTask.Assignee != reviseTask.Assignee)
            {
                return true;
            }

            if (!reviseTaskTitle.Equals(originalTaskTitle))
            {
                return true;
            }

            if (!originalTask.IsChecklistMode() || originalTask.Kind == null)
            {
                if (!reviseTaskContent.Equals(originalTaskContent))
                {
                    return true;
                }
            }

            if (IsReminderChanged(originalTask, reviseTask))
            {
                return true;
            }
            //if (IsRepeatChanged(originalTask, reviseTask)) {
            //    return true;
            //}

            if (originalTask.HasAttachment != reviseTask.HasAttachment)
            {
                return true;
            }

            if (originalTask.HasLocation() != reviseTask.HasLocation())
            {
                return true;
            }

            if (reviseTask.TaskStatus != originalTask.TaskStatus)
            {
                return true;
            }
            int priority = reviseTask.Priority;
            if (priority != originalTask.Priority)
            {
                return true;
            }

            //新创建的task，其他内容不变时，清单模式之间的切换无效（因为该task不被保存）
            if (originalTask.Id != TickTick.Enums.Constants.EntityIdentifie.DEFAULT_TASK_ID
                    && reviseTask.Id != TickTick.Enums.Constants.EntityIdentifie.DEFAULT_TASK_ID
                    && originalTask.Kind != reviseTask.Kind)
            {
                return true;
            }

            return false;
        }
        private async Task SaveMergedChecklistItems(Tasks task)
        {
            await ChecklistItemDal.DeleteChecklistPhysicalByTaskId(task.Id);
            foreach (var item in task.ChecklistItems)
            {
                item.TaskId = task.Id;
                item.TaskSid = task.SId;
                item.UserId = task.UserId;
                await ChecklistItemDal.CreateChecklistItem(item);
            }
        }
        public async Task<bool> SaveTask(bool forceSave, Tasks originalTask, Tasks reviseTask)
        {
            //TickTickApplicationBase application = TickTickApplicationBase.StaticApplication;
            FillReviseChecklistItems(reviseTask);
            bool checklistChanged = IsChecklistChanged(originalTask, reviseTask);
            if (reviseTask.Id == TickTick.Enums.Constants.EntityIdentifie.DEFAULT_TASK_ID)
            {
                if (forceSave || IsTaskChanged(originalTask, reviseTask) || checklistChanged)
                {
                    await AddTasks(reviseTask);
                    originalTask.Id = reviseTask.Id;
                    originalTask.SId = reviseTask.SId;
                    originalTask.UserId = reviseTask.UserId;
                    if (reviseTask.IsChecklistMode())
                    {
                        // android未实现 saveChecklist(originalTask, reviseTask);
                        await SaveMergedChecklistItems(reviseTask);
                    }
                    if (reviseTask.HasLocation())
                    {

                        // TODO 位置信息
                        //SaveOrInsertLocation(originalTask, reviseTask.Location);
                        //application.SendLocationAlertChangedBroadcast(reviseTask.Location.getGeofenceId());
                    }
                    if (reviseTask.DueDate != null)
                    {
                        // TODO 数据收集
                        //application.GetAnalyticsInstance().sendDueDateSetEvent(reviseTask.getDueDate());
                    }

                    if (reviseTask.IsReminder())
                    {
                        //application.SendTask2ReminderChangedBroadcast();
                        //application.GetAnalyticsInstance().sendReminderTimeSetEvent(reviseTask.GetReminderTime());
                    }

                    return true;
                }
            }
            else
            {
                bool attachmentChanged = CheckAttachmentChanges(originalTask, reviseTask);
                bool locationSaved = await SaveOrInsertLocation(originalTask, reviseTask.Location);
                bool isReminderChanged = IsReminderChanged(originalTask, reviseTask);
                if (forceSave || IsTaskChanged(originalTask, reviseTask) || checklistChanged || attachmentChanged || locationSaved)
                {
                    Tasks detlaTask = await GetFullTaskById(reviseTask.Id);
                    if (detlaTask == null)
                    {
                        reviseTask.SId = null;
                        reviseTask.Etag = null;
                        await AddTasks(reviseTask);
                    }
                    else
                    {
                        TaskUtils.MergeTask(originalTask, detlaTask, reviseTask);
                        if (reviseTask.ChecklistItems.Count > 0)
                        {
                            await SaveMergedChecklistItems(reviseTask);
                        }
                        reviseTask.SetRepeatAlertTime();
                        //reviseTask.SetTagsInner(); // TODO 未实现
                        if (isReminderChanged)
                        {
                            await UpdateTaskContentWithReminder(reviseTask);
                        }
                        else
                        {
                            await UpdateTaskContent(reviseTask);
                        }
                    }

                    bool completedStatusChanged = (originalTask.TaskStatus != reviseTask.TaskStatus);

                    if (completedStatusChanged && reviseTask.IsCompleted)
                    {
                        // Task打钩
                        if (reviseTask.IsRepeatTask())
                        {
                            // TODO 创建一个toast？==android里的toast，显示任务完成
                            //Toast.makeText(TickTickApplicationBase.getInstance(),
                            //        application.getString(R.string.repeat_task_complete_toast),
                            //        Toast.LENGTH_SHORT).show();
                        }
                        // TODO @chenchao saveTask 重复存task？
                        //await UpdateTaskCompleteStatus(reviseTask, true);
                        await UpdateTaskCompleteStatus(reviseTask);
                    }
                    if (completedStatusChanged || isReminderChanged)
                    {
                        // TODO 未实现
                        //application.SendTask2ReminderChangedBroadcast();
                    }

                    if (TaskUtils.IsDuedateChanged(originalTask, reviseTask))
                    {
                        //application.GetAnalyticsInstance().sendDueDateSetEvent(reviseTask.getDueDate());
                    }

                    if (locationSaved && reviseTask.Location != null)
                    {
                        // TODO 
                        //application.sendLocationAlertChangedBroadcast(reviseTask.getLocation()
                        //        .getGeofenceId());
                    }

                    if (isReminderChanged)
                    {
                        // TODO
                        //application.getAnalyticsInstance().sendReminderTimeSetEvent(
                        //        reviseTask.getReminderTime());
                    }
                    return true;
                }
            }

            return false;
        }
        //public async Task UpdateTaskCompleteStatus(Tasks task, bool newSelected)
        public async Task UpdateTaskCompleteStatus(Tasks task)
        {
            task.CompletedTime = DateTime.UtcNow;
            await UpdateTaskComplete(task);
        }
        private Tasks CloneRepeatTask(Tasks task)
        {
            Tasks cloneTask = null;
            if (task.TaskStatus == Constants.TaskStatus.COMPLETED)
            {
                if (task.CompletedTime == null)
                {
                    task.CompletedTime = DateTime.UtcNow;
                }
                DateTime nextDueDate = RepeatUtils.GetLatestNextDueDates(task)[0];
                if (nextDueDate != null)
                {// 还有下一个重复周期
                    task.TaskStatus = Constants.TaskStatus.UNCOMPLETED;
                    cloneTask = CloneCompletedTask(task);
                    task.DueDate = DateTimeUtils.ClearSecondOfDay(nextDueDate);
                    //task.ReminderTime = DateTimeUtils.CalculateReminderTime(task.Reminder, task.DueDate.Value);
                    task.SnoozeRemindTime = DateTimeUtils.CalculateReminderTime(task.Reminder, task.DueDate);
                    task.SetRepeatAlertTime();
                }
            }
            return cloneTask;
        }
        private Tasks CloneCompletedTask(Tasks task)
        {
            Tasks cloneTask = ObjectCopier.Clone<Tasks>(task);
            cloneTask.SId = StringUtils.GenerateShortStringGuid();//(Utils.randomUUID32());
            cloneTask.RepeatFlag = null;
            cloneTask.Etag = null;
            cloneTask.TaskStatus = Constants.TaskStatus.ARCHIVED;
            cloneTask.SortOrder = 0L;
            return cloneTask;
        }

        public async Task<long> GetTaskMaxSortOrderInProject(string projectId)
        {
            long? maxOrder = await TaskDal.GetMaxTaskSortOrderInProject(projectId);
            if (maxOrder == null)
            {
                return 0L;
            }
            return maxOrder.Value + Constants.OrderStepData.STEP;
        }
        private async Task UpdateTaskSortOrder(String userId, String taskSid, long targetOrder)
        {
            Tasks task = await TaskDal.GetTaskBySid(userId, taskSid);
            task.SortOrder = targetOrder;
            if (await TaskDal.UpdateTaskOrder(task))
            {
                await SyncStatusBll.AddSyncStatus(task, ModelStatusEnum.SYNC_TYPE_TASK_ORDER);
            }
        }
        private async Task UpdateTaskComplete(Tasks task)
        {
            bool isUnCompleted = task.IsUncompleted();
            // fqn修改
            Tasks cloneRepeatTask = null;
            if (task.IsRepeatTask())
            {
                // TODO 由于rrule 还未完成，先等等 cloneRepeatTask = CloneRepeatTask(task);
            }
            // fqn修改
            if (isUnCompleted)
            {
                long sortOrder = await GetTaskMaxSortOrderInProject(task.ProjectId);
                await UpdateTaskSortOrder(task.UserId, task.SId, sortOrder);
            }
            await UpdateTaskContent(task);
            if (task.HasLocation())
            {
                Location location = task.Location;
                await LocationDal.UpdateLocationStatus(ModelStatusEnum.ALERT_STATUS_NORMAL, location.Id, location.UserId);
            }
            if (cloneRepeatTask != null)
            {

                try
                {
                    // 有可能该任务已经存在了。（分享、多设备导致）
                    if (await CreateTask(cloneRepeatTask))
                    {
                        await SyncStatusBll.AddSyncStatus(cloneRepeatTask, ModelStatusEnum.SYNC_TYPE_TASK_CREATE);
                    }
                    await ChecklistItemBll.CopyChecklistItemToCloneTask(task, cloneRepeatTask.Id, cloneRepeatTask.SId);
                    await AttachmentBll.CopyAttachmentForCloneTask(task, cloneRepeatTask.Id, cloneRepeatTask.SId);

                    await LocationBll.CopyLocationToCloneTask(task, cloneRepeatTask.Id, cloneRepeatTask.SId);
                }
                catch (Exception e)//(SQLiteConstraintException e)
                {
                    //Log.e(TAG, e.getMessage(), e);
                }

            }

        }
        private bool IsReminderChanged(Tasks originalTask, Tasks reviseTask)
        {
            String reviseReminder = reviseTask.Reminder == null ? "" : reviseTask.Reminder;
            String originalReminder = originalTask.Reminder == null ? "" : originalTask.Reminder;
            if (!string.Equals(reviseReminder, originalReminder))
            {
                return true;
            }
            if (!DateTime.Equals(originalTask.DueDate, reviseTask.DueDate))
            {
                return true;
            }
            return false;
        }
        public async Task UpdateTaskContent(Tasks task)
        {
            if (await TaskDal.UpdateTaskContent(task))
            {
                await SyncStatusBll.AddSyncStatus(task, ModelStatusEnum.SYNC_TYPE_TASK_CONTENT);
            }
        }
        public async Task UpdateTaskContentWithReminder(Tasks task)
        {
            await SaveReminders(task);
            await UpdateTaskContent(task);
        }
        private async Task SaveReminders(Tasks task)
        {
            // TODO 暂时使用这个进行但闹钟的提醒操作
            task.SnoozeRemindTime = DateTimeUtils.CalculateReminderTime(task.Reminder, task.DueDate);
            // TODO 以下多闹钟
            await TaskReminderDal.DeleteRemindersPhysicalByTaskId(task.Id);
            if (task.Reminders == null)
            {
                return;
            }
            foreach (TaskReminder reminder in task.Reminders)
            {
                if (string.IsNullOrEmpty(reminder.Sid))
                {
                    reminder.Sid = StringUtils.GenerateShortStringGuid();//.setSid(Utils.randomUUID32());
                }
                reminder.TaskId = task.Id;
                reminder.TaskSid = task.SId;
                reminder.UserId = task.UserId;
                await TaskReminderDal.InsertTaskReminder(reminder);
            }
        }
        public async Task<Tasks> GetFullTaskById(int taskId)
        {
            Tasks task = await AppendLocations(await TaskDal.GetTaskById(taskId));
            if (task == null)
            {
                return null;
            }
            List<ChecklistItem> checklistItems = await ChecklistItemDal.GetChecklistItemsByTaskId(taskId, task.UserId, false);
            task.ChecklistItems = checklistItems;

            List<Attachment> attachments = await AttachmentBll.GetAllAttachmentByTaskId(taskId, task.UserId);
            task.Attachments = attachments;

            List<TaskReminder> reminders = await TaskReminderDal.GetRemindersByTaskId(task.Id);
            task.Reminders = reminders;

            return task;
        }

        public async Task<bool> SaveOrInsertLocation(Tasks originTask, Location revise)
        {
            Location origin = originTask.Location;
            if (origin == null && revise == null)
            {
                return false;
            }
            if (origin == null)
            {
                revise.TaskId = originTask.Id;
                revise.TaskSid = originTask.SId;
                revise.UserId = originTask.UserId;
                if (string.IsNullOrEmpty(revise.GeofenceId))
                {
                    revise.GeofenceId = StringUtils.GenerateShortStringGuid();//(Utils.randomUUID32());
                }
                revise.Status = ModelStatusEnum.SYNC_NEW;
                await LocationDal.InsertLocation(revise);
                originTask.Location = revise;
                return true;
            }
            else if (revise == null)
            {
                await DeleteLocation(originTask);
                return true;
            }
            else
            {
                if (revise.IsContentChanged(origin))
                {
                    revise.TaskId = originTask.Id;
                    revise.TaskSid = originTask.SId;
                    revise.UserId = originTask.UserId;
                    if (revise.Id == TickTick.Enums.Constants.EntityIdentifie.DEFAULT_LOCATION_ID)
                    {
                        if (string.IsNullOrEmpty(revise.GeofenceId))
                        {
                            revise.GeofenceId = StringUtils.GenerateShortStringGuid();//(Utils.randomUUID32());
                        }
                        revise.Status = ModelStatusEnum.SYNC_NEW;
                        await LocationDal.InsertLocation(revise);
                    }
                    else
                    {
                        revise.Status = ModelStatusEnum.SYNC_UPDATE;
                        revise.Id = origin.Id;
                        revise.GeofenceId = origin.GeofenceId;
                        await LocationDal.UpdateLocation(revise);
                    }
                    originTask.Location = revise;
                    return true;
                }
            }
            return false;
        }
        public async Task DeleteLocation(Tasks task)
        {
            Location location = task.Location;
            if (location != null)
            {
                if (location.Status == ModelStatusEnum.SYNC_NEW)
                {
                    await LocationDal.DeleteLocatonForever(location.Id);
                }
                else
                {
                    await LocationDal.DeleteLocationLogicById(location.Id);
                }
                task.Location = null;
            }
        }

        private bool CheckAttachmentChanges(Tasks originalTask, Tasks reviseTask)
        {
            List<Attachment> originalAttachs = originalTask.Attachments;
            List<Attachment> reviseAttachs = reviseTask.Attachments;

            if (originalAttachs == null && reviseAttachs == null)
            {
                return false;
            }
            else if (originalAttachs != null && reviseAttachs != null)
            {
                if (originalAttachs.Count <= 0 && reviseAttachs.Count <= 0)
                {
                    return false;
                }
                if (originalAttachs.Count != reviseAttachs.Count)
                {
                    return true;
                }
                HashSet<long> originalIds = new HashSet<long>();
                foreach (var attachment in originalAttachs)
                {
                    originalIds.Add(attachment.Id);
                }
                foreach (var attachment in reviseAttachs)
                {
                    if (!originalIds.Contains(attachment.Id))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }

        }

        public async Task<bool> SaveTask(Tasks originalTask, Tasks reviseTask)
        {
            return await SaveTask(false, originalTask, reviseTask);
        }
        private void FillLocationMap(Dictionary<long, Location> locationMap, List<Location> locations)
        {
            foreach (var location in locations)
            {
                if (!locationMap.ContainsKey(location.TaskId))
                {
                    locationMap.Add(location.TaskId, location);
                }
            }
        }
        private async Task<List<Tasks>> AppendLocations(List<Tasks> tasks, String userId, bool withDeleted)
        {
            if (tasks == null || tasks.Count <= 0)
            {
                return tasks;
            }
            List<Location> locations = await LocationDal.GetLocationsByUserId(userId, withDeleted);
            Dictionary<long, Location> locationMap = new Dictionary<long, Location>();
            FillLocationMap(locationMap, locations);
            foreach (var task in tasks)
            {
                if (locationMap.ContainsKey(task.Id))
                {
                    task.Location = locationMap[task.Id];
                }
            }
            return tasks;
        }
        private async Task<List<Tasks>> AppendLocations(List<Tasks> tasks, String userId)
        {
            return await AppendLocations(tasks, userId, false);
        }
        public async Task<List<Tasks>> GetTasksByProjectSId(String projectSid, String userId, bool withDeleted)
        {
            return await AppendLocations(await TaskDal.GetTasksByProjectSId(projectSid, userId, withDeleted), userId);
        }
        public async Task FinishTask(Tasks task)
        {
            task.TaskStatus = ModelStatusEnum.Task_Finished;
            task.ModifiedTime = DateTime.UtcNow;
            await TaskDal.UpdateAsync(task);
        }
        private async Task<Tasks> AppendLocations(Tasks task)
        {
            if (task == null)
            {
                return null;
            }
            Location location = await LocationDal.GetLocationsByTaskId(task.Id, false);
            task.Location = location;
            return task;
        }

        public async Task SaveCommitResultBackToDB(Dictionary<String, String> id2etag, List<String> deletedIds, String userId, long fromTime)
        {

            //查询提交同步对应的tasks
            List<String> taskSids = new List<String>();
            taskSids.AddRange(id2etag.Keys);
            taskSids.AddRange(deletedIds);
            Dictionary<String, Tasks> tasksMap = await GetCommittedFullTasksMap(userId, taskSids);

            //找出提交期间的CONTENT更新tasks
            //TODO 目前的数据结果无法准确判断期间发生的就是CONTENT更新
            List<String> recentChangedIds = await GetNeedPostTasksContentChanged(userId, fromTime);

            //找出接口中提交的CREATE tasks
            HashSet<String> createTaskIds = await SyncStatusBll.GetEntityIdsByType(userId, ModelStatusEnum.SYNC_TYPE_TASK_CREATE);

            foreach (String id in id2etag.Keys)
            {
                if (!tasksMap.ContainsKey(id))
                {
                    continue;
                }
                Tasks task = tasksMap[id];
                if (task == null)
                {
                    //Log.e(TAG, "$saveCommitResultBackToDB: no task found, sid = " + id);
                    if (LoggerHelper.IS_LOG_ENABLED)
                    {
                        await LoggerHelper.LogToAllChannels(LogLevel.INFO, string.Format("$saveCommitResultBackToDB: no task found, sid = {0}", id));
                    }
                    continue;
                }

                //更新Checklist，Location和Attachment的提交结果
                await ChecklistItemBll.SaveCommitResultBackToDB(task, id2etag[id], userId);
                await LocationBll.SaveCommitResultBackToDB(task, userId);
                await AttachmentBll.SaveCommitResultBackToDB(task);
                if (createTaskIds.Contains(task.SId))
                {
                    //保存CREATE的提交结果
                    await SyncStatusBll.DeleteSyncStatus(userId, id, ModelStatusEnum.SYNC_TYPE_TASK_CREATE);
                    await TaskDal.UpdateEtagToDb(userId, id, id2etag[id]);
                    if (IsNewRecentChanged(task, fromTime))
                    {
                        //提交期间有修改，需要更新task的同步状态
                        await SyncStatusBll.AddSyncStatus(task, ModelStatusEnum.SYNC_TYPE_TASK_CONTENT);
                    }
                }
                else if (recentChangedIds.Contains(id))
                {
                    //提交期间有更新操作，不需要删除CONTENT状态
                    await TaskDal.UpdateEtagToDb(userId, id, id2etag[id]);
                }
                else
                {
                    //提交期间无修改，删除CONTENT同步状态
                    await SyncStatusBll.DeleteSyncStatus(userId, id, ModelStatusEnum.SYNC_TYPE_TASK_CONTENT);
                    await TaskDal.UpdateEtagToDb(userId, id, id2etag[id]);
                }
            }

            //保存MoveToTrash操作结果
            foreach (String sid in deletedIds)
            {
                if (tasksMap.ContainsKey(sid))
                {
                    await SyncStatusBll.DeleteSyncStatus(userId, sid, ModelStatusEnum.SYNC_TYPE_TASK_TRASH);
                }
            }
        }
        public async Task<List<String>> GetNeedPostTasksContentChanged(String userId, long fromTime)
        {
            List<Tasks> tasks = await TaskDal.GetNeedPostTasksContentChanged(userId, fromTime);
            List<String> taskIds = new List<String>();
            foreach (Tasks task in tasks)
            {
                taskIds.Add(task.SId);
            }
            return taskIds;
        }

        // 新增的task在提交server时被修改
        private bool IsNewRecentChanged(Tasks task, long fromTime)
        {
            return task.ModifiedTime != null && task.ModifiedTime.GetAllMilliSeconds() > fromTime;// TODO ModifiedTime.GetTime() 此处有坑
        }

        private async Task<Dictionary<String, Tasks>> GetCommittedFullTasksMap(String userId, List<String> taskSids)
        {
            Dictionary<String, Tasks> map = await TaskDal.GetSidToTasksDic(userId, taskSids);
            await AssembleWholeTasks(map.Values, true);
            return map;
        }

        public async Task ExchangeToNewIdForError(String userId, String taskSid)
        {
            Tasks task = await TaskDal.GetTaskBySid(userId, taskSid);
            if (task == null)
            {
                return;
            }
            String newTaskSid = StringUtils.GenerateShortStringGuid();//  Utils.randomUUID32();
            task.SId = newTaskSid;
            task.Etag = null;
            if (await TaskDal.ExchangeToNewIdForError(userId, taskSid, newTaskSid))
            {
                await ChecklistItemDal.ExchangeToNewTaskSid(userId, taskSid, newTaskSid);
                await AttachmentDal.ExchangeToNewTaskSid(userId, taskSid, newTaskSid);
                await LocationDal.ExchangeToNewTaskSid(userId, taskSid, newTaskSid);
                await new CommentDal().ExchangeToNewTaskSid(taskSid, newTaskSid);
                await SyncStatusBll.DeleteSyncStatusPhysical(userId, taskSid);
                await SyncStatusBll.AddSyncStatus(task, ModelStatusEnum.SYNC_TYPE_TASK_CREATE);
            }
        }
        public async Task ExchangeTaskCreatedToUpdated(String userId, String sid)
        {
            Tasks task = await TaskDal.GetTaskBySid(userId, sid);
            if (task == null)
            {
                return;
            }
            task.Etag = "ETAG_NOT_NULL";
            if (await TaskDal.UpdateEtagToDb(userId, sid, "ETAG_NOT_NULL"))
            {
                await SyncStatusBll.DeleteSyncStatus(userId, sid, ModelStatusEnum.SYNC_TYPE_TASK_CREATE);
                await SyncStatusBll.AddSyncStatus(task, ModelStatusEnum.SYNC_TYPE_TASK_CONTENT);
            }
        }
        public async Task<List<Tasks>> GetNeedPostDeletedTasks(String userId)
        {
            return await TaskDal.GetNeedPostDeletedTasks(userId);
        }
        public async Task<List<Tasks>> GetNeedPostUpdatedFullTasks(String userId)
        {
            List<Tasks> tasks = await TaskDal.GetNeedPostUpdatedTasks(userId);
            if (tasks.Count < 0)
            {
                return tasks;
            }
            await AssembleWholeTasks(tasks, true);
            return tasks;
        }
        public async Task<List<Tasks>> GetNeedPostCreatedFullTasks(String userId)
        {
            List<Tasks> tasks = await TaskDal.GetNeedPostCreatedTasks(userId);
            await AssembleWholeTasks(tasks, true);
            return tasks;
        }

        public async Task<Dictionary<string, Tasks>> GetFullTasksToMap(string userId)
        {
            Dictionary<String, Tasks> tasksDic = await TaskDal.GetAllSidToTasksDic(userId);
            await AssembleWholeTasks(tasksDic.Values, true);
            return tasksDic;
        }

        private async Task AssembleWholeTasks(ICollection<Tasks> tasks, bool withDeleted)
        {
            if (tasks == null || tasks.Count <= 0)
            {
                return;
            }
            HashSet<long> tasksIds = new HashSet<long>();
            foreach (var item in tasks)
            {
                tasksIds.Add(item.Id);
            }
            Dictionary<int, Location> locationDic = await LocationDal.GetLocationsByTaskIds(tasksIds, withDeleted);
            Dictionary<int, List<ChecklistItem>> itemDic = await ChecklistItemDal.GetCheckListItemsByTaskIds(tasksIds, withDeleted);
            Dictionary<int, List<Attachment>> attachmentDic = await AttachmentDal.GetAttachmentsByTaskIds(tasksIds, withDeleted);
            Dictionary<int, List<TaskReminder>> reminderMap = await TaskReminderDal.GetTaskRemindersMap(tasksIds);
            //List<Location> locations = await LocationDal.GetLocationsByUserId(userId, true);
            //List<ChecklistItem> checklistItems = await ChecklistItemDal.GetAllCheckListItems(userId, true);
            //List<Attachment> attachments = await AttachmentBll.GetAllAttachment(userId, true);

            //FillLocationDic(locationDic, locations);

            //FillChecklistItemDic(itemDic, checklistItems);

            //FillAttachmentDic(attachmentDic, attachments);

            foreach (var task in tasks)
            {
                int taskId = task.Id;
                if (locationDic.ContainsKey(taskId))
                {
                    task.Location = locationDic[taskId];
                }
                if (itemDic.ContainsKey(taskId))
                {
                    task.ChecklistItems = itemDic[taskId];
                }
                if (attachmentDic.ContainsKey(taskId))
                {
                    task.Attachments = attachmentDic[taskId];
                }
                if (reminderMap.ContainsKey(taskId))
                {
                    task.Reminders = reminderMap[taskId];
                }
            }
        }


        private void FillLocationDic(Dictionary<int, Location> locationDic, List<Location> locations)
        {
            foreach (var location in locations)
            {
                if (!locationDic.ContainsKey(location.TaskId))
                {
                    locationDic.Add(location.TaskId, location);
                }
            }
        }
        private void FillChecklistItemDic(Dictionary<int, List<ChecklistItem>> itemDic, List<ChecklistItem> checklistItems)
        {
            foreach (var item in checklistItems)
            {
                if (itemDic.ContainsKey(item.TaskId))
                {
                    itemDic[item.TaskId].Add(item);
                }
                else
                {
                    List<ChecklistItem> values = new List<ChecklistItem>();
                    values.Add(item);
                    itemDic.Add(item.TaskId, values);
                }
            }
        }

        private void FillAttachmentDic(Dictionary<int, List<Attachment>> attachmentDic, List<Attachment> attachments)
        {
            foreach (var attachment in attachments)
            {
                if (attachmentDic.ContainsKey(attachment.TaskId))
                {
                    attachmentDic[attachment.TaskId].Add(attachment);
                }
                else
                {
                    List<Attachment> values = new List<Attachment>();
                    values.Add(attachment);
                    attachmentDic.Add(attachment.TaskId, values);
                }
            }
        }
        public async Task DeleteTaskIntoTrash(Tasks task)
        {
            task.Deleted = ModelStatusEnum.DELETED_TRASH;
            await TaskDal.UpdateAsync(task);
            task.Assignee = Constants.Removed.ASSIGNEE;
            await UpdateTaskAssignee(task);
            //ContentValues values = new ContentValues();
            //values.put(Task2Field._deleted.name(), task.getDeleted());
            //task2Dao.updateTaskByValues(task.getId(), values);
            //task.setAssignee(Removed.ASSIGNEE);
            //updateTaskAssignee(task);
        }
        public async Task UpdateTaskAssignee(Tasks task)
        {
            if (await TaskDal.UpdateTaskAssignee(task.Id, task.Assignee))
            {
                await SyncStatusBll.AddSyncStatus(task, ModelStatusEnum.SYNC_TYPE_TASK_ASSIGN);
            }
        }

        public async Task DeleteTaskIntoTrash(List<Tasks> deleteTasks)
        {
            await TaskDal.DeleteTaskIntoTrash(deleteTasks);
        }

        public async Task DeleteTasksPhysical(List<Tasks> deleteTasks)
        {
            foreach (var item in deleteTasks)
            {
                await ChecklistItemDal.DeleteChecklistPhysicalByTaskId(item.Id);

                await LocationDal.DeleteLocationsPhysicalByTaskId(item.Id);
                await TaskReminderDal.DeleteRemindersPhysicalByTaskId(item.Id);

                await AttachmentBll.DeleteAttachmentForeverByTaskId(item.Id);
                await CommentBll.DeleteCommentsByTaskSIdForever(item.SId, item.UserId);
                await TaskDal.DeleteTaskPhysical(item);
                await SyncStatusBll.DeleteSyncStatusPhysical(item.UserId, item.SId);
                await TaskSyncedJsonBll.DeleteTaskSyncedJsonPhysical(item.SId, item.UserId);
            }
        }
        public async Task BatchCreateTasksFromRemote(List<Tasks> adds)
        {
            foreach (var add in adds)
            {
                if (await CreateTask(add))
                {
                    await SaveMergedChecklistItems(add);
                }
            }
        }

        private async Task<bool> CreateTask(Tasks task)
        {
            // TODO 如果task已存在，是否创建？即是否允许存在sid相同的task

            if (string.IsNullOrEmpty(task.SId))
            {
                task.SId = StringUtils.GenerateShortStringGuid(); //  Utils.randomUUID32());
            }
            if (task.SortOrder == null)
            {
                task.SortOrder = await GetNewTaskSortOrderInProject(task.ProjectId);
            }
            if (string.IsNullOrEmpty(task.TimeZone))
            {
                // TODO 可能需要通过getRawOffset(),自己组装GMT+0800的格式
                task.TimeZone = NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault().Id;//.setTimeZone(TimeZone.getDefault().getID());
            }

            if (string.IsNullOrEmpty(task.RepeatTaskId))
            {
                task.RepeatTaskId = task.SId;
            }

            if (task.Title == null)
            {
                task.Title = string.Empty;
            }
            if (await TaskDal.CreateTask(task))
            {
                if (task.HasReminder())
                {
                    await SaveReminders(task);
                }
                return true;
            }
            return false;
        }
        public async Task<long> GetNewTaskSortOrderInProject(string projectId)
        {
            long? minOrder = await TaskDal.GetMinTaskSortOrderInProject(projectId);
            if (minOrder == null)
            {
                return 0L;
            }
            return minOrder.Value - Constants.OrderStepData.STEP;
        }
        public async Task BatchUpdateTasksFromRemote(TaskSyncBean taskSyncBean)
        {
            //云端修改覆盖本地，无需再Post该Task
            foreach (var update in taskSyncBean.Updated)
            {
                await TaskDal.UpdateTaskWithoutModifyDate(update);

                await SaveReminders(update);
                await SaveMergedChecklistItems(update);
            }

            //云端和本地Task执行合并操作后，需要把更新内容Post到云端
            foreach (var updating in taskSyncBean.Updating)
            {
                if (await TaskDal.UpdateTaskContentWithoutModifyDate(updating))
                {
                    await SyncStatusBll.AddSyncStatus(updating, ModelStatusEnum.SYNC_TYPE_TASK_CONTENT);
                }
                await SaveReminders(updating);
                await SaveMergedChecklistItems(updating);
            }
        }
        public async Task<Dictionary<String, int>> GetTaskSidToIdDic(String userId)
        {
            return await TaskDal.GetTaskSidToIdDic(userId);
        }

        #region 弃用
        //public async Task<List<Tasks>> GetAllTasksByTypeFlag(int typeFlag)
        //{
        //    //return await TaskDal.GetAllTasksByTypeFlagAndStatus(typeStatus, ModelStatusEnum.Task_Not_Finished);
        //    //return await (await TaskDal.ExecuteAsyncQueryTable()).Where((t) => t.Type == typeFlag && t.TaskStatus == ModelStatusEnum.Task_Not_Finished).ToListAsync();
        //    return await GetTasksByWhere(string.Empty, ModelStatusEnum.Task_Not_Finished, typeFlag);
        //}

        //public async Task<List<Tasks>> GetAllTasksByStatus(int status)
        //{
        //    //return await TaskDal.GetAllTasksByTypeFlagAndStatus(ModelStatusEnum.SYNC_TYPE_TASK_CONTENT, status);
        //    return await (await TaskDal.ExecuteAsyncQueryTable()).Where((t) => t.Type == ModelStatusEnum.SYNC_TYPE_TASK_CONTENT && t.TaskStatus == status).ToListAsync();
        //} 
        #endregion

        #region 暂时弃用
        //public async Task<List<Tasks>> GetAllTasksByTaskStatus(int taskStatus)
        //{
        //    return await GetTasksByProjectId(string.Empty, taskStatus);
        //}

        //public async Task<List<Tasks>> GetTasksByProjectId(string proId, int taskStatus)
        //{
        //    return await GetTasksByWhere(proId, taskStatus, ModelStatusEnum.SYNC_TYPE_TASK_CONTENT);
        //}

        //public async Task<List<Tasks>> GetTasksByWhere(string proId, int taskStatus, int taskType)
        //{
        //    var queryResultTask = (await TaskDal.ExecuteAsyncQueryTable()).Where((t) => t.Type == taskType && t.TaskStatus == taskStatus);
        //    if (!string.IsNullOrEmpty(proId))
        //    {
        //        //return await (await TaskDal.ExecuteAsyncQueryTable()).Where((t) => t.Type == taskType && t.TaskStatus == taskStatus).ToListAsync();
        //        queryResultTask = queryResultTask.Where((t) => t.ProjectId == proId);
        //    }
        //    return await queryResultTask.ToListAsync();
        //    //return await (await TaskDal.ExecuteAsyncQueryTable()).Where((t) => t.ProjectId == proId && t.Type == taskType && t.TaskStatus == taskStatus).ToListAsync();
        //} 
        #endregion

        /// <summary>
        ///与上面不同的是，这个方法可以查找所有task，然后在外面进行删选，这样在多次处理同一个表数据时，这个效率更高
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tasks>> GetAllTasksDeletedNo()
        {
            return await GetAllTasksByProjectIdDeletedNo(string.Empty);
        }
        public async Task<List<Tasks>> GetAllTasksByProjectIdDeletedNo(string proId)
        {
            return await GetAllTasksByProjectIdDeletedNo(proId, TasksSortEnum.Custom_Sort);
        }
        public async Task<List<Tasks>> GetAllTasksByProjectIdDeletedNo(string proId, TasksSortEnum tasksSortEnum)
        {
            var queryResultTask = (await TaskDal.ExecuteAsyncQueryTable()).Where((t) => t.Type == ModelStatusEnum.SYNC_TYPE_TASK_CONTENT);

            if (!string.IsNullOrEmpty(proId))
            {
                queryResultTask = queryResultTask.Where((t) => t.ProjectId == proId);
            }
            switch (tasksSortEnum)
            {
                case TasksSortEnum.Custom_Sort:
                    queryResultTask = queryResultTask.OrderBy((t) => t.SortOrder);
                    break;
                case TasksSortEnum.DateTime_Sort:
                    queryResultTask = queryResultTask.OrderBy((t) => t.DueDate);
                    break;
                case TasksSortEnum.Title_Sort:
                    queryResultTask = queryResultTask.OrderBy((t) => t.Title);
                    break;
                case TasksSortEnum.Priorities_Sort:
                    queryResultTask = queryResultTask.OrderBy((t) => t.Priority);
                    break;
                default:
                    queryResultTask = queryResultTask.OrderBy((t) => t.SortOrder);
                    break;
            }
            return await queryResultTask.ToListAsync();
        }
        #endregion

        #region android代码
        //    public void batchUpdateTasksFromRemote(final TaskSyncBean taskSyncBean) {
        //    dbHelper.doInTransaction(new Transactable<Void>() {

        //        @Override
        //        public Void doIntransaction(GTasksDBHelper dbHelper) {

        //            //云端修改覆盖本地，无需再Post该Task
        //            for (Task2 update : taskSyncBean.getUpdated()) {
        //                task2Dao.updateTaskWithoutModifyDate(update);
        //                saveMergedChecklistItems(update);
        //            }

        //            //云端和本地Task执行合并操作后，需要把更新内容Post到云端
        //            for (Task2 updating : taskSyncBean.getUpdating()) {
        //                if (task2Dao.updateTaskContentWithoutModifyDate(updating)) {
        //                    syncStatusService.addSyncStatus(updating, Status.SYNC_TYPE_TASK_CONTENT);
        //                }
        //                saveMergedChecklistItems(updating);
        //            }
        //            return null;
        //        }
        //    });
        //}
        //    private void fillAttachmentMap(HashMap<Long, ArrayList<Attachment>> attachmentMap,
        //        ArrayList<Attachment> attachments) {
        //    for (Attachment attachment : attachments) {
        //        if (attachmentMap.containsKey(attachment.getTaskId())) {
        //            attachmentMap.get(attachment.getTaskId()).add(attachment);
        //        } else {
        //            ArrayList<Attachment> values = new ArrayList<Attachment>();
        //            values.add(attachment);
        //            attachmentMap.put(attachment.getTaskId(), values);
        //        }
        //    }
        //}
        //    private void fillChecklistItemMap(HashMap<Long, ArrayList<ChecklistItem>> itemMap,
        //        ArrayList<ChecklistItem> checklistItems) {
        //    for (ChecklistItem item : checklistItems) {
        //        if (itemMap.containsKey(item.getTaskId())) {
        //            itemMap.get(item.getTaskId()).add(item);
        //        } else {
        //            ArrayList<ChecklistItem> values = new ArrayList<ChecklistItem>();
        //            values.add(item);
        //            itemMap.put(item.getTaskId(), values);
        //        }
        //    }
        //}
        //    private void fillLocationMap(HashMap<Long, Location> locationMap,
        //        ArrayList<Location> locations) {
        //    for (Location location : locations) {
        //        if (!locationMap.containsKey(location.getTaskId())) {
        //            locationMap.put(location.getTaskId(), location);
        //        }
        //    }
        //}
        #endregion

        protected override void SetCurrentDal()
        {
            CurrentDal = TaskDal;
        }

        public async Task DeleteForeverByProjectId(string proId)
        {
            var queryResult = await GetAllTasksByProjectIdDeletedNo(proId);
            await DeleteTaskIntoTrash(queryResult);
        }
        public async Task<Tasks> AddTasks(Tasks task)
        {
            //task.setRepeatAlertTime();
            //task.setTagsInner();
            if (await CreateTask(task))
            {
                await SyncStatusBll.AddSyncStatus(task, ModelStatusEnum.SYNC_TYPE_TASK_CREATE);
            }
            return task;
        }


        public async Task<Tasks> GetTasksByTasksId(int tasksId)
        {
            return await (await TaskDal.ExecuteAsyncQueryTable()).Where(t => t.Id == tasksId && t.Deleted == ModelStatusEnum.DELETED_NO).FirstOrDefaultAsync();
        }

        public async Task<Tasks> GetTasksWithCheckListByTasksId(int tasksId)
        {
            var tasks = await GetTasksByTasksId(tasksId);
            tasks.ChecklistItems = await ChecklistItemDal.GetChecklistItemsByTaskId(tasks.Id, string.Empty, false);
            return tasks;
        }
    }
}
