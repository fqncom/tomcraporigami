using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Enums;

namespace TickTick.Dal
{
    public class AttachmentDal : BaseDal<Attachment>
    {
        #region IBaseDal<Attachment> 成员

        //public Task<SQLite.SQLiteAsyncConnection> CreateTableAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Attachment>> ExecuteNonQuery(string sql, params object[] paras)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Attachment>> ExecuteTable()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DeleteData(Attachment t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAsync(Attachment t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> InsertAllAsync(List<Attachment> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAsync(Attachment t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAllAsync(List<Attachment> t)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DropTable()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region 自定义代码
        public async Task<List<Attachment>> GetAllAttachmentByTaskId(long taskId, String userId)
        {
            return await (await ExecuteAsyncQueryTable()).Where((a) => a.TaskId == taskId && a.UserId == userId).ToListAsync();
            //    String selection = AttachmentField.task_id.name() + "=? and "
            //            + AttachmentField.user_Id.name() + "=?";
            //    String[] selectionArgs = new String[] {
            //        taskId + "", userId
            //};
            //    return getAllAttachment(selection, selectionArgs, null, false);
        }
        public async Task UpdateSyncStatus(int syncDone, long id)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Attachment>().Where((a) => a.Id == id).FirstOrDefaultAsync();
            queryResult.Status = syncDone;
            await conn.UpdateAsync(queryResult);

            //    ContentValues values = new ContentValues();
            //    values.put(AttachmentField._status.name(), syncDone);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(AttachmentField._id.name()).append(" = ?");
            //    String[] whereArgs = {
            //        id + ""
            //};
            //    TABLE.updateWithoutModifyDate(values, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task ExchangeToNewTaskSid(String userId, String taskSid, String newTaskSid)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Attachment>().Where((a) => a.UserId == userId && a.TaskSid == taskSid).ToListAsync();
            foreach (var item in queryResult)
            {
                item.TaskSid = newTaskSid;
                item.Status = ModelStatusEnum.SYNC_NEW;
                item.UpDown = ModelStatusEnum.UP_DOWN_NEED_TO_UPLOAD;
            }
            await conn.UpdateAllAsync(queryResult);

            //    ContentValues values = new ContentValues();
            //    values.put(AttachmentField.task_sid.name(), newTaskSid);
            //    values.put(AttachmentField._status.name(), Status.SYNC_NEW);
            //    values.put(AttachmentField.up_down.name(), Status.UP_DOWN_NEED_TO_UPLOAD);
            //    StringBuilder whereClause = new StringBuilder();
            //    whereClause.append(AttachmentField.user_Id.name()).append(" = ? and ").append(AttachmentField.task_sid.name()).append(" = ?");
            //    String[] whereArgs = {
            //        userId, taskSid
            //};
            //    TABLE.updateWithoutModifyDate(values, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task<List<Attachment>> GetAllAttachment(String userId, bool withDeleted)
        {
            var conn = await CreateTableAsync();
            var queryResultTask = conn.Table<Attachment>().Where((a) => a.UserId == userId);
            if (!withDeleted)
            {
                queryResultTask = queryResultTask.Where((a) => a.Deleted == ModelStatusEnum.DELETED_NO);
            }
            else
            {
                queryResultTask = queryResultTask.Where((a) => a.Deleted != ModelStatusEnum.DELETED_FOREVER);
            }
            return await queryResultTask.OrderByDescending((a) => a.CreatedTime).ToListAsync();
        }
        public async Task DeleteAttachmentForeverByTaskId(int taskId)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Attachment>().Where((a) => a.TaskId == taskId).ToListAsync();
            foreach (var item in queryResult)
            {
                item.Deleted = ModelStatusEnum.DELETED_FOREVER;
                item.ModifiedTime = DateTime.UtcNow;
            }
            await conn.UpdateAllAsync(queryResult);
        }
        public async Task DeleteAttachmentForever(int id)
        {
            var conn = await CreateTableAsync();
            var queryResult = await conn.Table<Attachment>().Where((a) => a.Id == id).FirstOrDefaultAsync();
            queryResult.Deleted = ModelStatusEnum.DELETED_FOREVER;
            queryResult.ModifiedTime = DateTime.UtcNow;
            await conn.UpdateAsync(queryResult);

            //    ContentValues values = new ContentValues();
            //    values.put(AttachmentField._deleted.name(), Status.DELETED_FOREVER);
            //    StringBuffer whereClause = new StringBuffer();
            //    whereClause.append(AttachmentField._id.name()).append(" = ?");
            //    String[] whereArgs = {
            //        id + ""
            //};
            //    TABLE.update(values, whereClause.toString(), whereArgs, dbHelper);
        }
        public async Task<Attachment> InsertAttachment(Attachment attachment)
        {
            var conn = await CreateTableAsync();
            await conn.InsertAsync(attachment);
            return attachment;
            //long id = TABLE.create(makeFullContentValues(attachment), dbHelper);
            //attachment.setId(id);
            //return attachment;
        }
        public async Task<bool> UpdateAttachment(Attachment attachment)
        {
            var conn = await CreateTableAsync();
            attachment.ModifiedTime = DateTime.UtcNow;
            return await conn.UpdateAsync(attachment) > 0;

            //    ContentValues values = makeFullContentValues(attachment);
            //    String[] whereArgs = {
            //        attachment.getId() + "", attachment.getUserId()
            //};
            //    return TABLE.update(values, ID_USERID_WHERECLAUSE, whereArgs, dbHelper) > 0;
        }
        #endregion

