using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Models;

namespace TickTick.Synchronous.Transfer
{
    public class ProjectTransfer
    {
        public Projects ConvertServerToLocal(ProjectProfiles profile, Projects project)
        {
            project.Status = ModelStatusEnum.SYNC_DONE;
            project.SId = profile.Id;
            project.Name = profile.Name;
            project.Color = profile.Color ?? null;
            project.SortOrder = profile.SortOrder;
            project.UserCount = profile.UserCount;
            project.ShowInAll = profile.InAll;
            project.Etag = profile.Etag;
            project.SortType = profile.SortType;
            project.ModifiedTime = profile.ModifiedTime;
            if (profile.Closed != null)
            {
                project.Closed = profile.Closed ?? false ? ModelStatusEnum.CLOSED_YES : ModelStatusEnum.CLOSED_NO;
            }
            else
            {
                project.Closed = ModelStatusEnum.CLOSED_NO;
            }
            //试着记录日志
            return project;
        }

        public SyncProjectBean DescribleSyncProjectBean(List<Projects> localChanges)
        {

            SyncProjectBean projectBean = new SyncProjectBean();
            foreach (var project in localChanges)
            {
                if (project.IsLocalAdded())
                {
                    projectBean.Add.Add(ConvertLocalToServer(project));
                }
                else if (project.IsLocalUpdated())
                {
                    projectBean.Update.Add(ConvertLocalToServer(project));
                }
                else if (project.IsLocalDeleted())
                {
                    projectBean.Delete.Add(project.SId);
                }

            }
            return projectBean;
        }
        private ProjectProfiles ConvertLocalToServer(Projects local)
        {
            ProjectProfiles profile = new ProjectProfiles();
            profile.Id = local.SId;
            //profile.Color=Utils.ConvertColorToRGB(local.Color));      // TODO 颜色暂时不考虑
            profile.InAll = local.IsShowInAll();
            profile.ModifiedTime = local.ModifiedTime;
            profile.Name = local.Name;
            profile.SortOrder = local.SortOrder;
            profile.SortType = local.SortType;
            profile.Closed = local.IsClosed();
            return profile;
        }

        public Projects ConvertServerToLocal(ProjectProfiles serverProfile, string userId)
        {
            Projects project = new Projects();
            project.UserId = userId;
            return ConvertServerToLocal(serverProfile, project);
        }

        #region android代码

        //public Project convertServerToLocal(ProjectProfile serverProfile, String userId)
        //{
        //    Project project = new Project();
        //    project.setUserId(userId);
        //    return convertServerToLocal(serverProfile, project);
        //}
        //    public Project convertServerToLocal(ProjectProfile profile, Project project)
        //    {
        //        project.setStatus(Status.SYNC_DONE);
        //        project.setSid(profile.getId());
        //        project.setName(profile.getName());
        //        String color = profile.getColor();
        //        project.setColor(TextUtils.isEmpty(color) ? null : color);
        //        project.setSortOrder(profile.getSortOrder());
        //        project.setUserCount(profile.getUserCount());
        //        project.setShowInAll(profile.isInAll());
        //        project.setEtag(profile.getEtag());
        //        if (profile.isClosed() != null)
        //        {
        //            project.setClosed(profile.isClosed());
        //        }
        //        else
        //        {
        //            project.setClosed(false);
        //        }

        //        project.setSortType(SortType.getSortType(profile.getSortType()));
        //        project.setModifiedTime(profile.getModifiedTime());
        //        if (Log.IS_LOG_ENABLED)
        //        {
        //            Log.debugSync(project.toString());
        //        }
        //        return project;
        //    }

        //    public Project convertServerToLocal(ProjectProfile serverProfile, String userId)
        //    {
        //        Project project = new Project();
        //        project.setUserId(userId);
        //        return convertServerToLocal(serverProfile, project);
        //    }

        //    private ProjectProfile convertLocalToServer(Project local)
        //    {
        //        ProjectProfile profile = new ProjectProfile();
        //        profile.setId(local.getSid());
        //        profile.setColor(Utils.convertColorToRGB(local.getColor()));
        //        profile.setInAll(local.isShowInAll());
        //        profile.setModifiedTime(local.getModifiedTime());
        //        profile.setName(local.getName());
        //        profile.setSortOrder(local.getSortOrder());
        //        profile.setSortType(local.getSortType().getLabel());
        //        profile.setClosed(local.isClosed());
        //        return profile;
        //    }

        //    public SyncProjectBean describleSyncProjectBean(List<Project> localChanges) {

        //    SyncProjectBean projectBean = new SyncProjectBean();
        //    for (Project project : localChanges) {
        //        if (project.isLocalAdded()) {
        //            projectBean.getAdd().add(convertLocalToServer(project));
        //        } else if (project.isLocalUpdated()) {
        //            projectBean.getUpdate().add(convertLocalToServer(project));
        //        } else if (project.isLocalDeleted()) {
        //            projectBean.getDelete().add(project.getSid());
        //        }

        //    }
        //    return projectBean;
        //}
        #endregion

    }
}
