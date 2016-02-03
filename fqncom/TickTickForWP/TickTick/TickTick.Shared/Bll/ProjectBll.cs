using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Dal;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Utilities;

namespace TickTick.Bll
{
    public class ProjectBll : BaseBll<Projects>
    {
        /// <summary>
        /// Projects的dal层对象
        /// </summary>
        private ProjectDal ProjectDal = new ProjectDal();//=========之后可以考虑解耦==========
        private TaskDal TaskDal = new TaskDal();
        private TaskBll TaskBll = new TaskBll();
        private CommentDal CommentDal = new CommentDal();

        #region 自定义代码

        public async Task SaveCommitResultBackToDB(Dictionary<String, String> id2etag, List<String> deletedIds, String userId)
        {
            foreach (var id in id2etag.Keys)
            {
                await ProjectDal.UpdateEtagToDb(userId, id, id2etag[id]);
            }

            foreach (var sid in deletedIds)
            {
                List<Tasks> tasks = await TaskBll.GetTasksByProjectSId(sid, userId, true);
                foreach (var task in tasks)
                {
                    await TaskBll.DeleteTaskIntoTrash(task);
                }
                await ProjectDal.DeleteProjectForever(sid);
            }
            //return null;
            //dbHelper.doInTransaction(new Transactable<Object>() {

            //    @Override
            //    public Object doIntransaction(GTasksDBHelper dbHelper) {
            //        for (String id : id2etag.keySet()) {
            //            projectDao.updateEtag2Db(userId, id, id2etag.get(id));
            //        }

            //        for (String sid : deletedIds) {
            //            ArrayList<Task2> tasks = taskService.getTasksByProjectSid(sid, userId, true);
            //            for (Task2 task : tasks) {
            //                taskService.deleteTaskIntoTrash(task);
            //            }
            //            projectDao.deleteProjectForever(sid);
            //        }
            //        return null;
            //    }

            //});

        }

        public async Task<List<Projects>> GetAllProjectsWithTasksCount()
        {
            return await ProjectDal.GetAllProjectsWithTasksCount();
        }

        public async Task<List<Projects>> GetAllProjectsDeletedNo()
        {
            return await (await ProjectDal.GetAllProjectsDeletedNo()).ToListAsync();
        }
        public async Task ExchangeToNewIdForError(String userId, String sid)
        {
            String newSid = StringUtils.GenerateShortStringGuid();// Utils.randomUUID32();
            await ProjectDal.ExchangeToNewIdForErro(userId, sid, newSid);
            await TaskDal.ExchangeNewProjectSid(userId, sid, newSid);
            await CommentDal.ExchangeNewProjectSid(sid, newSid);
        }
        public async Task UpdateStatus(String userId, String sid, int status)
        {
            await ProjectDal.UpdateStatus(userId, sid, status);
        }
        public async Task<List<Projects>> GetNeedPostProject(String userId)
        {
            return await ProjectDal.GetNeedPostProject(userId);
        }
        /// <summary>
        /// 删除projects以及其下所有tasks到垃圾桶
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        public async Task DeleteForeverWithTasks(Projects projects)
        {
            await TaskBll.DeleteForeverByProjectId(projects.Id.ToString());
            await DeleteForever(projects);
        }
        /// <summary>
        /// 获取所有的projects以及其下tasks的数量
        /// </summary>
        /// <returns></returns>
        //public async Task<List<Projects>> GetAllProjectsWithTasksCount()
        //{
        //    var allProjects = await GetAllProjectsDeletedNoAndNotClosed();
        //    //var allTasks = await TaskBll.GetAllTasksDeletedNo();
        //    foreach (var item in allProjects)
        //    {
        //        //item.TasksCount = allTasks.FindAll((t) => t.ProjectId == item.Id.ToString()).Count;
        //        item.TasksCount = (await TaskBll.GetAllTasksByProjectIdDeletedNo(item.Id.ToString())).Count;
        //    }
        //    return allProjects;
        //}
        //public async Task<List<Projects>> GetAllProjects()
        //{
        //    return await ProjectDal.ExecuteTable();
        //}
        public async Task<Dictionary<String, Projects>> GetLocalProjectDic(String userId)
        {
            return await ProjectDal.GetAllSidIntoProjectsDic(userId);
        }

        public async Task SaveServerMergeData(List<Projects> added, List<Projects> updated, List<Projects> deleted)
        {
            // TODO 之后可以考虑使用事务进行处理
            await ProjectDal.SaveServerMergeData(added, updated, deleted);
        }

        /// <summary>
        /// 获取本地所有projects和其sid的字典
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Dictionary<String, string>> GetProjectSidToIdsDic(String userId)
        {
            return await ProjectDal.GetProjectSidToIdsDic(userId);
        }
        #endregion

        #region android代码
        //     public void saveServerMergeData(final List<Project> added, final List<Project> updated,
        //        final List<Project> deleted) {
        //    dbHelper.doInTransaction(new Transactable<Void>() {

        //        @Override
        //        public Void doIntransaction(GTasksDBHelper dbHelper) {
        //            for (Project add : added) {
        //                projectDao.createProject(add);
        //            }

        //            for (Project update : updated) {
        //                projectDao.update(update);
        //            }

        //            for (Project delete : deleted) {
        //                projectDao.deleteProjectForever(delete);
        //                taskService.deleteTaskIntoTrashByProjectId(delete.getId());
        //            }
        //            return null;
        //        }
        //    });

        //} 
        #endregion

        protected override void SetCurrentDal()
        {
            CurrentDal = ProjectDal;
        }

        public async Task<Projects> GetProjectByProjectId(int projectId)
        {
            return await (await ProjectDal.GetAllProjectsDeletedNo()).Where(p => p.Id == projectId).FirstOrDefaultAsync();
        }
    }
}
