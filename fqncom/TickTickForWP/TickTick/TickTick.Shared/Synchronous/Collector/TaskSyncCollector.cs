using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Bll;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Helper;
using TickTick.Models;
using TickTick.Synchronous.Transfer;
using TickTick.Utilities;

namespace TickTick.Synchronous.Collector
{
    public class TaskSyncCollector
    {
        private SyncStatusBll SyncStatusBll = new SyncStatusBll();
        private TaskSyncedJsonBll TaskSyncedJsonBll = new TaskSyncedJsonBll();
        private ProjectBll ProjectBll = new ProjectBll();
        private LocationSyncBean LocationSyncBean = new LocationSyncBean();

        private static String UserId;
        public TaskSyncCollector(string userId)
        {
            UserId = userId;
        }

        public async Task<TaskSyncModel> CollectSyncTaskBean(SyncTaskBean syncTaskBean, Dictionary<String, Tasks> localTasks)
        {
            TaskSyncModel taskSyncModel = new TaskSyncModel();

            await CollectDeletedTasksFromRemoteModel(syncTaskBean, localTasks, taskSyncModel);

            List<TasksServer> update = syncTaskBean.Update;//UpdateChangeToTasks(syncTaskBean.Update);

            if (update.Count <= 0)
            {
                return taskSyncModel;
            }
            await MergeUpdatedTasksFromService(localTasks, taskSyncModel, update);
            return taskSyncModel;
        }



        private async Task CollectDeletedTasksFromRemoteModel(SyncTaskBean syncTaskBean, Dictionary<String, Tasks> localTasks, TaskSyncModel taskSyncModel)
        {

            CollectDeleteForeverTasks(syncTaskBean, localTasks, taskSyncModel);

            await CollectDeleteInTrash(syncTaskBean, localTasks, taskSyncModel);

        }
        private async Task CollectDeleteInTrash(SyncTaskBean syncTaskBean, Dictionary<String, Tasks> localTasks, TaskSyncModel taskSyncModel)
        {

            //DELETED_IN_TRASH操作需要和本地task进行Merge
            List<TasksProjects> deletedTrashTasks = syncTaskBean.DeletedInTrash;//DeleteChangeToTaskProjects(syncTaskBean.Delete);//此处有坑DeletedInTrash;

            if (deletedTrashTasks.Count <= 0)
            {
                return;
            }

            HashSet<String> RestoreTaskIds = await SyncStatusBll.GetEntityIdsByType(UserId, ModelStatusEnum.SYNC_TYPE_TASK_RESTORE);

            foreach (var taskProject in deletedTrashTasks)
            {
                String taskSid = taskProject.TaskId;
                //本地restore操作覆盖remote删除操作
                if (RestoreTaskIds.Contains(taskSid))
                {
                    continue;
                }
                Tasks localTask = localTasks[taskSid];
                if (localTask != null)
                {
                    taskSyncModel.AddDeletedInTrashTask(localTask);
                }
            }
        }

        private void CollectDeleteForeverTasks(SyncTaskBean syncTaskBean, Dictionary<String, Tasks> localTasks, TaskSyncModel taskSyncModel)
        {
            // DELETED_FOREVER操作直接删除本地task，并清楚TaskSyncedJson
            List<TasksProjects> deletedForeverTasks = syncTaskBean.DeletedForever;//DeleteChangeToTaskProjects(syncTaskBean.Delete);
            foreach (var taskProject in deletedForeverTasks)
            {
                String taskSid = taskProject.TaskId;
                if (!localTasks.ContainsKey(taskSid))
                {
                    continue;
                }
                Tasks localTask = localTasks[taskSid];
                if (localTask != null)
                {
                    taskSyncModel.AddDeletedForeverTask(localTask);
                    localTasks.Remove(taskSid);
                    // delete original task
                    TaskSyncedJson json = new TaskSyncedJson();
                    json.UserId = localTask.UserId;
                    json.TaskSID = taskSid;
                    taskSyncModel.AddDeletedTaskSyncedJson(json);
                }
            }

        }


