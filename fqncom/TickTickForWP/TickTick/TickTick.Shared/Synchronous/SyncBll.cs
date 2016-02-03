using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Bll;
using TickTick.Entity;
using TickTick.Handler;
using TickTick.Helper;
using TickTick.Manager;
using TickTick.Models;
using TickTick.Synchronous.Transfer;
using WindowsUniversalLogger.Interfaces;

namespace TickTick.Synchronous
{
    public class SyncBll
    {
        private User User;// TODO 有坑，syncService.launch(accountManager.getAccountById(userId), syncResult);
        private UserBll UserBllTest = new UserBll();//===========临时代码
        private Communicator Communicator;
        private ProjectBatchHandler ProjectBatchHandler { get; set; }
        private TaskBatchHandler TaskBatchHandler { get; set; }
        private TickTickAccountManager AccountManager { get; set; }

        private SyncResult mSyncResult { get; set; }
        public SyncBll()
        {
            // TODO this.Application = application;
            this.AccountManager = new TickTickAccountManager();
        }

        #region 在找到代替googletaskmanager工具之前，先直接使用该方式进行同步
        public async Task<SyncResult> DoSyncAll(Object firstPhaseResult, int type)
        {
            //Log.d(TAG, "sync all begin");
            String userId = (String)firstPhaseResult;
            SyncResult syncResult = new SyncResult();
            if (!HttpHelper.IsConnectedToNetwork)
            {
                return syncResult;
            }

            this.Launch(await AccountManager.GetAccountById(userId), syncResult);

            await this.DoAsync(type);

            //Log.d(TAG, "sync all end");
            return syncResult;
        }
        #endregion

        public void Launch(User user, SyncResult syncResult)
        {
            this.mSyncResult = syncResult;

            this.User = user;//await this.AccountManager.GetAccountById(App.SignUserInfo.Sid);

            InitCommunicator();//考虑使用缓存队列形式

            InitBatchHandler(syncResult);

            //profileSyncService = new UserProfileSyncService(application, communicator, syncResult);
        }

        private void InitCommunicator()
        {
            this.Communicator = Communicator.Instance;
        }

        private void InitBatchHandler(SyncResult syncResult)
        {
            //ProjectGroupBatchHandler = new ProjectGroupBatchHandler(user.getId(), syncResult);
            ProjectBatchHandler = new ProjectBatchHandler(User.Sid, syncResult);
            TaskBatchHandler = new TaskBatchHandler(User.Sid, syncResult);
            //MoveProjectHandler = new MoveProjectBatchHandler(user.getId(), syncResult);
            //TaskSortOrderHandler = new TaskSortOrderHandler(user.getId(), syncResult);
            //TaskAssignHandler = new TaskAssignHandler(user.getId(), syncResult);
            //RestoreHandler = new TaskBatchRestoreHandler(user.getId(), syncResult);
            //DeleteForeverHandler = new TaskDeleteForeverHandler(user.getId(), syncResult);
            //TaskOrderBatchHandler = new TaskOrderBatchHandler(user.getId(), syncResult);
        }
        /// <summary>
        /// 开始同步
        /// </summary>
        public async Task DoAsync(int type)
        {
            //修复问题
            //new DidaDomainBugFixer(user).repair();

            //await LoggerHelper.LogToAllChannels(LogLevel.INFO, "doSync pullOtherData");
            //PullOtherData(type);

            //await LoggerHelper.LogToAllChannels(LogLevel.INFO, "doSync pull");
            await Pull();

            //PullTasksOfOpenedProjects();

            //await LoggerHelper.LogToAllChannels(LogLevel.INFO, "doSync commit");
            await Commit();

            //await LoggerHelper.LogToAllChannels(LogLevel.INFO, "doSync finish");
        }

