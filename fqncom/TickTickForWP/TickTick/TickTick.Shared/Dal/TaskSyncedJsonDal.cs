using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using Windows.Storage;

namespace TickTick.Dal
{
    class TaskSyncedJsonDal : BaseDal<TaskSyncedJson>
    {
        #region IBaseDal<TaskSyncedJson> 成员

        //public async Task<SQLiteAsyncConnection> CreateTableAsync()
        //{
        //    var conn = new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\note.db");
        //    await conn.CreateTableAsync<TaskSyncedJson>();
        //    return conn;
        //}

        //public Task<List<TaskSyncedJson>> ExecuteNonQuery(string sql, params object[] paras)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<TaskSyncedJson>> ExecuteTable()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DeleteData(TaskSyncedJson t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAsync(TaskSyncedJson t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAllAsync(List<TaskSyncedJson> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAsync(TaskSyncedJson t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAllAsync(List<TaskSyncedJson> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DropTable()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region 自定义代码
        public async Task<List<TaskSyncedJson>> GetAllTaskSyncedJsons(String userID)
        {
            var conn = await CreateTableAsync();
            return await conn.Table<TaskSyncedJson>().Where((t) => t.UserId == userID).ToListAsync();
        }
        public async Task DeleteTaskSyncedJsonForever(String taskSID, String userID)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<TaskSyncedJson>().Where((t) => t.TaskSID == taskSID && t.UserId == userID).FirstOrDefaultAsync();
            if (queryResult != null)
            {
                await conn.DeleteAsync(queryResult);
            }

            //StringBuffer sql = new StringBuffer();
            //sql.append("DELETE FROM ").append(TaskSyncedJsonField.TABLE_NAME).append(" WHERE ")
            //        .append(TaskSyncedJsonField.TASK_SID.name()).append(" = '").append(taskSID)
            //        .append("'").append(" AND ").append(TaskSyncedJsonField.user_id.name())
            //        .append(" = '").append(userID).append("'");
            //dbHelper.getWritableDatabase().execSQL(sql.toString());
        }
        public async Task<TaskSyncedJson> CreateTaskSyncedJson(TaskSyncedJson json)
        {
            var conn = await CreateTableAsync();
            await conn.InsertAsync(json);
            return json;

            //ContentValues values = makeContentValues(json);
            //long id = TABLE.create(values, dbHelper);
            //json.setId(id);
            //return json;
        }
        public async Task<bool> UpdateTaskSyncedJson(TaskSyncedJson json)
        {
            var conn = await CreateTableAsync();
            json.ModifiedTime = DateTime.UtcNow;
            return await conn.UpdateAsync(json) > 0;

            //    ContentValues values = makeContentValues(json);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(TaskSyncedJsonField.TASK_SID.name()).append(" = ? and ")
            //            .append(TaskSyncedJsonField.user_id.name()).append(" = ?");
            //    String[] whereArgs = new String[] {
            //        json.getTaskSID() + "", json.getUserID()
            //};
            //    return TABLE.update(values, whereClause.toString(), whereArgs, dbHelper) > 0;
        }
        #endregion

        public async Task DeleteTaskSyncedJsonPhysical(String taskSID, String userID)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<TaskSyncedJson>().Where(t => t.TaskSID == taskSID && t.UserId == userID).ToListAsync();
            foreach (var item in queryResult)
            {
                await conn.DeleteAsync(item);
            }

            //StringBuffer sql = new StringBuffer();
            //sql.append("DELETE FROM ").append(TaskSyncedJsonField.TABLE_NAME).append(" WHERE ")
            //        .append(TaskSyncedJsonField.TASK_SID.name()).append(" = '").append(taskSID)
            //        .append("'").append(" AND ").append(TaskSyncedJsonField.user_id.name())
            //        .append(" = '").append(userID).append("'");
            //dbHelper.getWritableDatabase().execSQL(sql.toString());
        }
    }
}