        private List<TasksProjects> DeleteChangeToTaskProjects(List<TasksProjects> delete)
        {
            List<TasksProjects> tasksProjectsList = new List<TasksProjects>();
            foreach (var item in delete)
            {
                tasksProjectsList.Add(new TasksProjects { ProjectId = item.ProjectId, SortOrder = item.SortOrder, TaskId = item.TaskId });
            }
            return tasksProjectsList;
        }

        /// <summary>
        /// 转换，暂时弃用
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        private List<Tasks> UpdateChangeToTasks(List<Tasks> update)
        {
            List<Tasks> tasksList = new List<Tasks>();
            foreach (var item in update)
            {
                tasksList.Add(new Tasks
                {
                    Id = item.Id,
                    Location = item.Location,
                    CreatedTime = item.CreatedTime,
                    UserCount = item.UserCount,
                    Title = item.Title,
                    ChecklistItems = item.ChecklistItems,
                    TimeZone = item.TimeZone,
                    TaskStatus = item.TaskStatus,
                    Type = item.Type,
                    UserId = item.UserId,
                    Tags = item.Tags,
                    Status = item.Status,
                    SortOrder = item.SortOrder,
                    SId = item.Id.ToString(),
                    RepeatTaskId = item.RepeatTaskId,
                    //RepeatReminderTime
                    RepeatFrom = item.RepeatFrom,
                    RepeatFlag = item.RepeatFlag,
                    RepeatFirstDate = item.RepeatFirstDate,
                    RemindTime = item.RemindTime,
                    Reminder = item.Reminder,
                    //ReminderTime
                    Projects  = item.Projects,
                    ProjectId = item.ProjectId,
                    Priority = item.Priority,
                    ModifiedTime = item.ModifiedTime,
                    //LocalId
                    Kind = item.Kind,
                    IsOwner = item.IsOwner,
                    //HasAttachment
                    //GoogleId
                    Etimestamp = item.Etimestamp,
                    Etag = item.Etag,
                    DueDate = item.DueDate,
                    Deleted = item.Deleted,
                    Creator = item.Creator,
                    Content = item.Content,
                    CompletedTime = item.CompletedTime,
                    CommentCount = item.CommentCount,
                    Attachments = item.Attachments,
                    Assignee = item.Assignee
                });
            }
            return tasksList;
        }