        #region android代码
        //public void deleteAttachmentForeverByTaskId(Long taskId)
        //{
        //    ContentValues values = new ContentValues();
        //    values.put(AttachmentField._deleted.name(), Status.DELETED_FOREVER);
        //    StringBuffer whereClause = new StringBuffer();
        //    whereClause.append(AttachmentField.task_id.name()).append(" = ?");
        //    String[] whereArgs = {
        //        taskId + ""
        //};
        //    TABLE.update(values, whereClause.toString(), whereArgs, dbHelper);
        //}
        //public ArrayList<Attachment> getAllAttachment(String userId, boolean withDeleted)
        //{
        //    StringBuffer selection = new StringBuffer();
        //    selection.append(AttachmentField.user_Id.name()).append(" = ?");
        //    String[] selectionArgs = {
        //        userId
        //};
        //    return getAllAttachment(selection.toString(), selectionArgs, null, withDeleted);
        //} 
        #endregion

        public async Task<Dictionary<int, List<Attachment>>> GetAttachmentsByTaskIds(HashSet<long> taskIds, bool withDeleted)
        {
            Dictionary<int, List<Attachment>> map = new Dictionary<int, List<Attachment>>();
            var queryResult = await ExecuteAsyncQueryTable();
            if (!withDeleted)
            {
                queryResult = queryResult.Where(a => a.Deleted == ModelStatusEnum.DELETED_NO);
            }
            List<Attachment> allAttachement = await GetDataTableByExpression(a => taskIds.Contains(a.TaskId), null);
            foreach (var item in allAttachement)
            {
                if (map.ContainsKey(item.TaskId))
                {
                    map[item.TaskId].Add(item);
                }
                else
                {
                    List<Attachment> list = new List<Attachment>();
                    list.Add(item);
                    map.Add(item.TaskId, list);
                }
            }
            return map;

            //StringBuilder selection = new StringBuilder();
            //if (!withDeleted) {
            //    selection.append(AttachmentField._deleted.name()).append(" = ").append(Status.DELETED_NO);
            //}
            //DBUtils.appendInLongIds(selection, AttachmentField.task_id.nameWithTable(), taskIds);
            //List<Attachment> attachments = getAllAttachment(selection.toString(), null, null, true);
            //for (Attachment attachment : attachments) {
            //    if (map.containsKey(attachment.getTaskId())) {
            //        map.get(attachment.getTaskId()).add(attachment);
            //    } else {
            //        List<Attachment> list = new ArrayList<Attachment>();
            //        list.add(attachment);
            //        map.put(attachment.getTaskId(), list);
            //    }
            //}
            //return map;
        }
    }
}
