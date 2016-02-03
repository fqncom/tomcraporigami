using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Enums;

namespace TickTick.Dal
{
    public class ChecklistItemDal : BaseDal<ChecklistItem>
    {


        #region IBaseDal<ChecklistItem> 成员

        //public Task<SQLite.SQLiteAsyncConnection> CreateTableAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<ChecklistItem>> ExecuteNonQuery(string sql, params object[] paras)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<ChecklistItem>> ExecuteTable()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DeleteData(ChecklistItem t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAsync(ChecklistItem t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAllAsync(List<ChecklistItem> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAsync(ChecklistItem t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAllAsync(List<ChecklistItem> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DropTable()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region 自定义代码
        public async Task UpdateCheckStatusByTask(String userId, long taskId, int status)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<ChecklistItem>().Where((c) => c.UserId == userId && c.UserId == userId).FirstOrDefaultAsync();
            queryResult.Checked = status;
            queryResult.Status = queryResult.Status == ModelStatusEnum.SYNC_NEW ? ModelStatusEnum.SYNC_NEW : ModelStatusEnum.SYNC_UPDATE;
            await conn.UpdateAsync(queryResult);

            //" checked = {status},status = case when status = {SYNC_NEW} then SYNC_NEW else SYNC_UPDATE end where userid = {userid} and taskId = {taskid}";

            //StringBuffer sql = new StringBuffer();
            //sql.append("UPDATE ").append(ChecklistItemField.TABLE_NAME).append(" SET ")
            //        .append(ChecklistItemField.checked.name()).append(" = ").append(status)
            //        .append(" , ").append(ChecklistItemField._status.name()).append(" = CASE WHEN ")
            //        .append(ChecklistItemField._status.name()).append(" = ").append(Status.SYNC_NEW)
            //        .append(" THEN ").append(Status.SYNC_NEW).append(" ELSE ")
            //        .append(Status.SYNC_UPDATE).append(" END WHERE ")
            //        .append(ChecklistItemField.user_Id.name()).append(" = '").append(userId)
            //        .append("' AND ").append(ChecklistItemField.task_id.name()).append(" = ")
            //        .append(taskiId);
            //dbHelper.getWritableDatabase().execSQL(sql.toString());
        }
        public async Task<List<ChecklistItem>> GetChecklistItemsByTaskId(long taskId, String userId, bool withDeleted)
        {
            var all = await ExecuteTable();
            var queryResultTask = (await ExecuteAsyncQueryTable()).Where((c) => c.TaskId == taskId);
            if (!string.IsNullOrEmpty(userId))
            {
                queryResultTask = queryResultTask.Where(c => c.UserId == userId);
            }
            if (!withDeleted)
            {
                queryResultTask = queryResultTask.Where((c) => c.Deleted == ModelStatusEnum.DELETED_NO);
            }
            return await queryResultTask.ToListAsync();
            //    StringBuffer selection = new StringBuffer();
            //    selection.append(ChecklistItemField.task_id.name()).append(" = ? and ")
            //            .append(ChecklistItemField.user_Id.name()).append(" = ?");
            //    if (!withDeleted)
            //    {
            //        selection.append(" and ").append(ChecklistItemField._deleted.name()).append(" = ")
            //                .append(Status.DELETED_NO);
            //    }

            //    String[] selectionArgs = new String[] {
            //        taskId + "", userId
            //};
            //    return getAllChecklistItems(selection.toString(), selectionArgs);
        }
        public async Task UpdateEtagToDbByTask(String userId, String taskSid, String etag)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<ChecklistItem>().Where((c) => c.UserId == userId && c.TaskSid == taskSid).ToListAsync();
            foreach (var item in queryResult)
            {
                item.Status = ModelStatusEnum.SYNC_DONE;
                item.Etag = etag;
            }
            await conn.UpdateAllAsync(queryResult);