        public async Task MergeUpdatedTasksFromService(Dictionary<String, Tasks> localTasks, TaskSyncModel taskSyncModel, List<TasksServer> updateServerTasks)
        {
            Dictionary<String, TaskSyncedJson> originalJsons = await TaskSyncedJsonBll.GetAllTaskSyncedJsonDic(UserId);
            Dictionary<String, String> moveListTasksDic = await SyncStatusBll.GetMoveFromIdDic(UserId);
            HashSet<String> contentChangeTaskIds = await SyncStatusBll.GetContentChangeEntityIds(UserId);

            HashSet<String> orderInGroupTaskIds = await SyncStatusBll.GetSortOrderChangeEntityIds(UserId);
            HashSet<String> assignTaskIds = await SyncStatusBll.GetAssignEntityIds(UserId);
            HashSet<String> moveToTrashStatusIds = await SyncStatusBll.GetMoveToTrashEntityIds(UserId);
            Dictionary<String, string> projectIdDic = await ProjectBll.GetProjectSidToIdsDic(UserId);

            //ObjectMapper mapper = new ObjectMapper();//json与对象映射，可以使用newton.json代替

            foreach (var serverTask in updateServerTasks)
            {
                //本地没有找到对应是Project，Task不添加到本地
                if (serverTask.ProjectId == null || !projectIdDic.ContainsKey(serverTask.ProjectId))
                {
                    //Log.e(TAG, "Local project not found : project_id = " + serverTask.getProjectId());
                    continue;
                }

                //Tasks localTask = localTasks[serverTask.Id];
                string projectId = projectIdDic[serverTask.ProjectId];
                if (serverTask.Id != null && localTasks.ContainsKey(serverTask.Id))
                {
                    Tasks localTask = localTasks[serverTask.Id];// 索引器必须有值，否则会报错    
                    if (LoggerHelper.IS_LOG_ENABLED)
                    {
                        await LoggerHelper.LogToAllChannels(null, "serverTask.etag = " + serverTask.Etag + " , localTask.etag = " + localTask.Etag);
                    }

                    // 本地的DELETE_FOREVER操作，舍弃server端的任何修改
                    if (localTask.IsDeletedForever())
                    {
                        continue;
                    }

                    // Etag作为判断两个版本是否一直的唯一标识
                    if (string.Equals(serverTask.Etag, localTask.Etag))
                    {
                        continue;
                    }

                    await MergeMoveListAction(moveListTasksDic, serverTask, localTask, projectId);

                    MergeTaskAssignee(assignTaskIds, serverTask, localTask);

                    await LocationSyncCollector.CollectRemoteLocations(serverTask, localTask, taskSyncModel.LocationSyncBean);

                    await AttachmentSyncCollector.CollectRemoteAttachments(serverTask, localTask, taskSyncModel.AttachmentSyncBean);

                    bool needMove2Trash = moveToTrashStatusIds.Contains(localTask.SId);

                    if (contentChangeTaskIds.Contains(localTask.SId) || orderInGroupTaskIds.Contains(localTask.SId))
                    {
                        // 先查找对应的Original版本
                        TaskSyncedJson originalJson = originalJsons[serverTask.Id];
                        Tasks originalTask = null;
                        if (originalJson != null)
                        {
                            originalTask = TaskTransfer.ConvertTaskSyncedJsonToLocal(originalJsons[serverTask.Id]);//这个方法中android的mapper可以在方法中使用newton.json替代进行json转对象的操作
                        }

                        // 没有Original版本时，不进行Merge，直接Local覆盖Server
                        if (originalTask != null)
                        {
                            //执行merge操作
                            Tasks deltaTask = TaskTransfer.ConvertServerTaskToLocalWithChecklistItem(serverTask);
                            TaskUtils.MergeTask(originalTask, deltaTask, localTask);

                            // 设置其他属性
                            localTask.Kind = localTask.ChecklistItems.Count <= 0 ? Constants.Kind.TEXT : Constants.Kind.CHECKLIST;
                            localTask.Etag = serverTask.Etag;
                            //localTask.RepeatReminderTime = localTask.IsRepeatTask() ? localTask.SnoozeRemindTime : DateTime.MinValue;
                            String repeatFromRemote = serverTask.RepeatFrom;
                            localTask.RepeatFrom = string.IsNullOrEmpty(repeatFromRemote) ? Constants.RepeatFromStatus.DEFAULT : repeatFromRemote;

                            if (needMove2Trash)
                            {
                                localTask.Deleted = ModelStatusEnum.DELETED_TRASH;
                            }
                            taskSyncModel.AddUpdatingTask(localTask);
                        }
                    }
                    else
                    {
                        TaskTransfer.ConvertServerTaskToLocalWithChecklistItem(localTask, serverTask);
                        if (needMove2Trash)
                        {
                            localTask.Deleted = ModelStatusEnum.DELETED_TRASH;
                        }
                        taskSyncModel.AddUpdatedTask(localTask);
                    }
                    taskSyncModel.AddUpdatedTaskSyncedJson(serverTask);
                }
                else
                {
                    await AddRemoteTaskToLocal(taskSyncModel, serverTask, projectId);
                }

            }
        }
        private async Task AddRemoteTaskToLocal(TaskSyncModel taskSyncModel, TasksServer serverTask, string projectId)
        {
            taskSyncModel.AddAddedTaskSyncedJson(serverTask);

            if (HasLocation(serverTask))
            {
                taskSyncModel.AddUpdateLocation(await LocationTransfer.ConvertServerToLocal(serverTask));
            }

            if (HasAttachment(serverTask))
            {
                taskSyncModel.AddAllAddedAttachments(await AttachmentTransfer.ConvertServerToLocal(serverTask.Attachments, UserId, serverTask.Id));
            }

            Tasks newTask = TaskTransfer.ConvertServerTaskToLocalWithChecklistItem(serverTask);
            newTask.ProjectId = projectId;
            newTask.UserId = UserId;
            newTask.HasAttachment = serverTask.Attachments != null && serverTask.Attachments.Count > 0;
            taskSyncModel.AddAddedTask(newTask);
        }
        private bool HasAttachment(TasksServer serverTask)
        {
            return serverTask.Attachments != null && serverTask.Attachments.Count > 0;
        }
        public void AddUpdateLocation(Location update)
        {
            LocationSyncBean.AddUpdateLocation(update);
        }
        private bool HasLocation(TasksServer serverTask)
        {
            return serverTask.Location != null;
        }
        private void MergeTaskAssignee(HashSet<String> assignTaskIds, TasksServer serverTask, Tasks localTask)
        {
            if (assignTaskIds.Contains(serverTask.Id))
            {
                //本地覆盖Server assignee
                serverTask.Assignee = localTask.Assignee;
            }
        }

