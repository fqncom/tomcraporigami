using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Bll;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Helper;
using TickTick.Models;
using TickTick.Synchronous.Collector;
using WindowsUniversalLogger.Interfaces;

namespace TickTick.Handler
{
    public class TaskBatchHandler : BatchHandler
    {
        private TaskBll TaskBll { get; set; }
        private TaskSyncCollector TaskSyncCollector { get; set; }
        private LocationBll LocationBll { get; set; }
        private AttachmentBll AttachmentBll { get; set; }
        private TaskSyncedJsonBll TaskSyncedJsonBll { get; set; }
        private SyncStatusBll SyncStatusBll { get; set; }
        public TaskBatchHandler(String userId, SyncResult syncResult)
            : base(userId, syncResult)
        {
            TaskBll = new TaskBll();//application.getTaskService();
            LocationBll = new LocationBll();//new LocationBll(application);
            AttachmentBll = new AttachmentBll();//application.AttachmentService();
            //SyncStatusService = new SyncStatusService(application.getDBHelper());
            TaskSyncCollector = new TaskSyncCollector(userId);
            TaskSyncedJsonBll = new TaskSyncedJsonBll();//new TaskSyncedJsonService(application.getDBHelper());
            SyncStatusBll = new SyncStatusBll();// new SyncStatusService(application.getDBHelper());
        }
        public async Task<List<Tasks>> GetLocalDeletedTasks()
        {
            return await TaskBll.GetNeedPostDeletedTasks(userId);
        }
        public async Task<List<Tasks>> GetLocalContentUpdatedTasks()
        {
            return await TaskBll.GetNeedPostUpdatedFullTasks(userId);
        }
        public async Task<List<Tasks>> GetLocalCreatedTasks()
        {
            return await TaskBll.GetNeedPostCreatedFullTasks(userId);
        }
        public async Task HandleCommitResult(Dictionary<String, String> id2eTag, List<String> errorIds, List<SyncTaskBean> taskBeans, long lastPostPoint)
        {
            if (LoggerHelper.IS_LOG_ENABLED)
            {
                await LoggerHelper.LogToAllChannels(null, string.Format("Post Tasks Result success Num ={0} ", id2eTag.Count));
            }

            Dictionary<String, String> addTaskProjectMap = new Dictionary<String, String>();
            List<TasksProjects> delete = new List<TasksProjects>();
            List<TasksServer> add = new List<TasksServer>();
            List<TasksServer> update = new List<TasksServer>();

            if (taskBeans != null)
            {
                foreach (SyncTaskBean taskBean in taskBeans)
                {
                    delete.AddRange(taskBean.Delete);
                    add.AddRange(taskBean.Add);
                    update.AddRange(taskBean.Update);
                }
            }
            TaskSyncedJsonBean jsonBean = new TaskSyncedJsonBean();
            List<String> deleteIds = new List<String>();
            foreach (TasksProjects taskProject in delete)
            {
                if (!errorIds.Contains(taskProject.TaskId))
                {
                    deleteIds.Add(taskProject.TaskId);

                    TaskSyncedJson taskSyncedJson = new TaskSyncedJson();
                    taskSyncedJson.TaskSID = taskProject.TaskId;
                    taskSyncedJson.UserId = userId;
                    jsonBean.AddToDeleted(taskSyncedJson);
                }
            }
            foreach (var task in add)
            {
                if (id2eTag.ContainsKey(task.Id.ToString()))
                {
                    jsonBean.AddToAdded(task);
                    addTaskProjectMap.Add(task.Id.ToString(), task.ProjectId);
                }
            }
            foreach (var task in update)
            {
                if (id2eTag.ContainsKey(task.Id.ToString()))
                {
                    jsonBean.AddToUpdated(task);
                }
            }
            await TaskSyncedJsonBll.SaveTaskSyncedJsons(jsonBean, userId);
            await TaskBll.SaveCommitResultBackToDB(id2eTag, deleteIds, userId, lastPostPoint);
        }

