using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Enums;

namespace TickTick.Dal
{
    public class CommentDal : BaseDal<Comment>
    {

        #region IBaseDal<Comment> 成员

        //public Task<SQLite.SQLiteAsyncConnection> CreateTableAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Comment>> ExecuteNonQuery(string sql, params object[] paras)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Comment>> ExecuteTable()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DeleteData(Comment t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAsync(Comment t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAllAsync(List<Comment> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAsync(Comment t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAllAsync(List<Comment> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DropTable()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region 自定义代码

        public async Task ExchangeNewProjectSid(String projectSid, String newProjectSid)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Projects>().Where((p) => p.SId == projectSid).ToListAsync();
            foreach (var item in queryResult)
            {
                item.SId = newProjectSid;
            }
            await conn.UpdateAllAsync(queryResult);
            //    ContentValues values = new ContentValues();
            //    values.put(CommentField.project_sid.name(), newProjectSid);
            //    StringBuilder whereClause = new StringBuilder();
            //    whereClause.append(CommentField.project_sid.name()).append(" = ?");
            //    String[] whereArgs = {
            //        projectSid
            //};
            //    TABLE.updateWithoutModifyDate(values, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task ExchangeToNewTaskSid(String taskSid, String newTaskSid)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Comment>().Where((c) => c.TaskSId == taskSid).ToListAsync();
            foreach (Comment item in queryResult)
            {
                item.TaskSId = newTaskSid;
                item.Status = ModelStatusEnum.SYNC_NEW;
            }
            await conn.UpdateAllAsync(queryResult);

            //    ContentValues values = new ContentValues();
            //    values.put(CommentField.task_sid.name(), newTaskSid);
            //    values.put(CommentField._status.name(), Field.Status.SYNC_NEW);
            //    StringBuilder whereClause = new StringBuilder();
            //    whereClause.append(CommentField.task_sid.name()).append(" = ?");
            //    String[] whereArgs = {
            //        taskSid
            //};
            //    TABLE.updateWithoutModifyDate(values, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task DeleteCommentsByTaskSIdForever(String taskSId, String userId)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Comment>().Where((c) => c.TaskSId == taskSId && c.UserId == userId).ToListAsync();
            foreach (var item in queryResult)
            {
                await conn.DeleteAsync(item);
            }
        }
        #endregion


        #region android代码
        //public void deleteCommentsByTaskSIdForever(String taskSId, String userId)
        //{
        //    StringBuffer whereClause = new StringBuffer();
        //    whereClause.append(CommentField.task_sid.name()).append("=? and ")
        //            .append(CommentField.user_id.name()).append("=?");
        //    String[] whereArgs = new String[] { taskSId, userId };
        //    dbHelper.getWritableDatabase()
        //            .delete(CommentField.TABLE_NAME, whereClause.toString(), whereArgs);
        //}
        #endregion
    }
}