        private void PullTasksOfOpenedProjects()
        {
            //   ArrayList<String> projectSIDs = projectBatchHandler.getNeedPullTasksProjectIDs();
            //Log.d(tag, "pullTasksOfOpenedProjects = " + projectSIDs);
            //if (!projectSIDs.isEmpty()) {
            //    Map<String, List<Task>> serverTasksMap = new HashMap<String, List<Task>>();
            //    for (String projectSID : projectSIDs) {
            //        List<Task> serverTasks = getCommunicator().getTasksByProject(projectSID);
            //        serverTasksMap.put(projectSID, serverTasks);
            //    }
            //    taskBatchHandler.mergeTasksOfOpenedProjects(serverTasksMap);
            //    projectBatchHandler.updateNeedPullTasksProjectDone(projectSIDs);
            //}
        }
        /// <summary>
        /// 开始获取
        /// </summary>
        private async Task Pull()
        {
            SyncBean syncBean = await Communicator.BatchCheck(1428303870729);
            //if (Log.IS_LOG_ENABLED)
            //{
            //    Log.debugSync("syncBean.getInboxId() = " + syncBean.getInboxId());
            //}
            // Merge project groups
            //ProjectGroupBatchHandler.mergeWithServer(syncBean.getProjectGroups());

            // Merge projects
            await ProjectBatchHandler.MergeWithServer(syncBean.ProjectProfiles);

            // Merge tasks
            await TaskBatchHandler.MergeWithServer(syncBean.SyncTaskBean);

            //Merge task orders
            //TaskOrderBatchHandler.mergeWithServer(syncBean.getSyncTaskOrderBean());

            await SaveCheckPoint(syncBean.CheckPoint);
        }
        private async Task Commit()
        {

            //commitProjectGroup(false);

            await CommitProject(false);

            //commitMoveProject(false);

            await CommitTask(false);

            //commitTaskOrder(false);

            //commitSortOrderTask(false);

            //commitAssignTask(false);

            //commitRestore(false);

            //commitDeleteForever(false);

            //tryToClearTrash();

        }
        /// <summary>
        /// 提交Projects
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private async Task CommitProject(bool tryAgain)
        {
            SyncProjectBean projectBean = await ProjectBatchHandler.CommitToServer();
            if (projectBean != null)
            {
                BatchUpdateResult result = await Communicator.BatchUpdateProject(projectBean);
                List<String> erroIds = await ProjectBatchHandler.HandleErrorResult(result.IdToError);
                if (result.IdToEtag.Count > 0)
                {
                    mSyncResult.SetPushedLocalChanges(true);
                }
                await ProjectBatchHandler.HandleCommitResult(result.IdToEtag, erroIds,
                        projectBean.Delete);
                if (erroIds.Count > 0 && !tryAgain)
                {
                    await CommitProject(true);
                }
            }
            else
            {
                //Log.i(tag, "No projects need to commit");
            }
        }

        private async Task SaveCheckPoint(long checkPoint)
        {
            //await AccountManager.SetCheckpoint(User.Id, checkPoint);
            await AccountManager.SetCheckpoint(User.Sid, checkPoint);
        }

        private async Task CommitTask(bool tryAgain)
        {
            List<Tasks> createdTasks = await TaskBatchHandler.GetLocalCreatedTasks();
            List<Tasks> updatedTasks = await TaskBatchHandler.GetLocalContentUpdatedTasks();
            List<Tasks> deletedTasks = await TaskBatchHandler.GetLocalDeletedTasks();
            if (createdTasks.Count <= 0 && updatedTasks.Count <= 0 && deletedTasks.Count <= 0)
            {
                //Log.i(tag, "No tasks need to commit");
                if (LoggerHelper.IS_LOG_ENABLED)
                {
                    await LoggerHelper.LogToAllChannels(LogLevel.INFO, "没有任务可以提交");
                }
                return;
            }
            //if (LoggerHelper.IS_LOG_ENABLED)
            //{
            //    await LoggerHelper.LogToAllChannels(null, string.Format("传输tasks：CreatedTasks.Size = {0};UpdateTasks.Size = {1};DeletedTakss.Size = {2}", createdTasks.Count, updatedTasks.Count, deletedTasks.Count));
            //}
            List<SyncTaskBean> taskBeans = TaskTransfer.DescribleSyncTaskBean(createdTasks, updatedTasks, deletedTasks);
            long lastPostPoint = DateTime.UtcNow.GetAllMilliSeconds();// (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);//System.CurrentTimeMillis();
            List<String> errorIds = new List<String>();
            Dictionary<String, String> idETagMap = new Dictionary<String, String>();
            foreach (var taskBean in taskBeans)
            {
                BatchUpdateResult result = await Communicator.BatchUpdateTask(taskBean);
                errorIds.AddRange(await TaskBatchHandler.HandleErrorResult(result.IdToError));
                foreach (var item in result.IdToEtag)
                {
                    idETagMap.Add(item.Key, item.Value);
                }
            }


            if (idETagMap.Count > 0)
            {
                mSyncResult.SetPushedLocalChanges(true);
            }
            await TaskBatchHandler.HandleCommitResult(idETagMap, errorIds, taskBeans, lastPostPoint);
            if (errorIds.Count > 0 && !tryAgain)
            {
                await CommitTask(true);
            }
        }

        #region android代码
        //private void pull()
        //{
        //    SyncBean syncBean = getCommunicator().batchCheck(getCheckPoint());
        //    if (Log.IS_LOG_ENABLED)
        //    {
        //        Log.debugSync("syncBean.getInboxId() = " + syncBean.getInboxId());
        //    }

        //    // Merge projects
        //    projectBatchHandler.mergeWithServer(syncBean.getProjectProfiles());

        //    // Merge tasks
        //    taskBatchHandler.mergeWithServer(syncBean.getSyncTaskBean());

        //    saveCheckPoint(syncBean.getCheckPoint());
        //} 
        #endregion
    }
}