        public async Task<List<String>> HandleErrorResult(Dictionary<String, ErrorType> idToError)
        {
            List<String> errorIds = new List<String>();
            foreach (String sId in idToError.Keys)
            {
                //Log.e(TAG, "$handleErrorResult : unexpected # Error: " + id2error[sid]
                //        + " # Id: " + sid);
                if (LoggerHelper.IS_LOG_ENABLED)
                {
                    await LoggerHelper.LogToAllChannels(LogLevel.ERROR, string.Format("$handleErrorResult : unexpected # Error: {0} # Id: {1}", idToError[sId], sId));
                }
                //application.getAnalyticsInstance().sendException(
                //        "TaskSyncError" + idToError.get(sId) + " # Id: " + sId + application
                //                .getAccountManager().getCurrentUser().getUsername()
                //);
                if (LoggerHelper.IS_LOG_ENABLED)
                {
                    await LoggerHelper.LogToAllChannels(LogLevel.ERROR, string.Format("Post Tasks Errors : [ id = {0}, ErrorCode = {1}]", sId, idToError[sId]));
                }
                switch (idToError[sId])
                {
                    case ErrorType.EXISTED:
                        await TaskBll.ExchangeTaskCreatedToUpdated(userId, sId);
                        errorIds.Add(sId);
                        break;
                    case ErrorType.DELETED:
                        await TaskBll.ExchangeToNewIdForError(userId, sId);
                        errorIds.Add(sId);
                        break;
                    case ErrorType.NOT_EXISTED:
                        await TaskBll.ExchangeToNewIdForError(userId, sId);
                        errorIds.Add(sId);
                        break;
                    case ErrorType.UNKNOWN:
                        break;
                    default:
                        if (LoggerHelper.IS_LOG_ENABLED)
                        {
                            await LoggerHelper.LogToAllChannels(LogLevel.ERROR, string.Format("$handleErrorResult : unexpected # Error: {0} # Id:{1} ", idToError[sId], sId));
                        }
                        //除了新增操作，其他操作不再执行
                        await SyncStatusBll.DeleteSyncStatusForeverExceptType(userId, sId, ModelStatusEnum.SYNC_TYPE_TASK_CREATE);
                        break;
                }
                // TODO 为什么一次循环之后，就直接终止了循环
                break;
            }
            return errorIds;
        }
        public async Task MergeWithServer(SyncTaskBean syncTaskBean)
        {
            Dictionary<string, Tasks> localTasks = await TaskBll.GetFullTasksToMap(userId);

            TaskSyncModel taskSyncModel = await TaskSyncCollector.CollectSyncTaskBean(syncTaskBean, localTasks);

            await BatchSaveTaskSyncModel(taskSyncModel);

            //尝试删除同步产生的已删除Attachment和文件清理 
            // TODO 暂时不实现
            //new AttachmentFileHelper().TryCleanDeletedAttachmentFile();
        }
        private async Task BatchSaveTaskSyncModel(TaskSyncModel taskSyncModel)
        {
            await SaveTaskSyncBean(taskSyncModel.TaskSyncBean);
            await SaveTaskOtherEntity(taskSyncModel);
            await SaveTaskJsonStrings(taskSyncModel);
        }
        private async Task SaveTaskJsonStrings(TaskSyncModel taskSyncModel)
        {
            await TaskSyncedJsonBll.SaveTaskSyncedJsons(taskSyncModel.TaskSyncedJsonBean, userId);
        }
        /**
     * 批量保存Task下面的Location和Attachment
     * @param taskSyncModel
     */
        private async Task SaveTaskOtherEntity(TaskSyncModel taskSyncModel)
        {
            LocationSyncBean locationSyncBean = taskSyncModel.LocationSyncBean;
            AttachmentSyncBean attachmentSyncBean = taskSyncModel.AttachmentSyncBean;
            if (locationSyncBean.IsEmpty() && attachmentSyncBean.IsEmpty())
            {
                return;
            }
            Dictionary<String, int> taskIdDic = await TaskBll.GetTaskSidToIdDic(userId);
            if (!locationSyncBean.IsEmpty())
            {
                if (LoggerHelper.IS_LOG_ENABLED)
                {
                    await LoggerHelper.LogToAllChannels(null,"Save remote location, " + locationSyncBean.ToString());
                }
                await LocationBll.SaveServerMergeToDB(locationSyncBean, userId, taskIdDic);
            }
            if (!attachmentSyncBean.IsEmpty())
            {
                if (LoggerHelper.IS_LOG_ENABLED)
                {
                    await LoggerHelper.LogToAllChannels(null, "Save remote attachment, " + attachmentSyncBean.ToString());
                }
                await AttachmentBll.SaveServerMergeToDB(attachmentSyncBean, userId, taskIdDic);
            }
        }
        private async Task SaveTaskSyncBean(TaskSyncBean taskSyncBean)
        {
            if (taskSyncBean.IsEmpty())
            {
                //没有更改需要保存
                return;
            }

            if (LoggerHelper.IS_LOG_ENABLED)
            {
                await LoggerHelper.LogToAllChannels(null, "Save Remote Task, " + taskSyncBean.ToString());
            }
            mSyncResult.RemoteTaskChanged = true;

            if (taskSyncBean.DeletedInTrash.Count > 0)
            {
                await TaskBll.DeleteTaskIntoTrash(taskSyncBean.DeletedInTrash);
            }

            if (taskSyncBean.DeletedForever.Count > 0)
            {
                await TaskBll.DeleteTasksPhysical(taskSyncBean.DeletedForever);
            }

            if (taskSyncBean.Added.Count > 0)
            {
                await TaskBll.BatchCreateTasksFromRemote(taskSyncBean.Added);
            }

            if (taskSyncBean.Updated.Count > 0)
            {
                await TaskBll.BatchUpdateTasksFromRemote(taskSyncBean);
            }
        }
    }
}
