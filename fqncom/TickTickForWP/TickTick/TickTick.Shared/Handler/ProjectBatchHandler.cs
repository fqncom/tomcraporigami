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

namespace TickTick.Handler
{
    public class ProjectBatchHandler : BatchHandler
    {
        private ProjectBll ProjectBll = new ProjectBll();
        private ProjectTransfer Transfer = new ProjectTransfer();

        public ProjectBatchHandler(string userId, SyncResult syncResult)
            : base(userId, syncResult)
        {

        }

        public async Task HandleCommitResult(Dictionary<String, String> id2eTag, List<String> errorIds, List<String> deletes)
        {
            List<String> deletedIds = new List<String>();
            if (errorIds.Count <= 0)
            {
                deletedIds = new List<String>(deletes);
            }
            else
            {
                foreach (var deleted in deletes)
                {
                    if (!errorIds.Contains(deleted))
                    {
                        deletedIds.Add(deleted);
                    }
                }
            }
            await ProjectBll.SaveCommitResultBackToDB(id2eTag, deletedIds, userId);

        }

        public async Task<List<String>> HandleErrorResult(Dictionary<String, ErrorType> idToError)
        {
            List<String> errorIds = new List<String>();
            foreach (var sid in idToError.Keys)
            {
                // TODO 数据收集等待处理
                //application.GetAnalyticsInstance().SendException(
                //        "ProjectSyncError" + idToError.get(sid) + " # Id: " + sid + application
                //                .getAccountManager().getCurrentUser().getUsername()
                //);
                switch (idToError[sid])
                {
                    case ErrorType.EXISTED:
                        await ProjectBll.UpdateStatus(userId, sid, ModelStatusEnum.SYNC_UPDATE);
                        errorIds.Add(sid);
                        break;
                    case ErrorType.DELETED:
                        await ProjectBll.ExchangeToNewIdForError(userId, sid);
                        errorIds.Add(sid);
                        break;
                    case ErrorType.NOT_EXISTED:
                        await ProjectBll.UpdateStatus(userId, sid, ModelStatusEnum.SYNC_NEW);
                        errorIds.Add(sid);
                        break;
                    case ErrorType.UNKNOWN:
                        await ProjectBll.UpdateStatus(userId, sid, ModelStatusEnum.SYNC_DONE);
                        break;
                    default:
                        //Log.e(TAG, "$handleErrorResult : unexpected # Erro: " + idToError.get(sid)
                        //        + " # Id: " + sid);
                        await ProjectBll.UpdateStatus(userId, sid, ModelStatusEnum.SYNC_DONE);
                        break;
                }

            }
            return errorIds;
        }
        public async Task<SyncProjectBean> CommitToServer()
        {
            List<Projects> localChanges = await ProjectBll.GetNeedPostProject(userId);
            if (localChanges.Count <= 0)
            {
                return null;
            }

            return Transfer.DescribleSyncProjectBean(localChanges);
        }

        public async Task MergeWithServer(List<ProjectProfiles> ppList)
        {
            if (ppList != null)
            {
                List<Projects> added = new List<Projects>();
                List<Projects> updated = new List<Projects>();

                Dictionary<String, Projects> localProjects = await ProjectBll.GetLocalProjectDic(userId);
                foreach (ProjectProfiles profile in ppList)
                {
                    if (profile.Id != null && localProjects.ContainsKey(profile.Id))
                    {
                        Projects localProject = localProjects[profile.Id];
                        localProjects.Remove(profile.Id);
                        if (object.Equals(profile.Etag, localProject.Etag) || localProject.IsMoveToTrash() || localProject.IsLocalUpdated())
                        {
                            continue;
                        }
                        // Update
                        bool originalClose = localProject.Closed == ModelStatusEnum.CLOSED_NO;
                        updated.Add(Transfer.ConvertServerToLocal(profile, localProject));
                        if (localProject.Closed == ModelStatusEnum.CLOSED_YES && originalClose != (localProject.Closed == ModelStatusEnum.CLOSED_YES))
                        {
                            // Server opened the project, we need to pull its tasks
                            // later.
                            localProject.NeedPullTasks = ModelStatusEnum.PROJECT_NEED_PULL_TASKS;
                        }
                    }
                    else
                    {
                        // Create
                        added.Add(Transfer.ConvertServerToLocal(profile, userId));
                    }
                }

                if (added.Count > 0 || updated.Count > 0 || localProjects.Count > 0)
                {
                    if (localProjects.Count > 0)
                    {
                        mSyncResult.RemoteTaskChanged = true;
                    }
                    mSyncResult.RemoteProjectChanged = true;

                    // Delete
                    List<Projects> deleted = new List<Projects>(localProjects.Values);
                    if (LoggerHelper.IS_LOG_ENABLED)
                    {
                        await LoggerHelper.LogToAllChannels(null, "Save Remote Project [ Add.size = " + added.Count + " , Updated.size = " + updated.Count + " , Deleted.size = " + deleted.Count + " ]");
                    }
                    await ProjectBll.SaveServerMergeData(added, updated, deleted);
                }
            }
        }

        #region android代码
        //   public void mergeWithServer(Collection<ProjectProfile> projectProfiles) {
        //    if (projectProfiles != null) {
        //        List<Project> added = new ArrayList<Project>();
        //        List<Project> updated = new ArrayList<Project>();

        //        Map<String, Project> localProjects = projectService.getLocalProjectMap(userId);
        //        for (ProjectProfile profile : projectProfiles) {
        //            if (localProjects.containsKey(profile.getId())) {
        //                Project localProject = localProjects.get(profile.getId());
        //                localProjects.remove(profile.getId());
        //                if (TextUtils.equals(profile.getEtag(), localProject.getEtag())
        //                        || localProject.isMove2Trash() || localProject.isLocalUpdated()) {
        //                    continue;
        //                }
        //                // Update
        //                boolean originalClose = localProject.isClosed();
        //                updated.add(transfer.convertServerToLocal(profile, localProject));
        //                if (!localProject.isClosed() && originalClose != localProject.isClosed()) {
        //                    // Server opened the project, we need to pull its tasks
        //                    // later.
        //                    localProject.setNeedPullTasks(true);
        //                }
        //            } else {
        //                // Create
        //                added.add(transfer.convertServerToLocal(profile, userId));
        //            }
        //        }

        //        if (!added.isEmpty() || !updated.isEmpty() || !localProjects.isEmpty()) {
        //            if (!localProjects.isEmpty()) {
        //                mSyncResult.setRemoteTaskChanged(true);
        //            }
        //            mSyncResult.setRemoteProjectChanged(true);

        //            // Delete
        //            List<Project> deleted = new ArrayList<Project>(localProjects.values());
        //            if (Log.IS_LOG_ENABLED) {
        //                Log.debugSync("Save Remote Project [ Add.size = " + added.size()
        //                        + " , Updated.size = " + updated.size() + " , Deleted.size = " + deleted
        //                        .size() + " ]");
        //            }
        //            projectService.saveServerMergeData(added, updated, deleted);
        //        }
        //    }
        //} 
        #endregion
    }
}
