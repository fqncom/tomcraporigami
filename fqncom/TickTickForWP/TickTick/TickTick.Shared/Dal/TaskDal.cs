using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Linq;
using TickTick.Entity;
using TickTick.Enums;
using System.Collections.ObjectModel;
using TickTick.Helper;
using Newtonsoft.Json;

namespace TickTick.Dal
{
    public class TaskDal : BaseDal<Tasks>
    {


        #region IBaseDal<Tasks> 成员

        //public async Task<SQLiteAsyncConnection> CreateTableAsync()
        //{
        //    var conn = new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\note.db");
        //    await conn.CreateTableAsync<Tasks>();
        //    return conn;
        //}

        //public async Task<List<Tasks>> ExecuteNonQuery(string sql, params object[] paras)
        //{
        //    var conn = await CreateTableAsync();
        //    return await conn.QueryAsync<Tasks>(sql, paras);
        //}

        //public async Task<List<Tasks>> ExecuteTable()
        //{
        //    var conn = await CreateTableAsync();
        //    return await conn.Table<Tasks>().ToListAsync();
        //}

        //public async Task<int> DeleteData(Tasks tasks)
        //{
        //    var conn = await CreateTableAsync();
        //    return await conn.DeleteAsync(tasks);
        //}

        //public async Task<int> InsertAsync(Tasks tasks)
        //{
        //    var conn = await this.CreateTableAsync();
        //    return await conn.InsertAsync(tasks);
        //}

        //public async Task<int> InsertAllAsync(List<Tasks> tasks)
        //{
        //    var conn = await this.CreateTableAsync();
        //    return await conn.InsertAllAsync(tasks);
        //}

        //public async Task<int> UpdateAsync(Tasks tasks)
        //{
        //    var conn = await this.CreateTableAsync();
        //    return await conn.UpdateAsync(tasks);
        //}

        //public async Task<int> UpdateAllAsync(List<Tasks> tasks)
        //{
        //    var conn = await this.CreateTableAsync();
        //    return await conn.UpdateAllAsync(tasks);
        //}

        #endregion

        #region 自定义方法
        public async Task<bool> UpdateTaskOrder(Tasks task)
        {
            task.ModifiedTime = DateTime.UtcNow;
            return await UpdateAsync(task) > 0;
            //ContentValues values = new ContentValues();
            //values.put(Task2Field.sort_order.name(), task.getSortOrder());
            //return updateTask(task.getId(), values);
        }
        public async Task<long?> GetMaxTaskSortOrderInProject(string projectId)
        {
            var queryResult = await (await ExecuteAsyncQueryTable()).Where((t) => t.ProjectId == projectId).OrderByDescending((t) => t.SortOrder).FirstOrDefaultAsync();
            return queryResult.SortOrder;
        }
        public async Task<bool> UpdateTaskContent(Tasks task)
        {
            var conn = await CreateTableAsync();
            task.ModifiedTime = DateTime.UtcNow;
            return await conn.UpdateAsync(task) > 0;
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(Task2Field._id.name()).append(" = ? ");
            //    String[] whereArgs = new String[] {
            //        taskId + ""
            //};
            //    return table.update(values, whereClause.toString(), whereArgs, dbHelper) > 0;
            //ContentValues values = makeContentValues(task);
            //values.remove(Task2Field.sort_order.name());
            //values.remove(Task2Field.PROJECT_ID.name());
            //values.remove(Task2Field.PROJECT_SID.name());
            //return updateTask(task.getId(), values);
        }
        public async Task<Tasks> GetTaskById(int taskId)
        {
            var conn = await CreateTableAsync();
            return await conn.Table<Tasks>().Where((t) => t.Id == taskId).FirstOrDefaultAsync();
            //    StringBuffer selection = new StringBuffer();
            //    selection.append(Task2Field._id.name()).append(" =?");
            //    String[] selectionArgs = new String[] {
            //        taskId + ""
            //};
            //    ArrayList<Task2> tasks = getAllTasks(selection.toString(), selectionArgs);
            //    if (tasks.size() > 0)
            //    {
            //        return tasks.get(0);
            //    }
            //    return null;
        }
        public async Task<bool> UpdateTaskAssignee(int id, long? assignee)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Tasks>().Where((t) => t.Id == id).FirstOrDefaultAsync();
            if (queryResult != null)
            {
                queryResult.Assignee = assignee;
                queryResult.ModifiedTime = DateTime.UtcNow;
            }
            return await conn.UpdateAsync(queryResult) > 0;
            //ContentValues values = new ContentValues();
            //values.put(Task2Field.assignee.name(), assignee);
            //return updateTask(id, values);
        }