        private async Task MergeMoveListAction(Dictionary<String, String> moveListTasksMap, TasksServer serverTask, Tasks localTask, string projectId)
        {
            if (string.Equals(serverTask.ProjectId, localTask.ProjectSid))
            {
                if (moveListTasksMap.ContainsKey(localTask.SId))
                {
                    //local和server做相同move to操作，直接删除move to同步状态
                    await SyncStatusBll.DeleteSyncStatus(localTask.UserId, localTask.SId, ModelStatusEnum.SYNC_TYPE_TASK_MOVE);
                }
            }
            else
            {
                if (moveListTasksMap.ContainsKey(localTask.SId))
                {
                    //需要更新moveFromProjectId，因为server也做了move操作，moveFromProjectId已经改变
                    if (!string.Equals(serverTask.ProjectId, moveListTasksMap[localTask.SId]))
                    {
                        await SyncStatusBll.UpdateMoveFromId(localTask.SId, localTask.UserId, serverTask.ProjectId);
                    }
                }
                else
                {
                    //本地没MOVE，Server的操作会覆盖本地，
                    //TODO 只更新了task的project是否有问题
                    localTask.ProjectId = projectId;
                    localTask.ProjectSid = serverTask.ProjectId;
                }
            }
        }

        #region android代码

        //    public void mergeUpdatedTasksFromService(Map<String, Task2> localTasks,
        //        TaskSyncModel taskSyncModel, List<Task> updateServerTasks) {
        //    Map<String, TaskSyncedJson> originalJsons = taskSyncedJsonService
        //            .getAllTaskSyncedJsonMap(userId);
        //    Map<String, String> moveListTasksMap = syncStatusService.getMoveFromIdMap(userId);
        //    Set<String> contentChangeTaskIds = syncStatusService.getContentChangeEntityIds(userId);
        //    Set<String> assignTaskIds = syncStatusService.getAssignEntityIds(userId);
        //    Map<String, Long> projectIdMap = projectService.getProjectSid2IdsMap(userId);
        //    ObjectMapper mapper = new ObjectMapper();
        //    for (Task serverTask : updateServerTasks) {
        //        //本地没有找到对应是Project，Task不添加到本地
        //        if (!projectIdMap.containsKey(serverTask.getProjectId())) {
        //            Log.e(TAG, "Local project not found : project_id = " + serverTask.getProjectId());
        //            continue;
        //        }

