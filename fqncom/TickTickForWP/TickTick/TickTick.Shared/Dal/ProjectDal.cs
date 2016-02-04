using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Enums;
using TickTick.Helper;
using Windows.Storage;

namespace TickTick.Dal
{
    public class ProjectDal : BaseDal<Projects>
    {

        #region IBaseDal<Projects> 成员
        /// <summary>
        /// 异步创建person数据库，若存在则不重复创建
        /// </summary>
        /// <returns>返回链接对象</returns>
        //public async Task<SQLiteAsyncConnection> CreateTableAsync()
        //{
        //    var conn = new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\note.db");
        //    await conn.CreateTableAsync<Projects>();
        //    return conn;
        //}

        /// <summary>
        /// 获取指定表的所有数据
        /// </summary>
        /// <returns></returns>
        //public async Task<List<Projects>> ExecuteTable()
        //{
        //    var conn = await CreateTableAsync();
        //    return await conn.Table<Projects>().ToListAsync();
        //}

        ///// <summary>
        ///// 基本查询操作
        ///// </summary>
        ///// <returns></returns>
        //public async Task<List<Projects>> ExecuteNonQuery(string sql, params object[] paras)
        //{
        //    var conn = await CreateTableAsync();
        //    return await conn.QueryAsync<Projects>(sql, paras);
        //}

        ///// <summary>
        ///// 直接删除项
        ///// </summary>
        ///// <param name="projects"></param>
        ///// <returns>返回受影响的行数</returns>
        //public async Task<int> DeleteData(Projects projects)
        //{
        //    var conn = await this.CreateTableAsync();
        //    return await conn.DeleteAsync(projects);
        //}

        //#region 暂时不用的插入数据方法，统一使用Execute方法

        ///// <summary>
        ///// 插入一条数据
        ///// </summary>
        ///// <param name="projects">插入的一条数据对象</param>
        ///// <returns>返回受影响的行数</returns>
        //public async Task<int> InsertAsync(Projects projects)
        //{
        //    var conn = await this.CreateTableAsync();
        //    return await conn.InsertAsync(projects);
        //}
        ///// <summary>
        ///// 一次插入多条数据
        ///// </summary>
        ///// <param name="projects">插入的数据对象的集合</param>
        ///// <returns>返回受影响的行数</returns>
        //public async Task<int> InsertAllAsync(List<Projects> projects)
        //{
        //    var conn = await this.CreateTableAsync();
        //    return await conn.InsertAllAsync(projects);
        //}

        //public Task<int> UpdateAsync(Projects t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAllAsync(List<Projects> t)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region 测试使用
        /// <summary>
        /// 删除整个表，慎用！！！
        /// </summary>
        /// <returns>返回删除条数</returns>
        //public async Task<int> DropTable()
        //{
        //    var conn = await this.CreateTableAsync();
        //    return await conn.DropTableAsync<Projects>();
        //}
        #endregion

        //public Projects GetInbox(String userId)
        //{ 

        //}

        public async Task DeleteProjectForever(String sid)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Projects>().Where((t) => t.SId == sid).FirstOrDefaultAsync();
            if (queryResult != null)
            {
                await conn.DeleteAsync(queryResult);
            }
        }