            //    ContentValues values = new ContentValues();
            //    values.put(ChecklistItemField._status.name(), Status.SYNC_DONE);
            //    values.put(ChecklistItemField.etag.name(), etag);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(ChecklistItemField.user_Id.name()).append(" = ? and ")
            //            .append(ChecklistItemField.task_sid.name()).append(" = ?");
            //    String[] whereArgs = {
            //        userId, taskSid
            //};
            //    table.updateWithoutModifyDate(values, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task ExchangeToNewTaskSid(String userId, String taskSid, String newTaskSid)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<ChecklistItem>().Where((c) => c.UserId == userId && c.TaskSid == taskSid).ToListAsync();
            foreach (var item in queryResult)
            {
                item.TaskSid = newTaskSid;
                item.Status = ModelStatusEnum.SYNC_NEW;
            }
            await conn.UpdateAllAsync(queryResult);
            //    ContentValues contentValues = new ContentValues();
            //    contentValues.put(ChecklistItemField.task_sid.name(), newTaskSid);
            //    contentValues.put(ChecklistItemField._status.name(), Status.SYNC_NEW);
            //    StringBuilder whereClause = new StringBuilder();
            //    whereClause.append(ChecklistItemField.user_Id.name()).append(" = ? and ").append(ChecklistItemField.task_sid.name()).append(" = ?");
            //    String[] whereArgs = {
            //        userId, taskSid
            //};
            //    table.updateWithoutModifyDate(contentValues, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task<List<ChecklistItem>> GetAllCheckListItems(String userId, bool withDeleted)
        {
            var conn = await CreateTableAsync();
            var queryResultTask = conn.Table<ChecklistItem>().Where((c) => c.UserId == userId);
            if (!withDeleted)
            {
                queryResultTask = queryResultTask.Where((c) => c.Deleted == ModelStatusEnum.DELETED_NO);
            }
            return await queryResultTask.OrderBy((c) => c.SortOrder).ToListAsync();
        }
        // TODO 任何删除操作都可以尝试使用事务进行处理。使用事务的时候不能使用异步编程
        public async Task DeleteChecklistPhysicalByTaskId(int taskId)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<ChecklistItem>().Where((c) => c.TaskId == taskId).ToListAsync();
            foreach (var item in queryResult)
            {
                await conn.DeleteAsync(item);
            }
        }
        public async Task<ChecklistItem> CreateChecklistItem(ChecklistItem item)
        {
            var conn = await CreateTableAsync();
            await conn.InsertAsync(item);
            //var all = await ExecuteTable();
            return item;
        }
        #endregion

        #region android代码
        //public ArrayList<ChecklistItem> getAllCheckListItems(String userId, boolean withDeleted)
        //{
        //    StringBuffer selection = new StringBuffer();
        //    selection.append(ChecklistItemField.user_Id.name()).append(" = ?");
        //    if (!withDeleted)
        //    {
        //        selection.append(" and ").append(ChecklistItemField._deleted.name()).append(" = ")
        //                .append(Status.DELETED_NO);
        //    }

        //    String[] selectionArgs = new String[] {
        //          userId
        //  };
        //    return getAllChecklistItems(selection.toString(), selectionArgs);
        //}
        #endregion

        public async Task<Dictionary<int, List<ChecklistItem>>> GetCheckListItemsByTaskIds(HashSet<long> taskIds, bool withDeleted)
        {
            var queryResult = await ExecuteAsyncQueryTable();
            if (!withDeleted)
            {
                queryResult = queryResult.Where(c => c.Deleted == ModelStatusEnum.DELETED_NO);
            }
            List<ChecklistItem> allCheckListItems = await GetDataTableByExpression(c => taskIds.Contains(c.TaskId), null);
            Dictionary<int, List<ChecklistItem>> map = new Dictionary<int, List<ChecklistItem>>();
            foreach (var item in allCheckListItems)
            {
                if (map.ContainsKey(item.TaskId))
                {
                    map[item.TaskId].Add(item);
                }
                else
                {
                    List<ChecklistItem> itemList = new List<ChecklistItem>();
                    itemList.Add(item);
                    map.Add(item.TaskId, itemList);
                }
            }

            return map;

            //StringBuilder selection = new StringBuilder();
            //if (!withDeleted) {
            //    selection.append(ChecklistItemField._deleted.name()).append(" = ").append(Status.DELETED_NO);
            //}
            //DBUtils.appendInLongIds(selection, ChecklistItemField.task_id.nameWithTable(), taskIds);
            //List<ChecklistItem> items = getAllChecklistItems(selection.toString(), null);
            //Map<Long, List<ChecklistItem>> map = new HashMap<Long, List<ChecklistItem>>();
            //for (ChecklistItem item : items) {
            //    if (map.containsKey(item.getTaskId())) {
            //        map.get(item.getTaskId()).add(item);
            //    } else {
            //        List<ChecklistItem> itemList = new ArrayList<ChecklistItem>();
            //        itemList.add(item);
            //        map.put(item.getTaskId(), itemList);
            //    }
            //}
            //return map;
        }
    }
}