        //        Task2 localTask = localTasks.get(serverTask.getId());
        //        long projectId = projectIdMap.get(serverTask.getProjectId());
        //        if (localTask != null) {
        //            if (Log.IS_LOG_ENABLED) {
        //                Log.debugSync("serverTask.etag = " + serverTask.getEtag()
        //                        + " , localTask.etag = " + localTask.getEtag());
        //            }

        //            // 本地的DELETE_FOREVER操作，舍弃server端的任何修改
        //            if (localTask.isDeletedForever()) {
        //                continue;
        //            }

        //            // Etag作为判断两个版本是否一直的唯一标识
        //            if (TextUtils.equals(serverTask.getEtag(), localTask.getEtag())) {
        //                continue;
        //            }

        //            mergeMoveListAction(moveListTasksMap, serverTask, localTask, projectId);

        //            mergeTaskAssignee(assignTaskIds, serverTask, localTask);

        //            LocationSyncCollector.collectRemoteLocations(serverTask, localTask,
        //                    taskSyncModel.getLocationSyncBean());

        //            AttachmentSyncCollector.collectRemoteAttachments(serverTask, localTask,
        //                    taskSyncModel.getAttachmentSyncBean());

        //            if (contentChangeTaskIds.contains(localTask.getSid())) {
        //                // 先查找对应的Original版本
        //                TaskSyncedJson originalJson = originalJsons.get(serverTask.getId());
        //                Task2 originalTask = null;
        //                if (originalJson != null) {
        //                    originalTask = TaskTransfer.convertTaskSyncedJsonToLocal(mapper,
        //                            originalJsons.get(serverTask.getId()));
        //                }

        //                // 没有Original版本时，不进行Merge，直接Local覆盖Server
        //                if (originalTask != null) {
        //                    //执行merge操作
        //                    Task2 deltaTask = TaskTransfer
        //                            .convertServerTaskToLocalWithChecklistItem(serverTask);
        //                    TaskUtils.mergeTask(originalTask, deltaTask, localTask);

        //                    // 设置其他属性
        //                    localTask.setKind(localTask.getChecklistItems().isEmpty() ? Kind.TEXT
        //                            : Kind.CHECKLIST);
        //                    localTask.setEtag(serverTask.getEtag());
        //                    localTask.setRepeatReminderTime(localTask.isRepeatTask() ? localTask
        //                            .getReminderTime() : null);
        //                    String repeatFromRemote = serverTask.getRepeatFrom();
        //                    localTask
        //                            .setRepeatFrom(TextUtils.isEmpty(repeatFromRemote) ?
        //                                    RepeatFromStatus.DEFAULT
        //                                    :
        //                                    repeatFromRemote);
        //                    taskSyncModel.addUpdatingTask(localTask);
        //                }
        //            } else {
        //                TaskTransfer.convertServerTaskToLocalWithChecklistItem(localTask, serverTask);
        //                taskSyncModel.addUpdatedTask(localTask);
        //            }
        //            taskSyncModel.addUpdatedTaskSyncedJson(serverTask);
        //        } else {
        //            addRemoteTaskToLocal(taskSyncModel, serverTask, projectId);
        //        }

        //    }
        //}
        //public TaskSyncModel collectSyncTaskBean(SyncTaskBean syncTaskBean,
        //  Map<String, Task2> localTasks)
        //{
        //    TaskSyncModel taskSyncModel = new TaskSyncModel();
        //    List<Task> update = syncTaskBean.getUpdate();

        //    // get delete tasks
        //    collectDeletedTasksFromRemoteModel(syncTaskBean, localTasks, taskSyncModel);

        //    // get add/update tasks & other entity
        //    if (update.isEmpty())
        //    {
        //        return taskSyncModel;
        //    }

        //    mergeUpdatedTasksFromService(localTasks, taskSyncModel, update);
        //    return taskSyncModel;
        //}
        #endregion
    }
}