        public async Task UpdateEtagToDb(String userId, String sid, String etag)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Projects>().Where((p) => p.UserId == userId && p.SId == sid).ToListAsync();
            foreach (var item in queryResult)
            {
                item.Status = ModelStatusEnum.SYNC_DONE;
                item.Etag = etag;
            }
            await conn.UpdateAllAsync(queryResult);
            //    ContentValues values = new ContentValues();
            //    values.put(ProjectField._status.name(), Status.SYNC_DONE);
            //    values.put(ProjectField.etag.name(), etag);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(ProjectField.User_Id.name()).append(" = ? and ")
            //            .append(ProjectField.sId.name()).append(" = ?");
            //    String[] whereArgs = {
            //        userId, sid
            //};
            //    table.updateWithoutModifyDate(values, whereClause.toString(), whereArgs, dbHelper);
        }

        public async Task<bool> ExchangeToNewIdForErro(String userId, String sid, String newSid)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Projects>().Where((p) => p.UserId == userId && p.SId == sid).ToListAsync();
            foreach (var item in queryResult)
            {
                item.SId = newSid;
                item.Status = ModelStatusEnum.SYNC_NEW;
                item.Etag = string.Empty;
                item.ModifiedTime = DateTime.UtcNow;
            }
            return await conn.UpdateAllAsync(queryResult) > 0;
            //    ContentValues values = new ContentValues();
            //    values.put(ProjectField.sId.name(), newSid);
            //    values.put(ProjectField._status.name(), Status.SYNC_NEW);
            //    values.putNull(ProjectField.etag.name());
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(ProjectField.User_Id.name()).append(" = ? and ")
            //            .append(ProjectField.sId.name()).append(" = ?");
            //    String[] whereArgs = new String[] {
            //        userId, sid
            //};
            //    int ret = table.update(values, whereClause.toString(), whereArgs, dbHelper);
            //    return ret > 0;
        }

        public async Task UpdateStatus(String userId, String sid, int syncDone)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Projects>().Where((p) => p.UserId == userId && p.SId == sid).ToListAsync();
            foreach (var item in queryResult)
            {
                item.Status = syncDone;
            }
            await conn.UpdateAllAsync(queryResult);
            //    ContentValues values = new ContentValues();
            //    values.put(ProjectField._status.name(), syncDone);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(ProjectField.User_Id.name()).append(" = ? and ")
            //            .append(ProjectField.sId.name()).append(" = ?");
            //    String[] whereArgs = {
            //        userId, sid
            //};
            //    table.updateWithoutModifyDate(values, whereClause.toString(), whereArgs, dbHelper);
        }

        /// <summary>
        /// 获取本地所有projects和其sid的字典
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Dictionary<String, Projects>> GetAllSidIntoProjectsDic(String userId)
        {
            Dictionary<string, Projects> projectsDic = new Dictionary<string, Projects>();

            var conn = await CreateTableAsync();

            var queryResult = await conn.Table<Projects>().Where(p => p.UserId == userId && p.Etag != null && p.SId != null).OrderByDescending(p => p.CreatedTime).ToListAsync();
            if (queryResult != null)
            {
                foreach (var item in queryResult)
                {
                    if (projectsDic.ContainsKey(item.SId))
                    {
                        projectsDic[item.SId] = item;
                    }
                    else
                    {
                        projectsDic.Add(item.SId, item);
                    }
                }
            }
            return projectsDic;
        }
        public async Task<List<Projects>> GetNeedPostProject(String userId)
        {
            List<Projects> projects = new List<Projects>();

            var conn = await CreateTableAsync();
            return await conn.Table<Projects>().Where((p) => p.UserId == userId && p.Status != ModelStatusEnum.SYNC_DONE && p.DefaultProject == ModelStatusEnum.NORMAL_PROJECT).ToListAsync();

            #region
            //    StringBuffer selection = new StringBuffer();
            //    selection.append(ProjectField.User_Id.name()).append(" = ? and ")
            //            .append(ProjectField._status.name()).append(" <> ? and ")
            //            .append(ProjectField.default_project.name()).append(" = ?");
            //    String[] selectionArgs = {
            //        userId, Status.SYNC_DONE + "", Status.NORMAL_PROJECT + ""
            //};

            //    Cursor c = null;
            //    try
            //    {
            //        c = table.query(selection.toString(), selectionArgs, ProjectField.sort_order, dbHelper);
            //        c.moveToFirst();
            //        while (!c.isAfterLast())
            //        {
            //            Project project = cursorToProject(c);
            //            projects.add(project);
            //            c.moveToNext();
            //        }
            //        return projects;
            //    }
            //    finally
            //    {
            //        if (c != null)
            //        {
            //            c.close();
            //        }
            //    } 
            #endregion
        }

        /// <summary>
        /// 本地与服务器内容融合，考虑是否使用异步，
        /// </summary>
        /// <param name="added"></param>
        /// <param name="updated"></param>
        /// <param name="deleted"></param>
        public async Task SaveServerMergeData(List<Projects> added, List<Projects> updated, List<Projects> deleted)
        {
            var conn = await CreateTableAsync();

            await conn.InsertAllAsync(added);
            await conn.UpdateAllAsync(updated);
            foreach (var item in deleted)
            {
                await conn.DeleteAsync(item);
                //deleteTasksByShareStatus(delete);
                //new TaskSortOrderInDateService(dbHelper).deleteForeverByProject(delete.getId());
            }
            // TODO 由于使用了事务，所以不能使用异步方法
            // var conn = new SQLiteConnection(ApplicationData.Current.LocalFolder.Path + "\\note.db");
            // conn.RunInTransaction(new Action(() =>
            //{
            //    conn.InsertAll(added);
            //    conn.UpdateAll(updated);
            //    foreach (var item in deleted)
            //    {
            //        conn.Delete(item);
            //    }
            //}));
        }
        public async Task<Dictionary<String, string>> GetProjectSidToIdsDic(String userId)
        {
            Dictionary<String, string> idsDic = new Dictionary<String, string>();
            var conn = await CreateTableAsync();
            // TODO 由于不知道index() 返回的是什么东西，所以之后还要继续改一下
            var queryResult = await conn.Table<Projects>().Where((p) => p.SId != null && p.UserId == userId && p.Deleted == ModelStatusEnum.DELETED_NO).OrderByDescending((p) => p.CreatedTime).ToListAsync();

            foreach (var item in queryResult)
            {
                if (idsDic.ContainsKey(item.SId))
                {
                    idsDic[item.SId] = item.Id.ToString();
                }
                else
                {
                    idsDic.Add(item.SId, item.Id.ToString());
                }
            }
            return idsDic;
        }


        #region android代码
        //public HashMap<String, Long> getProjectSid2IdsMap(String userId)
        //{
        //    StringBuffer selection = new StringBuffer();
        //    selection.append(ProjectField.sId.name()).append(" not null and ")
        //            .append(ProjectField.User_Id.name()).append(" =? and ")
        //            .append(ProjectField._deleted.name()).append(" = ?");
        //    String[] selectionArgs = {
        //        userId, Status.DELETED_NO + ""
        //};
        //    String[] columns = {
        //        ProjectField._id.name(), ProjectField.sId.name()
        //};
        //    Cursor c = null;
        //    HashMap<String, Long> idsMap = new HashMap<String, Long>();
        //    try
        //    {
        //        c = table.query(columns, selection.toString(), selectionArgs,
        //                ProjectField.createdTime.name() + " desc", dbHelper);
        //        c.moveToFirst();
        //        while (!c.isAfterLast())
        //        {
        //            idsMap.put(c.getString(ProjectField.sId.index()),
        //                    c.getLong(ProjectField._id.index()));
        //            c.moveToNext();
        //        }
        //        return idsMap;
        //    }
        //    finally
        //    {
        //        if (c != null)
        //        {
        //            c.close();
        //        }
        //    }
        //}
        //public HashMap<String, Project> getAllSid2ProjectsMap(String userId)
        //{
        //    StringBuffer selection = new StringBuffer();
        //    Cursor c = null;
        //    HashMap<String, Project> projects = new HashMap<String, Project>();
        //    selection.append(ProjectField.sId.name()).append(" not null and ")
        //            .append(ProjectField.User_Id.name()).append(" =? and ")
        //            .append(ProjectField.etag.name()).append(" not null");
        //    String[] selectionArgs = {
        //        userId
        //};

        //    try
        //    {

        //        c = table.query(null, selection.toString(), selectionArgs,
        //                ProjectField.createdTime.name() + " desc", dbHelper);
        //        if (c != null && c.moveToFirst())
        //        {
        //            do
        //            {
        //                Project p = cursorToProject(c);
        //                projects.put(p.getSid(), p);
        //            } while (c.moveToNext());
        //        }
        //        return projects;
        //    }
        //    finally
        //    {
        //        if (c != null)
        //        {
        //            c.close();
        //        }
        //    }
        //} 
        #endregion



        public async Task<AsyncTableQuery<Projects>> GetAllProjectsDeletedNo()
        {
            var conn = await CreateTableAsync();
            return conn.Table<Projects>().Where((p) => p.Deleted == ModelStatusEnum.DELETED_NO);
            //return await (await ExecuteAsyncQueryTable()).Where((p) => p.Deleted == ModelStatusEnum.DELETED_NO).ToListAsync();
        }

        public async Task<List<Projects>> GetAllProjectsWithTasksCount()
        {
            var conn = await CreateTableAsync();
            var projectsList = await conn.Table<Projects>().Where((p) => p.Deleted == ModelStatusEnum.DELETED_NO && p.Closed == ModelStatusEnum.CLOSED_NO).ToListAsync();
            var tasksList = conn.Table<Tasks>().Where(t => t.Deleted == ModelStatusEnum.DELETED_NO);
            foreach (var item in projectsList)
            {
                var id = item.Id.ToString();
                var notCompleted = ModelStatusEnum.NOT_COMPLETED;
                item.TasksCount = await tasksList.Where(t => t.ProjectId == id && t.TaskStatus == notCompleted).CountAsync();
            }
            return projectsList;
        }
    }
}