        public async Task<List<Tasks>> GetTasksByProjectSId(String projectSid, String userId, bool withDeleted)
        {
            var conn = await CreateTableAsync();
            var queryResultTask = conn.Table<Tasks>().Where((t) => t.ProjectId == projectSid && t.UserId == userId);
            if (withDeleted)
            {
                queryResultTask = queryResultTask.Where((t) => t.Deleted == ModelStatusEnum.DELETED_NO);
            }
            return await queryResultTask.ToListAsync();
            //    StringBuffer selection = new StringBuffer();
            //    selection.append(Task2Field.PROJECT_SID.name()).append(" =? and ")
            //            .append(Task2Field.User_Id.name()).append(" = ?");
            //    if (!withDeleted)
            //    {
            //        selection.append(" and ").append(Task2Field._deleted.name()).append(" = 0");
            //    }
            //    String[] selectionArgs = new String[] {
            //        projectSid, userId
            //};
            //    return getAllTasks(selection.toString(), selectionArgs);
        }
        public async Task ExchangeNewProjectSid(String userId, String projectSid, String newProjectSid)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Projects>().Where((p) => p.UserId == userId && p.SId == projectSid).ToListAsync();
            foreach (var item in queryResult)
            {
                item.SId = newProjectSid;
            }
            await conn.UpdateAllAsync(queryResult);
            //    ContentValues values = new ContentValues();
            //    values.put(Task2Field.PROJECT_SID.name(), newProjectSid);
            //    StringBuilder whereClause = new StringBuilder();
            //    whereClause.append(Task2Field.User_Id.name()).append(" = ? and ")
            //            .append(Task2Field.PROJECT_SID.name()).append(" = ?");
            //    String[] whereArgs = {
            //        userId, projectSid
            //};
            //    table.updateWithoutModifyDate(values, whereClause.toString(), whereArgs, dbHelper);
        }

        public async Task<List<Tasks>> GetNeedPostTasksContentChanged(String userId, long fromTime)
        {
            var conn = await CreateTableAsync();
            var query = await conn.Table<SyncStatus>().Where((s) => s.UserId == userId && s.Type == ModelStatusEnum.SYNC_TYPE_TASK_CONTENT).ToListAsync();
            //var queryResult = await conn.Table<Tasks>().Where((t) => t.UserId == userId).ToListAsync();

            var queryResultAsync = (await ExecuteAsyncQueryTable()).Where((t) => t.UserId == userId);

            if (fromTime > 0)
            {
                var fromTimeToDate = fromTime.GetDateTimeByMilliSeconds();
                queryResultAsync = queryResultAsync.Where((t) => t.ModifiedTime > fromTimeToDate);// TODO 此处有坑！！！时间的类型有问题，后续：基本修复，还是算的有问题
            }
            var queryResult = await queryResultAsync.ToListAsync();

            return (from t in queryResult
                    where (from s in query
                           select s.EntityId).Contains(t.SId)
                    select t).ToList();

            //    StringBuilder selection = new StringBuilder();
            //    selection.append(Task2Field.User_Id.name()).append(" = ? and ").append(
            //            Task2Field.sId.name())
            //            .append(" in ( select ").append(SyncStatusField.entity_id.name()).append(" from ")
            //            .append(SyncStatusField.TABLE_NAME).append(" where ")
            //            .append(SyncStatusField.user_id.name()).append(" = ? and ")
            //            .append(SyncStatusField._type.name()).append(" = ? )");
            //    if (fromTime > 0)
            //    {
            //        selection.append(" and ").append(Task2Field.modifiedTime.name()).append(" > ")
            //                .append(fromTime);
            //    }
            //    String[] selectionArgs = new String[] {
            //        userId, userId, Status.SYNC_TYPE_TASK_CONTENT + ""

            //};
            //    return getAllTasks(selection.toString(), selectionArgs);
        }
        public async Task<Dictionary<String, Tasks>> GetSidToTasksDic(String userId, List<String> taskSids)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Tasks>().Where((t) => t.UserId == userId).ToListAsync();

            queryResult = (from t in queryResult
                           where taskSids.Contains(t.SId)
                           select t).ToList();
            Dictionary<String, Tasks> dic = new Dictionary<string, Tasks>();
            foreach (var item in queryResult)
            {
                if (dic.ContainsKey(item.SId))
                {
                    dic[item.SId] = item;
                }
                else
                {
                    dic.Add(item.SId, item);
                }
            }
            return dic;
            //    StringBuilder selection = new StringBuilder();
            //    selection.append(Task2Field.User_Id.name()).append(" = ? and ")
            //            .append(Task2Field.sId.name()).append(" IN (");
            //    int index = 0;
            //    foreach (String taskSid in taskSids)
            //    {
            //        if (index > 0)
            //        {
            //            selection.append(",");
            //        }
            //        selection.append("'").append(taskSid).append("'");
            //        index++;
            //    }
            //    selection.append(")");
            //    String[] selectionArgs = {
            //        userId
            //};
            //    return getTasksMap(selection.toString(), selectionArgs, null);
        }
        public async Task<bool> ExchangeToNewIdForError(String userId, String sid, String newSid)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Tasks>().Where((t) => t.UserId == userId && t.SId == sid).ToListAsync();
            foreach (var item in queryResult)
            {
                item.SId = newSid;
                item.Etag = string.Empty;
                item.Deleted = ModelStatusEnum.DELETED_NO;
                item.ModifiedTime = DateTime.UtcNow;
            }
            return (await conn.UpdateAllAsync(queryResult)) > 0;

            //    ContentValues values = new ContentValues();
            //    values.put(Task2Field.sId.name(), newSid);
            //    values.putNull(Task2Field.etag.name());
            //    values.put(Task2Field._deleted.name(), Status.DELETED_NO);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(Task2Field.User_Id.name()).append(" = ? and ")
            //            .append(Task2Field.sId.name()).append(" = ?");
            //    String[] whereArgs = new String[] {
            //        userId, sid
            //};
            //    int ret = table.update(values, whereClause.toString(), whereArgs, dbHelper);
            //    return ret > 0;
        }
        public async Task<List<Tasks>> GetNeedPostDeletedTasks(String userId)
        {
            var conn = await CreateTableAsync();
            var query = await conn.Table<SyncStatus>().Where((s) => s.UserId == userId && s.Type == ModelStatusEnum.SYNC_TYPE_TASK_TRASH).ToListAsync();
            var queryResult = await conn.Table<Tasks>().Where((t) => t.UserId == userId).ToListAsync();
            return (from t in queryResult
                    where (from s in query
                           select s.EntityId).Contains(t.SId)
                    select t).ToList();

            //    StringBuilder selection = new StringBuilder();
            //    selection.append(Task2Field.User_Id.name()).append(" = ? and ")
            //            .append(Task2Field.sId.name()).append(" in ( select ")
            //            .append(SyncStatusField.entity_id.name()).append(
            //            " from ").append(SyncStatusField.TABLE_NAME).append(" where ")
            //            .append(SyncStatusField.user_id.name()).append(" = ? and ").append(
            //            SyncStatusField._type.name()).append(" = ? )");
            //    String[] selectionArgs = {
            //        userId, userId, Status.SYNC_TYPE_TASK_TRASH + ""
            //};
            //    return getAllTasks(selection.toString(), selectionArgs);
        }
        public async Task<List<Tasks>> GetNeedPostUpdatedTasks(String userId)
        {
            var conn = await CreateTableAsync();
            var query = await conn.Table<SyncStatus>().Where((s) => s.UserId == userId && s.Type == ModelStatusEnum.SYNC_TYPE_TASK_CONTENT).ToListAsync();
            var queryResult = await conn.Table<Tasks>().Where((t) => t.UserId == userId).ToListAsync();
            return (from t in queryResult
                    where (from s in query
                           select s.EntityId).Contains(t.SId)
                    select t).ToList();

            //    StringBuilder selection = new StringBuilder();
            //    selection.append(Task2Field.User_Id.name()).append(" = ? and ")
            //            .append(Task2Field.sId.name()).append(" in ( select ")
            //            .append(SyncStatusField.entity_id.name()).append(
            //            " from ").append(SyncStatusField.TABLE_NAME).append(" where ")
            //            .append(SyncStatusField.user_id.name()).append(" = ? and ").append(
            //            SyncStatusField._type.name()).append(" = ? )");
            //    String[] selectionArgs = {
            //        userId, userId, Status.SYNC_TYPE_TASK_CONTENT + ""
            //};
            //    return getAllTasks(selection.toString(), selectionArgs);
        }
        /// <summary>
        /// 获取需要提交的被创建的tasks
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Tasks>> GetNeedPostCreatedTasks(String userId)
        {
            var conn = await CreateTableAsync();
            var query = await conn.Table<SyncStatus>().Where((s) => s.UserId == userId && s.Type == ModelStatusEnum.SYNC_TYPE_TASK_CREATE).ToListAsync();
            var queryResult = await conn.Table<Tasks>().Where((t) => t.UserId == userId).ToListAsync();
            var result = (from s in queryResult
                          where (from t in query
                                 select t.EntityId).Contains(s.SId)
                          select s).ToList();
            return result;
            //    StringBuilder selection = new StringBuilder();
            //    selection.append(Task2Field.User_Id.name()).append(" = ? and ")
            //            .append(Task2Field.sId.name()).append(" in ( select ")
            //            .append(SyncStatusField.entity_id.name()).append(
            //            " from ").append(SyncStatusField.TABLE_NAME).append(" where ")
            //            .append(SyncStatusField.user_id.name()).append(" = ? and ").append(
            //            SyncStatusField._type.name()).append(" = ? )");
            //    String[] selectionArgs = {
            //        userId, userId, Status.SYNC_TYPE_TASK_CREATE + ""
            //};
            //    return getAllTasks(selection.toString(), selectionArgs);
        }

        #region 弃用
        /// <summary>
        /// 根据proid找到对应project下的tasks列表
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        //public async Task<List<Tasks>> ExecuteTableByProjectId(string proId)
        //{
        //    //不使用sql语句，使用扩展方法或者linq语句等
        //    //string sql = "select * from Projects where Id = " + proId;
        //    //return await ExecuteNonQuery(sql);

        //    //(await ExecuteTable()).Where<Tasks>(a => a.ProjectId == proId).ToList();
        //    var list = await ExecuteTable();
        //    return (from tasks in list
        //            where tasks.ProjectId == proId && tasks.Type == ModelStatusEnum.SYNC_TYPE_TASK_CONTENT && tasks.TaskStatus == ModelStatusEnum.Task_Not_Finished
        //            select tasks).ToList();
        //} 
        #endregion

        public async Task<Dictionary<string, Tasks>> GetAllSidToTasksDic(string userId)
        {
            Dictionary<string, Tasks> tasksDic = new Dictionary<string, Tasks>();
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Tasks>().Where((t) => t.SId != null && t.UserId == userId).OrderByDescending((t) => t.CreatedTime).ToListAsync();
            if (queryResult != null)
            {
                foreach (var item in queryResult)
                {
                    if (tasksDic.ContainsKey(item.SId))
                    {
                        tasksDic[item.SId] = item;
                    }
                    else
                    {
                        tasksDic.Add(item.SId, item);
                    }
                }
            }
            return tasksDic;
        }
        public async Task DeleteTaskIntoTrash(List<Tasks> deleteTasks)
        {
            var conn = await CreateTableAsync();//new SQLiteConnection(ApplicationData.Current.LocalFolder.Path + "\\note.db");
            foreach (var item in deleteTasks)
            {
                item.Deleted = ModelStatusEnum.DELETED_TRASH;
            }
            await conn.UpdateAllAsync(deleteTasks);

            //由于使用事务进行处理，要使用非异步的数据库连接，所以这里暂时使用异步的连接
            //conn.RunInTransaction(new Action(() =>
            //{
            //    conn.UpdateAll(deleteTasks);
            //}
            //));
        }
        public async Task DeleteTaskPhysical(Tasks task)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Tasks>().Where(t => t.Id == task.Id).ToListAsync();
            foreach (var item in queryResult)
            {
                await conn.DeleteAsync(item);
            }
        }
        public async Task<long?> GetMinTaskSortOrderInProject(string projectId)
        {
            var conn = await CreateTableAsync();
            // TODO 此处逻辑有误，具体要进行的操作之后再改，参考安卓代码 获取最小的sortorder？
            var queryResult = await conn.Table<Tasks>().Where((t) => t.ProjectId == projectId).OrderBy((t) => t.SortOrder).FirstOrDefaultAsync();
            if (queryResult == null)
            {
                return null;
            }
            return queryResult.SortOrder;
        }
        public async Task<bool> CreateTask(Tasks task)
        {
            var conn = await CreateTableAsync();
            var result = await conn.InsertAsync(task);
            return result > 0;
            // TODO 在安卓的代码中创建了一个virtualtask，这里由于要得到刚插入数据的id，而一般返回的都是受影响的行数，那么需要用ExecuteScalar执行sql语句，所以暂时不实现
            //ContentValues values = makeContentValues(task);
            //long id = table.create(values, dbHelper);
            //if (id > 0)
            //{
            //    task.setId(id);
            //    Task2.virtualTable.create(makeVirtualContentValues(task), dbHelper);
            //    return true;
            //}
            //return false;
        }
        public async Task UpdateTaskWithoutModifyDate(Tasks task)
        {
            var conn = await CreateTableAsync();
            await conn.UpdateAsync(task);
        }
        public async Task<bool> UpdateTaskContentWithoutModifyDate(Tasks task)
        {
            var conn = await CreateTableAsync();

            var result = await conn.UpdateAsync(task);
            return result > 0;
            // TODO 在安卓的代码中，不知道为什么要删掉一些参数，是为了更新到数据库的时候减少数据库压力？还是什么？
            //ContentValues values = makeContentValues(task);
            //values.remove(Task2Field.sort_order.name());
            //values.remove(Task2Field.PROJECT_ID.name());
            //values.remove(Task2Field.PROJECT_SID.name());
            //return updateTaskWithoutModifyDate(task.getId(), values);
        }

        public async Task<Dictionary<String, int>> GetTaskSidToIdDic(String userId)
        {
            Dictionary<String, int> sidToIdDic = new Dictionary<string, int>();
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Tasks>().Where((t) => t.SId != null && t.UserId == userId && t.Deleted == ModelStatusEnum.DELETED_NO).OrderByDescending((t) => t.CreatedTime).ToListAsync();
            foreach (var item in queryResult)
            {
                if (sidToIdDic.ContainsKey(item.SId))
                {
                    sidToIdDic[item.SId] = item.Id;
                }
                else
                {
                    sidToIdDic.Add(item.SId, item.Id);
                }
            }

            return sidToIdDic;
        }
        public async Task<Tasks> GetTaskBySid(String userId, String taskId)
        {
            var conn = await CreateTableAsync();
            return await conn.Table<Tasks>().Where((t) => t.UserId == userId && t.SId == taskId).FirstOrDefaultAsync();

            //    StringBuffer selection = new StringBuffer();
            //    selection.append(Task2Field.User_Id.name()).append(" =? and ")
            //            .append(Task2Field.sId.name()).append(" =?");
            //    String[] selectionArgs = new String[] {
            //        userId, taskId + ""
            //};
            //    ArrayList<Task2> tasks = getAllTasks(selection.toString(), selectionArgs);
            //    if (tasks.size() > 0)
            //    {
            //        return tasks.get(0);
            //    }
            //    return null;
        }
        public async Task<bool> UpdateEtagToDb(String userId, String sid, String etag)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Tasks>().Where((t) => t.UserId == userId && t.SId == sid).ToListAsync();
            foreach (var item in queryResult)
            {
                item.Etag = etag;
            }
            return (await conn.UpdateAllAsync(queryResult)) > 0;

            //    ContentValues values = new ContentValues();
            //    values.put(Task2Field.etag.name(), etag);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(Task2Field.User_Id.name()).append(" = ? and ")
            //            .append(Task2Field.sId.name()).append(" = ?");
            //    String[] whereArgs = {
            //        userId, sid
            //};
            //    return table.updateWithoutModifyDate(values, whereClause.toString(), whereArgs, dbHelper)
            //            > 0;
        }
        #endregion

        #region android代码
        //public Long getMinTaskSortOrderInProject(long projectId)
        //{
        //    Cursor c = null;
        //    StringBuffer selection = new StringBuffer();
        //    selection.append(Task2Field.PROJECT_ID.name()).append(" =?");
        //    String[] selectionArgs = new String[] {
        //        projectId + ""
        //};
        //    try
        //    {
        //        c = dbHelper.getWritableDatabase().query(table.tableName(), new String[] {
        //            String.format("min(%s)", Task2Field.sort_order.name()), "count()"
        //    }, selection.toString(), selectionArgs, null, null, null);
        //        if (c != null && c.moveToFirst())
        //        {
        //            int count = c.getInt(1);
        //            if (count == 0)
        //            {
        //                return null;
        //            }
        //            return c.getLong(0);
        //        }
        //        return null;
        //    }
        //    finally
        //    {
        //        if (c != null)
        //        {
        //            c.close();
        //        }
        //    }
        //}
        //public HashMap<String, Task2> getAllSid2Task2sMap(String userId)
        //{
        //    StringBuffer selection = new StringBuffer();
        //    selection.append(Task2Field.sId.name()).append(" not null and ")
        //            .append(Task2Field.User_Id.name()).append(" =?");
        //    String[] selectionArgs = {
        //        userId
        //};
        //    Cursor c = null;
        //    HashMap<String, Task2> sid2tasksMap = new HashMap<String, Task2>();
        //    try
        //    {
        //        c = table.query(null, selection.toString(), selectionArgs,
        //                Task2Field.createdTime.name() + " desc", dbHelper);
        //        c.moveToFirst();
        //        while (!c.isAfterLast())
        //        {
        //            Task2 t = cursorToTask(c);
        //            c.moveToNext();
        //            sid2tasksMap.put(t.getSid(), t);
        //        }
        //        return sid2tasksMap;
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

        #region 测试使用
        /// <summary>
        /// 删除整个表，慎用！！！
        /// </summary>
        /// <returns>返回删除条数</returns>
        //public async Task<int> DropTable()
        //{
        //    var conn = await this.CreateTableAsync();
        //    return await conn.DropTableAsync<Tasks>();
        //}
        #endregion


    }
}
